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

            Vector3 offsetPosition = transform.position + new Vector3(0f, 0f, 0f);  // record position, a littl offset can be added to avoid bugs
            CheckpointManager.Instance.SetCheckpoint(offsetPosition);

            Rigidbody2D playerRb = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();  // record gravity status
            CheckpointManager.Instance.SetGravityScale(playerRb.gravityScale);

            Vector3 playerLocalScale = GameObject.FindWithTag("Player").transform.localScale;
            CheckpointManager.Instance.SetLastLocalScale(transform.localScale); // record size status
        }
    }
}
