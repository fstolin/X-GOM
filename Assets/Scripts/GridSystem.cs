using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem
{
    // Grid dimensions / x & z axis
    private int width;
    private int height;
    private float cellSize;

    // GridSystem Constructor
    public GridSystem(int width, int height, float cellSize)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                // Debug visuals
                Debug.DrawLine(GetWorldPosition(i, j), GetWorldPosition(i, j) + Vector3.right * .2f, Color.white, 1000); 
            }
        }
    }

    // Get world position from grid position
    public Vector3 GetWorldPosition(int x, int z)
    {
        return new Vector3(x, 0, z) * cellSize;
    }

    // Get grid position from world position
    public GridPosition GetGridPosition(Vector3 position)
    {
        return new GridPosition(
            Mathf.RoundToInt(position.x / cellSize),
            Mathf.RoundToInt(position.z / cellSize)
            );
    }
}
