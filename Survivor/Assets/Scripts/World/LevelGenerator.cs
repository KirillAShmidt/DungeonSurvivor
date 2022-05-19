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
    private Room StartingRoom;

    private Room[,] spawnedRooms;

    public Action OnLevelLoaded;

    private IEnumerator Start()
    {
        spawnedRooms = new Room[11, 11];
        spawnedRooms[5, 5] = Instantiate(StartingRoom);

        for(int i = 0; i < 20; i++)
        {
            PlaceOneRoom();
            yield return new WaitForSecondsRealtime(0.3f);
        }

        OnLevelLoaded?.Invoke();
    }

    private void PlaceOneRoom()
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

        Room newRoom = Instantiate(_prefabs[UnityEngine.Random.Range(0, _prefabs.Length)]);

        int limit = 10;
        while (limit-- > 0)
        {
            Vector2Int position = vacantPlaces.ElementAt(UnityEngine.Random.Range(0, vacantPlaces.Count));

            if (ConnectToSomething(newRoom, position))
            {
                newRoom.transform.position = new Vector3(position.x - 5, 0, position.y - 5) * 12;
                spawnedRooms[position.x, position.y] = newRoom;
                break;
            }
        }
    }

    private bool ConnectToSomething(Room room, Vector2Int p)
    {
        int maxX = spawnedRooms.GetLength(0);
        int maxY = spawnedRooms.GetLength(1);

        List<Vector2Int> neighbours = new List<Vector2Int>();

        if (room.DoorU != null && p.y < maxY && spawnedRooms[p.x, p.y + 1]?.DoorD != null) neighbours.Add(Vector2Int.up);
        if (room.DoorD != null && p.y > 0 && spawnedRooms[p.x, p.y - 1]?.DoorU != null) neighbours.Add(Vector2Int.down);
        if (room.DoorR != null && p.x < maxX && spawnedRooms[p.x + 1, p.y]?.DoorL != null) neighbours.Add(Vector2Int.right);
        if (room.DoorL != null && p.x > 0 && spawnedRooms[p.x - 1, p.y]?.DoorR != null) neighbours.Add(Vector2Int.left);

        if (neighbours.Count == 0) return false;

        Vector2Int selectedDirection = neighbours[UnityEngine.Random.Range(0, neighbours.Count)];
        Room selectedRoom = spawnedRooms[p.x + selectedDirection.x, p.y + selectedDirection.y];

        if (selectedDirection == Vector2Int.up)
        {
            room.DoorU.SetActive(false);
            selectedRoom.DoorD.SetActive(false);
        }
        else if (selectedDirection == Vector2Int.down)
        {
            room.DoorD.SetActive(false);
            selectedRoom.DoorU.SetActive(false);
        }
        else if (selectedDirection == Vector2Int.right)
        {
            room.DoorR.SetActive(false);
            selectedRoom.DoorL.SetActive(false);
        }
        else if (selectedDirection == Vector2Int.left)
        {
            room.DoorL.SetActive(false);
            selectedRoom.DoorR.SetActive(false);
        }

        return true;
    }
}
