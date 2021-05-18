using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public delegate void Explode();
    public static Explode hasExploted;
    public float timeToExplode;
    private float actualTimer;
    private bool allreadyExplode;

    RaycastHit myHitLeft;
    RaycastHit myHitRight;
    RaycastHit myHitBack;
    RaycastHit myHitForward;
    Ray myRayLeft;
    Ray myRayRight;
    Ray myRayBack;
    Ray myRayForward;
    void Start()
    {
        actualTimer = 0;
        allreadyExplode = false;
    }

    // Update is called once per frame
    void Update()
    {
        actualTimer += Time.deltaTime;
        if (actualTimer >= timeToExplode && allreadyExplode == false)
        {
            allreadyExplode = true;
            Explosion();
        }
        if (actualTimer >= timeToExplode+1)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            this.GetComponent<SphereCollider>().isTrigger = false;
        }
    }

    void Explosion()
    {
        hasExploted?.Invoke();
        this.GetComponent<Renderer>().enabled = false;
       // this.GetComponentInChildren<Renderer>().enabled = false;
        this.GetComponent<Renderer>().enabled = false;
        this.GetComponent<Collider>().enabled = false;


        myRayForward = new Ray(this.transform.position, Vector3.forward);
        if (Physics.Raycast(myRayForward, out myHitForward, PlayerManager.bombsRange))
        {
            if (myHitForward.transform.gameObject.tag == "DestroyablePillar")
            {
                Destroy(myHitForward.transform.gameObject);
            }
            
        }
        
        myRayLeft = new Ray(this.transform.position, Vector3.left);
        if (Physics.Raycast(myRayLeft, out myHitLeft, PlayerManager.bombsRange))
        {
            if (myHitLeft.transform.gameObject.tag == "DestroyablePillar")
            {
                Destroy(myHitLeft.transform.gameObject);
            }

        }
        myRayRight = new Ray(this.transform.position, Vector3.right);
        if (Physics.Raycast(myRayRight, out myHitRight, PlayerManager.bombsRange))
        {
            if (myHitRight.transform.gameObject.tag == "DestroyablePillar")
            {
                Destroy(myHitRight.transform.gameObject);
            }

        }
        myRayBack = new Ray(this.transform.position, Vector3.back);
        if (Physics.Raycast(myRayBack, out myHitBack, PlayerManager.bombsRange))
        {
            if (myHitBack.transform.gameObject.tag == "DestroyablePillar")
            {
                Destroy(myHitBack.transform.gameObject);
            }

        }

    }
}
