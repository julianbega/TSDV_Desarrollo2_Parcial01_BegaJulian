using UnityEngine;
using UnityEngine.UI;
using System;

public class EndGame : MonoBehaviour
{
    GameManager gm;

    public Image Victoy;
    public Image Defeat;

    public Text Timer;
    public Text Score;
    public Text MaxBombs;
    public Text BombRange;
    public Text EnemiesKilled;
    public Text Lives;

    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        if (GameManager.victory)
        {
            Victoy.enabled = true;
        }
        else 
        {
            Defeat.enabled = true;
        }

        Score.text = "Score: " + gm.score;
        MaxBombs.text = "MaxBombs: " + PlayerManager.maxBombs;
        BombRange.text = "BombRange: " + PlayerManager.bombsRange;
        int realEnemiesKilled = ((gm.yellowEnemiesCuantity + gm.purpleEnemiesCuantity + gm.redEnemiesCuantity) - gm.totalEnemies);
        EnemiesKilled.text = "EnemiesKilled: " + realEnemiesKilled;
        Lives.text = "PlayerLives: " + PlayerManager.playerLives;

        int sec = (int)gm.timer % 60;
        int min = (int)gm.timer / 60;

        Timer.text = "Timer: " + min + " : " + sec;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
