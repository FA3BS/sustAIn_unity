using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // Respawn point settings
    public Transform respawnPoint; // The transform representing the respawn point

    public int maxHealth = 3;
    private int currentHealth;

    void Start()
    {
        // Set initial health to maxHealth
        currentHealth = maxHealth;
        Debug.Log("Player Health: " + currentHealth);
    }

    public void TakeDamage(int damage)
    {
        // Decrease health by damage amount
        currentHealth -= damage;
        Debug.Log("Player Health: " + currentHealth);

        // Check if the player has fallen below the fall threshold
        if (currentHealth <= 0)
        {
            Respawn(); // Trigger respawn
        }
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
        currentHealth = maxHealth;
        Debug.Log("Player Respawned. Health reset to: " + currentHealth);
    }
}