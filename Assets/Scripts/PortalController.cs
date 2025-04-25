using UnityEngine;
using System.Collections;

public class PortalController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public int mode = 1;

    public Color dayColor = new Color(0.5f, 0.8f, 1.0f);
    public Color nightColor = new Color(0.05f, 0.05f, 0.2f, 1f);

    public PortalController otherPortal;
    public float teleportYOffset;

    private bool playerInPortal = false;

    public float cooldownTime = 2f;
    public float colorchangingTime = 1f;
    private float lastTeleportTime = -999f;

    public bool isTransitioningColorChange = false;

    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        UpdateColor();
    }

    void Update()
    {
        
        // 按q切换颜色
        if (Input.GetKeyDown(KeyCode.Q) && !isTransitioningColorChange) // Detect if "Q" key is pressed
        {
            Debug.Log("PortalController: Q pressed");
            StartCoroutine(ChangeColor());
        }

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
        //switch (mode)
        //{
        //    case 1:
        //        spriteRenderer.color = nightColor;
        //        break;
        //    case 2:
        //        spriteRenderer.color = dayColor;
        //        break;
        //    default:
        //        Debug.LogWarning("Invalid mode, setting to default color.");
        //        spriteRenderer.color = dayColor;
        //        break;
        //}
        GameObject sun = GameObject.Find("Sun");
        DayNightController dnctrl = sun.GetComponent<DayNightController>();
        switch (mode)
        {
            case 1:
                if (dnctrl.isDay)
                {
                    spriteRenderer.color = nightColor;
                }
                else
                {
                    spriteRenderer.color = dayColor;
                }
                //spriteRenderer.color = nightColor;
                break;
            case 2:
                if (dnctrl.isDay)
                {
                    spriteRenderer.color = dayColor;
                }
                else
                {
                    spriteRenderer.color = nightColor;
                }
                break;
            //case 3:
            //    spriteRenderer.color = isDaytime ? nightColor : dayColor;
            //    break;
            default:
                Debug.LogWarning("Invalid mode, setting to default color.");
                spriteRenderer.color = dayColor;
                break;
        }
    }

    private IEnumerator ChangeColor()
    {
        float elapsedTime = 0f;
        isTransitioningColorChange = true;
        Color startColor = spriteRenderer.color;
        if (startColor != dayColor) // if the object is not transparent, make it transparent
        {
            while (elapsedTime < colorchangingTime)
            {
                elapsedTime += Time.deltaTime;
                spriteRenderer.color = Color.Lerp(startColor, dayColor, elapsedTime / cooldownTime);
                yield return null;
            }
            spriteRenderer.color = dayColor;
        }
        else // if the object already transparent, make it visible
        {
            while (elapsedTime < colorchangingTime)
            {
                elapsedTime += Time.deltaTime;
                spriteRenderer.color = Color.Lerp(startColor, nightColor, elapsedTime / cooldownTime);
                yield return null;
            }
            spriteRenderer.color = nightColor;
        }
        isTransitioningColorChange = false;
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
