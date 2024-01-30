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
    const string triggerShoot = "Shoot";
    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void Start(){
        Player.OnPlayerTakeDamage += Player_OnTakeDamageAction;
        Player.OnPlayerDying += Player_OnPlayerDyingAction;
        Weapon.OnShoot += Weapon_OnShootAction;
    }

    private void OnDisable() {
        Player.OnPlayerDying -= Player_OnPlayerDyingAction;
        Player.OnPlayerTakeDamage -= Player_OnTakeDamageAction;
    }

    private void Player_OnPlayerDyingAction(object sender, EventArgs e){
        animator.SetTrigger(triggerDead); 
    }
    private void Player_OnTakeDamageAction(object sender, EventArgs e){
        animator.SetTrigger(triggerHurt);
    }
    private void Weapon_OnShootAction(object sender, EventArgs e){
        animator.SetTrigger(triggerShoot);
    }
    private void Update() {

        animator.SetFloat(paramVelocity, Mathf.Abs(playerMovement.velocity.x));
         
    }

}
