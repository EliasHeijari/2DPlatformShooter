using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using System;
public class Weapon : MonoBehaviour
{
    [BoxGroup("Weapon Data")]
    [Label("Fire Rate In Seconds")]
    [SerializeField] private float fireRate = 1f;
    [BoxGroup("Weapon Data")]
    [SerializeField] private float shootForce = 300f;
    [BoxGroup("Weapon Data")]
    [SerializeField] private int damage = 1;
    [SerializeField] private GameObject bulletPrefab;
    public static event EventHandler OnShoot;
    float nextTimeToFire;
    private void Update() {

        nextTimeToFire += Time.deltaTime;
        if (GameInput.ShootPressed() && PlayerMovement.isGrounded && nextTimeToFire > fireRate){
            nextTimeToFire = 0;
            OnShoot?.Invoke(this, EventArgs.Empty);
            ShootBullet();
        }

    }

    private void ShootBullet(){
        // Spawn Bullet
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        // Add Force To Bullet
        bullet.GetComponent<Rigidbody2D>().AddForce(transform.right * shootForce);
        bullet.GetComponent<Bullet>().SetDamage(damage);
    }
}
