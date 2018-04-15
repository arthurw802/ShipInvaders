using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Dictates the behavior of the collection zone. If any objects move towards this protection zone (within its trigger)
/// the player will lose health
/// </summary>
public class ProtectionZoneController : MonoBehaviour {

    public AudioClip hurtSound;
    private PlayerScoreController playerScoreController;
    private bool hasTriggered = false;                          // Required for non-normal collidrs, otherwise can have multiple collisions


    //Remove the other object 
    //Remove health from the player
    //Play a "hurt" gui effect
    private void OnTriggerEnter(Collider other)
    {
        WorldObstacle worldObstacle = other.gameObject.GetComponent<WorldObstacle>();
        if (worldObstacle != null && !hasTriggered)
        {
            hasTriggered = true;
            //Adjusts the player's health, and flashes the 'hurt' gui element (red).
            playerScoreController.TakeDamage(worldObstacle.attackdamage, Color.red);
            //Play a hurt sound
            AudioSource.PlayClipAtPoint(hurtSound, transform.position);
            Destroy(other.gameObject);
            hasTriggered = false;
        }
    }

    // Use this for initialization
    void Start () {
        playerScoreController = GameObject.FindObjectOfType<PlayerScoreController>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
