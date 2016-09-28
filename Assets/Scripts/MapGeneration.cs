using UnityEngine;
using System.Collections;


[System.Serializable]
public class Tile
{
    public int number;
    public Color32 colour;
    public Sprite texture;
}

public class MapGeneration : MonoBehaviour {

    public int width = 12;
    public int height = 12;
    public GameObject tilePrefab;
    public GameObject playerObject;
    public Tile[] tiles;
    public Texture2D mapImage;

    public static int[][] tileMap;

    public static MapGeneration singleton;
    void Awake()
    {
        singleton = this;
    }
    void Start()
    {
        tileMap = new int[width][];
        for(int i = 0; i < width; i++)
        {
            tileMap[i] = new int[height];
        }
        CreateMap();
    }
    void ReadFromImage()
    {
        Color32[] pixels = mapImage.GetPixels32();
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                
            }
        }
    }
    void CreateMap()
    {
        for(int i = 0; i < width; i++)
        {
            for(int j = 0; j < height; j++)
            {
                int tileType;
                // If at border make tile a wall else randomise it
                if (i == width - 1 || j == height - 1 || i == 0 || j == 0)
                {
                    tileType = 1;
                }
                else
                {
                    
                    if(Mathf.RoundToInt(Random.Range(0, 100)) < 70)
                    {
                        tileType = 0;
                    }else
                    {
                        tileType = 1;
                    }
                }
                tileMap[i][j] = tileType;
                DrawTiles(tileType, i, j, new Color32(0, 0, 0, 0));
                

            }
        }
        playerObject.GetComponent<PlayerMovement>().FindStartPoint();
    }
    void DrawTiles(int tileType, int x, int y, Color32 pixelColour, bool usingColors = false)
    {
        if (usingColors)
        {
            foreach (Tile t in tiles)
            {
                if (t.colour.Equals(pixelColour))
                {
                    Instantiate(tilePrefab);
                    tilePrefab.transform.position = new Vector3(x + 0.5f, y + 0.5f, 0);
                    tilePrefab.GetComponent<SpriteRenderer>().sprite = t.texture;
                }
            }
        }
        else
        {
            foreach (Tile t in tiles)
            {
                if (t.number == tileType)
                {
                    Instantiate(tilePrefab);
                    tilePrefab.transform.position = new Vector3(x + 0.5f, y + 0.5f, 0);
                    tilePrefab.GetComponent<SpriteRenderer>().sprite = t.texture;
                }
            }
        }
    }
}
