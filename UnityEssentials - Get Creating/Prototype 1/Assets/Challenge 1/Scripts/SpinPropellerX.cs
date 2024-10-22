using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinPropellerX : MonoBehaviour
{
    public float propellerSpeed = 3000.0f;

    void Update()
    {
        // Rotate the propeller around the Z-axis
        transform.Rotate(Vector3.forward * propellerSpeed * Time.deltaTime);
    }
}