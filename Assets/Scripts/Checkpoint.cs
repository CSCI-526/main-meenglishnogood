using UnityEngine;
using System.Collections.Generic;

public class Checkpoint {
    
    private int numStars;
    private int numAbilities;
    private Size playerSize;
    private Vector3 playerPosition;
    private GravityMode playerGravityMode;
    private List<GameObject> changedObjects;

    public Checkpoint(int numStars, int numAbilities, Size playerSize, GravityMode playerGravityMode) {
        this.numStars = numStars;
        this.numAbilities = numAbilities;
        this.playerSize = playerSize;
        this.playerGravityMode = playerGravityMode;
        changedObjects = new List<GameObject>();
    }
}