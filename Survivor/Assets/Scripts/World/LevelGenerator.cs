using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField]
    private Room[] _prefabs;

    [SerializeField]
    private Room _startingRoom;

    [SerializeField]
    private Room _bossRoom;

    [SerializeField]
    private int _gridSize;

    [SerializeField]
    private int _roomsAmount;

    private Room[,] spawnedRooms;

    public Action OnLevelLoaded;

    private IEnumerator Start()
    {
        spawnedRooms = new Room[_gridSize, _gridSize];
        spawnedRooms[_gridSize / 2, _gridSize / 2] = Instantiate(_startingRoom);

        yield return new WaitForSecondsRealtime(0.3f);

        for (int i = 0; i < _roomsAmount - 2; i++)
        {
            // yield return new WaitForSecondsRealtime(0.3f);
            PlaceOneRoom(_prefabs[0]);
        }

        PlaceOneRoom(_bossRoom);

        OnLevelLoaded?.Invoke();
    }

    private void PlaceOneRoom(Room room)
    {
        HashSet<Vector2Int> vacantPlaces = new HashSet<Vector2Int>();

        for(int i = 0; i < spawnedRooms.GetLength(0); i++)
        {
            for(int j = 0; j < spawnedRooms.GetLength(1); j++)
            {
                if(spawnedRooms[i, j] == null)
                {
                    continue;
                }

                int maxX = spawnedRooms.GetLength(0) - 1;
                int maxY = spawnedRooms.GetLength(1) - 1;

                if(i > 0 && spawnedRooms[i - 1, j] == null)
                {
                    vacantPlaces.Add(new Vector2Int(i - 1, j));
                }
                if (j > 0 && spawnedRooms[i, j - 1] == null)
                {
                    vacantPlaces.Add(new Vector2Int(i, j - 1));
                }
                if(i < maxX && spawnedRooms[i + 1, j] == null)
                {
                    vacantPlaces.Add(new Vector2Int(i + 1, j));
                }
                if(j < maxY && spawnedRooms[i, j + 1] == null)
                {
                    vacantPlaces.Add(new Vector2Int(i, j + 1));
                }
            }
        }

        Room newRoom = Instantiate(room);

        int limit = 500;
        while (limit-- > 0)
        {
            Vector2Int position = vacantPlaces.ElementAt(UnityEngine.Random.Range(0, vacantPlaces.Count));

            if (ConnectToSomething(newRoom, position))
            {
                // TODO: исправить магическое "12" на размер комнаты
                newRoom.transform.position = new Vector3(position.x - (_gridSize / 2), 0, position.y - (_gridSize / 2)) * 12;
                spawnedRooms[position.x, position.y] = newRoom;
                return;
            }
        }

        Destroy(newRoom);
    }

    private bool ConnectToSomething(Room room, Vector2Int p)
    {
        int maxX = spawnedRooms.GetLength(0);
        int maxY = spawnedRooms.GetLength(1);

        List<Vector2Int> neighbours = new List<Vector2Int>();

        if (room.Doors["U"] != null && p.y < maxY && spawnedRooms[p.x, p.y + 1]?.Doors["D"] != null) neighbours.Add(Vector2Int.up);
        if (room.Doors["D"] != null && p.y > 0 && spawnedRooms[p.x, p.y - 1]?.Doors["U"] != null) neighbours.Add(Vector2Int.down);
        if (room.Doors["R"] != null && p.x < maxX && spawnedRooms[p.x + 1, p.y]?.Doors["L"] != null) neighbours.Add(Vector2Int.right);
        if (room.Doors["L"] != null && p.x > 0 && spawnedRooms[p.x - 1, p.y]?.Doors["R"] != null) neighbours.Add(Vector2Int.left);

        if (neighbours.Count == 0) return false;

        Vector2Int selectedDirection = neighbours[UnityEngine.Random.Range(0, neighbours.Count)];
        Room selectedRoom = spawnedRooms[p.x + selectedDirection.x, p.y + selectedDirection.y];

        if (selectedDirection == Vector2Int.up)
        {
            room.Doors["U"].gameObject.SetActive(false);
            selectedRoom.Doors["D"].gameObject.SetActive(false);
        }
        else if (selectedDirection == Vector2Int.down)
        {
            room.Doors["D"].gameObject.SetActive(false);
            selectedRoom.Doors["U"].gameObject.SetActive(false);
        }
        else if (selectedDirection == Vector2Int.right)
        {
            room.Doors["R"].gameObject.SetActive(false);
            selectedRoom.Doors["L"].gameObject.SetActive(false);
        }
        else if (selectedDirection == Vector2Int.left)
        {
            room.Doors["L"].gameObject.SetActive(false);
            selectedRoom.Doors["R"].gameObject.SetActive(false);
        }

        return true;
    }
}
