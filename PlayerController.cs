using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public NavMeshAgent agent;

    // Speed of movement
    public float moveSpeed = 5.0f;

    // Update is called once per frame
    void Update()
    {
        // Get input from keyboard (WASD or arrow keys)
        float moveHorizontal = Input.GetAxis("Horizontal");  // A (-1) and D (1) or Left and Right Arrow
        float moveVertical = Input.GetAxis("Vertical");      // W (1) and S (-1) or Up and Down Arrow

        // Create a direction vector based on input
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        // Check if there's input to move
        if (movement != Vector3.zero)
        {
            // Calculate the target position
            Vector3 targetPosition = transform.position + movement * moveSpeed * Time.deltaTime;

            // Move the agent to the new position
            agent.SetDestination(targetPosition);
        }
    }
}
