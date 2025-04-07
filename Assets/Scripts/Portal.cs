using UnityEngine;

public class Portal : MonoBehaviour, IDayNightMutable {

    private Portal pairedPortal;
    private readonly VisibilityMode visibilityMode;

    private Portal() {}
    
    public Portal(Portal pairedPortal, VisibilityMode visibilityMode) {
        this.pairedPortal = pairedPortal;
        this.visibilityMode = visibilityMode;
    }

    public bool IsVisible(TimeOfDay currentTimeOfDay) {
        return (visibilityMode == VisibilityMode.DAY && currentTimeOfDay == TimeOfDay.DAY) 
               || (visibilityMode == VisibilityMode.NIGHT && currentTimeOfDay == TimeOfDay.NIGHT);
    }
}