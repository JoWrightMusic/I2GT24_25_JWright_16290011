using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour  //Class responsible for player control, movement and interactions
{
    public float moveSpeed = 5f;                  // Movement speed for Peanut
    public GameObject bonePrefab;                  // Bone prefab to be thrown
    public Transform boneSpawnPoint;               // Point from where bones will be thrown
    public int boneCount = 10;                     // Initial bone count
    public TextMeshProUGUI gameMessage;            // UI element for displaying game message
    public TMP_Text boneCountText;                 // UI element for displaying remaining bones
    public TMP_Text livesCountText;                // UI element for displaying remaining lives
    public int lives = 3;                          // Sets initial lives
    public int collisionCount = 0;                 // Counts collisions       
    public AudioSource collisionSound;             // Collision sound AudioSource

    private Vector2 movement;                      // To store the movement direction
    private GameUIManager gameUIManager;          // Reference to GameUIManager for updates

    void Start()  //Method called on the frame when the script is enabled
    {
        gameUIManager = FindObjectOfType<GameUIManager>(); // Get the GameUIManager reference from the scene
        UpdateBoneCountUI(); // Initialize UI with the starting bone count
        UpdateLivesUI(); // Initialize UI with the starting lives count
    }

    void Update() // Update is called once per frame
    {
        if (gameUIManager.IsGameActive()) // Check if the game is active
        {
            Move();                  // Call movement function
            Rotate();                // Call rotation function
            if (Input.GetKeyDown(KeyCode.Space)) //Check if the space key is pressed to throw a bone
            {
                ThrowBone();         // Call bone-throwing function
            }
        }
    }

    public void CollectBone() // Method called to collect a bone, increase the bone count
    {
        boneCount++; // Increase bone count
        UpdateBoneCountUI(); // Update the UI to reflect new bone count
    }

    void Move()  // Player movement based on user input
    {
        // Get input from arrow keys
        float moveX = Input.GetAxisRaw("Horizontal"); // -1 for left, 1 for right
        float moveY = Input.GetAxisRaw("Vertical");   // -1 for down, 1 for up

        // Calculate movement vector and normalize it
        movement = new Vector2(moveX, moveY).normalized;

        // Move the player based on the movement vector
        transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);
    }

    void Rotate() // Player rotation based on movement direction
    {
        // Only rotate if there is movement
        if (movement != Vector2.zero)
        {
            float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;  // angle of the player rotation
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));  // applies rotation
        }
    }

    void ThrowBone()  // Method to throw a bone if one is available
    {
        Debug.Log("Attempting to throw a bone."); // log throw attempt
        // Check if there are bones available to throw
        if (boneCount > 0)
        {
            // Instantiate a new bone prefab at the specified spawn point and direction
            GameObject bone = Instantiate(bonePrefab, boneSpawnPoint.position + new Vector3(0, 1, 0), transform.rotation);

            // Debug log to check if the bone is instantiated
            Debug.Log("Bone thrown at position: " + bone.transform.position);

            // Give the bone a forward motion
            Rigidbody2D rb = bone.AddComponent<Rigidbody2D>();
            rb.velocity = transform.right * 10f;  // 10f is the speed of the thrown bone

            // Log bone count BEFORE Decreasing
            Debug.Log("Before decrement: Bones left = " + boneCount);

            // Decrease bone count
            boneCount--;

            // Log bone count AFTER Decreasing
            Debug.Log("After decrement: Bones left = " + boneCount);

            // Update UI
            UpdateBoneCountUI();

            Debug.Log("Throwing a bone. Bones left: " + boneCount); // Log action
        }
        else
        {
            Debug.Log("No bones left to throw!"); // Log if no bones are left to throw
        }
    }

    void UpdateBoneCountUI()  // Updates the UI element to show the current bone count
    {
        if (boneCountText != null)
        {
            boneCountText.text = "Bones: " + boneCount.ToString(); // Update the displayed count
            Debug.Log("UI Updated: Bones: " + boneCount); // Log to confirm UI is updating
        }
    }

    void OnTriggerEnter2D(Collider2D other)  // Method called when the player collides with other colliders
    {
        if (other.CompareTag("Bone")) // Check if collided with bone
        {
            CollectBone(); // Call method to collect bone
            Destroy(other.gameObject); // Destroy the bone after collection
            Debug.Log("Collected a bone!");  // log bone collection
        }
        else if (other.CompareTag("Enemy")) // If collided with enemy
        {
            HandleCollisionWithEnemy(); // Handle collision with an enemy 
            collisionSound.Play();  // Play the collision sound affect
        }
        //Debug.Log("Collected a bone!");
    }

    void HandleCollisionWithEnemy()  // Handle the collision with an enemy
    {
        collisionCount++; // Increase the collision count

        // Decrease player lives
        lives--;
        UpdateLivesUI(); // Update the UI display for lives

        if (lives <= 0)
        {
            GameOver(); // Call GameOver if lives reach 0
        }
    }

    void UpdateLivesUI() //Update the UI to show the player's current number of lives
    {
        if (livesCountText != null)
        {
            livesCountText.text = "Lives: " + lives; // Update the lives text count
        }
    }

    void GameOver()  //Triggers the Game Over sequence
    {
        Debug.Log("Game Over triggered in PlayerController.");  // Logs that the Game is Over
        gameUIManager.ShowGameOverScreen();  // Call method to show the Game Over Screen
    }
    // ResetPlayer method to reset the player state
    public void ResetPlayer()
    {
        transform.position = new Vector3(0, 0, 0); // Reset position to initial position
        lives = 3; // Reset lives
        collisionCount = 0; // Reset collision count
        UpdateLivesUI(); // Update lives UI
        boneCount = 10; // Reset bone count
        UpdateBoneCountUI(); // Update bone UI
    }
}
