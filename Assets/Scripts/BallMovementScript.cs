using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovementScript : MonoBehaviour
{
    public float speed = 5.0f;
    public float rotationSpeed = 2.0f;
    public float rotationDamping = 0.5f;
    public bool rotatePlayer = true;
    public Transform outerShell;
    public float Ospeed;

    private void Update()
    {
        // Handle object movement in the XY plane
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if (input.magnitude > 0)
        {
            // Calculate the new position based on input
            Vector3 movement = Camera.main.transform.rotation * input;
            movement.y = 0; // Ensure movement is only in the XY plane
            movement.Normalize(); // Normalize for consistent speed

            // Move the object
            transform.position += speed * Time.deltaTime * movement;
            outerShell.transform.Rotate(Vector3.right * Ospeed* Time.deltaTime);

            // Smoothly rotate the object
            if (rotatePlayer)
            {
                float t = Damper.Damp(1, rotationDamping, Time.deltaTime);
                Quaternion newRotation = Quaternion.LookRotation(movement, Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, t);
            }
        }
    }

}
