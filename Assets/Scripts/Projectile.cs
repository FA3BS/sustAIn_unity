using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Separate script for the projectile
public class Projectile : MonoBehaviour
{
    public float speed = 5f; // Geschwindigkeit des Projektils
    public int damage = 1;

    private Vector3 moveDirection;

    void Start()
    {
        // Finde den Player (stellen wir sicher, dass der Tag 'Player' für den Charakter gesetzt ist)
        GameObject player = GameObject.FindWithTag("Player");

        if (player != null)
        {
            // Berechne die Richtung vom Projektil zum Spieler
            Vector3 directionToPlayer = player.transform.position - transform.position;

            // Normalisiere den Vektor, um eine konstante Geschwindigkeit zu gewährleisten
            moveDirection = directionToPlayer.normalized;

            // Flippe den Sprite basierend auf der Position des Projektils relativ zum Spieler
            if (transform.position.x > player.transform.position.x)
            {
                // Projektil ist rechts vom Spieler, also flippe es
                transform.localScale = new Vector3(-1, 1, 1); // Flippe die X-Achse
            }
            else
            {
                // Projektil ist links vom Spieler, kein Flippen nötig
                transform.localScale = new Vector3(1, 1, 1); // Setze die X-Achse zurück
            }
        }
        else
        {
            // Wenn kein Spieler gefunden wird, Projektil in eine Standardrichtung schießen
            moveDirection = new Vector3(1, 0, 0).normalized; // Beispiel: nach rechts
        }
    }

    private void Update()
    {
        // Bewege das Projektil basierend auf der berechneten Richtung
        transform.position += moveDirection * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }

            // Zerstöre das Projektil nach dem Treffen des Spielers
            Destroy(gameObject);
        }

        // Optional: Projektil zerstören, wenn es auf Hindernisse trifft
        if (collision.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}
