using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class checkpoint : MonoBehaviour
// {
//     // Start is called before the first frame update
//     void Start()
//     {
        
//     }

//     // Update is called once per frame
//     void Update()
//     {
        
//     }
// }

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("🟢 Checkpoint Activated at: " + transform.position);
            CheckpointManager.Instance.SetCheckpoint(transform.position);
        }
    }
}
