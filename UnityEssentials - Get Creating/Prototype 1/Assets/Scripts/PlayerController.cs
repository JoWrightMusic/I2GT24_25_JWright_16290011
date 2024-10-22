using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // private variables
    private float speed = 5.0f;
    private float turnSpeed = 25.0f;
    private float horizontalInput; 
    private float forwardInput; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // this is where we get player input 
        horizontalInput = Input.GetAxis("Horizontal"); 
        forwardInput = Input.GetAxis("Vertical");

        // We move the vechicle forward
         transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);

         // We turn the vechicle 
         transform.Rotate(Vector3.up, turnSpeed * horizontalInput * Time.deltaTime); 
    }
}
