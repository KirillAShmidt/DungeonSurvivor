using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlacement : MonoBehaviour
{
    [SerializeField]
    private Vector3 _offset;

    private void Start()
    {
        FindObjectOfType<LevelGenerator>().OnLevelLoaded += PlaceCamera;
    }

    private void Update()
    {
        PlaceCamera();
    }

    private void PlaceCamera()
    {
        if (Room.ActiveRoom != null)
        {
            transform.position = Room.ActiveRoom.transform.position + _offset;
        }
        else
        {
            transform.position = _offset;
        }
    }
}