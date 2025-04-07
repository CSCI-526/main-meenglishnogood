using System.Collections.Generic;
using UnityEngine;
using static TimeOfDay;

public class GameState {
    public TimeOfDay TimeOfDay { get; set; } = DAY;
    public Camera MainCamera;
    private Checkpoint lastCheckPoint;
    public SpriteRenderer SunSprite;  
    private List<GameObject> dayNightVisibilityObjects;

    public GameState(Camera mainCamera, SpriteRenderer sunSprite) {
        MainCamera = mainCamera;
        SunSprite = sunSprite;
    }

    // private void RollBack() {
    //     // Roll back all size collectibles
    //     foreach (var obj in collectedCollectibles) {
    //         obj.SetActive(true);
    //         Debug.Log("Respawn: Reloaded Shrink Triangle");
    //     }
    // }

    public List<GameObject> GetDayNightMutableObjects() {
        return dayNightVisibilityObjects;
    }

    public void SetDayNightMutableObjects(List<GameObject> objects) {
        dayNightVisibilityObjects = objects;
    }
}