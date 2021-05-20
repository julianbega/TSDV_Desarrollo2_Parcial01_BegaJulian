using UnityEngine;
using System;

public class PlayerMovment : MonoBehaviour
{

    public bool allreadyMovingUpOrDown = false;
    public bool allreadyMovingLeftOrRight = false;
    public float speed = 1;

    void Start()
    {
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow)  && allreadyMovingUpOrDown == false)
        {
            Move(Vector3.forward, true, ref allreadyMovingLeftOrRight);
        }
        if (Input.GetKey(KeyCode.RightArrow)  && allreadyMovingUpOrDown == false)
        {
            Move(Vector3.back, true, ref allreadyMovingLeftOrRight);
        }
        if (Input.GetKey(KeyCode.UpArrow) && allreadyMovingLeftOrRight == false)
        {
            Move(Vector3.right, false, ref allreadyMovingUpOrDown);            
        }
        if (Input.GetKey(KeyCode.DownArrow)  && allreadyMovingLeftOrRight == false)
        {
            Move(Vector3.left, false, ref allreadyMovingUpOrDown);
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.UpArrow))
        {
            allreadyMovingUpOrDown = false;
            allreadyMovingLeftOrRight = false;
        }
    }

    public void Move(Vector3 direction, bool horizontalMovment, ref bool allreadyMovindDir)
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

                allreadyMovindDir = true;
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
            allreadyMovindDir = true;
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
}
