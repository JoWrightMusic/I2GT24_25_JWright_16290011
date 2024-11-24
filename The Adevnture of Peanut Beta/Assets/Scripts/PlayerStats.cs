using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStats : MonoBehaviour  // Class responsible for tracking the players bone count and lives 
{
    public TMP_Text boneCounterText;    // UI element to display the bones collected
    public TMP_Text livesCounterText;  // UI element to display player lives remaining 
    private int bones = 0;   // track bones collected
    private int lives = 3;  // track player lives reamining 

    void Start() // start called before the first frame
    {
        UpdateBoneCounter(); // Initialize bone counter UI display
        UpdateLivesCounter(); // Initalize lives counter UI display
    }

    public void CollectBone()  //Method to increase the bone count when a bone is collected 
    {
        bones++;         // increase bone count
        UpdateBoneCounter();  // update the UI display for the bone count
    }

    public void UseBone()  //Method to decrease the bone count when one is thrown
    {
        if (bones > 0)   //Checks availablity of bones
        {
            bones--;  // decrease bone count
            UpdateBoneCounter(); // update the UI display for the bone count
        }
    }

    public void TakeDamage()  // Method to decrease lives when the player takes damage 
    {
        lives--;  // decrease lives count
        UpdateLivesCounter();  //update UI display for lives count
        if (lives <= 0)  // check if lives have reached zero
        {
            // Trigger game over 
            Debug.Log("Game Over");
        }
    }

    private void UpdateBoneCounter()   //Method to update the UI display for the bone counter
    {
        boneCounterText.text = "Bones: " + bones;  // update the text to show the current bone count
    }

    private void UpdateLivesCounter()
    {
        livesCounterText.text = "Lives: " + lives;  //  update the text to show the current lives count
    }
}
