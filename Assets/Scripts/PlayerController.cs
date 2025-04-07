using Unity.VisualScripting;
using UnityEngine;
using static Constants;
using static GravityMode;
using static Size;

public class PlayerController : MonoBehaviour {
    
    private Rigidbody2D rb;
    private Vector2 movement;
    private GameState gameState;
    private PlayerState playerState;

    public GameObject abilityPrefab;
    public LayerMask groundLayer;
    public LayerMask ceilingLayer;

    void Start() {
        gameState = InitGameState();
        playerState = InitPlayerState();
        rb = InitRigidbody();
    }

    void Update() {
        
        if (Input.GetKeyDown(KeyCode.E)) {
            DropAbility();
        }
        
        var verticalInput = Input.GetButtonDown("Jump");
        var horizontalInput = Input.GetAxisRaw("Horizontal");

        if (playerState.GravityMode == NORMAL) {
            playerState.IsGrounded = Physics2D.Raycast(transform.position + Vector3.down * 0.4f, Vector2.down, 0.1f, groundLayer);
        } else {
            playerState.IsGrounded = Physics2D.Raycast(transform.position + Vector3.up * 0.4f, Vector2.up, 0.1f, ceilingLayer); 
        }

        movement = new Vector2(horizontalInput, 0);

        if (verticalInput) {
            if (playerState.GravityMode == NORMAL && playerState.IsGrounded) {
                rb.velocity = new Vector2(rb.velocity.x, PlayerJumpVerticalVelocity);
            } else if (playerState.GravityMode == ANTIGRAVITY && playerState.IsGrounded && playerState.IsSmall()) {
                rb.velocity = new Vector2(rb.velocity.x, -PlayerJumpVerticalVelocity);
            }
        }
    }

    void FixedUpdate() {
        rb.velocity = new Vector2(movement.x * PlayerHorizontalVelocity, rb.velocity.y);
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.TryGetComponent<ICollectible>(out var collectible)) {
            collectible.Collect(this);
        } else if (other.CompareTag("AntiGravityZone")) {
            EnterAntiGravityZone();
        } else if (other.CompareTag("Destination")) {
            ReachDestination();
        } else if (other.CompareTag("Checkpoint")) {
            // SaveCheckpoint();
        } else if (other.CompareTag("Portal")) {
            EnterPortal(other.GetComponent<Portal>());
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.CompareTag("Spike")) {
            // Invoke(nameof(Respawn), RespawnDelay); 
        }
    }
    
    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("AntiGravityZone")) {
            ExitAntiGravityZone();
        } else if (other.CompareTag("Portal")) {
            ExitPortal();
        }
    }

    // Refactor drop ability, essentially keep the list of ability objects
    // and maintain that list dynamically, create a new ability object only when
    // the list is empty so we don't create a new instance every time, that's too expensive
    private void DropAbility() {
        if (playerState.NumAbilities == 0) return;
        Debug.Log("Place ability.");

        var newAbility = Instantiate(abilityPrefab, transform.position + transform.right * AbilityInstantiateDistance, Quaternion.identity);
        
        newAbility.GetComponent<Rigidbody2D>().gravityScale = rb.gravityScale;
        --playerState.NumAbilities;
    }
    
    public void ResetGravity() {
        // Restore gravity 
        playerState.GravityMode = NORMAL;
        rb.gravityScale = NormalGravityScale; 
        
        Debug.Log("ResetGravity() is using. Gravity is normal！");
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
        
        Destroy(gameObject);

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
    
    // private void SaveCheckpoint() {
    //     Debug.Log("🟢 Checkpoint Activated at: " + transform.position);
    //
    //     var offsetPosition = transform.position + new Vector3(0f, 0f, 0f);  // record position, a littl offset can be added to avoid bugs
    //     CheckpointManager.Instance.SetCheckpoint(offsetPosition);
    //
    //     var playerRb = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();  // record gravity status
    //     CheckpointManager.Instance.SetGravityScale(playerRb.gravityScale);
    //
    //     var playerLocalScale = GameObject.FindWithTag("Player").transform.localScale;
    //     CheckpointManager.Instance.SetLastLocalScale(transform.localScale); // record size status
    // }
    
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

    public void CollectAbility() {
        ++playerState.NumAbilities;
    }

    public void CollectStar() {
        ++playerState.NumStars;
    }
    
    // public void Respawn() {
    //     gameState.RollBack();
    //     playerState.RollBack();
    //     if (CheckpointManager.Instance != null) {
    //         transform.position = CheckpointManager.Instance.GetLastCheckpoint(); // Respawn from checkpoint
    //
    //         transform.localScale = CheckpointManager.Instance.GetLastLocalScale(); // get last size
    //         // If not shrinking
    //         if(transform.localScale.x > 0.5f) {
    //             var controller = GetComponent<PlayerSizeControll2D>();
    //             controller.hasShrunk = false;
    //         }
    //         
    //         if(CheckpointManager.Instance.GetLastGravityScale() == 1f) {
    //             ResetGravity();
    //             playerState.Size = false;
    //         }
    //         
    //         Debug.Log("Respawn: Get last gravity scale: " + CheckpointManager.Instance.GetLastGravityScale());
    //         
    //         rb.velocity = Vector2.zero;
    //     }
    //     Debug.Log("🔁 Player Respawned at checkpoint!");
    // }

    // initialization
    private GameState InitGameState() {
        return GameManager.Instance.GameState;
    }

    private PlayerState InitPlayerState() {
        return GameManager.Instance.PlayerState;
    }
    
    private Rigidbody2D InitRigidbody() {
        var rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        return rb;
    }
}
