using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5.0f;
    public float rotationSpeed = 100.0f;
    private LineRenderer lineRenderer;
    private List<Vector3> pathPoints = new List<Vector3>();

    void Start()
    {
        // Initialize the LineRenderer component
        lineRenderer = GetComponent<LineRenderer>();
        if (lineRenderer == null)
        {
            Debug.LogError("LineRenderer component not found on " + gameObject.name);
            return;
        }
        lineRenderer.positionCount = 0;
        AddPathPoint(); // Add the initial position
        Debug.Log("Initialized LineRenderer and added starting path point.");
    }

    void Update()
    {
        if (lineRenderer == null) return;

        // Move forward/backward
        float translation = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        // Rotate left/right
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;

        transform.Translate(0, 0, translation);
        transform.Rotate(0, rotation, 0);

        // Check if moved and add path point
        if (translation != 0 || rotation != 0)
        {
            AddPathPoint();
            Debug.Log($"Player Position: {transform.position}"); // Log player’s current position
        }
    }

    // Adds a new point to the path and updates the LineRenderer
    void AddPathPoint()
    {
        pathPoints.Add(transform.position);
        lineRenderer.positionCount = pathPoints.Count;
        lineRenderer.SetPositions(pathPoints.ToArray());
    }

    // DisplayPath method to be called from SelectionManager
    public void DisplayPath()
    {
        if (pathPoints.Count > 0)
        {
            lineRenderer.positionCount = pathPoints.Count;
            lineRenderer.SetPositions(pathPoints.ToArray());
            lineRenderer.enabled = true; // Ensures the path is visible when called
            Debug.Log("Displaying path with " + pathPoints.Count + " points.");

            // Highlight the final position in the path
            lineRenderer.material.color = Color.red;
        }
        else
        {
            Debug.LogWarning("No path points to display.");
        }
    }
}
