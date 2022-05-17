using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : MonoBehaviour, IDamageble
{
    protected NavMeshAgent _navMeshAgent;
    protected Player _player;

    private int _health = 10;

    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _player = FindObjectOfType<Player>();
    }

    public void GetDamage(int damage)
    {
        _health -= damage;
        Debug.Log(_health);

        if(_health <= 0)
        {
            Destroy(gameObject);
        }
        
    }
}
