using UnityEngine;
using System.Collections.Generic;

public class CheckpointData {
    
    public readonly TimeOfDay TimeOfDay;
    public readonly int NumStars;
    public readonly int NumAbilities;
    public readonly Size PlayerSize;
    public Vector3 PlayerPosition { get; }
    public readonly GravityMode PlayerGravityMode;
    private readonly List<CollectibleRestoreEntry> obtainedCollectibles;
    private readonly List<PlatformRestoreEntry> changedMutables;

    public CheckpointData(TimeOfDay timeOfDay, int numStars, int numAbilities, Size playerSize, Vector3 playerPosition, 
        GravityMode playerGravityMode, List<CollectibleRestoreEntry> obtainedCollectibles, List<PlatformRestoreEntry> changedMutables) {
        
        TimeOfDay = timeOfDay;
        NumStars = numStars;
        NumAbilities = numAbilities;
        PlayerSize = playerSize;
        PlayerPosition = playerPosition; 
        PlayerGravityMode = playerGravityMode;
        this.obtainedCollectibles = obtainedCollectibles;
        this.changedMutables = changedMutables;
    }

    public List<PlatformRestoreEntry> GetChangedMutables() {
        return changedMutables;
    }

    public List<CollectibleRestoreEntry> GetObtainedCollectibles() {
        return obtainedCollectibles;
    }
}