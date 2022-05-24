using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField]
    private string _doorName;

    public string Name { get { return _doorName; } }

    public bool IsWall { get; set; }

    private void Start()
    {
        gameObject.SetActive(true);
    }
}
