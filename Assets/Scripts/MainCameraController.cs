using UnityEngine;

public class MainCameraController : MonoBehaviour {
    
    private void Start() {
        LevelManager.Instance.mainCamera = GetComponent<Camera>();
    }
}