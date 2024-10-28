using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneSpawner : MonoBehaviour  // Class responsible for spawning bones at regular intervals
{
    public GameObject bonePrefab;  // Reference to the bone prefab to be spawned
    public float spawnInterval = 5f;  // How often to spawn a bone in seconds
    public Vector2 spawnArea;  // Spawn area

    private void Start()  // Called before the first frame update
    {
        StartCoroutine(SpawnBones());  // Start the spawning 
    }

    private IEnumerator SpawnBones()  // Coroutine to generate the bone spawning at regular intervals
    {
        while (true) // Loop forever
        {
            // Generate a random position within the spawn area
            Vector2 spawnPosition = new Vector2(
                Random.Range(-spawnArea.x / 2, spawnArea.x / 2),
                Random.Range(-spawnArea.y / 2, spawnArea.y / 2)
            );

            // Instantiate the bone prefab at the generated position
            Instantiate(bonePrefab, spawnPosition, Quaternion.identity);

            yield return new WaitForSeconds(spawnInterval);  // Wait for the next spawn interval before spawning the next bone
        }
    }
}
