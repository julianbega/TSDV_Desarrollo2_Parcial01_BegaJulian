using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool isDoorOpen;
    public bool endGame;

    public delegate void Change(string scene);
    public static Change ChangeScene;
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
            ChangeScene?.Invoke("Credits");
        }
    }

    private void OpenDoor()
    {
        isDoorOpen = true;
    }
}
