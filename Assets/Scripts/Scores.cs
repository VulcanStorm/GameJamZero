using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Scores : MonoBehaviour {
    public static int player1Score;
    public static int player2Score;

    public static int coinsLeft;

    public Text player1Text;
    public Text player2Text;

    public GameObject gameOverPnl;
    public Text gameOverText;

    void Update()
    {
        player1Text.text = "Player 1: " + player1Score.ToString();
        player2Text.text = "Player 2: " + player2Score.ToString();
        if(coinsLeft <= 0)
        {
            string winner = "";
            if(player1Score > player2Score)
            {
                winner = "Player 1";
            }else if(player1Score < player2Score)
            {
                winner = "Player 2";
            }
            else
            {
                winner = "no one";
            }
            gameOverPnl.SetActive(true);
            gameOverText.GetComponent<Text>().text = "Game Over: " + winner + " has won";
            StartCoroutine(GameOverScreen());
        }
    }

    IEnumerator GameOverScreen()
    {
        yield return new WaitForSeconds(4.0f);

        player1Score = 0;
        player2Score = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    
	
}
