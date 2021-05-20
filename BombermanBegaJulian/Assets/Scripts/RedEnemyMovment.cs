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

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMoving)
        {

           int dir = SelectRandomDirection();

            switch (dir)
            {
                case 0:
                    Target = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z+1);
                    Move(Vector3.forward, true);
                    break;
                case 1:
                    Target = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z-1);
                    Move(Vector3.back, true);
                    break;
                case 2:
                    Target = new Vector3(this.transform.position.x+1, this.transform.position.y, this.transform.position.z);
                    Move(Vector3.right, false);
                    break;
                case 3:
                    Target = new Vector3(this.transform.position.x-1, this.transform.position.y, this.transform.position.z);
                    Move(Vector3.left, false);
                    break;
                case 4:
                    
                    break;
                default:
                    break;
            }
        }
    }
    public void Move(Vector3 direction, bool horizontalMovment)
    {
        RaycastHit myHit;
        Ray myRay;
        myRay = new Ray(this.transform.position, direction);
        if (Physics.Raycast(myRay, out myHit, 0.5f))
        {
            if (myHit.transform.gameObject.tag == "DestroyablePillar" || myHit.transform.gameObject.tag == "Map")
            {
            }
            else
            {
                transform.position += direction * speed * Time.deltaTime;
                Vector3 newPos = Vector3.zero;
                if (horizontalMovment)
                {
                    newPos = new Vector3((float)Math.Round(transform.position.x, 0), transform.position.y, transform.position.z);
                }
                else
                {
                    newPos = new Vector3(transform.position.x, transform.position.y, (float)Math.Round(transform.position.z, 0));
                }
                transform.position = Vector3.Lerp(transform.position, newPos, 1);
            }
        }
        else
        {
            transform.position += direction * speed * Time.deltaTime;
            Vector3 newPos = Vector3.zero;
            if (horizontalMovment)
            {
                newPos = new Vector3((float)Math.Round(transform.position.x, 0), transform.position.y, transform.position.z);
            }
            else
            {
                newPos = new Vector3(transform.position.x, transform.position.y, (float)Math.Round(transform.position.z, 0));
            }
            transform.position = Vector3.Lerp(transform.position, newPos, 1);
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
}

