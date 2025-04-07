using UnityEngine;
using System.Collections.Generic;

public class Checkpoint {
    
    public readonly int NumStars;
    public readonly int NumAbilities;
    public readonly Size PlayerSize;
    public Vector3 PlayerPosition { get; }
    public readonly GravityMode PlayerGravityMode;
    private readonly List<GameObject> changedObjects;

    public Checkpoint(int numStars, int numAbilities, Size playerSize, Vector3 playerPosition, GravityMode playerGravityMode) {
        NumStars = numStars;
        NumAbilities = numAbilities;
        PlayerSize = playerSize;
        PlayerPosition = playerPosition; 
        PlayerGravityMode = playerGravityMode;
        changedObjects = new List<GameObject>();
    }

    public List<GameObject> GetChangedObjects() {
        return changedObjects;
    }
}