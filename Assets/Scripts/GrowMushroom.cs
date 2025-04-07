using UnityEngine;

public class GrowMushroom : BaseCollectible {
    
    public override void Collect(PlayerController playerController) {
        playerController.Grow();
        gameObject.SetActive(false);
    }
}