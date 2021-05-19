using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private PlayerManager lookAtThat;

    [SerializeField] [Range(5, 15)] public float verticalDistance;
    [SerializeField] [Range(4, 10)] public float horizontalDistanceX;
    [SerializeField] [Range(-3, 3)] public float horizontalDistanceZ;
    private Vector3 zoom;

    private Vector3 posToMoveTowards;

    float timer;
    private void Start()
    {
        timer = 0;
        lookAtThat = FindObjectOfType<PlayerManager>();
    }
    void LateUpdate()
    {
        if (lookAtThat == null)
        {
            lookAtThat = FindObjectOfType<PlayerManager>();

        }
        MoveCameraToFolowTarget();

        if (timer <= 0.1)
        {
            timer += Time.deltaTime;
            LookAtPlayer();
        }
    }
    public void LookAtPlayer()
    {
        transform.LookAt(lookAtThat.transform);
    }
    public void MoveCameraToFolowTarget()
    {
        Vector3 myPos = transform.position;

        zoom = new Vector3(-horizontalDistanceX, verticalDistance, horizontalDistanceZ);

        posToMoveTowards = lookAtThat.transform.position + zoom;      

        transform.position = Vector3.Lerp(myPos, posToMoveTowards, Vector3.Distance(myPos, posToMoveTowards) * Time.deltaTime);
    }
}
