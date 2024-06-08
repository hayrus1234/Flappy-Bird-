using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LogicScript : MonoBehaviour
{
    public int playerScore;
    public TextMeshProUGUI scoreText;
    public GameObject gameOverScreen;
    public AudioSource ding;
    public AudioSource gameOverSound;
    private bool hasPlayedGameOverSound = false;
    public TextMeshProUGUI highScoreText;

    private bool isGameOver = false;

    void Start()
    {
        LoadHighScore(); // Load the high score at the start of the game
        UpdateUI(); // Update UI to display high score
    }

    void LoadHighScore()
    {
        playerScore = 0; // Set the player score to 0 at the start of the game
        highScoreText.text = "High Score: " + PlayerPrefs.GetInt("HighScore", 0).ToString();
    }

    void UpdateHighScore()
    {
        if (playerScore > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", playerScore); // Update high score in PlayerPrefs if a new high score is achieved
            PlayerPrefs.Save(); // Save changes to PlayerPrefs immediately
            highScoreText.text = "High Score: " + playerScore.ToString(); // Update high score text
        }
    }

    void UpdateUI()
    {
        scoreText.text =playerScore.ToString();
    }

    [ContextMenu("Increase Score")]
    public void addScore(int scoreToAdd)
    {
        if (!isGameOver)
        {
            playerScore += scoreToAdd;
            scoreText.text =playerScore.ToString();
            ding.Play();
            UpdateHighScore(); // Update high score when score increases
        }
    }

    public void restartGame()
    {
        isGameOver = false; // Reset game state
        playerScore = 0; // Reset player score when restarting the game
        scoreText.text = playerScore.ToString(); // Update UI to reset score display
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void gameOver()
    {
        isGameOver = true; // Set game state to game over
        gameOverScreen.SetActive(true);
        if (!hasPlayedGameOverSound && gameOverSound != null)
        {
            gameOverSound.Play();
            hasPlayedGameOverSound = true;
        }
    }

    public void OnButtonClick()
    {
        Debug.Log("Button clicked!");
    }
}
