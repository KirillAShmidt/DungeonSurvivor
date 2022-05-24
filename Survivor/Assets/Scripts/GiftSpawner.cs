using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftSpawner : MonoBehaviour
{
    [SerializeField]
    private Gift _gift;

    [SerializeField]
    private Vector3 _offset;

    private void Start()
    {
        GetComponent<EnemySpawner>().OnRoomCompleted += SpawnGift;
    }

    private void SpawnGift()
    {
        Instantiate(_gift, transform.position + _offset, Quaternion.identity);
    }
}
