using UnityEngine;

public class InvisibleWalls : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public int mode = 1;

    public Color dayColor = new Color(0.5f, 0.8f, 1.0f);
    public Color nightColor = new Color(0.05f, 0.05f, 0.2f, 1f);

    void Start()
    {
        UpdateColor();
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
}
