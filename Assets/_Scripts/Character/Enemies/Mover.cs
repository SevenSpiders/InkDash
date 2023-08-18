using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public float moveDistance = 2f;  // Distance to move up and down
    public float moveSpeed = 2f;     // Speed of the movement

    private Vector2 initialPosition;
    private float direction = 1f;

    private void Start()
    {
        initialPosition = transform.position;
    }

    private void Update()
    {
        // Calculate the new vertical position
        float newY = initialPosition.y + direction * moveDistance * Mathf.Sin(Time.time * moveSpeed);

        // Update the position of the obstacle
        transform.position = new Vector2(transform.position.x, newY);
    }
}
