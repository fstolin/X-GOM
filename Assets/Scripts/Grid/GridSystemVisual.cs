using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystemVisual : MonoBehaviour
{
    // Singleton pattern
    public static GridSystemVisual Instance { get; private set; }

    [SerializeField] private Transform gridSystemVisualPrefab;

    private GridSystemVisualSingle[,] gridSystemVisualSingleArray;
    [SerializeField] private bool shouldRenderVisuals = true;

    // Singleton instance awake
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There's modre than GridVisual! " + this.transform + " - " + Instance);
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
    }

    // Instantiates all grid visuals, adds them to VisualSingleArray
    private void Start()
    {
        LevelGrid levelGrid = LevelGrid.Instance;
        gridSystemVisualSingleArray = new GridSystemVisualSingle[levelGrid.GetWidth(), levelGrid.GetHeight()];

        for (int x = 0; x < LevelGrid.Instance.GetWidth(); x++)
        {
            for (int z = 0; z < LevelGrid.Instance.GetHeight(); z++)
            {
                GridPosition gridPosition = new GridPosition(x, z);
                Transform t = Instantiate(gridSystemVisualPrefab, levelGrid.GetWorldPosition(gridPosition), Quaternion.identity);
                gridSystemVisualSingleArray[x, z] = t.GetComponent<GridSystemVisualSingle>();
            }
        }
    }

    private void Update()
    {
        if (shouldRenderVisuals)
        {
            UpdateGridVisual();
        }        
    }

    public void HideAllGridVisuals()
    {
        for (int x = 0; x < LevelGrid.Instance.GetWidth(); x++)
        {
            for (int z = 0; z < LevelGrid.Instance.GetHeight(); z++)
            {
                gridSystemVisualSingleArray[x, z].Hide();
            }
        }
    }

    // Public method to enable render visuals
    public void EnableRenderVisuals()
    {
        shouldRenderVisuals = true;
    }


    // Public method to disable render visuals
    public void DisableRenderVisuals()
    {
        shouldRenderVisuals = false;
        HideAllGridVisuals();
    }

    public void ShowGridPositionList(List<GridPosition> gridPositionList)
    {
        foreach(GridPosition gridPosition in gridPositionList)
        {
            gridSystemVisualSingleArray[gridPosition.x, gridPosition.z].Show();
        }
    }


    // Updates the grid visuals
    private void UpdateGridVisual()
    {
        HideAllGridVisuals();
        BaseAction selectedAction = UnitActionSystem.Instance.getSelectedAction();
        if (selectedAction == null) { return; }

        ShowGridPositionList(selectedAction.GetValidActionGridPositionList());
    }

}
