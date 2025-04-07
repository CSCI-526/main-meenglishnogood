using UnityEngine;

public class GrowMushroom : MonoBehaviour, ICollectible{
    public void Collect(PlayerController playerController) {
        playerController.Grow();
        gameObject.SetActive(false);
    }
}