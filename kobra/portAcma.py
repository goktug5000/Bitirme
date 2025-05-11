import numpy as np
import pandas as pd
from sklearn.model_selection import train_test_split
from sklearn.ensemble import RandomForestClassifier
import joblib
from flask import Flask, request, jsonify

app = Flask(__name__)

model = joblib.load("animation_classifier.pkl")

@app.route("/predict", methods=["POST"])
def predict():
    try:
        print("Raw data:", request.data)
        print("Request content type:", request.content_type)
        print("Parsed JSON:", request.json)

        if not request.is_json:
            return jsonify({"error": "Content-Type must be application/json"}), 400

        data = request.json.get("input")
        if data is None:
            return jsonify({"error": "No 'input' key in JSON"}), 400

        input_array = np.array([data])
        prediction = model.predict(input_array)
        # return jsonify({"prediction": prediction[0]}) // TODO: ileride değer koyacaksan böyle yap
        return prediction[0]
    except Exception as e:
        return jsonify({"error": str(e)}), 500

if __name__ == "__main__":
    app.run(host="0.0.0.0", port=5000)