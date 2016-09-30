using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Scores : MonoBehaviour {
    public static int player1Score;
    public static int player2Score;

    public static int coinsLeft;

    public Text player1Text;
    public Text player2Text;

    public GameObject gameOverText;

    void Update()
    {
        player1Text.text = "Player 1: " + player1Score.ToString();
        player2Text.text = "Player 2: " + player2Score.ToString();
        if(coinsLeft <= 0)
        {
            gameOverText.SetActive(true);
            StartCoroutine(GameOverScreen());
        }
    }

    IEnumerator GameOverScreen()
    {
        yield return new WaitForSeconds(2.0f);
    }

    
	
}
