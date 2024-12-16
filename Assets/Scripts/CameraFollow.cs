using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;           // Reference to the player
    public float smoothSpeed = 0.125f; // Smoothing speed for camera movement
    public Vector3 offset;            // Offset to adjust camera's position relative to the player

    void Start()
    {
        // Make sure the camera is offset from the player initially
        if (player != null)
        {
            offset = transform.position - player.position;
        }
    }

    void LateUpdate()
    {
        // Follow the player's position with smoothing
        Vector3 desiredPosition = player.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, transform.position.z); // Keep z the same (2D)

        // Optionally, you can also make sure the camera stays within certain bounds or limit its movement on the z-axis if it's a 2D game.
    }
}
