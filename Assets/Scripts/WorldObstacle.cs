using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls the point value, and trigger/removal of obstacles in the world
/// Controls the sounds played when objects are hit, as well.
/// 
/// Written by: Arthur Wollocko (arthurw@oxyvita.us), Final Project
/// </summary>
public class WorldObstacle : MonoBehaviour {

    public int healthLower = 1;                                         //The number of collisions this object will take before awarding score and removing itself  
    public int healthUpper = 3;                                         // For randomization, the upper bound
    public int pointValue = 5;
    public AudioClip hitStillHealthSound;                               // Sound that will play if there is still health on impact.
    public AudioClip hitDeadSound;                                      // Sound that will play once there is no more health remaining

    // Attack information!
    public ProtectionZoneController protectionZoneOfInterest;           // What this obstacle should 'attack'. Can be null. If null, it is randomized into any in the world
    public int attackdamage = 5;                                      // How much damage this will do to the player if it hits a protection zone
    public float speed = .03f;                                           // How fast we want this object to lerp towards its destination (Upper bound)
    private Vector3 startingPosition;
    private float fractionOfMovement = 0;                               // The fraction of lerping that has been completed

    private Text hpText;
    private int health;

    private PlayerScoreController playerScoreController;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "FiredObject")
        {
            FiredObject fo = other.gameObject.GetComponent<FiredObject>();
            health -= fo.damage;
            hpText.text = "HP: " + health;

            //Determine if the other object has an explosiveController or not
            FiredObjectExplosionController foec = other.gameObject.GetComponent<FiredObjectExplosionController>();
            if(foec != null)
            {
                foec.Explode();
            }
            if (health <= 0)
            {
                //Play sound
                AudioSource.PlayClipAtPoint(hitDeadSound, transform.position);

                //Award pointValue points to the player
                playerScoreController.AdjustScore(pointValue);

                //Remove this obstacle
                Destroy(this.gameObject);
            }  else
            {
                //Else the object should persist, but have a lower health! Play non-death sound
                AudioSource.PlayClipAtPoint(hitStillHealthSound, transform.position);
            }
        }
       
    }


    // Use this for initialization
    void Start () {
        playerScoreController = GameObject.FindObjectOfType<PlayerScoreController>();
        //Randomize the speed
        speed = Random.Range(.01f, speed); //Augment the speed by the factor as they are created
        speed *= DifficultyController.obstacleSpeedFactor;      //Adjust speed as difficulty increases

        if(protectionZoneOfInterest == null)
        {
            ProtectionZoneController[] protectionZones = GameObject.FindObjectsOfType<ProtectionZoneController>();
            protectionZoneOfInterest = protectionZones[Random.Range(0, protectionZones.Length - 1)];
        }
        //Set the starting position
        startingPosition = transform.position;

        //Set the tag on all obstacles
        this.gameObject.tag = "Obstacle";

        //Initialize health -- Random between range
        health = Random.Range(healthLower, healthUpper);

        //Ensure there are default sounds;
        if (hitStillHealthSound == null)
            hitStillHealthSound = (AudioClip) Resources.Load("Sounds/hitStillHealth");

        if (hitDeadSound == null)
            hitDeadSound = (AudioClip)Resources.Load("Sounds/hitDeadSound");

        //Set the text component for future reference
        hpText = GetComponentInChildren<Text>();
        hpText.text = "HP: " + health;
    }
	
	// Update is called once per frame
	void Update () {
        //Lerp the movement if the fractionOfMovement is still less than 1
        //Lerping is perfect here for realism as objects that spawn further will come faster. Note: The player will also have more time to attack them!
        if (fractionOfMovement < 1)
        {
            fractionOfMovement += Time.deltaTime * speed;
            transform.position = Vector3.Lerp(startingPosition, protectionZoneOfInterest.transform.position, fractionOfMovement);
            transform.LookAt(protectionZoneOfInterest.gameObject.transform);
        }
    }
}
