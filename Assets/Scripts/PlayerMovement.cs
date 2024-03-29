using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{ 
    [BoxGroup("Movement")]
    [SerializeField] private float speed = 130f;
    [BoxGroup("Movement")]
    [SerializeField] private float jumpForce = 8f;

    [BoxGroup("Ground Check")]
    [SerializeField] private LayerMask groundLayer;
    [BoxGroup("Ground Check")]
    [SerializeField] private float groundCheckRadius = 0.1f;
    [BoxGroup("Ground Check")]
    [Required("Ground Check Transform From Player Bottom")]
    [SerializeField] private Transform groundCheckTransform;
    public static bool isGrounded { get; private set;}
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

        // Jump to velocity.y
        if (GameInput.JumpPressed() && isGrounded)
        {
            velocity = new Vector2(velocity.x, velocity.y + jumpForce);
        }

        // Update velocity, velocity depending on player inputs
        rigidBody.velocity = velocity;

        // Change Player Rotation, look left or right depending on the direction of movement (Velocity)
        switch(velocity.x){
            case > 0:
                // Player Looks Right
                transform.rotation = Quaternion.Euler(transform.rotation.x, 0f, transform.rotation.z);                
                break;
            case < 0:
                // Player Looks Left
                transform.rotation = Quaternion.Euler(transform.rotation.x, -180f, transform.rotation.z);
                break;
        }
    }
}
