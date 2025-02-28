//Jump function tutorial: https://www.youtube.com/watch?v=XhwRYNie-aI
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public Transform groundCheck;
    public LayerMask groundLayer;


    private Rigidbody2D rb;
    private Vector2 movement;
    [SerializeField] private int jumpPower = 5;
    private bool isGrounded;
    [SerializeField] private float groundCheckRadius = 0.4f;

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
        //isGrounded = Physics2D.OverlapCapsule(GroundCheck.position, new Vector2(0.39f, 0.37f), CapsuleDirection2D.Horizontal, 0, groundLayer);
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

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
