using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavigationBaker : MonoBehaviour
{

    private NavMeshSurface[] surfaces;

    private void Awake()
    {
        FindObjectOfType<LevelGenerator>().OnLevelLoaded += UpdateNavmesh;
    }

    private void UpdateNavmesh()
    {
        surfaces = FindObjectsOfType<NavMeshSurface>();

        for (int i = 0; i < surfaces.Length; i++)
        {
            surfaces[i].BuildNavMesh();
        }
    }
}