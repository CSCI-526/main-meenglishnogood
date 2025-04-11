using System;
using UnityEngine;

public class InputHandler : MonoBehaviour {
    
    public static InputHandler instance;
    public float Horizontal { get; private set; }
    public static event Action JumpPressed;
    public static event Action DropAbilityPressed;
    public static event Action ToggleDayNightPressed;


    private void Awake() {
        instance = this;
    }
    
    private void Update() {
        if (LevelManager.Instance.LevelState.IsPanning) return;
        
        Horizontal = Input.GetAxis("Horizontal");
        
        if (Input.GetButtonDown("Jump")) {
            JumpPressed?.Invoke();
        } else if (Input.GetKeyDown(KeyCode.E)) {
            DropAbilityPressed?.Invoke();
        } else if (Input.GetKeyDown(KeyCode.Q)) {
            ToggleDayNightPressed?.Invoke();
        }
    }
}