using UnityEngine;

public class AbilityEntry {
    
    public GameObject GameObject { get; private set; }
    public Rigidbody2D Rb { get; private set; }

    public AbilityEntry(GameObject gameObject) {
        GameObject = gameObject;
        Rb = gameObject.GetComponent<Rigidbody2D>();
    }
}