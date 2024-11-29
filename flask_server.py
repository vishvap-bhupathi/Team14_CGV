from flask import Flask, request, jsonify
import joblib
import numpy as np

app = Flask(__name__)

# Load your saved model
model = joblib.load("lr.pkl")  # Ensure "lr.pkl" is in the same folder

@app.route('/')
def home():
    return "Welcome to the ML model API!"


@app.route('/predict', methods=['GET', 'POST'])
def predict():
    if request.method == 'POST':
        print("Received POST request")  # Add this print statement to debug

        try:
            # Receive data from the client
            data = request.get_json()  # `request.json` is an alias for `request.get_json()`
            
            # Parse features from the received JSON
            x_coordinate = data['X-Coordinate']
            y_coordinate = data['Y-Coordinate']
            z_coordinate = data['Z-Coordinate']
            object_name = data['Object Name']  # Assumes object names are encoded as integers
            visual_angle = data['Visual Angle']
            time_taken = data['Time Taken']
            
            # Combine features into a single array
            features = np.array([[x_coordinate, y_coordinate, z_coordinate, object_name, visual_angle, time_taken]])

            # Perform prediction
            prediction = model.predict(features)[0]  # Get the predicted class label
            probability = model.predict_proba(features)[0]  # Get the prediction probabilities

            # Return the result
            return jsonify({
                'prediction': int(prediction),
                'confidence': float(max(probability))  # Maximum probability
            })
        
        except Exception as e:
            return jsonify({'error': str(e)}), 400  # Send a 400 error code for bad requests

    else:
        # Handle GET request
        return jsonify({"message": "GET method is not supported, use POST instead."})


if __name__ == "__main__":
    # Make server accessible locally (127.0.0.1) or externally (0.0.0.0)
    app.run(host="0.0.0.0", port=5000)  # Run Flask on port 5000
