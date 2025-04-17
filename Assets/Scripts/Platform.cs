using UnityEngine;
using static Constants;

public class Platform : MonoBehaviour, IDayNightMutable {
    
    private SpriteRenderer spriteRenderer;
    [SerializeField] public VisibilityMode visibilityMode;

    private Platform() {}
    
    public Platform(VisibilityMode visibilityMode) {
        this.visibilityMode = visibilityMode;
    }

    public bool IsVisible(TimeOfDay currentTimeOfDay) {
        return visibilityMode == VisibilityMode.PERSISTENT 
               || (visibilityMode == VisibilityMode.DAY && currentTimeOfDay == TimeOfDay.DAY) 
               || (visibilityMode == VisibilityMode.NIGHT && currentTimeOfDay == TimeOfDay.NIGHT);
    }

    public bool IsPersistent() {
        return visibilityMode == VisibilityMode.PERSISTENT;
    }
    
    public void SetVisibilityMode(VisibilityMode newVisibilityMode) {
        visibilityMode = newVisibilityMode;
        UpdateColor();
    }
    
    private void OnValidate() {
        if (spriteRenderer == null) {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        if (spriteRenderer == null) return;
        UpdateColor();
    }
    
    private void UpdateColor() {
        spriteRenderer.color = visibilityMode switch {
            VisibilityMode.DAY => NightBackgroundColor,
            VisibilityMode.NIGHT => DayBackgroundColor,
            VisibilityMode.PERSISTENT => PersistentColor,
            _ => spriteRenderer.color
        };
    }
}