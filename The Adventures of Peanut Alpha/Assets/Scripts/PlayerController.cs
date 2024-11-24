using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // Player movement speed
    public GameObject bonePrefab; // Bone prefab to be thrown
    public Transform boneSpawnPoint; // Point where bones are spawned
    public int boneCount = 10; // Initial number of bones
    public float boneSpeed = 10f; // Base speed of the bone
    public float maxBoneSpeed = 20f; // Maximum allowed speed
    public float minBoneSpeed = 5f; // Minimum allowed speed
    public TMP_Text boneCountText; // UI element to display remaining bones
    public TMP_Text livesCountText; // UI element to display remaining lives
    public int lives = 3; // Player lives
    public AudioSource collisionSound; // Collision sound

    private Vector2 movement; // Stores movement direction
    private GameUIManager gameUIManager; // Game UI manager

    // Game area boundaries
    public float minX = -10f, maxX = 10f, minY = -5f, maxY = 5f;

    void Start()
    {
        // Get references and initialize UI
        gameUIManager = FindObjectOfType<GameUIManager>();
        UpdateBoneCountUI();
        UpdateLivesUI();
    }

    void Update()
    {
        if (gameUIManager.IsGameActive())
        {
            HandleMovement(); // Movement and rotation
            ClampPosition(); // Keep player within bounds
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ThrowBone();
            }
        }
    }

    void HandleMovement()
    {
        // Get player input
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        // Create a movement vector
        movement = new Vector2(moveX, moveY).normalized;

        // Apply movement
        transform.position += (Vector3)(movement * moveSpeed * Time.deltaTime);

        // Rotate the player to face the movement direction
        if (movement != Vector2.zero)
        {
            float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle); // Rotate only on the Z-axis
        }
    }

    void ClampPosition()
    {
        // Prevent the player from leaving the game area
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, minX, maxX);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, minY, maxY);

        // Lock Z-axis to ensure the player stays in the correct plane
        clampedPosition.z = 0;

        transform.position = clampedPosition;
    }

    void ThrowBone()
    {
        if (boneCount > 0)
        {
            // Get mouse position in world space if using a mouse for direction
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 throwDirection = (mousePosition - boneSpawnPoint.position).normalized;

            // If not using mouse, adjust throw direction with controller input
            if (Input.GetKey(KeyCode.UpArrow))
            {
                throwDirection = Vector2.up;
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                throwDirection = Vector2.down;
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                throwDirection = Vector2.left;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                throwDirection = Vector2.right;
            }

            // Adjust bone speed based on a modifier key (e.g., shift for fast throw)
            float currentBoneSpeed = boneSpeed;
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                currentBoneSpeed = Mathf.Clamp(currentBoneSpeed * 1.5f, minBoneSpeed, maxBoneSpeed);
            }

            // Spawn the bone at the spawn point
            GameObject bone = Instantiate(bonePrefab, boneSpawnPoint.position, Quaternion.identity);

            // Add Rigidbody2D for movement
            Rigidbody2D rb = bone.GetComponent<Rigidbody2D>();
            if (rb == null) // Ensure Rigidbody2D exists on the bonePrefab
            {
                rb = bone.AddComponent<Rigidbody2D>();
            }

            // Apply velocity based on calculated throw direction
            rb.velocity = throwDirection * currentBoneSpeed;

            // Add Collider2D if not already on the bonePrefab
            Collider2D collider = bone.GetComponent<Collider2D>();
            if (collider == null)
            {
                collider = bone.AddComponent<BoxCollider2D>();
            }
            collider.isTrigger = true; // Ensure it triggers collisions

            // Add BoneController script to handle collisions
            BoneController boneController = bone.GetComponent<BoneController>();
            if (boneController == null)
            {
                bone.AddComponent<BoneController>();
            }

            // Decrease the bone count and update UI
            boneCount--;
            UpdateBoneCountUI();
        }
        else
        {
            Debug.Log("No bones left to throw!");
        }
    }



    void UpdateBoneCountUI()
    {
        if (boneCountText != null)
        {
            boneCountText.text = "Bones: " + boneCount;
        }
    }

    void UpdateLivesUI()
    {
        if (livesCountText != null)
        {
            livesCountText.text = "Lives: " + lives;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bone"))
        {
            CollectBone();
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Enemy"))
        {
            HandleCollisionWithEnemy();
            Destroy(other.gameObject); // Destroy the enemy after collision
        }
    }

    public void CollectBone()
    {
        boneCount++; // Increase bone count
        UpdateBoneCountUI();
    }

    void HandleCollisionWithEnemy()
    {
        lives--; // Decrease lives
        UpdateLivesUI();

        if (lives <= 0)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        Debug.Log("Game Over!");
        gameUIManager.ShowGameOverScreen();
    }

    public void ResetPlayer()
    {
        transform.position = Vector3.zero; // Reset position to the origin
        lives = 3;
        boneCount = 10;
        UpdateLivesUI();
        UpdateBoneCountUI();
    }
}
