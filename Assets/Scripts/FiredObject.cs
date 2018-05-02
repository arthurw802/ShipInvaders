using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Dictates the behavior of a firedObject within this project.
/// Attached when the object is fired.
/// 
/// Written by: Arthur Wollocko (arthurw@oxyvita.us), Final Project CS50
/// </summary>
public class FiredObject : MonoBehaviour {

    public int damage = 1;              // The amount of damage this fired object will do

    void DestroyFireObject()
    {
        Destroy(this.gameObject);
    }

    /// <summary>
    /// Method that will invoke the destruction process after a set amount of time
    /// </summary>
    /// <param name="timeToDestroy"></param>
    public void BeginDestructionProcess(float timeToDestroy)
    {
        Invoke("DestroyFireObject", timeToDestroy);
    }

    // Use this for initialization
    void Start () {
       
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
