using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Abstract -> can't instantiate this class
public abstract class BaseAction : MonoBehaviour
{

    protected Unit unit;
    protected bool isActive = false;
    protected Action onActionComplete;
    protected string actionName;

    protected virtual void Awake()
    {
        unit = GetComponent<Unit>();
    }

    // abstract = have to implement
    public abstract string GetActionName();

    // generic take action method
    public abstract void TakeAction(GridPosition gridPosition, Action onActionComplete);

    public virtual bool IsValidActionGridPosition(GridPosition gridPosition)
    {
        return GetValidActionGridPositionList().Contains(gridPosition);
    }

    protected virtual void HandleBusyUI()
    {
        if (!isActive)
        {
            GridSystemVisual.Instance.EnableRenderVisuals();
        } else
        {
            GridSystemVisual.Instance.DisableRenderVisuals();
        }
        UnitActionSystemUI.Instance.ToggleBusyUI();
    }

    public abstract List<GridPosition> GetValidActionGridPositionList();

}
