using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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

    // Returns whether grid position list contains specified grid position
    public virtual bool IsValidActionGridPosition(GridPosition gridPosition)
    {
        return GetValidActionGridPositionList().Contains(gridPosition);
    }

    // Handles UI (hides it) during being busy
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

    // Abstract method, implement checking valid grid positions for specified actions.
    public abstract List<GridPosition> GetValidActionGridPositionList();

    // Returns number of action points, that the action costs
    public virtual int GetActionPointsCost()
    {
        return 1;
    }

    // Use at the start of each action, set the callback, handle UI
    protected void ActionStart(Action onActionComplete)
    {
        isActive = true;
        this.onActionComplete = onActionComplete;
        HandleBusyUI();
    }

    // Action completed - call the callback, handle UI
    protected void ActionComplete()
    {
        isActive = false;
        onActionComplete();
        HandleBusyUI();
    }

}
