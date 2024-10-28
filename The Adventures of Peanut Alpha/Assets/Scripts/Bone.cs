using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bone : MonoBehaviour  // Class responsible for the bones you are able to collect
{
    private void OnTriggerEnter2D(Collider2D other) // Method called when another collider enters the trigger collider attached to this object
    {
        if (other.CompareTag("Player")) // Make sure the player has the tag "Player"
        {
            other.GetComponent<PlayerController>().CollectBone(); // Get the PlayerController Method component from the player object and call the CollectBone method
            Destroy(gameObject); // Destroy the bone object after collecting it
        }
    }
}
