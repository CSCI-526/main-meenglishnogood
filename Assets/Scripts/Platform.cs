using UnityEngine;

public class Platform : MonoBehaviour, IDayNightMutable {
    
    [SerializeField] private VisibilityMode visibilityMode;

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
    }
}