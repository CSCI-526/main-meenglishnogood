﻿using UnityEngine;

public class PortalController : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public int mode = 1;

    public Color dayColor = new Color(0.5f, 0.8f, 1.0f);
    public Color nightColor = new Color(0.05f, 0.05f, 0.2f, 1f);

    public PortalController otherPortal;
    public float teleportYOffset;

    private bool playerInPortal = false;

    public float cooldownTime = 1.0f;
    private float lastTeleportTime = -999f;

    void Start()
    {
        UpdateColor();
    }

    void Update()
    {
        TryTeleport();
    }

    public void TryTeleport()
    {
        var dayNight = FindObjectOfType<DayNightController>();
        if (playerInPortal && otherPortal != null && dayNight.isTransitioning)
        {
            if (Time.time - lastTeleportTime < cooldownTime) return;

            if ( mode == 1)
            {
                TeleportPlayer();
            }
            else if ( mode == 2)
            {
                TeleportPlayer();
            }
        }
    }

    public void SetMode(int newMode)
    {
        mode = newMode;
        UpdateColor();
    }

    private void UpdateColor()
    {
        switch (mode)
        {
            case 1:
                spriteRenderer.color = nightColor;
                break;
            case 2:
                spriteRenderer.color = dayColor;
                break;
            default:
                Debug.LogWarning("Invalid mode, setting to default color.");
                spriteRenderer.color = dayColor;
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInPortal = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInPortal = false;
        }
    }

    private void TeleportPlayer()
    {

        lastTeleportTime = Time.time;
        if (otherPortal != null)
        {
            otherPortal.lastTeleportTime = Time.time;
        }

        Vector3 teleportPosition = otherPortal.transform.position + Vector3.up * teleportYOffset;
        FindObjectOfType<PlayerController>().transform.position = teleportPosition;
    }

    public void UpdatePortalState(bool isDay)
    {
        spriteRenderer.color = isDay ? dayColor : nightColor;
    }
}
