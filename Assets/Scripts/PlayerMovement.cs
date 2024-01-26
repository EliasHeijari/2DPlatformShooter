using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform groundCheckTransform;

    [Header("Movement Settings")]
    [SerializeField] private float speed = 130f;
    [SerializeField] private float jumpForce = 8f;

    [Header("Ground Check")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckRadius = 0.1f;
    bool isGrounded;
    Rigidbody2D rigidBody;
    public Vector2 velocity { get; private set; }
   
    private void Awake() {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Use In FixedUpdate, Get Inputs from Game Input script and uses rigidbody to move this gameobject
    /// </summary>
    public void HandleMovement(){
        float horizontal = GameInput.HorizontalInput();

        // Ground Check
        isGrounded = Physics2D.OverlapCircle(groundCheckTransform.position, groundCheckRadius, groundLayer);
            
        // horizontal input to velocity
        velocity = new Vector2(horizontal * speed * Time.fixedDeltaTime, rigidBody.velocity.y);

        // Jump velocity to player
        if (GameInput.JumpPressed() && isGrounded)
        {
            velocity = new Vector2(velocity.x, velocity.y + jumpForce);
        }

        // Add Force to the rigidbody
        rigidBody.velocity = velocity;
    }
}
