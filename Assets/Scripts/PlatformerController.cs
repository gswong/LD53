using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float fallMultiplier = 2.5f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float groundCheckRadius = 0.2f;
    private float lastInputDirection;

    private Rigidbody2D rb;
    private float moveInput;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // better game gravity to feel more like mario game
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        // Check if the character is on the ground
        //isGrounded = Physics.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        
        // Get horizontal input for movement
        moveInput = Input.GetAxis("Horizontal");

        // Apply horizontal velocity to the Rigidbody
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Check if the player pressed the Jump button and is on the ground
        if (Input.GetButtonDown("Jump"))
        {
            rb.velocity = Vector2.up * jumpForce;
        }

        if (Mathf.Abs(moveInput) > 0.01f)
        {
            lastInputDirection = moveInput;
            FlipSprite();
        }
    }

    private void FlipSprite()
    {
        Debug.Log("FlipSprite");
        Vector3 localScale = transform.localScale;

        // If moving right, make sure the x scale is positive
        if (lastInputDirection > 0)
        {
            localScale.x = -Mathf.Abs(localScale.x);
        }
        // If moving left, make sure the x scale is negative
        else if (lastInputDirection < 0)
        {
            localScale.x = Mathf.Abs(localScale.x);
        }

        transform.localScale = localScale;
    }
}