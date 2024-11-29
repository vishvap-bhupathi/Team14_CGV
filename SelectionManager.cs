using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectionManager : MonoBehaviour
{
    public TextMeshProUGUI objectListText; // Reference to the object list TextMeshPro UI element
    public TextMeshProUGUI timerText;      // Reference to the timer TextMeshPro UI element
    public TextMeshProUGUI gameOverText;   // Reference for "GAME OVER" message
    public TextMeshProUGUI scoreText;      // Reference for score display

    public GameObject instructionsPanel;   // Panel for instructions
    public Button startButton;             // Start button to begin the game

    public DataLogger dataLogger;          // Reference to DataLogger

    private Dictionary<string, bool> objectSelectionStatus = new Dictionary<string, bool>(); // Track selection
    private float timer = 30.0f; // Timer for the game (30 seconds)
    private bool gameStarted = false;
    private bool gameEnded = false;
    public TextMeshProUGUI buttonText; // Reference to the button text component

    void Start()
    {
        // Initialize the object names in the dictionary with selection status as false (not selected)
        objectSelectionStatus.Add("Sofa", false);
        objectSelectionStatus.Add("Coffee Table", false);
        objectSelectionStatus.Add("Ketchup", false);
        objectSelectionStatus.Add("Plate of Fruits", false);
        objectSelectionStatus.Add("TV", false);
        objectSelectionStatus.Add("Flower Plant", false);
        objectSelectionStatus.Add("Toy Car", false);
        objectSelectionStatus.Add("Wall Lamp", false);
        objectSelectionStatus.Add("Water Bottle", false);
        objectSelectionStatus.Add("Printer", false);
        objectSelectionStatus.Add("Chair", false);
        objectSelectionStatus.Add("Dish on the Table", false);
        objectSelectionStatus.Add("Camera", false);

        // Hide game UI elements initially
        objectListText.gameObject.SetActive(false);
        timerText.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(false);

        // Show the instructions panel
        instructionsPanel.SetActive(true);

        // Add listener to the start button
        startButton.onClick.AddListener(StartGame);
    }

    void Update()
    {
        // Decrease timer if the game has started and not ended
        if (gameStarted && !gameEnded)
        {
            timer -= Time.deltaTime;
            UpdateTimerText();

            // End the game if timer runs out
            if (timer <= 0)
            {
                EndGame();
            }
        }
    }

    private void StartGame()
    {
        Debug.Log("Start button clicked. Starting the game...");

        gameStarted = true;

        // Hide the instructions panel
        instructionsPanel.SetActive(false);

        // Show the game UI elements
        objectListText.gameObject.SetActive(true);
        timerText.gameObject.SetActive(true);

        UpdateObjectListText();
        UpdateTimerText();

        Debug.Log("Instructions panel hidden, game UI elements shown.");
    }

    // Method to mark an object as selected and update the list
    public void MarkObjectSelected(string objectName, Vector3 playerPosition, float visualAngle, float timeTaken)
    {
        if (objectSelectionStatus.ContainsKey(objectName) && !objectSelectionStatus[objectName])
        {
            Debug.Log($"MarkObjectSelected called for: {objectName}");
            objectSelectionStatus[objectName] = true; // Mark as selected
            UpdateObjectListText(); // Update the text display

            // Log the selection data
            if (dataLogger != null)
            {
                dataLogger.LogData(playerPosition, objectName, visualAngle, timeTaken, 1); // Detected = 1
            }
        }
    }

    // Update the UI text with strike-through for selected objects
    private void UpdateObjectListText()
    {
        objectListText.text = ""; // Clear the existing text

        foreach (var item in objectSelectionStatus)
        {
            if (item.Value)
            {
                // Strike-through text with custom color
                objectListText.text += $"<color=#FF0000><s>{item.Key}</s></color>\n"; // Red strike-through
            }
            else
            {
                // Regular text
                objectListText.text += $"{item.Key}\n"; // Default color for unselected objects
            }
        }
    }

    // Update the timer display
    private void UpdateTimerText()
    {
        timerText.text = $"Time Left: {Mathf.Max(0, timer):0} seconds";
    }

    // End game, display score and game over message
    private void EndGame()
    {
        gameEnded = true;
        timerText.gameObject.SetActive(false); // Hide the timer
        gameOverText.gameObject.SetActive(true); // Show Game Over text
        scoreText.gameObject.SetActive(true); // Show Score text

        // Calculate and display the score
        int score = 0;
        foreach (var item in objectSelectionStatus)
        {
            if (item.Value) score++;
        }
        int totalObjects = objectSelectionStatus.Count;
        scoreText.text = $"Score: {score}/{totalObjects} objects selected";
        gameOverText.text = "GAME OVER";

        // Save data to CSV
        if (dataLogger != null)
        {
            dataLogger.SaveDataToCSV();
        }

        // Optionally, pause the game
        Time.timeScale = 0f;
    }
}


