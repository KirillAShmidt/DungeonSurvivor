using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Player _playerPrafab;

    [SerializeField]
    private Camera _camera;

    private void Start()
    {
        FindObjectOfType<LevelGenerator>().OnLevelLoaded += SpawnPlayer;
    }

    private void SpawnCamera()
    {

    }

    private void SpawnPlayer()
    {
        var player = Instantiate(_playerPrafab);
        player.transform.position = _camera.transform.position;
    }
}
