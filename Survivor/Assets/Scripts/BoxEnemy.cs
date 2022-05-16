using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxEnemy : Enemy
{
    private void Update()
    {
        _navMeshAgent.SetDestination(_player.transform.position);
    }
}
