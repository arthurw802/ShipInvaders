using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Responsible for creating the game objects known as obstacles within the world.
/// This spawning should have an intelligent but random location.
/// Can have multiple obstacle spawners. Each on its own timer
/// Obstactles will spawn from the heavens, and rain down on the game!!!
/// 
/// Written by: Arthur Wollocko (arthurw@oxyvita.us), 2018
/// </summary>
public class ObstacleSpawner : MonoBehaviour {

    public WorldObstacle[] obstaclePrefabs;         // The list of objects that could be spawned (choose one randomly)
    public float spawnInterval = 2f;                // How often to spawn a new object. Should be scaled by some difficulty factor
    private float spawnTimer = 0f;                   // Amount of time since last spawn


    private void CreateObstacle()
    {
        int index = Random.Range(0, obstaclePrefabs.Length);
        GameObject currentObstactleToCreate = obstaclePrefabs[index].gameObject;

        //determine intelligent position
        float xSpawn = Random.Range(-50, 50);
        float ySpawn = Random.Range(10, 50) + DifficultyController.noSpawnRadius;
        float zSpawn = Random.Range(-50, 50);

        GameObject newObject = GameObject.Instantiate(currentObstactleToCreate);
        //Assign its position
        newObject.transform.position = new Vector3(xSpawn, ySpawn, zSpawn);

        //Handle rotation to have it look at its target. Rotation will be handled in the WorldObstacle class to ensure it happens at each frame

        //Adjust scale to make them all different. from 80% - 120%
        float randomScale = newObject.transform.localScale.x * Random.Range(.8f, 1.2f);
        newObject.transform.localScale = new Vector3(randomScale, randomScale, randomScale);
    }

    void Start()
    {
        //Reset the variables of the DifficultyController
    }

    void Update()
    {
        spawnTimer += Time.deltaTime;
        
        if(spawnTimer >= spawnInterval * DifficultyController.obstacleSpawnRateFactor) //Consider the spawnRateFactor for difficulty increasing
        {
            //spawn new object
            spawnTimer = 0f;
            CreateObstacle();
        }
    }
}
