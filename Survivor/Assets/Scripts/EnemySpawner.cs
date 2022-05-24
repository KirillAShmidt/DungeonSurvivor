using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private Enemy[] _enemies;

    [SerializeField]
    private int _amountOfEnemies;

    private List<Enemy> _spawnedEnemies = new List<Enemy>();

    public bool Iscompleted { get; set; } = false;

    public Action OnRoomEntered;
    public Action OnRoomCompleted;

    private void Start()
    {
        if (_spawnedEnemies.Count == 0)
        {
            OnRoomCompleted?.Invoke();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.gameObject.GetComponent<Player>();

        if(player != null && !Iscompleted && _amountOfEnemies != 0)
        {
            OnRoomEntered?.Invoke();

            for (int i = 0; i < _amountOfEnemies; i++)
            {
                SpawnEnemy();
            }
        }
    }

    private void SpawnEnemy()
    {
        Enemy enemyClone = Instantiate(_enemies[UnityEngine.Random.Range(0, _enemies.Length)]);
        enemyClone.transform.position = transform.position + new Vector3(UnityEngine.Random.Range(2, 7), 1, UnityEngine.Random.Range(2, 7));
        enemyClone.OnEnemyDead += DeleteEnemy;
        _spawnedEnemies.Add(enemyClone);
    }

    private void DeleteEnemy(Enemy enemy)
    {
        enemy.OnEnemyDead -= DeleteEnemy;
        _spawnedEnemies.Remove(enemy);

        if (_spawnedEnemies.Count == 0)
        {
            OnRoomCompleted?.Invoke();
            Iscompleted = true;
        }
    }
}
