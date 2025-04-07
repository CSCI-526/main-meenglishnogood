using UnityEngine;
using static Constants;

public class CollectibleSpinEffect : MonoBehaviour {

    void Update() {
        transform.Rotate(0, CollectibleRotationSpeed * Time.deltaTime, 0);
    }
}   