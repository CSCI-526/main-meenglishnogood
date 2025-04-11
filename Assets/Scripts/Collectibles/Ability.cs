using UnityEngine;
using static VisibilityMode;

public class Ability : BaseCollectible {
    
    public override void Collect(PlayerController playerController) {
        playerController.CollectAbility();
        gameObject.SetActive(false);
    }
    
    private void OnCollisionEnter2D(Collision2D other) {
        if (!other.collider.CompareTag("Platform")) return; 
        var platform = other.gameObject.GetComponent<Platform>();
        if (platform.IsPersistent()) return;
        platform.SetVisibilityMode(PERSISTENT);
        gameObject.SetActive(false);
    }
}