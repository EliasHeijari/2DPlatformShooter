using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Player : MonoBehaviour, IDamageable
{
    PlayerMovement playerMovement;
    public int Health { get; private set; } = 10;
    public static event EventHandler OnPlayerDying;
    public static event EventHandler OnPlayerTakeDamage;
    private void Awake(){
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Start() {
        OnPlayerDying += OnPlayerDying_Action;
    }

    private void OnPlayerDying_Action(object sender, EventArgs e){
        playerMovement.enabled = false;
        this.enabled = false;
        OnPlayerDying -= OnPlayerDying_Action;
    }

    private void FixedUpdate() {
        playerMovement.HandleMovement();
    }

    public void TakeDamage(int damage){
        Health -= damage;
        OnPlayerTakeDamage?.Invoke(this, EventArgs.Empty);
        if (Health <= 0){
            Health = 0;
            OnPlayerDying?.Invoke(this, EventArgs.Empty);
        }
    }
}
