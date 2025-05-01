using System.Collections.Generic;
using UnityEngine;
using static TimeOfDay;

public class LevelState {

    public bool IsPanning { get; set; }
    public TimeOfDay TimeOfDay { get; set; } = DAY;

    private CheckpointData startCheckpointData;
    private CheckpointData lastCheckpoint;

    private List<DayNightMutableEntry> dayNightMutables;
    private List<CollectibleRestoreEntry> collectedSinceLastCheckpoint;
    private List<PlatformRestoreEntry> platformsChangedSinceLastCheckpoint;

    // Initialization
    public void Init() {
        dayNightMutables = new List<DayNightMutableEntry>();
        collectedSinceLastCheckpoint = new List<CollectibleRestoreEntry>();
        platformsChangedSinceLastCheckpoint = new List<PlatformRestoreEntry>();

        foreach (var mono in Object.FindObjectsOfType<MonoBehaviour>(true)) {
            if (mono is IDayNightMutable mutable) {
                var go = mono.gameObject;
                go.SetActive(mutable.IsVisible(DAY));
                dayNightMutables.Add(new DayNightMutableEntry(go, mutable));
            }
        }
    }

    // Saving Checkpoints
    public void SaveStartCheckpoint(PlayerState playerState, Vector3 playerPosition) {
        Debug.Log($"🟢 Start Checkpoint Activated at: {playerPosition}.");
        startCheckpointData = CreateCheckpointData(playerState, playerPosition);
        lastCheckpoint = startCheckpointData;
    }

    public void SaveCheckpoint(PlayerState playerState, Vector3 playerPosition) {
        Debug.Log($"🟢 Checkpoint Activated at: {playerPosition}.");
        lastCheckpoint = CreateCheckpointData(playerState, playerPosition);
        collectedSinceLastCheckpoint.Clear();
        platformsChangedSinceLastCheckpoint.Clear();
    }

    private CheckpointData CreateCheckpointData(PlayerState playerState, Vector3 position) {
        return new CheckpointData(
            TimeOfDay,
            playerState.NumStars,
            playerState.NumAbilities,
            playerState.Size,
            position,
            playerState.GravityMode,
            collectedSinceLastCheckpoint,
            platformsChangedSinceLastCheckpoint
        );
    }

    // Rollback
    public void Rollback(CheckpointData checkpoint) {
        TimeOfDay = checkpoint.TimeOfDay;
        Debug.Log($"Respawn: Time of day restored to {TimeOfDay}.");

        RestoreCollectibles(checkpoint.GetObtainedCollectibles());
        RestorePlatforms(checkpoint.GetChangedMutables());
    }

    private void RestoreCollectibles(IEnumerable<CollectibleRestoreEntry> collectibles) {
        foreach (var entry in collectibles) {
            entry.GameObject.transform.position = entry.Position;
            entry.GameObject.SetActive(true);
        }
        Debug.Log("Respawn: Collectibles obtained after reaching the last checkpoint were restored.");
    }

    private void RestorePlatforms(IEnumerable<PlatformRestoreEntry> platforms) {
        foreach (var entry in platforms) {
            entry.Platform.SetVisibilityMode(entry.PreviousVisibilityMode);
        }
        Debug.Log("Respawn: Platforms changed after reaching the last checkpoint were restored.");
    }

    // Data Entry Tracking
    public void AddCollectibleRestoreEntry(CollectibleRestoreEntry entry) {
        collectedSinceLastCheckpoint.Add(entry);
    }

    public void AddPlatformRestoreEntry(PlatformRestoreEntry entry) {
        platformsChangedSinceLastCheckpoint.Add(entry);
    }

    // Getters
    public List<DayNightMutableEntry> GetDayNightMutableObjects() => dayNightMutables;
    public CheckpointData GetLastCheckPoint() => lastCheckpoint;
}
