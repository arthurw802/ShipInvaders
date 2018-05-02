using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Utility class to get and set the top 3 scores from the PlayerPrefs
/// 
/// Written by: Arthur Wollocko 
/// </summary>
public class HighScoreController : MonoBehaviour {

    public static string GetHighScores()
    {
        string scores = "";

        scores += "1 - " + PlayerPrefs.GetInt("score1", 0);
        scores += "\n2 - " + PlayerPrefs.GetInt("score2", 0);
        scores += "\n3 - " + PlayerPrefs.GetInt("score3", 0);

        return scores;

    }

    public static void SetHighScore(int newScore)
    {
        int score1 = PlayerPrefs.GetInt("score1", 0);
        int score2 = PlayerPrefs.GetInt("score2", 0);
        int score3 = PlayerPrefs.GetInt("score3", 0);

        if (newScore <= score3) {
            return;
        } else
        {
            if (newScore <= score2)
            {
                PlayerPrefs.SetInt("score3", newScore);
            } else if (newScore <= score1)
            {
                PlayerPrefs.SetInt("score3", score2);
                PlayerPrefs.SetInt("score2", newScore);
            } else
            {
                PlayerPrefs.SetInt("score3", score2);
                PlayerPrefs.SetInt("score2", score1);
                PlayerPrefs.SetInt("score1", newScore);
            }
        }


    }

	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
