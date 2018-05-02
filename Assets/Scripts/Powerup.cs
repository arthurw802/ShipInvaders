using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the powerup component, which will be spawned in the world. If a player's projectile hits the pwoerup, a noise is played, and 
/// the CannonFireController is adjusted and notified of the powerup
/// 
/// Written by: Arthur Wollocko
/// </summary>
public class Powerup : MonoBehaviour {

    public AudioClip powerupSound;                              // The sound that will play when the powerup is attained
    public GameObject powerupProjectilePrefab;                  // The prefab that should be switched to (launching from the cannon)
    public float firePower = 2000f;                             // The amount of power the projectile should launch at (some powerups may be slow, some fast, etc)
    public float powerupDuration = 6f;                          // The amount of time the powerup should be active for
    public float timePowerupProjectileAlive = 5f;               // The amount of time these projectiles should 'live' in the world prior to being destroyed
    public AudioClip powerupFireSound;                          // If not null, the sound the cannnon should change to when firing this powerup projectile
    public float powerupRefireSpeed = .5f;                      // How fast this powerup can be refired

    private CannonFireController cannonFireController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "FiredObject")
        {
           //Play sound
           AudioSource.PlayClipAtPoint(powerupSound, transform.position);
            // Pass powerup data to the CammonFireController
            cannonFireController.ReceivePowerup(this);

            //Remove one count of powerup from the powerup spawner
            GameObject.FindObjectOfType<PowerupSpawner>().RemovePowerup();

            //Remove the firedobject from the world
            Destroy(other.gameObject);

            //Remove the powerup from the world
            Destroy(this.gameObject);


        }

    }

    // Use this for initialization
    void Start () {
        cannonFireController = GameObject.FindObjectOfType<CannonFireController>(); //Only one in the whole scene, safe to use find
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
