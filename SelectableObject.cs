using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SelectableObject : MonoBehaviour
{
    public Material selectedMaterial;       // Material to indicate selection
    public Camera playerCamera;             // Reference to the player's camera
    private TextMeshPro selectionText;      // Text display for selection info

    private Renderer objectRenderer;
    private Material originalMaterial;
    private bool isSelected = false;
    private float selectionStartTime;       // Timestamp for when the game starts or object is enabled

    // Reference to SelectionManager
    public SelectionManager selectionManager;

    // Reference to DataLogger for saving selection data
    public DataLogger dataLogger;

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        originalMaterial = objectRenderer.material;

        if (playerCamera == null)
        {
            playerCamera = GameObject.FindWithTag("PlayerCamera").GetComponent<Camera>();
        }

        selectionText = playerCamera.GetComponentInChildren<TextMeshPro>();

        if (selectionText != null)
        {
            selectionText.text = "";
        }

        selectionStartTime = Time.time; // Record the game start time
    }

    void OnMouseDown()
    {
        isSelected = !isSelected;

        if (isSelected && selectedMaterial != null)
        {
            objectRenderer.material = selectedMaterial;

            // Calculate the angle and display the text with object name and angle
            if (selectionText != null)
            {
                float angleToCamera = CalculateAngleToCamera();
                selectionText.text = $"{gameObject.name} is selected (Angle: {angleToCamera:F1}°)";
            }

            // Log data in SelectionManager and DataLogger
            if (selectionManager != null)
            {
                float angleToCamera = CalculateAngleToCamera();
                float timeTaken = Time.time - selectionStartTime; // Time taken to select the object
                selectionManager.MarkObjectSelected(gameObject.name, playerCamera.transform.position, angleToCamera, timeTaken);

                // Log directly to DataLogger
                if (dataLogger != null)
                {
                    dataLogger.LogData(playerCamera.transform.position, gameObject.name, angleToCamera, timeTaken, 1);
                }
            }
        }
        else
        {
            objectRenderer.material = originalMaterial;

            if (selectionText != null)
            {
                selectionText.text = "";
            }
        }
    }

    // Method to calculate the angle between the camera and the object
    private float CalculateAngleToCamera()
    {
        Vector3 directionToObject = transform.position - playerCamera.transform.position;
        float angle = Vector3.Angle(playerCamera.transform.forward, directionToObject);
        return angle;
    }
}
