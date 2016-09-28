using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    public MapGeneration map;

    private int currentX;
    private int currentY;

    private bool isMoving = false;

    public void FindStartPoint()
    {
        for(int x = 0; x < map.width; x++)
        {
            for (int y = 0; y < map.height; y++)
            {
                if (map.tileMap[x][y] == 0)
                {
                    currentX = x;
                    currentY = y;
                    transform.position = new Vector3(x + 0.5f, 0.1f, y + 0.5f);
                    return;
                }
            }
        }
    }

    void Update()
    {
        if (isMoving)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(currentX + 0.5f, 0.1f, currentY + 0.5f), 0.1f);

            if ((int)transform.position.x == (int)currentX && (int)transform.position.z == (int)currentY)
            {
                isMoving = false;
            }
            return;
        }
        int hInput = (int)Input.GetAxisRaw("Horizontal");
        Debug.Log(hInput);
        if(hInput == 0)
        {
            return;
        }

        if (CheckForObstruction(currentX + hInput, currentY))
        {
            currentX += hInput;
            isMoving = true;
        }
    }
	
    bool CheckForObstruction(int x, int y)
    {
        Debug.Log(map.tileMap[x][y]);
        if (map.tileMap[x][y] == 1 || map.tileMap[x][y] == 0)
        {

            return true;
        }else
        {

            return true;
        }
    }
}
