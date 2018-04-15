using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Responsible for creating the game objects known as powerups
/// This spawning should have an intelligent but random location.
/// Can have multiple obstacle spawners. Each on its own timer
/// 
/// Written by: Arthur Wollocko (arthurw@oxyvita.us), 2018
/// </summary>
public class PowerupSpawner : MonoBehaviour
{

    public Powerup[] powerupPrefabs;                    // The list of objects that could be spawned (choose one randomly)
    public float spawnIntervalLow = 5f;                 // How often to spawn a new object. Should be scaled by some difficulty factor
    public float spawnIntervalHigh = 15f;               // Upperbound on spawn rate
    private float spawnTimer = 0f;                      // Amount of time since last spawn
    private float spawnInterval = 0f;

    private void CreatePowerup()
    {
        print("Spawning a powerup!");
        int index = Random.Range(0, powerupPrefabs.Length);
        GameObject currentPowerupToCreate = powerupPrefabs[index].gameObject;

        //determine intelligent position
        float xSpawn = Random.Range(-15, 15);
        float ySpawn = 0; //On the ground
        float zSpawn = Random.Range(-15, 15);

        GameObject newObject = GameObject.Instantiate(currentPowerupToCreate);
        //Assign its position
        newObject.transform.position = new Vector3(xSpawn, ySpawn, zSpawn);

        //Make them all children of the spawner
        newObject.transform.SetParent(this.transform);

        //Adjust scale to make them all different. from 80% - 120%
        float randomScale = newObject.transform.localScale.x * Random.Range(.8f, 1.2f);
        newObject.transform.localScale = new Vector3(randomScale, randomScale, randomScale);
    }

    void Start()
    {
        spawnInterval = Random.Range(spawnIntervalLow, spawnIntervalHigh);
    }

    void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval) 
        {
            //spawn new object
            spawnTimer = 0f;
            CreatePowerup();
        }
    }
}
