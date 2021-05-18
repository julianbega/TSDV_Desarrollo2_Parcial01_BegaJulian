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
    }
}
