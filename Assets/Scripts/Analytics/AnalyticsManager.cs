using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AnalyticsManager : MonoBehaviour
{
    [SerializeField] private string URL = "https://darklight-escape-default-rtdb.firebaseio.com/";
    // [SerializeField] private string key = "lHPqSFZELuNPr1YR1jyCKQBJ4fNzpYnZZF022erF";
    private string sessionId;

    void Start()
    {
        sessionId = Guid.NewGuid().ToString();
        // Initialize Firebase
        // FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
        //     if (task.Result == DependencyStatus.Available) {
        //         db = FirebaseFirestore.DefaultInstance; // Access Firestore directly
        //         Debug.Log("Firestore initialized successfully.");
        //     } else {
        //         Debug.LogError("Could not resolve Firebase dependencies: " + task.Result);
        //     }
        // });
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

        string json = JsonUtility.ToJson(heatmapData);
        SendPayload("positions", json);
    }

    // Taken from class resources: how to send data to firebase real-time database
    private IEnumerator SendPayload(string extension, string json) {
        using (var uwr = new UnityWebRequest(URL + extension + ".json", "POST")) {
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
            using UploadHandlerRaw uploadHandler = new UploadHandlerRaw(jsonToSend);
            uwr.uploadHandler = uploadHandler;
            uwr.downloadHandler = new DownloadHandlerBuffer();
            uwr.disposeUploadHandlerOnDispose = true;
            uwr.disposeDownloadHandlerOnDispose = true;
            uwr.SetRequestHeader("Content-Type", "application/json");
            uwr.timeout = 5;
            //Send the request then wait here until it returns
            yield return uwr.SendWebRequest();

            if (uwr.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("Error While Sending: " + uwr.error);
            }
            else
            {
                Debug.Log("Data Received: " + uwr.downloadHandler.text);
            }
        }
    }
        // Add the document to the 'playerheatmap' collection
    //     db.Collection("playerheatmap").Document(sessionId + "+" + gameTime.ToString()).SetAsync(heatmapData).ContinueWith(task => {
    //         if (task.IsCompleted) {
    //         } else {
    //             Debug.LogError("AddHeatMapData at " + gameTime.ToString() + " failed: " + task.Exception);
    //         }
    //     });
    // }
}