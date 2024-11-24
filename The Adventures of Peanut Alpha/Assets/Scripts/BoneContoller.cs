using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneController : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy")) // Check for collision with enemies
        {
            Destroy(other.gameObject); // Destroy the enemy
            Destroy(this.gameObject);  // Destroy the bone
        }
    }

    void OnBecameInvisible()
    {
        Destroy(this.gameObject); // Destroy the bone when it leaves the screen
    }
}
