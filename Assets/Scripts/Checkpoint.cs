using UnityEngine;
using System.Collections.Generic;

public class Checkpoint {
    
    public readonly TimeOfDay TimeOfDay;
    public readonly int NumStars;
    public readonly int NumAbilities;
    public readonly Size PlayerSize;
    public Vector3 PlayerPosition { get; }
    public readonly GravityMode PlayerGravityMode;
    private readonly List<GameObject> obtainedCollectibles;
    private readonly List<ChangedPlatformEntry> changedMutables;

    public Checkpoint(TimeOfDay timeOfDay, int numStars, int numAbilities, Size playerSize, Vector3 playerPosition, GravityMode playerGravityMode) {
        TimeOfDay = timeOfDay;
        NumStars = numStars;
        NumAbilities = numAbilities;
        PlayerSize = playerSize;
        PlayerPosition = playerPosition; 
        PlayerGravityMode = playerGravityMode;
        changedMutables = new List<ChangedPlatformEntry>();
        obtainedCollectibles = new List<GameObject>();
    }

    public List<ChangedPlatformEntry> GetChangedMutables() {
        return changedMutables;
    }

    public List<GameObject> GetObtainedCollectibles() {
        return obtainedCollectibles;
    }
}