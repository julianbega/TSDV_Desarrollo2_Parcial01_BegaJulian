using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public int startingLives = 3;
    public static int playerLives = 3;
    public GameObject bombPrefab;
    public int startingMaxBombs;
    public static int maxBombs = 1;
    public int actualBombs = 0;

    public int startingBombsRange;
    public static int bombsRange =1;

    public float invulnerabilityTimeAfterHit;
    bool wasHitted;
    float timer;

    public delegate void Change(string scene);    
    public static Change ChangeScene;

    void Start()
    {
        bombsRange = startingBombsRange;
        maxBombs = startingMaxBombs;
        playerLives = startingLives;
        Bomb.hasExploted += ReduceActualBombs;
        Bomb.DamagePlayer += GetDamage;
        RedEnemyMovment.DamagePlayer += GetDamage;
    }

    private void OnDisable()
    {
        Bomb.hasExploted -= ReduceActualBombs;
        Bomb.DamagePlayer -= GetDamage;
    }
    // Update is called once per frame
    void Update()
    {
        if (wasHitted)
        {
            timer += Time.deltaTime;
        }
        if (timer >= invulnerabilityTimeAfterHit)
        {
            timer = 0;
            wasHitted = false;
        }
        if (playerLives <= 0)
        {
            GameManager.victory = false;
            ChangeScene?.Invoke("Credits");
        }
        if (Input.GetKeyDown(KeyCode.Space) && actualBombs < maxBombs)
        {
            actualBombs++;
            Instantiate(bombPrefab, new Vector3((float)Math.Round(transform.position.x, 0), transform.position.y, (float)Math.Round(transform.position.z, 0)), Quaternion.identity);
        }
    }
    void GetDamage()
    {
        if (!wasHitted)
        {
            wasHitted = true;
            this.transform.position = new Vector3(1, this.transform.lossyScale.y / 3, 1);
            playerLives--;
        }
    }
    void ReduceActualBombs()
    {
        actualBombs--;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "BombRangePowerUp")        
        {
            PowerUpGivePower pUp = other.transform.gameObject.GetComponent<PowerUpGivePower>();
            if (pUp.givePower == false)
            {
                pUp.givePower = true;
                bombsRange++;
            }
            Destroy(other.gameObject);
        }
        if (other.transform.tag == "HPPowerUp")
        {
            PowerUpGivePower pUp = other.transform.gameObject.GetComponent<PowerUpGivePower>();
            if (pUp.givePower == false)
            {
                pUp.givePower = true;
                playerLives++;
            }
            Destroy(other.gameObject);
        }
        if (other.transform.tag == "MaxBombPowerUp")
        {
            PowerUpGivePower pUp = other.transform.gameObject.GetComponent<PowerUpGivePower>();
            if (pUp.givePower == false)
            {
                pUp.givePower = true;
                maxBombs++;
            }
            Destroy(other.gameObject);
        }
    }
}

