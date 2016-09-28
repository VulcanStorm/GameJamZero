using UnityEngine;
using System.Collections;


[System.Serializable]
public class Tile
{
    public int number;
    public Sprite texture;
}

public class MapGeneration : MonoBehaviour {

    public int width = 12;
    public int height = 12;
    public GameObject tilePrefab;
    public GameObject playerObject;
    public Tile[] tiles;

    public int[][] tileMap;
    
    void Start()
    {
        tileMap = new int[width][];
        for(int i = 0; i < width; i++)
        {
            tileMap[i] = new int[height];
        }
        CreateMap();
    }

    void CreateMap()
    {
        for(int i = 0; i < width; i++)
        {
            for(int j = 0; j < height; j++)
            {
                // Generate tile type
                int tileType = Mathf.RoundToInt(Random.Range(0, 2));
                tileMap[i][j] = tileType;
                DrawTiles(tileType, i, j);
                

            }
        }
        playerObject.GetComponent<PlayerMovement>().FindStartPoint();
    }
    void DrawTiles(int tileType, int x, int y)
    {
        foreach (Tile t in tiles)
        {
            if (t.number == tileType)
            {
                Instantiate(tilePrefab);
                tilePrefab.transform.position = new Vector3(x + 0.5f, 0, y+0.5f);
                tilePrefab.GetComponent<SpriteRenderer>().sprite = t.texture;
            }
        }
    }
}
