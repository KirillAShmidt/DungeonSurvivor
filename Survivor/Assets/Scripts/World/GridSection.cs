using UnityEngine;

public class GridSection
{
    public Vector3 Section { get; set; }
    
    public bool IsCreated { get; set; }

    public GridSection(Vector3 section)
    {
        Section = section;
    }    
}
