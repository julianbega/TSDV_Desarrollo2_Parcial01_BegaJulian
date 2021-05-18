using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text Timer;
    public Text Score;
    public Text MaxBombs;
    public Text BombRange;
    public Text EnemiesLeft;
    public Text Lives;

    private PlayerManager playerInfo;
    private GameManager gameInfo;
    bool start = false;

    // Update is called once per frame
    void Update()
    {
        if (start == false)
        {
            start = true;
            playerInfo = FindObjectOfType<PlayerManager>();
            gameInfo = FindObjectOfType<GameManager>();
        }

        Timer.text = "Timer: " + gameInfo.timer;
        Score.text = "Score: " + gameInfo.score;
        MaxBombs.text = "MaxBombs: " + playerInfo.maxBombs;
        BombRange.text= "BombRange: " + playerInfo.bombsRange;
        EnemiesLeft.text= "EnemiesLeft: " + gameInfo.totalEnemies;
        Lives.text= "PlayerLives: " + playerInfo.playerLives;
    }
}
