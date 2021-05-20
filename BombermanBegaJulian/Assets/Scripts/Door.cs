using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool isDoorOpen;
    public bool endGame;
    void Start()
    {
        isDoorOpen = false;
        GameManager.checkOpenDoor += OpenDoor;
    }

    private void OnDisable()
    {
        GameManager.checkOpenDoor -= OpenDoor;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && isDoorOpen == true)
        {
            endGame = true;
        }
    }

    private void OpenDoor()
    {
        isDoorOpen = true;
    }
}
