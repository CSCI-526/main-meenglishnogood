using UnityEngine;
using System.Collections;

public class InvisibleWalls : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public int mode = 1;

    public Color dayColor = new Color(0.5f, 0.8f, 1.0f);
    public Color nightColor = new Color(0.05f, 0.05f, 0.2f, 1f);
    public bool isDaytime = true;
    public float transitionDuration = 1f;

    public bool isTransitioning = false;


    void Start()
    {
        UpdateColor();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !isTransitioning) // Detect if "Q" key is pressed
        {
            StartCoroutine(ChangeColor());
        }
    }

    private IEnumerator ChangeColor()
    {

        isTransitioning = true;
        SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();
        Color startColor = renderer.color;
        float elapsedTime = 0f;
        if (startColor != dayColor) // if the object is not transparent, make it transparent
        {
            while (elapsedTime < transitionDuration)
            {
                elapsedTime += Time.deltaTime;
                renderer.color = Color.Lerp(startColor, dayColor, elapsedTime / transitionDuration);
                yield return null;
            }
            renderer.color = dayColor;
        }
        else // if the object already transparent, make it visible
        {
            while (elapsedTime < transitionDuration)
            {
                elapsedTime += Time.deltaTime;
                renderer.color = Color.Lerp(startColor, nightColor, elapsedTime / transitionDuration);
                yield return null;
            }
            renderer.color = nightColor;

        }
        isTransitioning = false;
    }


    public void UpdateColor()
    {
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
            case 3:
                spriteRenderer.color = isDaytime ? nightColor : dayColor;
                break;
            default:
                Debug.LogWarning("Invalid mode, setting to default color.");
                spriteRenderer.color = dayColor;
                break;
        }

    }
}
