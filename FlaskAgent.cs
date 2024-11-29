using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class FlaskAPIClient : MonoBehaviour
{
    // URL of the Flask API
    private string apiUrl = "http://127.0.0.1:5000/predict";

    // Example data to send (replace with actual data from your Unity app)
    private float xCoordinate = -0.738221f;
    private float yCoordinate = 1.46f;
    private float zCoordinate = -3.955398f;
    private int objectName = 0;
    private float visualAngle = 34.60249f;
    private float timeTaken = 8.996854f;

    void Start()
    {
        // Start the POST request when the game starts (or trigger it by an event)
        StartCoroutine(SendPredictionRequest());
    }

    // Coroutine to send data to Flask API and handle the response
    IEnumerator SendPredictionRequest()
    {
        // Create JSON data for POST request
        string jsonData = JsonUtility.ToJson(new PredictionRequestData
        {
            XCoordinate = xCoordinate,
            YCoordinate = yCoordinate,
            ZCoordinate = zCoordinate,
            ObjectName = objectName,
            VisualAngle = visualAngle,
            TimeTaken = timeTaken
        });

        // Create the POST request
        using (UnityWebRequest www = UnityWebRequest.PostWwwForm(apiUrl, jsonData))
        {
            // Set the Content-Type to application/json
            www.SetRequestHeader("Content-Type", "application/json");

            // Send the request and wait for a response
            yield return www.SendWebRequest();

            // Check if the request was successful
            if (www.result == UnityWebRequest.Result.Success)
            {
                // Parse the JSON response
                string response = www.downloadHandler.text;
                Debug.Log("Response: " + response);

                // Handle the response (prediction and confidence)
                // Example: {"prediction": 1, "confidence": 0.9847}
                PredictionResponse predictionResponse = JsonUtility.FromJson<PredictionResponse>(response);
                Debug.Log($"Prediction: {predictionResponse.prediction}, Confidence: {predictionResponse.confidence}");
            }
            else
            {
                // Handle errors (e.g., failed connection)
                Debug.LogError("Error: " + www.error);
            }
        }
    }

    // Class to represent the request data structure
    [System.Serializable]
    public class PredictionRequestData
    {
        public float XCoordinate;
        public float YCoordinate;
        public float ZCoordinate;
        public int ObjectName;
        public float VisualAngle;
        public float TimeTaken;
    }

    // Class to represent the response data structure
    [System.Serializable]
    public class PredictionResponse
    {
        public int prediction;
        public float confidence;
    }
}
