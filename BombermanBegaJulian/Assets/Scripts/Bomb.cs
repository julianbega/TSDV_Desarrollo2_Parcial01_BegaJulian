using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float timeToExplode;
    private float actualTimer;
    void Start()
    {
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
        Destroy(this.gameObject);
    }
}
