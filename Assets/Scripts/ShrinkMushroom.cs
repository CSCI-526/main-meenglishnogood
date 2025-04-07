using UnityEngine;

public class ShrinkMushroom : MonoBehaviour, ICollectible{
    public void Collect(PlayerController playerController) {
        playerController.Shrink();
        gameObject.SetActive(false);
    }
}