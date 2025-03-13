using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PositionRecorder : MonoBehaviour
{
    public AnalyticsManager db;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LogPlayerPosition());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator LogPlayerPosition() {
        while (true) {
            db.AddHeatmapData(transform.position.x, transform.position.y, SceneManager.GetActiveScene().name);
            
            // Logs every second
            yield return new WaitForSeconds(1f);
        }
    }
}
