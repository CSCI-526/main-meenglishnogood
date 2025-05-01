using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class AnalyticsManager : MonoBehaviour
{
    [SerializeField] private string databaseUrl = "https://darklight-escape-default-rtdb.firebaseio.com/";

    private string sessionId;

    [Serializable]
    private class HeatmapData
    {
        public float x;
        public float y;
        public string level;
        public float gameTime;
        public string sessionId;
    }

    [Serializable]
    private class CollectibleData
    {
        public string item;
        public string level;
        public float gameTime;
        public string sessionId;
    }

    private void Start()
    {
        sessionId = Guid.NewGuid().ToString();
    }

    public void AddHeatmapData(float x, float y, string level)
    {
        var data = new HeatmapData
        {
            x = x,
            y = y,
            level = level,
            gameTime = Time.time,
            sessionId = sessionId
        };

        CreatePayloadAndSend("positions", data);
    }

    public void AddCollectibleData(string item, string level)
    {
        var data = new CollectibleData
        {
            item = item,
            level = level,
            gameTime = Time.time,
            sessionId = sessionId
        };

        CreatePayloadAndSend("collectibles", data);
    }

    private void CreatePayloadAndSend(string endpoint, object data)
    {
        var json = JsonUtility.ToJson(data);
        StartCoroutine(SendPayload(endpoint, json));
    }

    private IEnumerator SendPayload(string endpoint, string json)
    {
        using var request = new UnityWebRequest($"{databaseUrl}{endpoint}.json", "POST");
        byte[] bodyRaw = new System.Text.UTF8Encoding().GetBytes(json);

        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.disposeUploadHandlerOnDispose = true;
        request.disposeDownloadHandlerOnDispose = true;
        request.SetRequestHeader("Content-Type", "application/json");
        request.timeout = 5;

        yield return request.SendWebRequest();
    }
}
