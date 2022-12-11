using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem
{
    // Grid dimensions / x & z axis
    private int width;
    private int height;
    private float cellSize;
    private GridObject[,] gridObjectArray;

    // GridSystem Constructor
    public GridSystem(int width, int height, float cellSize)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;

        gridObjectArray = new GridObject[width,height];
        // Cycling through our grid
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                // Creating grid objects for each grid tile
                GridPosition gridPosition = new GridPosition(x, z);
                gridObjectArray[x, z] = new GridObject(gridPosition, this);
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

    public void CreateDebugObjects(Transform debugPrefab)
    {
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                GameObject.Instantiate(debugPrefab, GetWorldPosition(x, z), Quaternion.identity);   
            }
        }
    }
}
