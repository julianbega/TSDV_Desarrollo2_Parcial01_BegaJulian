using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Range(11, 99)]
    public int mapRows = 13;
    [Range(11, 99)]
    public int mapColumn = 31;
    [Range(1, 246)]
    public int defaultdDestructablePillar = 10;

    [Range(0, 10)]
    public int destructablePillarsWithBombRangePowerUp;
    [Range(0, 10)]
    public int destructablePillarsWithHPPowerUp;
    [Range(0, 10)]
    public int destructablePillarsWithMaxBombPowerUp;

    public int redEnemiesCuantity;
    private int purpleEnemiesCuantity;
    private int yellowEnemiesCuantity;
    public int totalEnemies = 0;
    public int pointsPerEnemyKill;

    public int score = 0;
    public float timer = 0;
    public static bool victory = true;



    public GameObject player;
    public GameObject redEnemyPrefab;
    public GameObject purpleEnemyPrefab;
    public GameObject yellowEnemyPrefab;
    public GameObject doorPrefab;
    public GameObject DPillars;

    public GameObject PillarsParent;
    public GameObject DPillarsParent;
    public GameObject EnemiesParent;
    public GameObject DoorParent;
    public GameObject PowerUpParent;

    public GameObject BombRangePowerUp;
    public GameObject HPPowerUp;
    public GameObject MaxBombPowerUp;

    public delegate void DoorIsOpen(int enemies);
    public static DoorIsOpen checkOpenDoor;


    List<Vector3> FreePositionsToSpawn = new List<Vector3>();
    void Start()
    {
        Bomb.ReduceTotalEnemies += ReduceTotalEnemies;
        totalEnemies = redEnemiesCuantity + purpleEnemiesCuantity + yellowEnemiesCuantity;
        if (mapRows % 2 == 0)
        {
            Debug.LogWarning("Las filas deben ser impares para la correcta creacion de mapa");
        }
        if (mapColumn % 2 == 0)
        {
            Debug.LogWarning("Las columnas deben ser impares para la correcta creacion de mapa");
        }
        int maxPosibleDColumns = ((mapRows - 2) * (mapColumn - 2)) - (((mapRows - 2) * (mapColumn - 2)) / 4) - 3 - totalEnemies;
        if (defaultdDestructablePillar + destructablePillarsWithBombRangePowerUp  + destructablePillarsWithHPPowerUp + destructablePillarsWithMaxBombPowerUp > maxPosibleDColumns)
        {
            defaultdDestructablePillar = maxPosibleDColumns - destructablePillarsWithBombRangePowerUp - destructablePillarsWithHPPowerUp - destructablePillarsWithMaxBombPowerUp;
            Debug.LogWarning("Hay demasiadas columnas destruibles");
        }
        DontDestroyOnLoad(this.gameObject);
        CreateMap();
        Instantiate(player, new Vector3(1, player.transform.lossyScale.y/3, 1), Quaternion.identity);
        SpawnEnemies(redEnemyPrefab, redEnemiesCuantity);
        SpawnEnemies(purpleEnemyPrefab, purpleEnemiesCuantity);
        SpawnEnemies(yellowEnemyPrefab, yellowEnemiesCuantity);

        checkOpenDoor?.Invoke(totalEnemies);
    }

    private void OnDisable()
    {       
        Bomb.ReduceTotalEnemies -= ReduceTotalEnemies;       
    }

    void Update()
    {
        timer += Time.deltaTime;
    }
    
    void CreateMap()
    {
        CreateBaseMap();
        CreateSpawnPositionList();
        CreateDestroyablePillars();
    }

    private void CreateBaseMap()
    {
        GameObject mapBase = GameObject.CreatePrimitive(PrimitiveType.Cube);
        mapBase.transform.position = new Vector3(mapRows / 2, -1, mapColumn / 2);
        mapBase.transform.localScale = new Vector3(mapRows, 1, mapColumn);
        mapBase.GetComponent<Renderer>().material.color = new Color32(180, 254, 180, 1);
        mapBase.transform.gameObject.tag = "Map";
        mapBase.name = "mapBase";
        mapBase.transform.SetParent(PillarsParent.transform);
        mapBase.isStatic = true;
        for (int i = 0; i < mapRows; i++)
        {
            for (int j = 0; j < mapColumn; j++)
            {
                if ((j % 2 == 0 && i % 2 == 0) || j == 0 || i == 0 || j == mapColumn - 1 || i == mapRows - 1)
                {
                    GameObject pillar = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    pillar.transform.position = new Vector3(i, 0.0f, j);
                    pillar.transform.SetParent(PillarsParent.transform);
                    pillar.GetComponent<Renderer>().material.color = Color.gray;
                    pillar.transform.gameObject.tag = "Map";
                    pillar.name = "pillar";
                    pillar.isStatic = true;
                }

            }
        }
    }
    private void CreateDestroyablePillars()
    {
        int posInListOfDoor = UnityEngine.Random.Range(0, defaultdDestructablePillar);

        for (int i = 0; i < defaultdDestructablePillar; i++)
        {
            GameObject desPillar = Instantiate(DPillars);
            int actualDesPillar = UnityEngine.Random.Range(0, FreePositionsToSpawn.Count);
            if (i == posInListOfDoor)
            {
                CreatDoor(actualDesPillar);

            }
            desPillar.transform.position = FreePositionsToSpawn[actualDesPillar];
            FreePositionsToSpawn.Remove(FreePositionsToSpawn[actualDesPillar]);
            desPillar.transform.SetParent(DPillarsParent.transform);
        }
        CreatePowerUpPillars(BombRangePowerUp, destructablePillarsWithBombRangePowerUp);
        CreatePowerUpPillars(HPPowerUp, destructablePillarsWithHPPowerUp);
        CreatePowerUpPillars(MaxBombPowerUp, destructablePillarsWithMaxBombPowerUp);

    }

    private void CreatePowerUpPillars(GameObject PowerUpType, int cuantity)
    {
        for (int i = 0; i < cuantity; i++)
        {

            int actualDesPillar = UnityEngine.Random.Range(0, FreePositionsToSpawn.Count);
            GameObject desPillar = Instantiate(DPillars);
            desPillar.transform.position = FreePositionsToSpawn[actualDesPillar];            
            desPillar.transform.SetParent(DPillarsParent.transform);

            GameObject powerUp = Instantiate(PowerUpType);
            powerUp.transform.position = FreePositionsToSpawn[actualDesPillar];
            powerUp.transform.SetParent(PowerUpParent.transform);

            FreePositionsToSpawn.Remove(FreePositionsToSpawn[actualDesPillar]);
        }
    }
    private void CreatDoor(int pos)
    {        
        GameObject door = Instantiate(doorPrefab, new Vector3(FreePositionsToSpawn[pos].x, -0.45f, FreePositionsToSpawn[pos].z), Quaternion.identity);
        door.transform.SetParent(DoorParent.transform);       
        door.GetComponent<Collider>().isTrigger = true;
    }

    private void SpawnEnemies(GameObject EnemyPrefab, int EnemyCount)
    {
        for (int i = 0; i < EnemyCount; i++)
        {
            int actualEnemy = UnityEngine.Random.Range(0, FreePositionsToSpawn.Count);
            GameObject Enemy;
            Enemy = Instantiate(EnemyPrefab, new Vector3(FreePositionsToSpawn[actualEnemy].x, EnemyPrefab.transform.localScale.y / 2, FreePositionsToSpawn[actualEnemy].z), Quaternion.identity);

            FreePositionsToSpawn.Remove(FreePositionsToSpawn[actualEnemy]);
            Enemy.transform.SetParent(EnemiesParent.transform);
        }
    }

    private void CreateSpawnPositionList()
    {
        for (int i = 0; i < mapRows; i++)
        {
            for (int j = 0; j < mapColumn; j++)
            {
                //ignora bordes del mapa y posiciones donde X y Z son pares (ya que si ambos son pares hay una columna)
                if ((j % 2 != 0 || i % 2 != 0) && j != mapColumn - 1 && i != mapRows - 1 && j != 0 && i != 0)
                {
                    //ignora el punto de spawn del player y las casillas adyacentes
                    if (j == 1 && i == 1 || j == 1 && i == 2 || j == 2 && i == 1) { }
                    else
                    {
                        Vector3 Aux;
                        Aux = new Vector3(i, 0.0f, j);
                        FreePositionsToSpawn.Add(Aux);
                    }
                }

            }
        }
    }

    private void ReduceTotalEnemies()
    {
        totalEnemies--;
        score += pointsPerEnemyKill;
        checkOpenDoor?.Invoke(totalEnemies);
    }

}
