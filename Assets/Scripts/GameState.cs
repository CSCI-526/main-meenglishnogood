using System.Collections.Generic;
using UnityEngine;
using static TimeOfDay;

public class GameState {
    public TimeOfDay TimeOfDay { get; set; } = DAY;
    public Camera MainCamera;
    private Checkpoint lastCheckPoint;
    public SpriteRenderer SunSprite;  
    private List<GameObject> dayNightMutableObjects;

    public GameState(Camera mainCamera, SpriteRenderer sunSprite) {
        MainCamera = mainCamera;
        SunSprite = sunSprite;
    }

    public void Rollback(Checkpoint checkpoint) {
        foreach (var obj in checkpoint.GetChangedObjects()) {
            obj.SetActive(true);
        }
        Debug.Log("Respawn: Collectibles collected after reaching the last checkpoint were restored.");
    }

    public List<GameObject> GetDayNightMutableObjects() {
        return dayNightMutableObjects;
    }

    public void SetDayNightMutableObjects(List<GameObject> objects) {
        dayNightMutableObjects = objects;
    }

    public Checkpoint GetLastCheckPoint() {
        return lastCheckPoint;
    }

    public void SetCheckpoint(Checkpoint checkpoint) {
        lastCheckPoint = checkpoint;
    }
}