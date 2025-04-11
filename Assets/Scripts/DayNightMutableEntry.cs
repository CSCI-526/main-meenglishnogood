using UnityEngine;

public class DayNightMutableEntry {
    
    public GameObject GameObject { get; private set; }
    public IDayNightMutable Mutable { get; private set; }

    public DayNightMutableEntry(GameObject gameObject, IDayNightMutable mutable) {
        GameObject = gameObject;
        Mutable = mutable;
    }
}