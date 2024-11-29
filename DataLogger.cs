using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataLogger : MonoBehaviour
{
    private List<DataEntry> dataEntries = new List<DataEntry>(); // List to store collected data
    private string filePath;

    void Start()
    {
        string desktopPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop);
        filePath = Path.Combine(desktopPath, "GameData.csv");
    }

    // Method to add data entries
    public void LogData(Vector3 playerPosition, string objectName, float visualAngle, float timeTaken, int detectionLabel)
    {
        dataEntries.Add(new DataEntry
        {
            X = playerPosition.x,
            Y = playerPosition.y,
            Z = playerPosition.z,
            ObjectName = objectName,
            VisualAngle = visualAngle,
            TimeTaken = timeTaken,
            Detected = detectionLabel
        });
    }

    // Method to write data to a CSV file
    public void SaveDataToCSV()
    {
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            // Write headers
            writer.WriteLine("X-Coordinate,Y-Coordinate,Z-Coordinate,Object Name,Visual Angle,Time Taken,Detected");

            // Write data
            foreach (var entry in dataEntries)
            {
                writer.WriteLine($"{entry.X},{entry.Y},{entry.Z},{entry.ObjectName},{entry.VisualAngle},{entry.TimeTaken},{entry.Detected}");
            }
        }

        Debug.Log($"Data saved to {filePath}");
    }
}

// Data structure to hold individual data entries
public class DataEntry
{
    public float X, Y, Z;
    public string ObjectName;
    public float VisualAngle;
    public float TimeTaken;
    public int Detected;
}
