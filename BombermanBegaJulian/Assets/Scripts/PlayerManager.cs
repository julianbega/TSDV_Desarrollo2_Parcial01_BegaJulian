using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int playerLives = 3;
    public float speed = 1;
    private GameManager gameManager;
    public GameObject bombPrefab;
    private bool allreadyMovingUpOrDown = false;
    private bool allreadyMovingLeftOrRight = false;
    public int maxBombs = 1;
    public int actualBombs = 0;
    public int bombsRange = 1;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        Bomb.hasExploted += ReduceActualBombs;
    }

    private void OnDisable()
    {
        Bomb.hasExploted -= ReduceActualBombs;
    }
    // Update is called once per frame
    void Update()
    {
        PlayerMovment();
        if (Input.GetKey(KeyCode.Space) && actualBombs < maxBombs)
        {
            actualBombs++;
            Instantiate(bombPrefab, new Vector3((float)Math.Round(transform.position.x, 0), transform.position.y, (float)Math.Round(transform.position.z, 0)), Quaternion.identity);
        }
    }

    void ReduceActualBombs()
    {
        actualBombs--;
    }
    void PlayerMovment()
    {               
        if (Input.GetKey(KeyCode.LeftArrow) && ((float)Math.Round(transform.position.x, 0) % 2 != 0) && transform.position.z <= gameManager.mapColumn-2 && allreadyMovingUpOrDown==false)
        {
            RaycastHit myHit;
            Ray myRay;
            myRay = new Ray(this.transform.position, Vector3.forward);
            if (Physics.Raycast(myRay, out myHit, 0.5f))
            {
                if (/*myHit.transform.gameObject.tag == "Bomb" || */myHit.transform.gameObject.tag == "DestroyablePillar")
                {
                }
                else
                {
                    allreadyMovingLeftOrRight = true;
                    transform.position += Vector3.forward * speed * Time.deltaTime;                    
                    Vector3 newPos = new Vector3((float)Math.Round(transform.position.x, 0), transform.position.y, transform.position.z);
                    transform.position = Vector3.Lerp(transform.position, newPos, 1);
                }
            }
            else 
            {
                allreadyMovingLeftOrRight = true;
                transform.position += Vector3.forward * speed * Time.deltaTime;
                Vector3 newPos = new Vector3((float)Math.Round(transform.position.x, 0), transform.position.y, transform.position.z);
                transform.position = Vector3.Lerp(transform.position, newPos, 1);
            }            
        }        
        if (Input.GetKey(KeyCode.RightArrow) && ((float)Math.Round(transform.position.x, 0) % 2 != 0) && transform.position.z >=1 && allreadyMovingUpOrDown == false)
        {
            RaycastHit myHit;
            Ray myRay;
            myRay = new Ray(this.transform.position,Vector3.back);
            if (Physics.Raycast(myRay, out myHit, 0.5f))
            {
                if (/*myHit.transform.gameObject.tag == "Bomb" || */myHit.transform.gameObject.tag == "DestroyablePillar")
                {
                }
                else
                {
                    allreadyMovingLeftOrRight = true;
                    transform.position += Vector3.back * speed * Time.deltaTime;
                    Vector3 newPos = new Vector3((float)Math.Round(transform.position.x, 0), transform.position.y, transform.position.z);
                    transform.position = Vector3.Lerp(transform.position, newPos, 1);
                }
            }
            else
            {
                allreadyMovingLeftOrRight = true;
                transform.position += Vector3.back * speed * Time.deltaTime;
                Vector3 newPos = new Vector3((float)Math.Round(transform.position.x, 0), transform.position.y, transform.position.z);
                transform.position = Vector3.Lerp(transform.position, newPos, 1);
            }
        }
        if (Input.GetKey(KeyCode.UpArrow) && ((float)Math.Round(transform.position.z, 0) % 2 != 0) && transform.position.x <= gameManager.mapRows-2 && allreadyMovingLeftOrRight == false)
        {
            RaycastHit myHit;
            Ray myRay;
            myRay = new Ray(this.transform.position, Vector3.right);
            if (Physics.Raycast(myRay, out myHit, 0.5f))
            {
                if (/*myHit.transform.gameObject.tag == "Bomb" || */myHit.transform.gameObject.tag == "DestroyablePillar")
                {              
                }
                else
                {
                    allreadyMovingUpOrDown = true;
                    transform.position += Vector3.right * speed * Time.deltaTime;
                    transform.position = new Vector3(transform.position.x, transform.position.y, (float)Math.Round(transform.position.z, 0));
                }
            }
            else
            {
                allreadyMovingUpOrDown = true;
                transform.position += Vector3.right * speed * Time.deltaTime;
                transform.position = new Vector3(transform.position.x, transform.position.y, (float)Math.Round(transform.position.z, 0));
            }
        }
        if (Input.GetKey(KeyCode.DownArrow) && ((float)Math.Round(transform.position.z, 0) % 2 != 0) && transform.position.x >= 1 && allreadyMovingLeftOrRight == false)
        {

            RaycastHit myHit;
            Ray myRay;
            myRay = new Ray(this.transform.position, Vector3.left);
            if (Physics.Raycast(myRay, out myHit, 0.5f))
            {
                if (/*myHit.transform.gameObject.tag == "Bomb" ||*/ myHit.transform.gameObject.tag == "DestroyablePillar")
                {
                }
                else
                {
                    allreadyMovingUpOrDown = true;
                    transform.position += Vector3.left * speed * Time.deltaTime;
                    transform.position = new Vector3(transform.position.x, transform.position.y, (float)Math.Round(transform.position.z, 0));
                }
            }
            else
            {
                allreadyMovingUpOrDown = true;
                transform.position += Vector3.left * speed * Time.deltaTime;
                transform.position = new Vector3(transform.position.x, transform.position.y, (float)Math.Round(transform.position.z, 0));
            }
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.UpArrow))
        {
            allreadyMovingUpOrDown = false;
            allreadyMovingLeftOrRight = false;
        }
       
    }
}

