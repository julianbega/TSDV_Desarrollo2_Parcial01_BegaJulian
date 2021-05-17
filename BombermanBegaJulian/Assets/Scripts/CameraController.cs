using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] public PlayerManager lookAtThat;

    [SerializeField] [Range(5, 15)] public float verticalDistance;
    [SerializeField] [Range(3, 10)] public float horizontalDistanceX;
    [SerializeField] [Range(-3, 3)] public float horizontalDistanceZ;
    public float smothSpeed;
    private Vector3 zoom;
    bool start = false;

    private Vector3 posToMoveTowards;

    private void Start()
    {
        smothSpeed = 5;
    }
    void LateUpdate()
    {
        if (start == false)
        {
            start = true;
            lookAtThat = FindObjectOfType<PlayerManager>();
        }
        FocusToTargetAndMove();
    }
    public void FocusToTargetAndMove()
    {
        Vector3 myPos = transform.position;

        zoom = new Vector3(-horizontalDistanceX, verticalDistance, horizontalDistanceZ);

        posToMoveTowards = lookAtThat.transform.position + zoom;

       transform.LookAt(lookAtThat.transform);

        transform.position = Vector3.Lerp(myPos, posToMoveTowards, Vector3.Distance(myPos, posToMoveTowards) * Time.deltaTime * smothSpeed);
    }
}
