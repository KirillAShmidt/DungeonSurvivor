using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField]
    private Room _prefab;

    [SerializeField]
    private int _size;

    private Vector3[,] _grid;

    private IEnumerator Start()
    {
        _grid = CreateGrid();

        for (int i = 0; i < _size; i++)
        {
            for (int j = 0; j < _size; j++)
            {
                yield return new WaitForSecondsRealtime(0.3f);

                if(Random.value > 0.5f)
                {
                    var newRoom = Instantiate(_prefab);
                    newRoom.transform.position = _grid[i, j];
                }
            }
        }
    }

    private Vector3[,] CreateGrid()
    {
        var grid = new Vector3[_size, _size];

        grid[0, 0] = Vector3.zero;

        for (int i = 0; i < _size; i++)
        {
            if (i > 0)
            {
                grid[i, 0] = grid[i - 1, 0] + new Vector3(0, 0, _prefab.GetComponent<BoxCollider>().size.z);
            }

            for (int j = 1; j < _size; j++)
            {
                grid[i, j] = grid[i, j - 1] + new Vector3(_prefab.GetComponent<BoxCollider>().size.x, 0, 0);
            }
        }

        return grid;
    }
}
