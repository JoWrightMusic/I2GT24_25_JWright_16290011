using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour   //class to manage the game timer function
{
    public float levelTime = 60f; // Level's duration
    public TMP_Text timerText; // TimerText UI element
    private float timer; // Internal timer
    private GameUIManager gameUIManager; // reference to GameUIManger for game state control

    void Start()  // Method called when the script is being loaded
    {
        timer = levelTime; // Initialize timer with the level time
        gameUIManager = FindObjectOfType<GameUIManager>(); // Find the GameUIManager in the scene
        ResetTimer(); // Initialize the timer at the start
    }

    void Update() // Method called once per frame
    {
        // Only run the timer if the game is active and the timer is greater than zero
        if (gameUIManager != null && gameUIManager.IsGameActive() && timer > 0)
        {
            timer -= Time.deltaTime; // Decrease timer
            Debug.Log("Timer: " + timer); // Log the timer value

            // Update the timer UI and calculate the minutes and seconds from the remaining time
            int minutes = Mathf.FloorToInt(timer / 60F); // minutes
            int seconds = Mathf.FloorToInt(timer % 60F); // seconds 
            timerText.text = string.Format("Timer: {0:00}:{1:00}", minutes, seconds);  // Update the timer display

            // Check if the timer has reached zero
            if (timer <= 0)
            {
                timer = 0; // Clamp timer to zero
                timerText.text = "Time's Up!"; // Display a message when the time runs out - not yet working
                GameOver(); // Call GameOver function
            }
        }
    }

    // Method to stop the timer and update the game state
    public void StopTimer()
    {
        gameUIManager.SetGameActive(false); // Mark the game as inactive using GameUIManager
    }

    // Method to start the timer
    public void StartTimer()
    {
        gameUIManager.SetGameActive(true); // Mark the game as inactive using GameUIManager
    }

    // Method to reset the timer
    public void ResetTimer()
    {
        timer = levelTime; // Reset the timer to the initial level time
        int minutes = Mathf.FloorToInt(timer / 60F); // calculate the reset minutes
        int seconds = Mathf.FloorToInt(timer % 60F);  // calculate the reset seconds 
        timerText.text = string.Format("Timer: {0:00}:{1:00}", minutes, seconds); // Update the timer display
        StartTimer(); // Start the timer
    }

    private void GameOver()  //private method to handle the game over logic
    {
        StopTimer(); // Stop the timer when game over
        Debug.Log("Game Over!"); // For debugging

        // Use GameUIManager's method to show the game over screen
        if (gameUIManager != null)
        {
            gameUIManager.ShowGameOverScreen(); // Call GameUIManager's method to show Game Over screen
        }
    }
}
