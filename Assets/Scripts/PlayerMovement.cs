using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    public MapGeneration map;

    public Vector3 startPoint;

    private int currentX;
    private int currentY;

    private bool isMoving = false;

    private string direction = "right";
    public string horizontalName = "Horizontal";
    public string verticalName = "Vertical";


    public void FindStartPoint()
    {
        for(int x = 0; x < map.width; x++)
        {
            for (int y = 0; y < map.height; y++)
            {
                if (MapGeneration.tileMap[x][y] == 0)
                {
                    currentX = x;
                    currentY = y;
                    transform.position = new Vector3(x + 0.5f, y + 0.5f, -0.1f);
                    return;
                }
            }
        }
        startPoint = transform.position;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Coin")
        {
            other.gameObject.SetActive(false);

            if (gameObject.name == "Player1")
            {
                Scores.player1Score += 1;
                Scores.coinsLeft -= 1;
            }else
            {
                Scores.player2Score += 1;
                Scores.coinsLeft -= 1;
            }
        }
    }
    void Update()
    {
        if (isMoving)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(currentX + 0.5f, currentY + 0.5f, 0.1f), 10 * Time.deltaTime);
            if (Mathf.Abs(transform.position.x - (currentX +0.5f)) <0.2f && Mathf.Abs(transform.position.y - (currentY+0.5f)) < 0.2f)
            {
                transform.position = new Vector3(currentX + 0.5f, currentY + 0.5f, 0.1f);
                isMoving = false;
            }
            
        }
        int hInput = (int)Input.GetAxisRaw(horizontalName);
        if(hInput == 1)
        {
            direction = "right";
        }else if(hInput == -1)
        {
            direction = "left";
        }
        int vInput = (int)Input.GetAxisRaw(verticalName);
        if(vInput == 1)
        {
            direction = "up";
        }else if (vInput == -1)
        {
            direction = "down";
        }
        if (!isMoving)
        {
            if (direction == "right")
            {
                if (CheckForObstruction(currentX + 1, currentY))
                {
                    currentX += 1;
                    isMoving = true;
                }
                else
                {
                    direction = "";
                }
            }
            else if (direction == "left")
            {
                if (CheckForObstruction(currentX - 1, currentY))
                {
                    currentX -= 1;
                    isMoving = true;
                }
                else
                {
                    direction = "";
                }
            }
            else if (direction == "up")
            {
                if (CheckForObstruction(currentX, currentY + 1))
                {
                    currentY += 1;
                    isMoving = true;
                }
                else
                {
                    direction = "";
                }
            }
            else if (direction == "down")
            {
                if (CheckForObstruction(currentX, currentY - 1))
                {
                    currentY -= 1;
                    isMoving = true;
                }
                else
                {
                    direction = "";
                }
            }
        }

        
    }
	
    bool CheckForObstruction(int x, int y)
    {
        if (MapGeneration.tileMap[x][y] == 0 || MapGeneration.tileMap[x][y] == 2)
        {

            return true;
        }else if(MapGeneration.tileMap[x][y] == 4)
        {
            //MapGeneration.singleton.ToggleGates();
            return true;
        }
        else
        {

            return false;
        }
    }
}
