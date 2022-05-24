using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public abstract class Enemy : MonoBehaviour, IDamageble
{
    protected NavMeshAgent _navMeshAgent;
    protected Player _player;

    private int _health = 10;

    public Action<Enemy> OnEnemyDead;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _player = FindObjectOfType<Player>();
    }

    public void GetDamage(int damage)
    {
        _health -= damage;

        if(_health <= 0)
        {
            OnEnemyDead?.Invoke(this);
            Destroy(gameObject);
        }
        
    }
}
