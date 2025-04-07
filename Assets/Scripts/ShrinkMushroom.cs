using UnityEngine;

public class ShrinkMushroom : BaseCollectible {
    
    public override void Collect(PlayerController playerController) {
        playerController.Shrink();
        gameObject.SetActive(false);
    }
}