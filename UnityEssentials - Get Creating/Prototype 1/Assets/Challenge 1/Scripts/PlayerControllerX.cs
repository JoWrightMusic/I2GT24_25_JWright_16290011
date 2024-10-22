using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public float speed = 5.0f;
    public float rotationSpeed = 100.0f;
    public float verticalInput;

    void FixedUpdate()
    {
        // Get the user's vertical input (up/down arrows)
        verticalInput = Input.GetAxis("Vertical");

        // Move the plane forward at a constant rate
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        // Tilt the plane up/down based on vertical input
        if (verticalInput != 0)
        {
            transform.Rotate(Vector3.right * verticalInput * rotationSpeed * Time.deltaTime);
        }
    }
}
