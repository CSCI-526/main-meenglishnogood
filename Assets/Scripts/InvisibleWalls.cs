using UnityEngine;

public class InvisibleWalls : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public int mode = 1;

    public Color dayColor = new Color(0.5f, 0.8f, 1.0f);
    public Color nightColor = new Color(0.05f, 0.05f, 0.2f, 1f);
    public bool isDaytime = true;
    void Start()
    {
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
