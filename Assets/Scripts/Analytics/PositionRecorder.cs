using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PositionRecorder : MonoBehaviour
{
    public AnalyticsManager db;
    private float x;
    private float y;
    // Start is called before the first frame update
    void Start()
    {
        x = transform.position.x;
        y = transform.position.y;
        StartCoroutine(LogPlayerPosition());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator LogPlayerPosition() {
        while (true) {
            // Added to prevent excessive logging of idle players
            if (x != transform.position.x || y != transform.position.y) {
                db.AddHeatmapData(transform.position.x, transform.position.y, SceneManager.GetActiveScene().name);
                x = transform.position.x;
                y = transform.position.y;
            }
            // Logs every second
            yield return new WaitForSeconds(1f);
        }
    }
}
