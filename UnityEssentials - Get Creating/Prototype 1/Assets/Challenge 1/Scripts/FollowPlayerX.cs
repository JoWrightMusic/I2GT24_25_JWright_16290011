using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlane : MonoBehaviour
{
    public GameObject plane;  // Assign the plane object in the Inspector
    public Vector3 offset = new Vector3(30, 0, 10);  // Side-on offset

    void Update()
    {
        // Position the camera beside the plane
        transform.position = plane.transform.position + offset;

        // Rotate the camera to face the plane from the side
        transform.rotation = Quaternion.Euler(0, -90, 0);  // Y-axis rotation of -90 degrees
    }
}
