import requests

# URL of the Flask API endpoint
url = "http://127.0.0.1:5000/predict"

# Data you want to send in JSON format
data = {
    "X-Coordinate": 4.2,
    "Y-Coordinate": 1.46,
    "Z-Coordinate": 6.66,
    "Object Name": 0,
    "Visual Angle": 4.5,
    "Time Taken": 6.5
}

# Send the POST request with the JSON data
response = requests.post(url, json=data)

# Print the response from the server
print(response.json())
