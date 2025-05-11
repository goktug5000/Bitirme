import numpy as np
import pandas as pd
from sklearn.model_selection import train_test_split
from sklearn.ensemble import RandomForestClassifier
import joblib
from sklearn.ensemble import RandomForestClassifier
from sklearn.metrics import accuracy_score, classification_report

def load_data(file_path):
    data = []
    labels = []
    with open(file_path, 'r') as file:
        for line in file:
            parts = line.strip().rsplit(' ', 1)
            if len(parts) != 2:
                continue
            
            numbers = list(map(float, parts[0].split()))
            label = parts[1].strip()
            
            data.append(numbers)
            labels.append(label)
    
    return np.array(data), np.array(labels)

def add_new_data(file_path, new_line):
    with open(file_path, 'a') as file:
        file.write('\n' + new_line)
    print("Yeni veri eklendi!")

X, y = load_data("Datas.txt")

X_train, X_test, y_train, y_test = train_test_split(X, y, test_size=0.01, random_state=42)
model = RandomForestClassifier(n_estimators=100, random_state=42, class_weight='balanced')
model.fit(X_train, y_train)

joblib.dump(model, "animation_classifier.pkl")
print("Model güncellendi ve kaydedildi.")




# ⬇️ Offline test ⬇️
print("\n🧪 OFFLINE TEST BAŞLIYOR...")

# Tek bir örnek üzerinde test
sample_input = X_test[0].reshape(1, -1)
predicted = model.predict(sample_input)
actual = y_test[0]

print("🔹 Tahmin Edilen Etiket :", predicted[0])
print("🔹 Gerçek Etiket        :", actual)

# Tüm test verisi üzerinde doğruluk ve rapor
y_pred = model.predict(X_test)
print("\n📊 Accuracy:", accuracy_score(y_test, y_pred))
print("\n🧾 Classification Report:\n", classification_report(y_test, y_pred))