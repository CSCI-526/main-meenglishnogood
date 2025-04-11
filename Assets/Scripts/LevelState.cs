using System.Collections.Generic;
using UnityEngine;
using static TimeOfDay;

public class LevelState {
    
    public TimeOfDay TimeOfDay { get; set; } = DAY;
    private Checkpoint lastCheckPoint;
    private List<DayNightMutableEntry> dayNightMutableObjects;

    public void Rollback(Checkpoint checkpoint) {
        foreach (var obtainedCollectible in checkpoint.GetObtainedCollectibles()) {
            obtainedCollectible.SetActive(true);
        }
        Debug.Log("Respawn: Collectibles obtained after reaching the last checkpoint were restored.");
        
        foreach (var changedMutableEntry in checkpoint.GetChangedMutables()) {
            var platform = changedMutableEntry.Platform;
            var previousVisibilityMode = changedMutableEntry.PreviousVisibilityMode;
            platform.SetVisibilityMode(previousVisibilityMode);
        }
        Debug.Log("Respawn: Platforms changed after reaching the last checkpoint were restored.");
    }

    public List<DayNightMutableEntry> GetDayNightMutableObjects() {
        return dayNightMutableObjects;
    }

    public Checkpoint GetLastCheckPoint() {
        return lastCheckPoint;
    }

    public void SetCheckpoint(Checkpoint checkpoint) {
        lastCheckPoint = checkpoint;
    }

    public void Init() {
        dayNightMutableObjects = new List<DayNightMutableEntry>();
        foreach (var mono in Object.FindObjectsOfType<MonoBehaviour>(true)) {
            if (mono is not IDayNightMutable mutable) continue;
            var go = mono.gameObject;
            go.SetActive(mutable.IsVisible(DAY));
            dayNightMutableObjects.Add(new DayNightMutableEntry(go, mutable));
        }
    }
}