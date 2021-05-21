using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RedEnemyMovment : MonoBehaviour
{
    private GameManager gameManager;
    public float speed = 1;
    Vector3 Target;
    bool isMoving;

    private RaycastHit myHitLeft;
    private RaycastHit myHitRight;
    private RaycastHit myHitBack;
    private RaycastHit myHitForward;
    private Ray myRayLeft;
    private Ray myRayRight;
    private Ray myRayBack;
    private Ray myRayForward;

    public delegate void HitPlayer();
    public static HitPlayer DamagePlayer;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

        int dir = 4;
        if (!isMoving)
        {
            dir = SelectRandomDirection();
            this.transform.position = new Vector3((float)Math.Round(transform.position.x, 0), transform.position.y, (float)Math.Round(transform.position.z, 0));
            switch (dir)
            {                 
                case 0:
                    Target = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + 1);
                    break;
                case 1:
                    Target = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z - 1);
                    break;
                case 2:
                    Target = new Vector3(this.transform.position.x + 1, this.transform.position.y, this.transform.position.z);
                    break;
                case 3:
                    Target = new Vector3(this.transform.position.x - 1, this.transform.position.y, this.transform.position.z);
                    break;
                case 4:

                    break;
                default:
                    break;
            }
        }
        Move();

    }
    public void Move()
    {
        isMoving = true;
        transform.position = Vector3.MoveTowards(this.transform.position, Target, speed * Time.deltaTime);
        if (transform.position == Target)
        {
            isMoving = false;
        }
    }

    int SelectRandomDirection()
    {
        bool CanGoForward = CheckDir(myRayForward, myHitForward, Vector3.forward);
        bool CanGoBack = CheckDir(myRayBack, myHitBack, Vector3.back);
        bool CanGoRight = CheckDir(myRayRight, myHitRight, Vector3.right);
        bool CanGoLeft = CheckDir(myRayLeft, myHitLeft, Vector3.left);

        int dir = UnityEngine.Random.Range(0, 4);
        if (CanGoForward == false && CanGoBack == false && CanGoRight == false && CanGoLeft == false )
        {
            return 4;
        }
        while ((CanGoForward == false && dir == 0) || (CanGoBack == false && dir == 1) || (CanGoRight == false && dir == 2) || (CanGoLeft == false && dir == 3))
        {
            dir = UnityEngine.Random.Range(0, 4);
        }
        return dir;
    }

    bool CheckDir(Ray myRay, RaycastHit myRHit, Vector3 direction)
    {
        myRay = new Ray(this.transform.position, direction);
        if (Physics.Raycast(myRay, out myRHit, PlayerManager.bombsRange + 0.4f))
        {
            if (myRHit.transform.gameObject.tag == "DestroyablePillar" || myRHit.transform.gameObject.tag == "Map")
            {
                return false;
            }                    
        }
        else
        {
            return true;
        }
        return true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Player")
        {
            DamagePlayer?.Invoke();
        }
    }
}

