using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(EnemySpawner))]
public class Room : MonoBehaviour
{
    public static Room ActiveRoom;

    public Dictionary<string, Door> Doors { get; set; } = new Dictionary<string, Door>();

    private List<Door> _openedDoors = new List<Door>();
    private EnemySpawner _enemySpawner;

    private void Awake()
    {
        _enemySpawner = GetComponent<EnemySpawner>();
        _enemySpawner.OnRoomEntered += CloseDoors;
        _enemySpawner.OnRoomCompleted += OpenDoors;

        List<Door> doors = new List<Door>();
        doors.AddRange(GetComponentsInChildren<Door>());
        
        for(int i = 0; i < doors.Count; i++)
        {
            Doors.Add(doors[i].Name, doors[i]);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Player>();

        if (player != null)
        {
            ActiveRoom = this;
        }
    }

    private void CloseDoors()
    {
        foreach (Door door in Doors.Values)
        {
            if (!door.gameObject.activeInHierarchy)
            {
                _openedDoors.Add(door);
                door.gameObject.SetActive(true);
            }
        }
    }

    private void OpenDoors()
    {
        foreach (Door door in _openedDoors)
        {
            door.gameObject.SetActive(false);
        }
    }
}
