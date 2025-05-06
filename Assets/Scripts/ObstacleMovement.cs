using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private bool moveRightToLeft = true;
    [SerializeField] private float boundaryWidth = 25f; // Distance beyond which obstacles are repositioned

    [Header("Audio")]
    [SerializeField] private AudioSource movementSound;

    private Vector3 movementDirection;
    private float startX;
    private bool initialized = false;

    private void Start()
    {
        if (!initialized)
        {
            Initialize(moveRightToLeft, speed);
        }

        // Play sound if assigned (useful for train passing)
        if (movementSound != null && gameObject.CompareTag("Train"))
        {
            movementSound.Play();
        }
    }

    public void Initialize(bool rightToLeft, float moveSpeed)
    {
        moveRightToLeft = rightToLeft;
        speed = moveSpeed;
        movementDirection = moveRightToLeft ? Vector3.right : Vector3.left;
        startX = transform.position.x;
        initialized = true;
    }

    private void Update()
    {
        // Move the obstacle
        transform.Translate(movementDirection * speed * Time.deltaTime);

        // Check if obstacle has gone beyond boundaries and reposition
        float currentX = transform.position.x;

        if (moveRightToLeft && currentX > boundaryWidth)
        {
            // If moving right to left and gone too far right, reposition to the left
            Vector3 newPosition = transform.position;
            newPosition.x = -boundaryWidth;
            transform.position = newPosition;

            // Play sound again for trains
            if (movementSound != null && gameObject.CompareTag("Train"))
            {
                movementSound.Play();
            }
        }
        else if (!moveRightToLeft && currentX < -boundaryWidth)
        {
            // If moving left to right and gone too far left, reposition to the right
            Vector3 newPosition = transform.position;
            newPosition.x = boundaryWidth;
            transform.position = newPosition;

            // Play sound again for trains
            if (movementSound != null && gameObject.CompareTag("Train"))
            {
                movementSound.Play();
            }
        }
    }

    // Method for HardMode to increase speed over time
    public void IncreaseSpeed(float amount)
    {
        speed += amount;
    }
}