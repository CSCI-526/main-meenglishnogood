using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    private void Start()
    {
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Checkpoint Activated at: " + transform.position);

            Vector3 offsetPosition = transform.position + new Vector3(0f, 0f, 0f);  // record position, a littl offset can be added to avoid bugs
            CheckpointManager.Instance.SetCheckpoint(offsetPosition);

            Rigidbody2D playerRb = other.gameObject.GetComponent<Rigidbody2D>();  // record gravity status
            CheckpointManager.Instance.SetGravityScale(playerRb.gravityScale);

            Vector3 playerLocalScale = other.gameObject.transform.localScale;
            CheckpointManager.Instance.SetLastLocalScale(playerLocalScale); // record size status

            // powerups and platforms
            CheckpointManager.Instance.SavePowerupsStates();  // powerup
            CheckpointManager.Instance.GetChangedPlatforms();

            // ability num
            CheckpointManager.Instance.SetAbilityCount(other.gameObject.GetComponent<PickupAndPlace>().abilityNum);

            //After passing the checkpoint, change the checkpoint's color to green
            SpriteRenderer[] childRenderers = GetComponentsInChildren<SpriteRenderer>();
            foreach (SpriteRenderer renderer in childRenderers)
            {
                renderer.color = Color.green;
            }
        }
    }

    //public void CheckPointSave()
    //{

    //    CheckpointManager.Instance.SetCheckpoint(new Vector3(0f, 0f, 0f));

    //    CheckpointManager.Instance.SetGravityScale(1f);

    //    CheckpointManager.Instance.SetLastLocalScale(new Vector3(0.74f, 0.7f, 1f)); // record size status

    //    // powerups and platforms
    //    CheckpointManager.Instance.SavePowerupsStates(); 
    //    CheckpointManager.Instance.GetChangedPlatforms();

    //    // ability num
    //    CheckpointManager.Instance.SetAbilityCount(0);
    //}
}
