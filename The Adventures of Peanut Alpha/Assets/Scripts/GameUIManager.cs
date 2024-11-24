using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 
using TMPro;

public class GameUIManager : MonoBehaviour
{
    public GameObject startScreen;           // Reference to the start screen UI
    public GameObject endScreen;             // Reference to the end screen UI
    public GameObject gameOverScreen;        // Reference to the Game Over screen UI
    public TextMeshProUGUI gameOverMessage; // Reference to the Game Over message text
    public GameObject levelCompleteScreen;   // Reference to the Level Complete screen UI
    public AudioSource themeMusic;           // Reference to the theme music AudioSource
    public AudioSource gameplayMusic;        // Reference to the gameplay music AudioSource
    public Button restartButton;             // Reference to the restart button
    public bool gameActive = false;          // Track if the game is active
    public float levelTime = 60f;            // Desired level time
    private float currentTime;               // Track the current time

    void Start() // Method call at the start of the game
    {
        ShowStartScreen();     // Display the start scree  
        currentTime = levelTime; // Initialize current time
    }

    public void ShowStartScreen() // Method to show the start screen
    {
        startScreen.SetActive(true); //Activate the start screen UI
        endScreen.SetActive(false); // Hide the end screen UI
        gameOverScreen.SetActive(false); // Ensure Game Over screen is hidden
        levelCompleteScreen.SetActive(false); // Ensure Level Complete screen is hidden
        restartButton.gameObject.SetActive(false); // Hide the restart button
        Time.timeScale = 0; // Pauses the game until the start button is clicked
        gameActive = false; //Mark the game as inactive

        if (themeMusic != null && !themeMusic.isPlaying)
        {
            themeMusic.Play();  // Play the theme music
        }

        if (gameplayMusic != null && gameplayMusic.isPlaying)
        {
            gameplayMusic.Stop();  // Stop gameplay music, just in case
        }
    }

    public void StartGame() // Method to start the same
    {
        startScreen.SetActive(false); // Hide the start screen when starting the game play
        endScreen.SetActive(false);  // Ensure the end screen is hidden
        gameOverScreen.SetActive(false); // Ensure Game Over screen is hidden
        levelCompleteScreen.SetActive(false); // Ensure Level Complete screen is hidden
        restartButton.gameObject.SetActive(false); // Ensure the restart button is hidden when starting the game
        Time.timeScale = 1; // Unpauses the game
        gameActive = true; // Start the game
        currentTime = levelTime; // Reset the timer

        if (themeMusic != null && themeMusic.isPlaying)
        {
            themeMusic.Stop();  // Stop the theme music when the game starts
        }

        if (gameplayMusic != null)
        {
            gameplayMusic.Play();  // Start the gameplay music
        }
    }

    public void ShowGameOverScreen() // Method to show the Game Over screen
    {
        Debug.Log("Displaying Game Over Screen"); //Log the display action for debugging
        gameOverScreen.SetActive(true);         // Display Game Over screen
        levelCompleteScreen.SetActive(false);   // Hide Level Complete screen
        restartButton.gameObject.SetActive(true); // Show restart button
        Time.timeScale = 0; // Pause the game

        if (gameOverMessage != null)
        {
            gameOverMessage.text = "Game Over"; // Set the Game Over message text
        }

        if (gameplayMusic != null && gameplayMusic.isPlaying)
        {
            gameplayMusic.Stop();  // Stop gameplay music
        }
    }

    public void ShowLevelCompleteScreen() // Method to show the Level Complete screen
    {
        levelCompleteScreen.SetActive(true);  // Display Level Complete screen
        gameOverScreen.SetActive(false);      // Hide Game Over screen
        Time.timeScale = 0; // Pause the game
    }

    public void RestartGame() //Method to Restart the game
    {
        Time.timeScale = 1; // Ensure game is unpaused
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reloads current scene

        // Resetting game state
        currentTime = levelTime; // Reset the timer to the level time
        gameActive = true; // Set game to active
        FindObjectOfType<GameTimer>().ResetTimer(); // Reset the game timer component
        FindObjectOfType<PlayerController>().ResetPlayer(); // Reset the player state
        ShowStartScreen(); // Show the Start Screen
    }

    public void SetGameActive(bool isActive)  //Method to set the game state
    {
        gameActive = isActive; // Set the game active state
    }

    public bool IsGameActive()  // Method to check if the game is currently active
    {
        return gameActive;  // Return the current state of the game
    }
}
