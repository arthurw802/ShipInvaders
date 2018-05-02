using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Controller that will take in a number of IntroPositions, and display their data on the screen
/// </summary>
public class IntroSceneController : MonoBehaviour {

    public IntroPositions[] positions;
    public Text contentArea;
    public float timePerPosition = 5f;

    private int currentPosition = -1;
    private bool hasCompletedTutorial = false;

    void SwitchPosition()
    {
        currentPosition++;
        if(currentPosition >= positions.Length)
        {
            hasCompletedTutorial = true;
            currentPosition = 0;
        }

        //move camera to next position
        Camera.main.transform.position = positions[currentPosition].transform.position;
        Camera.main.transform.rotation = positions[currentPosition].transform.rotation;

        //Set the text according to what is on the IntroPosition script
        contentArea.text = positions[currentPosition].textToDisplay;
    }

	void Start () {
        //Invoke the switching
        InvokeRepeating("SwitchPosition", 0f, timePerPosition);
	}
	
	void Update () {

        if(hasCompletedTutorial && Input.GetButtonDown("Fire1"))
        {
            //All set, move onto the main level!
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
		
	}
}
