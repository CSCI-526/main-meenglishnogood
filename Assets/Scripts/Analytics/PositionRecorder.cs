using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using static Constants;

public class PositionRecorder : MonoBehaviour {
    
    public AnalyticsManager db;
    
    void Start() {
        StartCoroutine(LogPlayerPosition());
    }

    IEnumerator LogPlayerPosition() {
        while (true) {
            db.AddHeatmapData(transform.position.x, transform.position.y, SceneManager.GetActiveScene().name);
            
            // Logs once per AnalyticsLogCoolDownTime seconds
            yield return new WaitForSeconds(AnalyticsLogCoolDownTime);
        }
    }
}
