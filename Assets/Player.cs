using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
   public float moveSpeed = 5f;
    public float jumpForce = 22f;
    public float gravity = 13f; // Adjust as needed
    [SerializeField] private float capsuleCastRadius = 0.5f;
    [SerializeField] private float castDistance = 0.1f;

    [SerializeField] private Vector2 castHeight;

    private bool isGrounded;
    private Vector3 velocity;

    public float groundCheckRadius = 0.1f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    private void Update()
    {
        // Check if the player is grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Character movement
        float horizontalInput = Input.GetAxis("Horizontal");

        // Move Player Horizontaly
        velocity.x = horizontalInput * moveSpeed;

        // Set Horizontal movement to zero if object is on moving direction
        // TODO: Use CapsuleCast instead of BoxCast, Can easily cause problems with groundCheck
        float castDistance = 0.01f;
        if (Physics2D.BoxCast(transform.position, new Vector2(1f, 1.8f), 0, (Vector2.right * velocity.x).normalized, castDistance, groundLayer)) 
        {   
            // No obstacle in a way, can move
            velocity.x = 0;
        }

        // Apply gravity if on ground otherwise just set y velo to 0
        if (!isGrounded)
            velocity.y -= gravity * Time.deltaTime;
        else if (velocity.y <= 0) velocity.y = 0;
        // Move the character based on the velocity
        transform.Translate(velocity * Time.deltaTime);

        // Check for jumping
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            velocity.y = jumpForce;
        }
    }

    // OnDrawGizmos can be used if you want the gizmos to be drawn even when the script is not selected
    private void OnDrawGizmos()
    {
        Vector2 groundCheckPosition = groundCheck != null ? groundCheck.position : transform.position;
        Gizmos.DrawWireSphere(groundCheckPosition, groundCheckRadius);

    }
}
