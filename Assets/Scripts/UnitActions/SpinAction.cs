using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class SpinAction : BaseAction
{
    // Simple delegate definition for complete spin action - defining our own
    /*
    public delegate void SpinCompleteDelegate();
    private SpinCompleteDelegate onSpinComplete;
    */

    // Delegate from using System - refactoring it to base Class
    /*
    private Action onSpinComplete;
        */

    private float alreadySpinned = 0f;


    private void Update()
    {
        if (!isActive) return;        

        float spinAddAmount = 360f * Time.deltaTime;
        alreadySpinned += spinAddAmount;

        if (alreadySpinned > 360f)
        {
            ActionComplete();
        }

        transform.eulerAngles += new Vector3(0, spinAddAmount, 0);
    }

    // Spins the the player 360 degrees
    public override void TakeAction(GridPosition gridPosition, Action onActionComplete)
    {
        ActionStart(onActionComplete);
        alreadySpinned = 0f;

    }

    public override string GetActionName()
    {
        return "SPIN";
    }

    // Return valid position -> only the unit position is valid
    public override List<GridPosition> GetValidActionGridPositionList()
    {
        List<GridPosition> validGridPositionList = new List<GridPosition>();
        GridPosition unitGridPosition = unit.GetGridPosition();

        return new List<GridPosition>
        {
            unitGridPosition
        };
    }

    // Return 2 points for Spinaction
    public override int GetActionPointsCost()
    {
        return 2;
    }
}
