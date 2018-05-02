using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Controls the player's score, with functions to add or subtract.
/// Single and final entity that will hold the score of the player.
/// 
/// Written by: Arthur Wollocko (arthurw@oxyvita.us), Final project CS50
/// </summary>
public class PlayerScoreController : MonoBehaviour {

    public Text scoreValueText;
    public Text healthValueText;
    public Text highScoreText;
    public int totalHealth = 100;                                       // Default to 100 but allow editor override
    public GameObject flashPanel;
    public AudioClip healthSound;
    public float difficultyIncrementInterval = 5f;                      // How often to increment the difficulty of the level
    public float difficultyIncrementScoreInterval = 20f;                // At what factor of scores should the difficulty pick up too

    private int totalScore = 15;

    public void AdjustScore(int amount)
    {
        this.totalScore += amount;
        scoreValueText.text = "" + GetTotalScore();

        if (GetTotalScore() % 100 == 0 && GetTotalScore() != 0)
        {
            //Grant extra health when you get 100 score!
            TakeDamage(-20, Color.green);
        }

        if (GetTotalScore() % difficultyIncrementScoreInterval == 0 && GetTotalScore() != 0)
        {
            //in addition to time based intervals on increasing difficulties, do so based on score as well!
            IncrementDifficulty();
        }

       

    }

    /// <summary>
    /// Applies damage tot he player and updates the text element on screen
    /// </summary>
    /// <param name="amount"></param>
    public void TakeDamage(int amount, Color color)
    {
        if(amount < 0)
        {
            AudioSource.PlayClipAtPoint(healthSound, transform.position);
        }

        totalHealth = (int)Mathf.Max(totalHealth - amount, 0);
        if (totalHealth <= 0)
        {
            print("Ending game!");
            //Persist the score
            HighScoreController.SetHighScore(this.totalScore);
            SceneManager.LoadScene("GameOver");
        }

        healthValueText.text = "" + totalHealth;
        if(color != Color.black)
            ScreenFlash(color);
    }

    public int GetTotalScore()
    {
        return this.totalScore;
    }

    /// <summary>
    /// Conducts a screen flash in the color provided
    /// modifies the color to have .5 opacity 
    /// </summary>
    /// <param name="color"></param>
    private void ScreenFlash(Color color)
    {
        Color c = new Color(color.r, color.g, color.b, .5f);
        flashPanel.GetComponent<Image>().color = c;
        flashPanel.SetActive(true);
        Invoke("RemoveFlashPanel", .1f);
    }

    /// <summary>
    /// Method that will call static variables within DifficultyController and set them higher at a set interval
    /// </summary>
    private void IncrementDifficulty()
    {
        print("Incrementing difficulty");
        DifficultyController.IncrementDifficulty();
    }

    private void RemoveFlashPanel()
    {
        flashPanel.SetActive(false);
    }

	// Use this for initialization
	void Start () {
        //Reset the difficulty on load
        DifficultyController.ResetVariables();

        highScoreText.text = HighScoreController.GetHighScores();

        flashPanel.SetActive(false);
        AdjustScore(0); //Initialize the score text to current;
        TakeDamage(0, Color.black); //Initialize the text to the current health

        //Begin the incrememnting of the difficulty
        //Every 20 seconds, increment difficulty
        InvokeRepeating("IncrementDifficulty", .2f, difficultyIncrementInterval);
    }
	
	// Update is called once per frame
	void Update () {

    }
}
