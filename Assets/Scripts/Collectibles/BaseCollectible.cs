using UnityEngine;
using static Constants;

public abstract class BaseCollectible : MonoBehaviour, ICollectible {
    
    void Update() {
        transform.Rotate(0, CollectibleRotationSpeed * Time.deltaTime, 0);
    }

    public abstract void Collect(PlayerController playerController);
}