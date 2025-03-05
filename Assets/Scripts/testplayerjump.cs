using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testplayerjump : MonoBehaviour
{
    public float speed = 5f;
    public Transform groundCheck;
    public LayerMask groundLayer;


    private Rigidbody2D rb;
    private Vector2 movement;
    [SerializeField] private int jumpPower = 5;
    private bool isGrounded;
    [SerializeField] private float groundCheckRadius = 0.35f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        //float verticalInput = Input.GetAxisRaw("Vertical");
        bool jumpInput = Input.GetButtonDown("Jump");
        //isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        isGrounded = Physics2D.Raycast(transform.position + Vector3.down * 0.5f, Vector2.down, 0.2f, groundLayer);



        // Set movement direction based on input
        movement = new Vector2(horizontalInput, 0);

        if (jumpInput && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
        }

    }

    void FixedUpdate()
    {
        // Apply movement to the player in FixedUpdate for physics consistency
        rb.velocity = new Vector2(movement.x * speed, rb.velocity.y);
    }
}
