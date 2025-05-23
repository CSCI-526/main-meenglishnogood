using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Sprite activatedSprite;

    private void Start()
    {
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Checkpoint Activated at: " + transform.position);

            Vector3 offsetPosition = transform.position + new Vector3(0f, 0f, 0f);  // record position, a little offset can be added to avoid bugs
            CheckpointManager.Instance.SetCheckpoint(offsetPosition);

            Rigidbody2D playerRb = other.gameObject.GetComponent<Rigidbody2D>();  // record gravity status
            CheckpointManager.Instance.SetGravityScale(playerRb.gravityScale);

            Vector3 playerLocalScale = other.gameObject.transform.localScale;
            bool hasShrunk = other.gameObject.GetComponent<PlayerSizeControll2D>().hasShrunk;
            bool isSmall = other.gameObject.GetComponent<PlayerController>().isSmall;
            CheckpointManager.Instance.SetLastLocalScale(playerLocalScale, isSmall, hasShrunk); // record size status

            // platforms
            // CheckpointManager.Instance.SavePowerupsStates();  // powerup
            // CheckpointManager.Instance.GetChangedPlatforms();

            // ability num
            CheckpointManager.Instance.SetAbilityCount(other.gameObject.GetComponent<PickupAndPlace>().abilityNum);

            //After passing the checkpoint, change the checkpoint's color to green
            SpriteRenderer[] childRenderers = GetComponentsInChildren<SpriteRenderer>();
            foreach (SpriteRenderer renderer in childRenderers)
            {
                renderer.sprite = activatedSprite; // �����µ�sprite
            }
        }
    }

}
