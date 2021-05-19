using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public delegate void Explode();
    public static Explode hasExploted;
    public delegate void HitPlayer();
    public static Explode DamagePlayer;
    public float timeToExplode;
    private float actualTimer;
    private bool allreadyExplode;

    [SerializeField]public GameObject flames;

    private RaycastHit myHitLeft;
    private RaycastHit myHitRight;
    private RaycastHit myHitBack;
    private RaycastHit myHitForward;
    private Ray myRayLeft;
    private Ray myRayRight;
    private Ray myRayBack;
    private Ray myRayForward;

    
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
        if (actualTimer >= timeToExplode + 1)
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

        Quaternion leftSide = new Quaternion(-1, 0, 0, 1);
        Quaternion rightSide = new Quaternion(1, 0, 0, 1);
        Quaternion frontSide = new Quaternion(0, 0, 1, 1);
        Quaternion backSide = new Quaternion(0, 0, -1, 1);
        ExplosionRaycast(myRayForward, myHitForward, Vector3.forward, frontSide);
        ExplosionRaycast(myRayLeft, myHitLeft, Vector3.left, leftSide);
        ExplosionRaycast(myRayRight, myHitRight, Vector3.right, rightSide);
        ExplosionRaycast(myRayBack, myHitBack, Vector3.back, backSide);

        Instantiate(flames, this.transform.position, Quaternion.identity);  
    }

    void ExplosionRaycast(Ray myRay, RaycastHit myRHit, Vector3 direction, Quaternion fireDir)
    {
        myRay = new Ray(this.transform.position, direction);
        if (Physics.Raycast(myRay, out myRHit, PlayerManager.bombsRange + 0.4f))
        {
            if (myRHit.transform.gameObject.tag == "DestroyablePillar" || myRHit.transform.gameObject.tag == "Enemy")
            {
                Destroy(myRHit.transform.gameObject);

                GameObject fire1;
                for (int i = 0; i < PlayerManager.bombsRange; i++)
                {
                    fire1 = Instantiate(flames, this.transform.position, fireDir);
                    fire1.transform.position += (direction * (i + 1));
                }
            }
            if (myRHit.transform.gameObject.tag == "Player")
            {
                DamagePlayer?.Invoke();
                GameObject fire1;
                for (int i = 0; i < PlayerManager.bombsRange; i++)
                {
                    fire1 = Instantiate(flames, this.transform.position, fireDir);
                    fire1.transform.position += (direction * (i + 1));
                }
            }
        }
        else 
        {
            GameObject fire1;
            for (int i = 0; i < PlayerManager.bombsRange; i++)
            {
                fire1 = Instantiate(flames, this.transform.position, fireDir);
                fire1.transform.position += (direction * (i + 1));
            }
        }
        
    }
}

