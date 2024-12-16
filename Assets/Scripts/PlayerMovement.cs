using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Movement speed
    public float moveSpeed = 5f;

    // Jump settings
    public float jumpForce = 10f;
    public float groundCheckThreshold = 0.1f;

    // Dash settings
    public float dashSpeed = 20f; // How fast the dash should be
    public float dashDuration = 0.2f; // How long the dash lasts
    private float dashTime = 0f; // Time left for the dash
    private bool isDashing = false; // To prevent overlapping dash input
    private bool airDashUsed = false; // To track if air dash has been used

    // Respawn point settings
    public Transform respawnPoint; // The transform representing the respawn point
    public float fallThreshold = -10f; // The y position at which the player will respawn if they fall

    private Rigidbody2D rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Check if the vertical velocity is close to zero to determine if grounded
        isGrounded = Mathf.Abs(rb.velocity.y) < groundCheckThreshold;

        // Handle horizontal movement
        float moveInput = Input.GetAxisRaw("Horizontal");
        if (!isDashing) // Only allow regular movement if not dashing
        {
            rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        }

        // Handle jump
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce); // Apply jump force only if grounded
        }

        // Handle dash (allow dashing both on the ground and in the air)
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isDashing) // Only allow dash if not already dashing
        {
            if (isGrounded || (!airDashUsed && !isGrounded)) // Dash if grounded or air dash is not used yet
            {
                StartDash(moveInput);
            }
        }

        // If dashing, reduce dash time
        if (isDashing)
        {
            dashTime -= Time.deltaTime;
            if (dashTime <= 0f)
            {
                isDashing = false; // End dash after duration
                if (!isGrounded) airDashUsed = true; // Lock air dash once used
            }
        }

        // If the player lands, reset the air dash
        if (isGrounded && airDashUsed)
        {
            airDashUsed = false; // Reset air dash when landing
        }

        // Check if the player has fallen below the fall threshold
        if (transform.position.y < fallThreshold)
        {
            Respawn(); // Trigger respawn
        }

        // Debugging: Log if grounded
        // Debug.Log("Is Grounded: " + isGrounded);
    }

    void StartDash(float moveInput)
    {
        // Start the dash by applying a force
        isDashing = true;
        dashTime = dashDuration;

        // Apply dash force in the direction the player is facing (right or left)
        Vector2 dashDirection = new Vector2(moveInput, 0).normalized; // Ensure normalized to avoid faster diagonal dashes
        rb.velocity = new Vector2(dashDirection.x * dashSpeed, rb.velocity.y); // Apply horizontal dash force while maintaining vertical velocity
    }

    // Respawn the player to the respawn point if they fall
    void Respawn()
    {
        // If a respawn point is set, teleport the player to the respawn point
        if (respawnPoint != null)
        {
            transform.position = respawnPoint.position; // Reset position to respawn point
        }
        else
        {
            Debug.LogWarning("No respawn point set! The player cannot respawn.");
        }
    }

    void OnDrawGizmosSelected()
    {
        // You can visualize the GroundCheck threshold in the editor if needed
        if (rb != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, 0.2f); // Optional: visualize player position
        }
    }
}
