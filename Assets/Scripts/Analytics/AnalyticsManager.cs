using System;
using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using static AnalyticsManager;

public class AnalyticsManager : MonoBehaviour {
    
    private string sessionId;
    [SerializeField] private string URL = "https://darklight-escape-default-rtdb.firebaseio.com/";
    
    [Serializable]
    public class HeatmapData {
        public float x;
        public float y;
        public string level;
        public float gameTime;
        public string sessionId;
    }

    [Serializable]
    public class CollectibleData {
        public string item;
        public string level;
        public float gameTime;
        public string sessionId;
    }

    void Start() {
        sessionId = Guid.NewGuid().ToString();
    }
    
    public void AddHeatmapData(float x, float y, string level) {
        var gameTime = Time.time;
        var data = new HeatmapData {
            x = x,
            y = y,
            level = level,
            gameTime = gameTime,
            sessionId = sessionId
        };

        var json = JsonUtility.ToJson(data);
        StartCoroutine(SendPayload("positions", json));
    }

    public void AddCollectibleData(string item, string level) {
        var gameTime = Time.time;
        var data = new CollectibleData {
            item = item,
            level = level,
            gameTime = gameTime,
            sessionId = sessionId
        };

        var json = JsonUtility.ToJson(data);
        StartCoroutine(SendPayload("collectibles", json));
    }

    // Taken from class resources: how to send data to firebase real-time database
    private IEnumerator SendPayload(string extension, string json) {
        using (var uwr = new UnityWebRequest(URL + extension + ".json", "POST")) {
            var jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
            using var uploadHandler = new UploadHandlerRaw(jsonToSend);
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