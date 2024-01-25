using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    SpriteRenderer spriteRenderer;
    Animator animator;
    const string paramVelocity = "Velocity";
    const string triggerDead = "Dead";
    const string triggerHurt = "Hurt";
    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void Start(){
        Player.OnPlayerTakeDamage += Player_OnTakeDamageAction;
        Player.OnPlayerDying += Player_OnPlayerDyingAction;
    }

    private void Player_OnPlayerDyingAction(object sender, EventArgs e){
        animator.SetTrigger(triggerDead);
    }
    private void Player_OnTakeDamageAction(object sender, EventArgs e){
        animator.SetTrigger(triggerHurt);
    }
    private void Update() {

        animator.SetFloat(paramVelocity, Mathf.Abs(playerMovement.velocity.x));

        switch(playerMovement.velocity.x){
            case > 0:
                spriteRenderer.flipX = false; 
                break;
            case < 0:
                spriteRenderer.flipX = true;
                break;
        }
    }

}
