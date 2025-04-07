using UnityEngine;

public class Star : BaseCollectible {
    
    public override void Collect(PlayerController playerController) {
        playerController.CollectStar();
        gameObject.SetActive(false);
    }
}