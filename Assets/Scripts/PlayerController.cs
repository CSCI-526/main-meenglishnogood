using UnityEngine;
using static Constants;
using static GravityMode;
using static Size;

public class PlayerController : MonoBehaviour {

    private Rigidbody2D rb;
    private Vector2 movement;
    private PlayerState playerState;
    private LayerMask groundLayer;

    private void Start() {
        rb = InitializeRigidbody();
        playerState = LevelManager.Instance.PlayerState;
        groundLayer = LayerMask.GetMask("Ground");
        SaveStartCheckpoint();
    }

    private void OnEnable() {
        InputHandler.JumpPressed += HandleJump;
        InputHandler.DropAbilityPressed += HandleDropAbility;
    }

    private void OnDisable() {
        InputHandler.JumpPressed -= HandleJump;
        InputHandler.DropAbilityPressed -= HandleDropAbility;
    }

    private void Update() {
        playerState.IsGrounded = IsGrounded(playerState.GravityMode == NORMAL ? Vector2.down : Vector2.up);
    }

    private void FixedUpdate() {
        rb.velocity = new Vector2(InputHandler.instance.Horizontal * PlayerHorizontalVelocity, rb.velocity.y);
    }

    private void HandleJump() {
        if (playerState.IsGrounded) {
            PerformJump();
        }
    }

    private void HandleDropAbility() {
        if (playerState.Abilities.Count > 0) {
            DropAbility();
        }
    }

    private void PerformJump() {
        float direction = (playerState.GravityMode == NORMAL) ? 1f : -1f;
        if (playerState.GravityMode == NORMAL || (playerState.GravityMode == ANTIGRAVITY && playerState.IsSmall())) {
            rb.velocity = new Vector2(rb.velocity.x, direction * PlayerJumpVerticalVelocity);
        }
    }

    private void DropAbility() {
        Debug.Log("Place ability.");

        playerState.NumAbilities--;
        var abilityEntry = playerState.Abilities.Pop();

        var ability = abilityEntry.GameObject;
        var abilityRb = abilityEntry.Rb;

        ability.transform.position = transform.position + transform.right * AbilityInstantiateDistance;
        abilityRb.gravityScale = rb.gravityScale;
        ability.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        switch (other.tag) {
            case "AntiGravityZone":
                EnterAntiGravityZone();
                break;
            case "Destination":
                ReachDestination();
                break;
            case "Checkpoint":
                ActivateCheckpoint(other.GetComponent<Checkpoint>());
                break;
            case "Portal":
                EnterPortal(other.GetComponent<Portal>());
                break;
            default:
                if (other.TryGetComponent(out BaseCollectible collectible)) {
                    collectible.Process(this);
                }
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        switch (other.tag) {
            case "AntiGravityZone":
                ExitAntiGravityZone();
                break;
            case "Portal":
                ExitPortal();
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.CompareTag("Spike")) {
            Respawn();
        }
    }

    private void EnterAntiGravityZone() {
        Debug.Log("The player enters the anti-gravity zone.");
        playerState.GravityMode = ANTIGRAVITY;

        if (playerState.IsBig()) {
            Debug.Log("The player is too big to be affected by the anti-gravity zone.");
            return;
        }

        rb.gravityScale = AntiGravityScale;
        rb.velocity = new Vector2(rb.velocity.x, 5f);
        Debug.Log("Gravity mode switched.");
    }

    private void ExitAntiGravityZone() {
        Debug.Log("The player leaves the anti-gravity zone.");
        playerState.GravityMode = NORMAL;

        if (!playerState.IsBig()) {
            rb.gravityScale = NormalGravityScale;
            rb.velocity = new Vector2(rb.velocity.x, 0f);
        }
    }

    private void EnterPortal(Portal portal) {
        Debug.Log("The player enters the portal zone.");
        playerState.InPortal = true;
        playerState.CurrentPortal = portal;
    }

    private void ExitPortal() {
        Debug.Log("The player exits the portal zone.");
        playerState.InPortal = false;
        playerState.CurrentPortal = null;
    }

    private void ReachDestination() {
        // Placeholder for end-level logic
    }

    public void Shrink() {
        if (playerState.IsBig()) {
            transform.localScale *= ShrinkFactor;
            playerState.Size = SMALL;
            AdjustGravityForSize();
            Debug.Log("Player has shrunk.");
        }
    }

    public void Grow() {
        if (playerState.IsSmall()) {
            transform.localScale *= GrowFactor;
            playerState.Size = BIG;
            AdjustGravityForSize();
            Debug.Log("Player has grown.");
        }
    }

    private void AdjustGravityForSize() {
        if (playerState.GravityMode == ANTIGRAVITY) {
            rb.gravityScale = playerState.IsSmall() ? AntiGravityScale : NormalGravityScale;
            rb.velocity = new Vector2(rb.velocity.x, playerState.IsSmall() ? 5f : 0f);
        }
    }

    public void CollectAbility(GameObject go) {
        ++playerState.NumAbilities;
        playerState.PickAbility(go);
    }

    public void CollectStar() {
        ++playerState.NumStars;
    }

    private void ActivateCheckpoint(Checkpoint checkpoint) {
        if (!checkpoint.IsActivated) {
            checkpoint.Activate();
            SaveCheckpoint();
        }
    }

    private void SaveCheckpoint() {
        LevelManager.Instance.LevelState.SaveCheckpoint(playerState, transform.position);
    }

    private void SaveStartCheckpoint() {
        LevelManager.Instance.LevelState.SaveStartCheckpoint(playerState, transform.position);
    }

    private void Respawn() {
        var checkpoint = LevelManager.Instance.LevelState.GetLastCheckPoint();
        LevelManager.Instance.LevelState.Rollback(checkpoint);
        playerState.Rollback(checkpoint);
        rb.velocity = Vector2.zero;
        transform.position = checkpoint.PlayerPosition;
        Debug.Log("🔁 Player Respawned at checkpoint!");
    }

    private bool IsGrounded(Vector2 dir) {
        var origin = (Vector2)transform.position + dir * 0.36f;
        return Physics2D.Raycast(origin, dir, 0.02f, groundLayer);
    }

    private Rigidbody2D InitializeRigidbody() {
        var body = GetComponent<Rigidbody2D>();
        body.constraints = RigidbodyConstraints2D.FreezeRotation;
        return body;
    }
}
