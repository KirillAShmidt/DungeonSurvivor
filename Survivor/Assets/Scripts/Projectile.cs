using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Projectile : MonoBehaviour
{
    protected float _speed;
    protected int _damage = 5;

    private float _timer = 1f;

    public Action OnHitEnemy;

    public void Initialize(ProjectileData data)
    {
        _speed = data.speed;
        _damage = data.damage;
    }

    private void FixedUpdate()
    {
        _timer -= Time.fixedDeltaTime * 2;

        if (_timer <= 0)
        {
            Destroy(gameObject); 
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.gameObject.GetComponent<Player>())
        {
            Destroy(gameObject);
        }

        IDamageble enemy = collision.gameObject.GetComponent<Enemy>();
        
        if (enemy != null)
        {
            enemy.GetDamage(_damage);
        }
    }
}
