using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using static AnalyticsManager;

public class AnalyticsManager : MonoBehaviour
{
    [SerializeField] private string URL = "https://darklight-escape-default-rtdb.firebaseio.com/";
    [System.Serializable]
    public class HeatmapData {
        public float x;
        public float y;
        public string level;
        public string notes;
        public float gameTime;
        public string sessionId;
    }

    // Similarly for collectibles
    [System.Serializable]
    public class CollectibleData {
        public string item;
        public string level;
        public string notes;
        public float gameTime;
        public string sessionId;
    }
    private string sessionId;

    void Start()
    {
        sessionId = Guid.NewGuid().ToString();
    }
    
    public void AddHeatmapData(float x, float y, string level, string notes = "") {
        float gameTime = Time.time;
        HeatmapData data = new HeatmapData {
            x = x,
            y = y,
            level = level,
            notes = notes,
            gameTime = gameTime,
            sessionId = sessionId
        };

        string json = JsonUtility.ToJson(data);
        if (notes != "") {
            Debug.Log("Logging " + notes);
        }
        StartCoroutine(SendPayload("positionsbeta", json));
    }

    public void AddCollectibleData(string item, string level, string notes = "") {
        float gameTime = Time.time;
        CollectibleData data = new CollectibleData {
            item = item,
            level = level,
            notes = notes,
            gameTime = gameTime,
            sessionId = sessionId
        };

        string json = JsonUtility.ToJson(data);
        if (notes != "") {
            Debug.Log("Logging " + notes);
        }
        StartCoroutine(SendPayload("collectiblesbeta", json));
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
        }
    }
}