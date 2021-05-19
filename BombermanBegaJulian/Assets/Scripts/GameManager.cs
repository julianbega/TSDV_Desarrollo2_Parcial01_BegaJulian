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
    public int destructableColumns = 10;

    public GameObject player;
    public GameObject redEnemy;

    public int redEnemiesToSpawn;
    public int purpleEnemiesToSpawn;
    public int yellowEnemiesToSpawn;
    public int totalEnemies = 0;

    public int score = 0;
    public float timer = 0;

    public GameObject PillarsParent;
    public GameObject DPillarsParent;
    public GameObject EnemiesParent;
    public GameObject DoorParent;

    List<Vector3> FreePositionsToSpawn = new List<Vector3>();
    void Start()
    {
        totalEnemies = redEnemiesToSpawn + purpleEnemiesToSpawn + yellowEnemiesToSpawn;
        if (mapRows % 2 == 0)
        {
            Debug.LogWarning("Las filas deben ser impares para la correcta creacion de mapa");
        }
        if (mapColumn % 2 == 0)
        {
            Debug.LogWarning("Las columnas deben ser impares para la correcta creacion de mapa");
        }
        int maxPosibleDColumns = ((mapRows - 2) * (mapColumn - 2)) - (((mapRows - 2) * (mapColumn - 2)) / 4) - 3 - totalEnemies;
        if (destructableColumns > maxPosibleDColumns)
        {
            destructableColumns = maxPosibleDColumns;
            Debug.LogWarning("Hay demasiadas columnas destruibles");
        }
        // tirar logwarning si setea algo mal del mapa
        DontDestroyOnLoad(this.gameObject);
        CreateMap();
        Instantiate(player, new Vector3(1, player.transform.localScale.y/2, 1), Quaternion.identity);
        SpawnRedEnemies();
    }

   

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
    }

    void CreateMap()
    {
        GameObject mapBase = GameObject.CreatePrimitive(PrimitiveType.Cube);
        mapBase.transform.position = new Vector3(mapRows/2, -1, mapColumn/2);
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
                if((j % 2==0 && i % 2 == 0 )||j == 0 || i == 0|| j == mapColumn-1 || i== mapRows-1)
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
        CreateSpawnPositionList();
        CreateDestroyablePillars(mapBase);
    }
    void CreateDestroyablePillars(GameObject mapBase)
    {
        int posInListOfDoor = UnityEngine.Random.Range(0, destructableColumns);        
        
        for (int i = 0; i < destructableColumns; i++)
        {            
            GameObject dPillar = GameObject.CreatePrimitive(PrimitiveType.Cube);
            int actualDPillar = UnityEngine.Random.Range(0, FreePositionsToSpawn.Count);
            if (i == posInListOfDoor)
            {
                CreatDoor(actualDPillar);
               
            }
            dPillar.transform.position = FreePositionsToSpawn[actualDPillar];
            FreePositionsToSpawn.Remove(FreePositionsToSpawn[actualDPillar]);
            dPillar.transform.SetParent(DPillarsParent.transform);
            dPillar.GetComponent<Renderer>().material.color = Color.black;
            dPillar.transform.gameObject.tag = "DestroyablePillar";
            dPillar.name = "DestroyablePillar";
            dPillar.isStatic = true;

        }

    }

    private void CreatDoor(int pos)
    {
        GameObject door = GameObject.CreatePrimitive(PrimitiveType.Cube);
        door.transform.position = FreePositionsToSpawn[pos];
        door.transform.SetParent(DoorParent.transform);
        door.GetComponent<Renderer>().material.color = Color.red;
        door.transform.localScale = new Vector3(door.transform.localScale.x - door.transform.localScale.x / 3, door.transform.localScale.y - door.transform.localScale.y / 3, door.transform.localScale.z - door.transform.localScale.z / 3);
        door.transform.gameObject.tag = "Door";
        door.name = "Door";
        door.isStatic = true;
        door.GetComponent<Collider>().isTrigger = true;
    }
    private void SpawnRedEnemies()
    {

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
}
