using System.Collections.Generic;
using UnityEngine;
using static TimeOfDay;

public class LevelState {
    
    public bool IsPanning { get; set; }
    public TimeOfDay TimeOfDay { get; set; } = DAY;
    private CheckpointData startCheckpointData;
    private CheckpointData lastCheckPoint;
    private List<DayNightMutableEntry> dayNightMutableObjects;
    private List<CollectibleRestoreEntry> obtainedCollectibles;
    private List<PlatformRestoreEntry> changedMutables;

    public void Rollback(CheckpointData checkpointData) {
        TimeOfDay = checkpointData.TimeOfDay;
        // TODO Here update the color of the SunSprite, MainCamera background, and blocks processing, etc
        // so essentially mimic an instant ToggleDayNight();
        
        Debug.Log("Respawn: Time of day restored to " + TimeOfDay + ".");
        
        foreach (var collectibleRestoreEntry in checkpointData.GetObtainedCollectibles()) {
            var go = collectibleRestoreEntry.GameObject;
            var position = collectibleRestoreEntry.Position;
            go.transform.position = position;
            go.SetActive(true);
        }
        Debug.Log("Respawn: Collectibles obtained after reaching the last checkpoint were restored.");
        
        foreach (var changedMutableEntry in checkpointData.GetChangedMutables()) {
            var platform = changedMutableEntry.Platform;
            var previousVisibilityMode = changedMutableEntry.PreviousVisibilityMode;
            platform.SetVisibilityMode(previousVisibilityMode);
        }
        Debug.Log("Respawn: Platforms changed after reaching the last checkpoint were restored.");
    }

    public List<DayNightMutableEntry> GetDayNightMutableObjects() {
        return dayNightMutableObjects;
    }

    public CheckpointData GetLastCheckPoint() {
        return lastCheckPoint;
    }

    public void SaveCheckpoint(PlayerState playerState, Vector3 playerPosition) {
        Debug.Log("🟢 Checkpoint Activated at: " + playerPosition + ".");
        lastCheckPoint = new CheckpointData(TimeOfDay, playerState.NumStars, playerState.NumAbilities, playerState.Size,
            playerPosition, playerState.GravityMode, obtainedCollectibles, changedMutables);
        obtainedCollectibles.Clear();
        changedMutables.Clear();
    }
    
    public void SaveStartCheckpoint(PlayerState playerState, Vector3 playerPosition) {
        Debug.Log("🟢 Start Checkpoint Activated at: " + playerPosition + ".");
        startCheckpointData = new CheckpointData(TimeOfDay, playerState.NumStars, playerState.NumAbilities, playerState.Size,
            playerPosition, playerState.GravityMode, obtainedCollectibles, changedMutables);
        lastCheckPoint = startCheckpointData;
    }
    
    public void AddPlatformRestoreEntry(PlatformRestoreEntry platformRestoreEntry) {
        changedMutables.Add(platformRestoreEntry);    
    }

    public void AddCollectibleRestoreEntry(CollectibleRestoreEntry collectibleRestoreEntry) {
        obtainedCollectibles.Add(collectibleRestoreEntry);
    }
    
    /// initialization //

    public void Init() {
        dayNightMutableObjects = new List<DayNightMutableEntry>();
        obtainedCollectibles = new List<CollectibleRestoreEntry>();
        changedMutables = new List<PlatformRestoreEntry>();
        foreach (var mono in Object.FindObjectsOfType<MonoBehaviour>(true)) {
            if (mono is not IDayNightMutable mutable) continue;
            var go = mono.gameObject;
            go.SetActive(mutable.IsVisible(DAY));
            dayNightMutableObjects.Add(new DayNightMutableEntry(go, mutable));
        }
    }
}