using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Firestore;

public class AnalyticsManager : MonoBehaviour
{
    private FirebaseFirestore db;
    private string sessionId;

    void Start()
    {
        // Initialize Firebase
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            if (task.Result == DependencyStatus.Available) {
                db = FirebaseFirestore.DefaultInstance; // Access Firestore directly
                Debug.Log("Firestore initialized successfully.");
                sessionId = Guid.NewGuid().ToString();
            } else {
                Debug.LogError("Could not resolve Firebase dependencies: " + task.Result);
            }
        });
    }

    public void AddHeatmapData(float x, float y) {
        float gameTime = Time.time;
        // Create a dictionary with provided X and Y coordinates
        Dictionary<string, object> heatmapData = new Dictionary<string, object>
        {
            { "x", x },
            { "y", y },
            { "gameTime", gameTime },
            { "sessionId", sessionId }
        };

        // Add the document to the 'playerheatmap' collection
        db.Collection("playerheatmap").Document(sessionId + "+" + gameTime.ToString()).SetAsync(heatmapData).ContinueWith(task => {
            if (task.IsCompleted) {
            } else {
                Debug.LogError("AddHeatMapData at " + gameTime.ToString() + " failed: " + task.Exception);
            }
        });
    }
}