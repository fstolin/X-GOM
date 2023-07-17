using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    // Actions
    private MoveAction moveAction;
    private SpinAction spinAction;

    private GridPosition gridPosition;

    private void Awake()
    {
        moveAction = GetComponent<MoveAction>();
        spinAction = GetComponent<SpinAction>();
    }

    private void Start()
    {
        // Get grid position from the units world position
        gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);

        Debug.Assert(gridPosition != null);
        LevelGrid.Instance.AddUnitAtGridPosition(gridPosition, this);
    }

    private void Update()
    {
        // Check whether Unit already left the tile & entered a new one -> due to lable changes
        GridPosition newGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        if (newGridPosition != gridPosition)
        {
            // Unit has changed grid position
            LevelGrid.Instance.UnitMovedGridPosition(this, gridPosition, newGridPosition);
            gridPosition = newGridPosition;
        }
    }


    public override string ToString()
    {
        return this.transform.name; 
    }

    public MoveAction GetMoveAction()
    {
        return moveAction;
    }

    public SpinAction GetSpinAction() { 
        return spinAction;
    }

    public GridPosition GetGridPosition() {
        return gridPosition;
    }

}
