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
    public Vector3 GetWorldPosition(GridPosition gridPosition)
    {
        return new Vector3(gridPosition.x, 0, gridPosition.z) * cellSize;
    }

    // Get grid position from world position
    public GridPosition GetGridPosition(Vector3 position)
    {
        return new GridPosition(
            Mathf.RoundToInt(position.x / cellSize),
            Mathf.RoundToInt(position.z / cellSize)
            );
    }

    // Gets the grid obejct from grid array
    public GridObject GetGridObject(GridPosition gridPosition)
    {
        return gridObjectArray[gridPosition.x, gridPosition.z]; 
    }

    // Creates grid visualization coordinates
    public void CreateDebugObjects(Transform debugPrefab)
    {
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                // Position in grid 
                GridPosition position = new GridPosition(x, z);
                // Instantiate the text (debug object)
                Transform debugTransform = GameObject.Instantiate(debugPrefab, GetWorldPosition(position), Quaternion.identity);
                // Get the Grid Debug Object component - to set the GridObject
                GridDebugObject gridDebugObject = debugTransform.GetComponent<GridDebugObject>();
                // Assign the gridObject to a DebugObject
                gridDebugObject.SetGridObject(GetGridObject(position));
            }
        }
    }

    // Checks whether the gridPosition is within height, widht and not lesser than one
    public bool isValidGridPosition(GridPosition gridPosition)
    {
        return  gridPosition.x >=0 && 
                gridPosition.z >= 0 && 
                gridPosition.x < width && 
                gridPosition.z < height;
    }

    public int GetWidth()
    {
        return width;
    }

    public int GetHeight()
    {
        return height;
    }
}
