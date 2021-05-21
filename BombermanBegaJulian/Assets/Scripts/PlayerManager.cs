using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public int playerLives = 3;
    private GameManager gameManager;
    public GameObject bombPrefab;
    public int maxBombs = 1;
    public int actualBombs = 0;
    [SerializeField]public static int bombsRange =1;

    public float invulnerabilityTimeAfterHit;
    bool wasHitted;
    float timer;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
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
            Destroy(gameManager.gameObject);
            SceneManager.LoadScene("Game");
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
}

