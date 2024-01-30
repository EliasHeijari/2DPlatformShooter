using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    
    private int damage;


    public void SetDamage(int damage){
        this.damage = damage;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        
        if (other.TryGetComponent(out IDamageable damageable)){
            damageable.TakeDamage(damage);
        }

        Destroy(this.gameObject);
    }
}
