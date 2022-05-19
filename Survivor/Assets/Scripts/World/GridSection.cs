using UnityEngine;

public class GridSection
{
    public Vector3 Coordinates { get; set; }
    
    public bool IsCreated { get; set; }

    public GridSection(Vector3 coordinates)
    {
        Coordinates = coordinates;
    }    
}
