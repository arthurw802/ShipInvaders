using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that will contain static variables pertaining to multiplying difficulty.
/// Incremented when certain conditions are met
/// 
/// </summary>
public class DifficultyController : MonoBehaviour {

    public static float obstacleSpeedFactor = 1f;                                      // The overall speed factor. Potentially augment this as players progress?
    public static float obstacleSpawnRateFactor = 1f;                                  // The spawn rate is multiplied by this variable (should decrease it to increase difficulty)
    public static float noSpawnRadius = 25f;                                            // Area around the player that nothing can spawn in

    public static void ResetVariables()
    {
        obstacleSpeedFactor = 1f;
        obstacleSpawnRateFactor = 1f;
    }

    public static void IncrementDifficulty()
    {
        obstacleSpeedFactor += .04f;
        obstacleSpawnRateFactor -= .05f;
    }
}
