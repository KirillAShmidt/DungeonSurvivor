using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageble
{
    private int _health;

    public void GetDamage(int damage)
    {
        _health -= damage;

        if(_health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
