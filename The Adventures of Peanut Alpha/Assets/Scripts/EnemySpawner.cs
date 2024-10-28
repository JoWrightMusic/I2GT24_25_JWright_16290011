using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour      // Class responsible for spawning the game enemies 
{
    public GameObject[] enemyPrefabs;          // Array to store different enemy types
    public Transform player;                   // Reference to the player for the enemy targets
    public float spawnInterval = 2f;           // Time interval between spawns
    public float spawnYPosition = 5f;          // Y-position at the top of the screen for spawning

    private float timer;   // timer to keep track of spawn intervals 

    void Start()  // Start is called before the first frame update
    {
        timer = spawnInterval; // Initialize the timer to the spawn interval
    }

    void Update()  // Update called once per frame
    {
        timer -= Time.deltaTime; // decrease the timer 
        if (timer <= 0f)         //  Check if it's time to spawn a new enemy
        {
            SpawnEnemy();        // Call method to spawn a new enemy
            timer = spawnInterval;  // Rest timer for the next spawn
        }
    }

    void SpawnEnemy()   // Method to spawn an enemy
    {
        // Choose a random enemy prefab from the array
        int randomIndex = Random.Range(0, enemyPrefabs.Length);       // random enemy
        GameObject enemy = Instantiate(enemyPrefabs[randomIndex]); // Instantiate the chosen enemy prefab

        // Set random X-position at the top of the screen
        float randomX = Random.Range(-8f, 8f);  //  screen size
        enemy.transform.position = new Vector3(randomX, spawnYPosition, 0);  // set enemy position

        // Add SeekPlayer component to the spawned enemy for targetting the player
        SeekPlayer seekPlayer = enemy.AddComponent<SeekPlayer>();   //Add the SeekPlayer script
        seekPlayer.target = player;         // set the player as the target for the enemy
        seekPlayer.speed = Random.Range(1f, 3f); // Assign a random speed to the enemy between 1 and 3 
    }
}
