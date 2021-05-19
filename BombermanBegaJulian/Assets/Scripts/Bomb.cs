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
    public bool allreadyHitLeftPillar;
    public bool allreadyHitRightPillar;
    public bool allreadyHitFrontPillar;
    public bool allreadyHitBackPillar;
    private bool allreadyHitPlayer;
    private int actualParticlesInstanciated;
    private int particlesToInstanciate;

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
        allreadyHitLeftPillar = false;
        allreadyHitRightPillar = false;
        allreadyHitFrontPillar = false;
        allreadyHitBackPillar = false;
        allreadyHitPlayer = false;
        actualParticlesInstanciated = 0;
        particlesToInstanciate = PlayerManager.bombsRange *4 + 1;
        actualTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        actualTimer += Time.deltaTime;
        if (actualTimer >= timeToExplode)
        {
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


        ExplosionRaycast(myRayForward, myHitForward, Vector3.forward, frontSide, ref allreadyHitFrontPillar);
        ExplosionRaycast(myRayLeft, myHitLeft, Vector3.left, leftSide, ref allreadyHitLeftPillar);
        ExplosionRaycast(myRayRight, myHitRight, Vector3.right, rightSide, ref allreadyHitRightPillar);
        ExplosionRaycast(myRayBack, myHitBack, Vector3.back, backSide, ref allreadyHitBackPillar);
        if (actualParticlesInstanciated < particlesToInstanciate)
        {
            Instantiate(flames, this.transform.position, Quaternion.identity);
            actualParticlesInstanciated++;
        }

        Debug.Log("allreadyHitFrontPillar " + allreadyHitFrontPillar);
        Debug.Log("allreadyHitLeftPillar" + allreadyHitLeftPillar);
        Debug.Log("allreadyHitRightPillar" + allreadyHitRightPillar);
        Debug.Log("allreadyHitBackPillar" + allreadyHitBackPillar);
    }

    void ExplosionRaycast(Ray myRay, RaycastHit myRHit, Vector3 direction, Quaternion fireDir, ref bool PillarHitted)
    {
        myRay = new Ray(this.transform.position, direction);
        if (Physics.Raycast(myRay, out myRHit, PlayerManager.bombsRange + 0.4f))
        {
            if (myRHit.transform.gameObject.tag == "DestroyablePillar" && PillarHitted == false)
            {
                PillarHitted = true;
                Destroy(myRHit.transform.gameObject);
                                  
                    GameObject fire1;
                for (int i = 0; i < Mathf.Ceil(myRHit.distance); i++)
                {
                    actualParticlesInstanciated++;
                    fire1 = Instantiate(flames, this.transform.position, fireDir);
                    fire1.transform.position += (direction * (i + 1));
                }

            }
            if (myRHit.transform.gameObject.tag == "Player" && allreadyHitPlayer == false && PillarHitted == false)
            {
                allreadyHitPlayer = true;
                DamagePlayer?.Invoke();
                if (actualParticlesInstanciated < particlesToInstanciate)
                {
                    GameObject fire1;
                    for (int i = 0; i < PlayerManager.bombsRange; i++)
                    {
                        actualParticlesInstanciated++;
                        fire1 = Instantiate(flames, this.transform.position, fireDir);
                        fire1.transform.position += (direction * (i + 1));
                    }
                }
            }
        }
        else
        {
            if (actualParticlesInstanciated < particlesToInstanciate && PillarHitted == false)
            {
                GameObject fire1;
                for (int i = 0; i < PlayerManager.bombsRange; i++)
                {
                    actualParticlesInstanciated++;
                    fire1 = Instantiate(flames, this.transform.position, fireDir);
                    fire1.transform.position += (direction * (i + 1));
                }
            }
        }

    }
}

