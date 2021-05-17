using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int mapRows = 13;
    public int mapColumn = 31;
    public int destructableColumns = 0;
    // Start is called before the first frame update
    void Start()
    {
        CreateMap();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateMap()
    {
        GameObject mapBase = GameObject.CreatePrimitive(PrimitiveType.Cube);
        mapBase.transform.position = new Vector3(mapRows/2, -1, mapColumn/2);
        mapBase.transform.localScale = new Vector3(mapRows, 1, mapColumn);
        mapBase.GetComponent<Renderer>().material.color = new Color32(180, 254, 180, 1);
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
                }
                
            }
        }

    }
}
