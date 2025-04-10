//Jump function tutorial: https://www.youtube.com/watch?v=XhwRYNie-aI
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public LayerMask ceilingLayer;

    private Rigidbody2D rb;
    private Vector2 movement;
    [SerializeField] private float jumpPower = 6.5f;
    [SerializeField] float fallMultiplier;
    Vector2 VecGravity;
    private bool isGrounded;
    private bool isCeiling;
    private bool isInAntiGravity = false;
    private bool isSmall = false; 
    private bool isFalling = false;
    //[SerializeField] private float groundCheckRadius = 0.8f;

    private List<GameObject> shrinkTriangles = new List<GameObject>();
    private List<GameObject> growTriangles = new List<GameObject>();

    public AnalyticsManager db;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        VecGravity = new Vector2(0, -Physics2D.gravity.y);

        // record all size changing objects for reload when respawn from checkpoint
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("ShrinkTriangle")) 
        {
            shrinkTriangles.Add(obj);
        }

        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("GrowTriangle"))
        {
            growTriangles.Add(obj);
        }

        CheckpointManager.Instance.CheckpointInitialization();

    }

    void Update()
    {
        // Debug.Log("isCeiling: " + isCeiling + ", isSmall: " + isSmall);
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        bool jumpInput = Input.GetButtonDown("Jump");

        isFalling = rb.velocity.y < 0;


        if (!isInAntiGravity)
        {
            //This method causes the player jump twice when the player closes to obstacles
            //isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
            //Use ray to detect ground vertically under the player
            //isGrounded = Physics2D.Raycast(transform.position + Vector3.down * 0.4f, Vector2.down, 0.1f, groundLayer);
            isGrounded = IsGrounded();
            isCeiling = false;
            if (rb.velocity.y < 0)
            {
                rb.velocity -= VecGravity * fallMultiplier * Time.deltaTime;
            }
        }
        else
        {
            isGrounded = false;
            //isCeiling = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, ceilingLayer);
            isCeiling = Physics2D.Raycast(transform.position + Vector3.up * 0.4f, Vector2.up, 0.1f, groundLayer); 
        }

        movement = new Vector2(horizontalInput, 0);

        if (jumpInput)
        {
            if (!isInAntiGravity && isGrounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            }
            else if (isInAntiGravity && isCeiling && isSmall)
            //else if (isInAntiGravity && isSmall)
            {
                rb.velocity = new Vector2(rb.velocity.x, -jumpPower);
            }
        }
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(movement.x * speed, rb.velocity.y);
    }

    // Enter anti-gravity zone
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("AntiGravityZone"))
        {
            if (isSmall) 
            {
                isInAntiGravity = true;
                rb.gravityScale = -1f;
                FlipGroundCheck();
                rb.velocity = new Vector2(rb.velocity.x, 5f); // be pulled up
                Debug.Log("The player has shrinked. Enter anti-gravity zone.");
            }
            else
            {
                Debug.Log("The player is too big to enter anti-gravity zone.");
            }
        }
    }



    // Leave anti-gravity zone
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("AntiGravityZone") && isSmall)
        {
            isInAntiGravity = false;
            rb.gravityScale = 1f;
            FlipGroundCheck();
        }
    }

    // flip groundCheck's position
    private void FlipGroundCheck()
    {
        if (groundCheck == null)
        {
            Debug.LogError("groundCheck is None！");
            return;
        }

        float newY = -groundCheck.localPosition.y; 
        groundCheck.localPosition = new Vector3(groundCheck.localPosition.x, newY, groundCheck.localPosition.z);
        //Debug.Log("groundCheck flips，current Y is: " + groundCheck.localPosition.y);
    }

    public void SetSmallState(bool small)
    {
        isSmall = small;
    }

    public bool IsSmall()
    {
        return isSmall;
    }
    public void ResetGravity()
    {
        isInAntiGravity = false;
        rb.gravityScale = 1f; // Restore gravity 
        FlipGroundCheck(); // Restore groundCheck 
        Debug.Log("ResetGravity() is using. Gravity is normal！");
    }
    public void Respawn()
    {
        if (CheckpointManager.Instance != null)
        {
            db.AddHeatmapData(transform.position.x, transform.position.y, SceneManager.GetActiveScene().name, "Checkpoint Restart");
            transform.position = CheckpointManager.Instance.GetLastCheckpoint(); // respawn from checkpoint

            // Abilty on the map and platforms recover
            CheckpointManager.Instance.RestoreAbilityStates(); // restore the consumed abilities
            CheckpointManager.Instance.RecoverChangedPlatforms(); // destroy all the platforms changed and se the previous platforms active

            // Set last ability num
            PickupAndPlace pickScript = GetComponent<PickupAndPlace>(); // get the pickup and place script
            int count = CheckpointManager.Instance.GetAbilityCount();
            pickScript.abilityNum = count; // set the num of ability to use
            PowerupStarCollisionTracking powerupCollisionScript = GetComponent<PowerupStarCollisionTracking>(); // update UI
            powerupCollisionScript.SetConsumCountAndUpdateUI(count);


            transform.localScale = CheckpointManager.Instance.GetLastLocalScale(); // get last size
            if(transform.localScale.x > 0.5f)  // if not shrinking
            {
                PlayerSizeControll2D controller = GetComponent<PlayerSizeControll2D>();
                controller.hasShrunk = false;
            }
            

            if(CheckpointManager.Instance.GetLastGravityScale() == 1f)
            {
                ResetGravity();
                SetSmallState(false);
            }
            Debug.Log("Respawn: Get last gravity scale: " + CheckpointManager.Instance.GetLastGravityScale());
            //Rigidbody2D rb = GetComponent<Rigidbody2D>();
            //rb.gravityScale = CheckpointManager.Instance.GetLastGravityScale(); 


            // reload all size changing items
            foreach (GameObject obj in shrinkTriangles)
            {
                obj.SetActive(true);
                Debug.Log("Respawn: Reloaded Shrink Triangle");
            }
            foreach (GameObject obj in growTriangles)
            {
                obj.SetActive(true);
            }


            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            Debug.Log("🔁 Player Respawned at checkpoint!");
        }
    }

    private bool IsGrounded()
    {
        float rayLength = 0.1f;
        Vector3 leftRayStart = transform.position + new Vector3(-0.32f, -0.4f, 0);
        Vector3 centerRayStart = transform.position + new Vector3(0f, -0.4f, 0);
        Vector3 rightRayStart = transform.position + new Vector3(0.32f, -0.4f, 0);

        bool leftHit = Physics2D.Raycast(leftRayStart, Vector2.down, rayLength, groundLayer);
        bool centerHit = Physics2D.Raycast(centerRayStart, Vector2.down, rayLength, groundLayer);
        bool rightHit = Physics2D.Raycast(rightRayStart, Vector2.down, rayLength, groundLayer);

        return leftHit || centerHit || rightHit;
    }


}
