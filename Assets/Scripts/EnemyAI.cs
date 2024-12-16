using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 2f;
    public float moveDistance = 3f;

    [Header("Shooting Settings")]
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float shootInterval = 3f;

    private Vector3 startPosition;
    private bool movingForward = true;
    private float shootTimer;

    void Start()
    {
        startPosition = transform.position;
        shootTimer = shootInterval; // Initialize the timer
    }

    void Update()
    {
        Move();
        Shoot();
    }

    private void Move()
    {
        // Calculate the target position based on the direction
        Vector3 targetPosition = startPosition + (movingForward ? Vector3.right : Vector3.left) * moveDistance;

        // Move towards the target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Check if the enemy has reached the target position
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            movingForward = !movingForward; // Reverse direction
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z); // Flip direction
        }

        
    }

    private void Shoot()
    {
        // Decrease the timer
        shootTimer -= Time.deltaTime;

        // If timer reaches 0, shoot a projectile
        if (shootTimer <= 0f)
        {
            if (projectilePrefab != null && firePoint != null)
            {
                Instantiate(projectilePrefab, firePoint.position, transform.rotation);
            }

            shootTimer = shootInterval; // Reset the timer
        }
    }
}


