//Jump function tutorial: https://www.youtube.com/watch?v=XhwRYNie-aI
//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

// public class PlayerController : MonoBehaviour
// {
//     public float speed = 5f;
//     public Transform groundCheck;
//     public LayerMask groundLayer;


//     private Rigidbody2D rb;
//     private Vector2 movement;
//     [SerializeField] private int jumpPower = 5;
//     private bool isGrounded;
//     [SerializeField] private float groundCheckRadius = 0.4f;

//     // Start is called before the first frame update
//     void Start()
//     {
//         rb = GetComponent<Rigidbody2D>();

//         rb.constraints = RigidbodyConstraints2D.FreezeRotation;
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         float horizontalInput = Input.GetAxisRaw("Horizontal");
//         //float verticalInput = Input.GetAxisRaw("Vertical");
//         bool jumpInput = Input.GetButtonDown("Jump");
//         //isGrounded = Physics2D.OverlapCapsule(GroundCheck.position, new Vector2(0.39f, 0.37f), CapsuleDirection2D.Horizontal, 0, groundLayer);
//         isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

//         // Set movement direction based on input
//         movement = new Vector2(horizontalInput, 0);

//         if (jumpInput && isGrounded)
//         {
//             rb.velocity = new Vector2(rb.velocity.x, jumpPower);
//         }

//     }

//     void FixedUpdate()
//     {
//         // Apply movement to the player in FixedUpdate for physics consistency
//         rb.velocity = new Vector2(movement.x * speed, rb.velocity.y);
//     }


// }
// using UnityEngine;

// public class PlayerController : MonoBehaviour
// {
//     public float speed = 5f;
//     public Transform groundCheck;
//     public LayerMask groundLayer;
//     public LayerMask ceilingLayer; // 用于检测天花板

//     private Rigidbody2D rb;
//     private Vector2 movement;
//     [SerializeField] private int jumpPower = 5;
//     private bool isGrounded;
//     private bool isCeiling; // 是否在天花板上（反重力地面）
//     private bool isInAntiGravity = false; // 是否处于反重力区域
//     private bool isFalling = false; // 记录玩家是否正在下落
//     [SerializeField] private float groundCheckRadius = 0.8f;

//     void Start()
//     {
//         rb = GetComponent<Rigidbody2D>();
//         rb.constraints = RigidbodyConstraints2D.FreezeRotation;
//     }

//     void Update()
//     {
//         Debug.Log("isCeiling: " + isCeiling);
//         float horizontalInput = Input.GetAxisRaw("Horizontal");
//         bool jumpInput = Input.GetButtonDown("Jump");

//         // **检测玩家是否正在下落**
//         isFalling = rb.velocity.y < 0;

//         // **普通重力状态：检测地面**
//         if (!isInAntiGravity)
//         {
//             isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
//             isCeiling = false; // 普通模式不检测天花板
//         }
//         // **反重力状态：检测天花板作为“地面”**
//         else
//         {
//             isGrounded = false; // 反重力时，不再检测地面
//             isCeiling = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, ceilingLayer);
//         }

//         // **玩家水平移动**
//         movement = new Vector2(horizontalInput, 0);

//         // **玩家跳跃**
//         if (jumpInput)
//         {
//             if (!isInAntiGravity && isGrounded) // **普通状态：正常跳跃**
//             {
//                 rb.velocity = new Vector2(rb.velocity.x, jumpPower);
//             }
//             else if (isInAntiGravity && isCeiling) // **反重力状态：向下跳跃**
//             {
//                 rb.velocity = new Vector2(rb.velocity.x, -jumpPower);
//             }
//         }
//     }

//     void FixedUpdate()
//     {
//         // **玩家移动**
//         rb.velocity = new Vector2(movement.x * speed, rb.velocity.y);
//     }

//     // **进入反重力区域**
//     private void OnTriggerEnter2D(Collider2D other)
//     {
//         if (other.CompareTag("AntiGravityZone"))
//         {
//             isInAntiGravity = true;

//             // **确保玩家是下落状态时才切换重力**
//             if (isFalling)
//             {
//                 rb.gravityScale = -1f; // **重力反转**
//                 FlipGroundCheck(); // **翻转 `groundCheck` 位置**
//             }
//         }
//     }

//     // **离开反重力区域**
//     private void OnTriggerExit2D(Collider2D other)
//     {
//         if (other.CompareTag("AntiGravityZone"))
//         {
//             isInAntiGravity = false;
//             rb.gravityScale = 1f; // **恢复正常重力**
//             FlipGroundCheck(); // **恢复 `groundCheck` 位置**
//         }
//     }

//     // **翻转 `groundCheck` 位置**
//     private void FlipGroundCheck()
//     {
//         if (groundCheck == null)
//         {
//             Debug.LogError("groundCheck 未赋值！");
//             return;
//         }

//         // **确保 groundCheck 在 `Player` 脚下或头顶**
//         float newY = -groundCheck.localPosition.y; 
//         groundCheck.localPosition = new Vector3(groundCheck.localPosition.x, newY, groundCheck.localPosition.z);
        
//         Debug.Log("groundCheck 翻转，当前 Y 位置: " + groundCheck.localPosition.y);
//     }

// }
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public LayerMask ceilingLayer;

    private Rigidbody2D rb;
    private Vector2 movement;
    [SerializeField] private int jumpPower = 5;
    private bool isGrounded;
    private bool isCeiling;
    private bool isInAntiGravity = false;
    private bool isSmall = false; 
    private bool isFalling = false;
    [SerializeField] private float groundCheckRadius = 0.8f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    void Update()
    {
        Debug.Log("isCeiling: " + isCeiling + ", isSmall: " + isSmall);
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        bool jumpInput = Input.GetButtonDown("Jump");

        isFalling = rb.velocity.y < 0;

        if (!isInAntiGravity)
        {
            //This method causes the player jump twice when the player closes to obstacles
            //isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
            //Use ray to detect ground vertically under the player
            isGrounded = Physics2D.Raycast(transform.position + Vector3.down * 0.4f, Vector2.down, 0.1f, groundLayer);
            isCeiling = false;
        }
        else
        {
            isGrounded = false;
            isCeiling = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, ceilingLayer);
        }

        movement = new Vector2(horizontalInput, 0);

        if (jumpInput)
        {
            if (!isInAntiGravity && isGrounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            }
            else if (isInAntiGravity && isCeiling && isSmall)
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
        Debug.Log("groundCheck flips，current Y is: " + groundCheck.localPosition.y);
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

}
