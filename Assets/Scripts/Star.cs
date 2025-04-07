using UnityEngine;

public class Star : MonoBehaviour, ICollectible{
    public void Collect(PlayerController playerController) {
        playerController.CollectStar();
        gameObject.SetActive(false);
    }
}