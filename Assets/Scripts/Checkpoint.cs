using UnityEngine;
using static Constants;

public class Checkpoint : MonoBehaviour{
    
    public bool IsActivated { get; private set; }

    public void Activate() {
        IsActivated = true;
        var spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        foreach (var spriteRenderer in spriteRenderers) {
            spriteRenderer.color = CheckpointActiveColor;
        }
    }
    
}