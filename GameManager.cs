using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject instructionPanel; // Reference to the instruction panel GameObject
    public Button startButton;          // Reference to the start button

    void Start()
    {
        // Ensure the instruction panel is visible at the start
        instructionPanel.SetActive(true);
        Time.timeScale = 0f; // Pause the game initially

        // Add listener for the start button
        startButton.onClick.AddListener(StartGame);
    }

    void StartGame()
    {
        instructionPanel.SetActive(false); // Hide the instructions
        Time.timeScale = 1f; // Resume the game
    }
}
