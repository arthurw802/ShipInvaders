using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the firing of the provided object (lets say a cannon ball) into the world.
/// 
/// Written by: Arthur Wollocko, Final Project
/// </summary>
public class CannonFireController : MonoBehaviour {

    public Transform firePoint;                         // Represents the object at the end of the cannon. Where the 'explosion' should occur at. Good for visuals and to cue this script
    public GameObject objectToFire;                     // The object to instantiate and propel into the world. Should have a rigid body attached to it, and needs to have a trigger on it for point calculation
    public float fireForce = 200f;                      // The force that will be applied to the instantiated object
    public float timeObjectAlive = 3f;                  // The amount of time before removing the newly created game object
    public float refireSpeed = .1f;                     // The speed at which you can re-fire the cannon with this powerup
    public AudioClip fireSound;                         // The sound the cannon should make when firing

    //Powerup Variables
    private float powerupCounter = 0f;                  // The amount of time the powerup has been going for
    private float powerupDuration = 0f;                 // The duration the power up will last (provided by the powerup OnTriggerEnter)
    private GameObject originalProjectile;              // Reference to the original projectile to switch back to once the powerup duration ends
    private float originalFireForce = 0f;               // Reference to the original fire force
    private float originalTimeAlive = 3f;               // Reference to the original time the projectile should be alive
    private AudioClip originalFireSound;
    private float originalRefireSpeed = .1f;              // Refernece to the original fire speed

    private GameObject fireObject;
    private float refireTimer = 0f;

    /// <summary>
    /// Instantiates the provided objectToFire at the firePoint, and uses the transform.forward to apply force to the object
    /// </summary>
    /// <param name="point"></param>
    void InstantiateObjectAndFire()
    {
        fireObject = GameObject.Instantiate(objectToFire);
        fireObject.transform.position = firePoint.position + (firePoint.forward * .2f);        //Instantiate it slightly infront of the firePoint

        //Get the rigidbody on the game object, or add one if not present
        Rigidbody rigid = fireObject.GetComponent<Rigidbody>();
        if (rigid == null)
            rigid = fireObject.AddComponent<Rigidbody>();       //If no rigid body on prefab, add a default one to provide some functionality. But allow rigid body effects to be determined by prefab

        rigid.AddForce(firePoint.forward * fireForce);

        //Tag the object with the 'FiredObject' tag (For assistance in collision detection)
        fireObject.tag = "FiredObject";

        AudioSource.PlayClipAtPoint(fireSound, transform.position);

        //Add the FiredObject script to the object to dictate its behavior (and remove after a certain amount of time)
        FiredObject fo = fireObject.GetComponent<FiredObject>();
        fo.BeginDestructionProcess(timeObjectAlive);

    }

    /// <summary>
    /// Helper function called when a powerup is received.
    /// Updates all the data with the data from the powerup, and resets the powerupCounter
    /// to ensure new powerups override all the previous
    /// </summary>
    /// <param name="powerup"></param>
    public void ReceivePowerup(Powerup powerup)
    {
        powerupCounter = 0;
        this.objectToFire = powerup.powerupProjectilePrefab;
        this.fireForce = powerup.firePower;
        this.timeObjectAlive = powerup.timePowerupProjectileAlive;
        this.powerupDuration = powerup.powerupDuration;
        this.refireSpeed = powerup.powerupRefireSpeed;
        if(powerup.powerupFireSound != null)
            this.fireSound = powerup.powerupFireSound;
    }



	void Start () {
        originalProjectile = objectToFire;
        originalFireForce = fireForce;
        originalTimeAlive = timeObjectAlive;
        originalFireSound = fireSound;
        originalRefireSpeed = refireSpeed;

        refireTimer = refireSpeed; // To enable the first shot to always be able to be fired.
	}
	
	// Update is called once per frame
	void Update () {
        refireTimer += Time.deltaTime;

        //Determine if a powerup counter should be kept, and when expired, switch all variables back to original. User powerupDuration as a flag
        if (this.powerupDuration > 0)
        {
            powerupCounter += Time.deltaTime;

            if(powerupCounter >= powerupDuration)
            {
                //Powerup has expired, reset all variables to normal
                powerupDuration = 0; //Change the flag so this doesn't execute all the time
                powerupCounter = 0;
                this.objectToFire = originalProjectile;
                this.fireForce = originalFireForce;
                this.timeObjectAlive = originalTimeAlive;
                this.fireSound = originalFireSound;
                this.refireSpeed = originalRefireSpeed;

            }
        }

        //Space is the 'fire' key typically, or mouse 0
        if(Input.GetButtonDown("Fire1") )
        {
            //Ensure you're within your refirespeed before firing. If so, reset the timer
            if(refireTimer >= refireSpeed)
            {
                refireTimer = 0f;
                InstantiateObjectAndFire();
            }
        }
		
	}
}
