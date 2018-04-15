using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Simple script that will have any transform with this on it constantly look at the player (camera)
/// 
/// Written by: Arthur Wollocko
/// </summary>
public class LookAtPlayer : MonoBehaviour {

	void Update () { 
        //Subtract the Vectors to achieve looking at the camera (similar to setting an offset)
        transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
    }
}
