using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using NaughtyAttributes;

public class Player : MonoBehaviour, IDamageable
{
    PlayerMovement playerMovement;

    [SerializeField] private bool DebugMode = true;
    [Space]
    [HorizontalLine(2f, EColor.White)]
    [ProgressBar("Health", 100, EColor.Red)]
    [SerializeField] private int health; 
    public int Health {
        get {
            return health;
        }
        set {
            health = value;
            if (health <= 0){
                health = 0;
                OnPlayerDying?.Invoke(this, EventArgs.Empty);
            }
        }
    }
    public static event EventHandler OnPlayerDying;
    public static event EventHandler OnPlayerTakeDamage;
    
    [ShowIf("DebugMode")]
    [Button("Damage 10", EButtonEnableMode.Playmode)]
    public void TestDamage(){
        TakeDamage(10);
    }
    private void Awake(){
        playerMovement = GetComponent<PlayerMovement>();
        Health = 100;
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
    }
}
