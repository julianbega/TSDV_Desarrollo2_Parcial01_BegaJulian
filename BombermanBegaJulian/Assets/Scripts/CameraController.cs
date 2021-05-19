using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private PlayerManager lookAtThat;

    [SerializeField] [Range(5, 15)] public float verticalDistance;
    [SerializeField] [Range(4, 10)] public float horizontalDistanceX;
    [SerializeField] [Range(-3, 3)] public float horizontalDistanceZ;
    public float smothSpeed;
    private Vector3 zoom;
    bool start = false;
    bool allreadyFocusCamera;

    private Vector3 posToMoveTowards;

    private void Start()
    {
        start = false;
        allreadyFocusCamera = false;
        smothSpeed = 5;
    }
    void LateUpdate()
    {
        if (start == false)
        {
            start = true;
            lookAtThat = FindObjectOfType<PlayerManager>();
        }
        MoveCameraToFolowTarget();
        
        if (!allreadyFocusCamera)
        {
            allreadyFocusCamera = true;
            transform.LookAt(lookAtThat.transform);
        }
    }
    public void MoveCameraToFolowTarget()
    {
        Vector3 myPos = transform.position;

        zoom = new Vector3(-horizontalDistanceX, verticalDistance, horizontalDistanceZ);

        posToMoveTowards = lookAtThat.transform.position + zoom;
      

        transform.position = Vector3.Lerp(myPos, posToMoveTowards, Vector3.Distance(myPos, posToMoveTowards) * Time.deltaTime * smothSpeed);
    }
}
