using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAction : BaseAction
{
    private float alreadySpinned = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isActive) return;

        float spinAddAmount = 360f * Time.deltaTime;
        alreadySpinned += spinAddAmount;

        if (alreadySpinned > 360f)
        {
            isActive = false;
            onActionComplete();
            HandleBusyUI();
        }

        transform.eulerAngles += new Vector3(0, spinAddAmount, 0);
    }
    public override string GetActionName()
    {
        return "SHOOT";
    }

    public override List<GridPosition> GetValidActionGridPositionList()
    {
        List<GridPosition> validGridPositionList = new List<GridPosition>();
        GridPosition unitGridPosition = unit.GetGridPosition();

        return new List<GridPosition>
        {
            unitGridPosition
        };
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
