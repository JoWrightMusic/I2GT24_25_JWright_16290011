using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Goal : MonoBehaviour  // Class to manage the Goal of the game
{
    public TextMeshProUGUI gameMessage;  // Reference the UI element that displays in the game message
    public AudioSource goalSound;  // Reference to the audio source that plays the goal sound

    private void OnTriggerEnter2D(Collider2D other)  // Method called when another collider enters the trigger collider attached to this Game Object
    {
        if (other.CompareTag("Player")) // check if the collider that entered the trigger has the player tag
        {
            GameUIManager gameUIManager = FindObjectOfType<GameUIManager>();  // Finds the GameUIManager in the scene
            gameUIManager.ShowLevelCompleteScreen(); // Display Level Complete screen
            gameUIManager.SetGameActive(false); // Set game to inactive to stop gameplay

            if (goalSound != null)  // check if a goal sound has been assigned and then play it
            {
                goalSound.Play(); // Play the goal sound if assigned
            }

            if (gameMessage != null) // check if the game message text element is assigned and display it
            {
                gameMessage.text = "Level Complete!"; // Display the level complete message
            }

            Debug.Log("Level Complete!"); // log debug 
        }
    }
}
