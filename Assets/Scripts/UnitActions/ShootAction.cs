using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAction : BaseAction
{

    [SerializeField] private int maxShootDistance = 7;
    // Shooting states 
    private enum State
    {
        Aiming,
        Shooting,
        Cooloff,
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isActive) return;


    }
    public override string GetActionName()
    {
        return "SHOOT";
    }

    public override List<GridPosition> GetValidActionGridPositionList()
    {
        List<GridPosition> validGridPositionList = new List<GridPosition>();
        if (unit == null) return validGridPositionList;

        // Unit grid position
        GridPosition unitGridPosition = unit.GetGridPosition();

        // Cycle offsets
        for (int x = -maxShootDistance; x <= maxShootDistance; x++)
        {
            for (int z = -maxShootDistance; z <= maxShootDistance; z++)
            {
                GridPosition offsetGridPosition = new GridPosition(x, z);
                GridPosition inRangeGridPosition = unitGridPosition + offsetGridPosition;
                // Do nothing if position is invalid
                if (!LevelGrid.Instance.IsValidGridPosition(inRangeGridPosition))
                {
                    continue;
                }

                // Distance check - circular range
                int testDistance = Mathf.Abs(x) + Mathf.Abs(z);
                if (testDistance > maxShootDistance)
                {
                    continue;
                }

                // Occupied slots = valid
                if (!LevelGrid.Instance.HasAnyUnitsOnGridPosition(inRangeGridPosition))
                {
                    continue;
                }
       
                Unit targetUnit = LevelGrid.Instance.GetUnitAtGridPosition(inRangeGridPosition);
                // Do not add friendly units - compare this unit with target unit
                if (targetUnit.IsEnemy() == unit.IsEnemy())
                {
                    continue;
                }

                // Add it to list if it's valid
                validGridPositionList.Add(inRangeGridPosition);
            }
        }

        return validGridPositionList;
    }

    public override void TakeAction(GridPosition gridPosition, Action onActionComplete)
    {
        // assign spinComplete reference to this Spin Action
        this.onActionComplete = onActionComplete;
        isActive = true;
        alreadySpinned = 0f;
        HandleBusyUI();
    }
}
