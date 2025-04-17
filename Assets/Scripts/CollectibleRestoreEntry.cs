using UnityEngine;

public class CollectibleRestoreEntry {
    
    public GameObject GameObject { get; private set; }
    public Vector3 Position { get; private set; }

    public CollectibleRestoreEntry(GameObject gameObject) {
        GameObject = gameObject;
        Position = gameObject.transform.position;
    }
}