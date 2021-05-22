using UnityEngine;
using System;

public class YellowEnemieMovment : MonoBehaviour
{
    public float speed = 1;
    Vector3 Target;
    Vector3 Origin;
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

    public GameObject backShield;
    public GameObject frontShield;
    public GameObject leftShield;
    public GameObject rightShield;

    void Start()
    {
        Target = this.transform.position;
        Origin = Target;
        isMoving = false;
    }

    // Update is called once per frame
    void Update()
    {

        int dir = 4;
        if (!isMoving)
        {
            dir = SelectRandomDirection();
            this.transform.position = new Vector3((float)Math.Round(transform.position.x, 0), transform.position.y, (float)Math.Round(transform.position.z, 0));
            Origin = this.transform.position;
            switch (dir)
            {
                case 0:
                    Target = this.transform.position + Vector3.forward;
                    frontShield.SetActive(true);
                    backShield.SetActive(false);
                    rightShield.SetActive(false);
                    leftShield.SetActive(false);
                    break;
                case 1:
                    Target = this.transform.position + Vector3.back;
                    backShield.SetActive(true);
                    rightShield.SetActive(false);
                    leftShield.SetActive(false);
                    frontShield.SetActive(false);
                    break;
                case 2:
                    Target = this.transform.position + Vector3.right;
                    rightShield.SetActive(true);
                    backShield.SetActive(false);
                    leftShield.SetActive(false);
                    frontShield.SetActive(false);
                    break;
                case 3:
                    Target = this.transform.position + Vector3.left;
                    leftShield.SetActive(true);
                    backShield.SetActive(false);
                    rightShield.SetActive(false);
                    frontShield.SetActive(false);
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
        bool CanGoForward = CheckDir(Vector3.forward);
        bool CanGoBack = CheckDir(Vector3.back);
        bool CanGoRight = CheckDir(Vector3.right);
        bool CanGoLeft = CheckDir(Vector3.left);

        int dir = UnityEngine.Random.Range(0, 4);
        if (CanGoForward == false && CanGoBack == false && CanGoRight == false && CanGoLeft == false)
        {
            return 4;
        }
        while ((CanGoForward == false && dir == 0) || (CanGoBack == false && dir == 1) || (CanGoRight == false && dir == 2) || (CanGoLeft == false && dir == 3))
        {
            dir = UnityEngine.Random.Range(0, 4);
        }
        return dir;
    }

    bool CheckDir(Vector3 direction)
    {
        RaycastHit[] myHits = Physics.RaycastAll(this.transform.position, direction, 1.4f);
        for (int i = 0; i < myHits.Length; i++)
        {
            if (myHits[i].transform.gameObject.tag == "DestroyablePillar" || myHits[i].transform.gameObject.tag == "DestroyablePillar" || myHits[i].transform.gameObject.tag == "Bomnb" || myHits[i].transform.gameObject.tag == "DestroyablePillar")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        return false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("choca con algo");
        if (collision.transform.tag == "Player")
        {
            DamagePlayer?.Invoke();
        }
        
    }
}
