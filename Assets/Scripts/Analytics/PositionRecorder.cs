using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            db.AddHeatmapData(transform.position.x, transform.position.y);
            
            // Logs every second
            yield return new WaitForSeconds(1f);
        }
    }
}
