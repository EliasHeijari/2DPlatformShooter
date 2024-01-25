using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private bool DebugMode = false;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    public static PlayerMovementController Instance {get; private set;}
    
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 22f;
    [SerializeField] private static float gravity = 13f; // Adjust as needed
    [SerializeField] private float castDistance = 0.1f;
    [SerializeField] private float groundCheckRadius = 0.1f;
    public bool isGrounded { get; private set; }
    public TransformMovement transformMovement {get; set;}
    private void Start() {
        if (Instance != null && Instance != this){
            Destroy(this);
        } 
        else Instance = this;

        transformMovement = new TransformMovement(groundCheck, groundLayer, transform, moveSpeed,
             jumpForce, castDistance, groundCheckRadius);
    }

    private void Update() {
        if (transformMovement.enable)
            transformMovement.HandleMovement();
        if (DebugMode)
            transformMovement.UpdateData();
    }
    
    public class RigidbodyMovement{
        private Transform groundCheck;
        private LayerMask groundLayer;
        
    }
    public class TransformMovement{ 
        private Transform groundCheck;
        private LayerMask groundLayer;
        Transform transform;
        private float moveSpeed = 5f;
        private float jumpForce = 22f;
        private float gravity;
        private float castDistance = 0.1f;
        private float groundCheckRadius = 0.1f;
        public bool isGrounded { get; private set;}
        private Vector3 velocity;
        public bool enable {get; set; }

        public TransformMovement(Transform groundCheck, LayerMask groundLayer, Transform playerTransform,
         float moveSpeed, float jumpForce, float castDistance, float groundCheckRadius){
            this.groundCheck = groundCheck;
            this.groundLayer = groundLayer;
            this.transform = playerTransform;
            gravity = PlayerMovementController.gravity;

            UpdateData();
        }

        public void UpdateData(){
            moveSpeed = Instance.moveSpeed;
            jumpForce = Instance.jumpForce;
            castDistance = Instance.castDistance;
            groundCheckRadius = Instance.groundCheckRadius;
        }
        public void HandleMovement()
        {
            // Check if the player is grounded
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

            // Character movement
            float horizontalInput = Input.GetAxis("Horizontal");

            // Move Player Horizontaly
            velocity.x = horizontalInput * moveSpeed;

            // Set Horizontal movement to zero if object is on moving direction
            // TODO: Use CapsuleCast instead of BoxCast, Can easily cause problems with groundCheck
            if (Physics2D.BoxCast(transform.position, new Vector2(1f, 1.75f), 0, (Vector2.right * velocity.x).normalized, castDistance, groundLayer)) 
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
    }
}
