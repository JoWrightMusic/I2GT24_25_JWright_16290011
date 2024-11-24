using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekPlayer : MonoBehaviour   //Class responsible for making the enemy follow/ seek the player
{
    public Transform target;    // Reference to the player's transform
    public float speed = 2f;    // Movement speed

    private GameObject player;  // Reference to the player GameObject
    private GameUIManager gameUIManager;  //Reference to the GameUIManager for game state tracking

    void Start()  // Start is called before the first frame update
    {
        player = GameObject.FindWithTag("Player");  // find the player object by its tag
        gameUIManager = FindObjectOfType<GameUIManager>();  // find the GameUIManager to check the game state
    }

    void Update()  // Called once per frame
    {
       // if (target != null)
        //{
            // Move towards the player
            //Vector3 direction = (target.position - transform.position).normalized;
            //transform.position += direction * speed * Time.deltaTime;
        //}
        {
            if (gameUIManager.gameActive)  // check the game is active before seeking the player
            {
                SeekPlayer();  // call the method to move towards the player
            }
        }

        void SeekPlayer()  // Method to move towards the player's position
        {
            Vector3 direction = (player.transform.position - transform.position).normalized;  // Calculate the direction vector from the enemy to the player
            transform.position += direction * speed * Time.deltaTime; // Move the enemy towards the player's position at its set speed
        }
    }

}
