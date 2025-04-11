using UnityEngine;

public class Sun : MonoBehaviour {
    
    private void Start() {
        LevelManager.Instance.sunSprite = GetComponent<SpriteRenderer>();
    }
}