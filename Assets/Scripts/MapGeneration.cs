using UnityEngine;
using System.Collections.Generic;


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
    public GameObject[] playerObject;
    public Tile[] tiles;
    public Texture2D mapImage;

    public static int[][] tileMap;

    public Sprite openGate;
    public Sprite closedGate;
    public GameObject gateToggleObject;
    public GameObject coinPrefab;

    Color32[] pixels;
    public List<Transform> tileTransforms;
    public static MapGeneration singleton;
    void Awake()
    {
        singleton = this;
    }
    void Start()
    {
        pixels = mapImage.GetPixels32();
        height = mapImage.height;
        width = mapImage.width;
        tileTransforms = new List<Transform>();
        tileMap = new int[width][];
        for(int i = 0; i < width; i++)
        {
            tileMap[i] = new int[height];
        }
        ReadFromImage();
        SetGateOrientation();
        //CreateMap();
    }
    void ReadFromImage()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                DrawTiles(0, i, j, pixels[j * height + i], true);
            }
        }
        foreach (GameObject p in playerObject)
        {
            p.GetComponent<PlayerMovement>().FindStartPoint();
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
        //playerObject.GetComponent<PlayerMovement>().FindStartPoint();
    }
    void DrawTiles(int tileType, int x, int y, Color32 pixelColour, bool usingColors = false)
    {
        if (usingColors)
        {
            foreach (Tile t in tiles)
            {
                if (t.colour.Equals(pixelColour))
                {
                    tileMap[x][y] = t.number;

                    GameObject newTile = (GameObject)Instantiate(tilePrefab);
                    newTile.transform.position = new Vector3(x + 0.5f, y + 0.5f, 0);
                    tileTransforms.Add(newTile.transform);
                    newTile.GetComponent<SpriteRenderer>().sprite = t.texture;
                    if (t.number == 0)
                    {
                        Instantiate(coinPrefab, new Vector3(newTile.transform.position.x, newTile.transform.position.y, -0.1f), Quaternion.identity);
                        Scores.coinsLeft += 1;
                    }
                    if (t.number == 4)
                    {
                        Instantiate(gateToggleObject, new Vector3(newTile.transform.position.x, newTile.transform.position.y, -0.1f), Quaternion.identity);
                    }
                    
                }
            }
        }
        else
        {
            foreach (Tile t in tiles)
            {
                if (t.number == tileType)
                {
                    GameObject newTile = (GameObject) Instantiate(tilePrefab);
                    newTile.transform.position = new Vector3(x + 0.5f, y + 0.5f, 0);
                    tilePrefab.GetComponent<SpriteRenderer>().sprite = t.texture;
                }
            }
        }
    }
    void SetGateOrientation()
    {
        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                if(tileMap[x][y] == 2 || tileMap[x][y] == 3)
                {
                    if(tileMap[x][y+1] == 1 && tileMap[x][y - 1] == 1)
                    {
                        tileTransforms[(x * width) + y].Rotate(new Vector3(0, 0, 90));
                    }
                }
            }
        }
    }

    public void ToggleGates()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if(tileMap[x][y] == 2)
                {
                    tileMap[x][y] = 3;
                    tileTransforms[(x * width) + y].GetComponent<SpriteRenderer>().sprite = closedGate;
                }else if(tileMap[x][y] == 3)
                {
                    tileMap[x][y] = 2;
                    tileTransforms[(x * width) + y].GetComponent<SpriteRenderer>().sprite = openGate;
                }
            }
        }
    }
}
