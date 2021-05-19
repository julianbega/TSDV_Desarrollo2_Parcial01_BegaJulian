using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Range(3, 99)]
    public int mapRows = 13;
    [Range(3, 99)]
    public int mapColumn = 31;
    [Range(1, 246)]
    public int destructableColumns = 10;
    public int score = 0;
    public float timer = 0;
    public GameObject player;
    public int totalEnemies = 0;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        CreateMap();
        Instantiate(player, new Vector3(1, player.transform.localScale.y/2, 1), Quaternion.identity);
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
        mapBase.isStatic = true;
        for (int i = 0; i < mapRows; i++)
        {
            for (int j = 0; j < mapColumn; j++)
            {
                if((j % 2==0 && i % 2 == 0 )||j == 0 || i == 0|| j == mapColumn-1 || i== mapRows-1)
                {
                    GameObject pillar = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    pillar.transform.position = new Vector3(i, 0.0f, j);
                    pillar.transform.SetParent(mapBase.transform);
                    pillar.GetComponent<Renderer>().material.color = Color.gray;
                    pillar.transform.gameObject.tag = "Map";
                    pillar.name = "pillar";
                    pillar.isStatic = true;
                }
                
            }
        }
        CreateDestroyablePillars(mapBase);
    }
    void CreateDestroyablePillars(GameObject mapBase)
    {
        int posInListOfDoor = Random.Range(0, destructableColumns);
        List<Vector3> PosibleDestroyablePillarsPositions = new List<Vector3>();
        for (int i = 0; i < mapRows; i++)
        {
            for (int j = 0; j < mapColumn; j++)
            {
                if ((j % 2 != 0 || i % 2 != 0) && j != mapColumn - 1 && i != mapRows - 1 && j !=0 && i !=0 )
                {
                    if (j == 1 && i == 1 || j == 1 && i == 2 || j == 2 && i == 1) { }
                    else
                    {
                        Vector3 Aux;
                        Aux = new Vector3(i, 0.0f, j);
                        PosibleDestroyablePillarsPositions.Add(Aux);
                    }
                }

            }
        }
        for (int i = 0; i < destructableColumns; i++)
        {            
            GameObject dPillar = GameObject.CreatePrimitive(PrimitiveType.Cube);
            int actualDPillar = Random.Range(0, PosibleDestroyablePillarsPositions.Count);
            if (i == posInListOfDoor)
            {
                GameObject door = GameObject.CreatePrimitive(PrimitiveType.Cube);
                door.transform.position = PosibleDestroyablePillarsPositions[actualDPillar];
                door.transform.SetParent(mapBase.transform);
                door.GetComponent<Renderer>().material.color = Color.red;
               // door.transform.localScale = new Vector3(1, 1, 1);
                door.transform.localScale = new Vector3(door.transform.localScale.x- door.transform.localScale.x/3, door.transform.localScale.y - door.transform.localScale.y / 3, door.transform.localScale.z - door.transform.localScale.z / 3);
                door.transform.gameObject.tag = "Door";
                door.name = "Door";
                door.isStatic = true;
                door.GetComponent<Collider>().isTrigger = true;
            }
            dPillar.transform.position = PosibleDestroyablePillarsPositions[actualDPillar];
            PosibleDestroyablePillarsPositions.Remove(PosibleDestroyablePillarsPositions[actualDPillar]);
            dPillar.transform.SetParent(mapBase.transform);
            dPillar.GetComponent<Renderer>().material.color = Color.black;
            dPillar.transform.gameObject.tag = "DestroyablePillar";
            dPillar.name = "DestroyablePillar";
            dPillar.isStatic = true;


        }

    }
}
