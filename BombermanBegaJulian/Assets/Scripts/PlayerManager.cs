using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int playerLives = 3;
    public float speed = 1;
    private GameManager gameManager;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovment();
    }


    void PlayerMovment()
    {
        if (Input.GetKey(KeyCode.LeftArrow) && ((float)Math.Round(transform.position.x, 0) % 2 != 0) && transform.position.z <= gameManager.mapColumn-2)
        {
            transform.position += Vector3.forward * speed * Time.deltaTime;
            transform.position = new Vector3((float)Math.Round(transform.position.x, 0), transform.position.y, transform.position.z);
        }
        if (Input.GetKey(KeyCode.RightArrow) && ((float)Math.Round(transform.position.x, 0) % 2 != 0) && transform.position.z >=1)
        {
            transform.position += Vector3.back * speed * Time.deltaTime;
            transform.position = new Vector3((float)Math.Round(transform.position.x, 0), transform.position.y, transform.position.z);
        }
        if (Input.GetKey(KeyCode.UpArrow) && ((float)Math.Round(transform.position.z, 0) % 2 != 0) && transform.position.x <= gameManager.mapRows-2)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
            transform.position = new Vector3(transform.position.x, transform.position.y, (float)Math.Round(transform.position.z, 0));
        }
        if (Input.GetKey(KeyCode.DownArrow) && ((float)Math.Round(transform.position.z, 0) % 2 != 0) && transform.position.x >= 1)
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
            transform.position = new Vector3(transform.position.x, transform.position.y, (float)Math.Round(transform.position.z, 0));
        }
    }
}
