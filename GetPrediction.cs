using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class GetPrediction : MonoBehaviour
{
    // This method simulates sending data (e.g., from the player) to the server for prediction
    void Start()
    {
        // Simulate sending test data
        float[] sampleFeatures = { -3.68f, 1.46f, -4.94f, 28.40415f, 1.569804f };
        string objectName = "Sofa";  // Object name as string, ensure to pass a valid object name
        SendDataToMLServer(sampleFeatures, objectName);
    }

    // This method sends the sample features to the server using a GET request
    IEnumerator SendDataToMLServer(float[] features, string objectName)
    {
        // Extract values from the features array
        float xCoordinate = features[0];
        float yCoordinate = features[1];
        float zCoordinate = features[2];
        float visualAngle = features[3];
        float timeTaken = features[4];

        // URL encode the parameters (for safety)
        string encodedObjectName = UnityWebRequest.EscapeURL(objectName);
        string url = $"http://127.0.0.1:5000/predict?X-Coordinate={xCoordinate.ToString()}&Y-Coordinate={yCoordinate.ToString()}&Z-Coordinate={zCoordinate.ToString()}&Visual%20Angle={visualAngle.ToString()}&Time%20Taken={timeTaken.ToString()}&Object%20Name={encodedObjectName}";

        // Log the URL for debugging purposes
        Debug.Log("Request URL: " + url);

        // Send GET request
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        // Check the response from the server
        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Prediction Response: " + request.downloadHandler.text);
        }
        else
        {
            Debug.LogError("Error: " + request.error);
        }
    }
}
