using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    
    PlayerInputActions playerInputActions;
    static float horizontalInput;
    static bool isJumpPressed;
    static bool isShootPressed;
    private void Start() {
        if (playerInputActions == null){
            playerInputActions = new PlayerInputActions();
        }
        playerInputActions.Enable();
        playerInputActions.Player.HorizontalMovement.performed += PlayerHorizontalMovement_Performed;
        playerInputActions.Player.HorizontalMovement.canceled += PlayerHorizontalMovement_Canceled;
        playerInputActions.Player.Jump.performed += PlayerJump_Performed;
        playerInputActions.Player.Jump.canceled += PlayerJump_Canceled;
        playerInputActions.Player.Shoot.performed += PlayerShoot_Performed;
        playerInputActions.Player.Shoot.canceled += PlayerShoot_Canceled;
    }

    private void PlayerHorizontalMovement_Performed(InputAction.CallbackContext context){
        horizontalInput = context.ReadValue<float>();
    }
    private void PlayerHorizontalMovement_Canceled(InputAction.CallbackContext context){
        // When horizontal input is not pressed
        horizontalInput = 0;
    }
    private void PlayerJump_Performed(InputAction.CallbackContext context){
        isJumpPressed = true;
    }
    private void PlayerJump_Canceled(InputAction.CallbackContext context){
        isJumpPressed = false;
    }
    private void PlayerShoot_Performed(InputAction.CallbackContext context){
        isShootPressed = true;
    }
    private void PlayerShoot_Canceled(InputAction.CallbackContext context){
        isShootPressed = false;
    }
    public static float HorizontalInput(){
        return horizontalInput;
    }
    public static bool JumpPressed(){
        return isJumpPressed;
    }
    public static bool ShootPressed(){
        return isShootPressed;
    }

    private void OnDisable() {
        playerInputActions.Player.HorizontalMovement.performed -= PlayerHorizontalMovement_Performed;
        playerInputActions.Player.HorizontalMovement.canceled -= PlayerHorizontalMovement_Canceled;
        playerInputActions.Player.Jump.performed -= PlayerJump_Performed;
        playerInputActions.Player.Jump.canceled -= PlayerJump_Canceled;
        playerInputActions.Player.Shoot.performed -= PlayerShoot_Performed;
        playerInputActions.Player.Shoot.canceled -= PlayerShoot_Canceled;
    }
}
