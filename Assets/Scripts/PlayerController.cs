using UnityEngine;
using static Constants;
using static GravityMode;
using static Size;

public class PlayerController : MonoBehaviour {
    
    private Rigidbody2D rb;
    private Vector2 movement;
    private PlayerState playerState;
    public GameObject abilityPrefab;
    private LayerMask groundLayer;

    void Start() {
        playerState = InitPlayerState();
        rb = InitRigidbody();
        SaveCheckpoint();
        groundLayer = LayerMask.GetMask("Ground");
    }

    private void OnEnable() {
        InputHandler.JumpPressed += HandleJump;
        InputHandler.DropAbilityPressed += HandleDropAbility;
    }
    
    private void OnDisable() {
        InputHandler.JumpPressed -= HandleJump;
        InputHandler.DropAbilityPressed -= HandleDropAbility;
    }
    
    private void HandleJump() {
        if (!playerState.IsGrounded) return;
        Jump();
    }

    private void HandleDropAbility() {
        if (playerState.Abilities.Count == 0) return;
        DropAbility();
    }

    private void Jump() {
        if (playerState.GravityMode == NORMAL) {
            rb.velocity = new Vector2(rb.velocity.x, PlayerJumpVerticalVelocity);
        } else if (playerState.GravityMode == ANTIGRAVITY && playerState.IsSmall()) {
            rb.velocity = new Vector2(rb.velocity.x, -PlayerJumpVerticalVelocity);
        }
    }
    
    // Refactor drop ability, essentially keep the list of ability objects
    // and maintain that list dynamically, create a new ability object only when
    // the list is empty so we don't create a new instance every time, that's too expensive
    private void DropAbility() {
        Debug.Log("Place ability.");

        var abilityEntry = playerState.Abilities.Pop();
        var ability = abilityEntry.GameObject;
        var abilityRb = abilityEntry.Rb;
        ability.transform.position = transform.position + transform.right * AbilityInstantiateDistance;
        abilityRb.gravityScale = rb.gravityScale;
        ability.SetActive(true);
        
        // var newAbility = Instantiate(abilityPrefab, transform.position + transform.right * AbilityInstantiateDistance, Quaternion.identity);
        // newAbility.GetComponent<Rigidbody2D>().gravityScale = rb.gravityScale;
        --playerState.NumAbilities;
    }
    
    void Update() {
        playerState.IsGrounded = GroundCheck(playerState.GravityMode == NORMAL ? Vector2.down : Vector2.up);
    }

    void FixedUpdate() {
        rb.velocity = new Vector2(InputHandler.instance.Horizontal * PlayerHorizontalVelocity, rb.velocity.y);
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.TryGetComponent<ICollectible>(out var collectible)) {
            collectible.Collect(this);
        } else if (other.CompareTag("AntiGravityZone")) {
            EnterAntiGravityZone();
        } else if (other.CompareTag("Destination")) {
            ReachDestination();
        } else if (other.CompareTag("Checkpoint")) {
            SaveCheckpoint();
        } else if (other.CompareTag("Portal")) {
            EnterPortal(other.GetComponent<Portal>());
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.CompareTag("Spike")) {
            Respawn();
        }
    }
    
    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("AntiGravityZone")) {
            ExitAntiGravityZone();
        } else if (other.CompareTag("Portal")) {
            ExitPortal();
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
        
        if (playerState.IsBig()) return;
        
        rb.gravityScale = NormalGravityScale;
        rb.velocity = new Vector2(rb.velocity.x, 0f);
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
        
        // Destroy(gameObject);

        // Time.timeScale = 0f;
        // endUI.SetActive(true);
        // grayOut.SetActive(true);
        // if (starTracking.starCount > 0) {
        //     leftStar.SetActive(true);
        // }
        // if (starTracking.starCount > 1) {
        //     midStar.SetActive(true);
        // }
        // if (starTracking.starCount > 2) {
        //     rightStar.SetActive(true);
        // }
        // menuUI.SetActive(true);
        // settingsButton.SetActive(false);
    }
    
    public void Shrink() {
        if (playerState.IsBig()) {
            transform.localScale *= ShrinkFactor;
            playerState.Size = SMALL;
            if (playerState.GravityMode == ANTIGRAVITY) {
                rb.gravityScale = AntiGravityScale;
                rb.velocity = new Vector2(rb.velocity.x, 5f); 
            }
        }
        Debug.Log("Player has shrunk.");
    }

    public void Grow() {
        if (playerState.IsSmall()) {
            transform.localScale *= GrowFactor;
            playerState.Size = BIG;
            if (playerState.GravityMode == ANTIGRAVITY) {
                rb.gravityScale = NormalGravityScale;
                rb.velocity = new Vector2(rb.velocity.x, 0f); 
            }
        }
        Debug.Log("Player has grown.");
    }

    public void CollectAbility(GameObject go) {
        ++playerState.NumAbilities;
        playerState.PickAbility(go);
    }

    public void CollectStar() {
        ++playerState.NumStars;
    }

    private void SaveCheckpoint() {
        LevelManager.Instance.LevelState.SaveCheckpoint(playerState, transform.position);
    }
    
    private void Respawn() {
        var checkpoint = LevelManager.Instance.LevelState.GetLastCheckPoint();
        LevelManager.Instance.LevelState.Rollback(checkpoint);
        playerState.Rollback(checkpoint);
        rb.velocity = Vector2.zero;
        transform.position = checkpoint.PlayerPosition;
        Debug.Log("🔁 Player Respawned at checkpoint!");
    }

    private bool IsCollectible(Collider2D other) {
        return other.TryGetComponent<ICollectible>(out _);
    }
    
    private bool GroundCheck(Vector2 dir) {
        var origin = (Vector2)transform.position + dir * 0.36f;
        return Physics2D.Raycast(origin, dir, 0.02f, groundLayer);
    }
    
    /// initialization //

    private PlayerState InitPlayerState() {
        return LevelManager.Instance.PlayerState;
    }
    
    private Rigidbody2D InitRigidbody() {
        var rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        return rb;
    }
}
