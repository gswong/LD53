using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 8f;
    public float fallMultiplier = 4;

    public Transform groundCheck;
    public LayerMask groundLayer;
    public float groundCheckRadius = 0.1f;
    private float lastInputDirection;

    private Animator animator;

    private Rigidbody2D rb;
    private float moveInput;
    private bool isGrounded;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        animator.SetBool("IsGrounded", isGrounded);

        // Check if the player pressed the Jump button and is on the ground
        // Initiate take off animation
        if (isGrounded == true && Input.GetButtonDown("Jump"))
        {
            animator.SetTrigger("TakeOff");
            rb.velocity = Vector2.up * jumpForce;
        }

        // Play jump animation while player is in the air
        if (isGrounded == true)
        {
            animator.SetBool("IsJumping", false);
        }
        else
        {
            animator.SetBool("IsJumping", true);
        }

        float moveInput = Input.GetAxis("Horizontal");

        // Set the walking animation when the player is moving horizontally
        if (Mathf.Abs(moveInput) > 0.01f)
        {
            animator.SetBool("IsWalking", true);

            lastInputDirection = moveInput;
            FlipSprite();
        } 
        else 
        {
            animator.SetBool("IsWalking", false);
        }

        // better game gravity to feel more like mario game
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }

        // Apply horizontal velocity to the Rigidbody
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
    }

    private void FlipSprite()
    {
        Debug.Log("FlipSprite");
        Vector3 localScale = transform.localScale;

        // If moving right, make sure the x scale is positive
        if (lastInputDirection < 0)
        {
            localScale.x = -Mathf.Abs(localScale.x);
        }
        // If moving left, make sure the x scale is negative
        else if (lastInputDirection > 0)
        {
            localScale.x = Mathf.Abs(localScale.x);
        }

        transform.localScale = localScale;
    }
}