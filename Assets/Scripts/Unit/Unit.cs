using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    // Seriazables
    [SerializeField] private int defaultActionPoints = 2;
    [SerializeField] private bool isEnemy;

    // Static event -> gets fired on ANY class of Unit, that's why we name it OnAny
    public static event EventHandler OnAnyActionPointsChanged;

    // Actions
    private MoveAction moveAction;
    private SpinAction spinAction;
    private BaseAction[] baseActionArray;
    private int actionPoints;

    private GridPosition gridPosition;

    private void Awake()
    {
        moveAction = GetComponent<MoveAction>();
        spinAction = GetComponent<SpinAction>();
        baseActionArray = GetComponents<BaseAction>();
        this.actionPoints = defaultActionPoints;
    }

    private void Start()
    {
        // Get grid position from the units world position
        gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);

        Debug.Assert(gridPosition != null);
        LevelGrid.Instance.AddUnitAtGridPosition(gridPosition, this);

        // Listen to next turn
        TurnSystem.Instance.OnNextTurnHappening += TurnSystem_OnNextTurnHappening;
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

    // Tries to deduct action points for an action and if succesful, returns true
    public bool TrySpendActionPointsToTakeAction(BaseAction action)
    {
        if (CanSpendActionPointsToTakeAction(action))
        {
            SpendActionPoints(action.GetActionPointsCost());
            return true;
        } else
        {
            return false;
        }
    }

    // Action points check
    public bool CanSpendActionPointsToTakeAction(BaseAction action)
    {
        if (action.GetActionPointsCost() <= actionPoints)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // Next turn -> reset action points
    private void TurnSystem_OnNextTurnHappening(object sender, EventArgs e)
    {
        if (IsEnemy() && !TurnSystem.Instance.IsPlayerTurn() || // Is enemy & not players turn
            !IsEnemy() && TurnSystem.Instance.IsPlayerTurn())   // Is the player & it is players turn
        {
            actionPoints = defaultActionPoints;
            OnAnyActionPointsChanged?.Invoke(this, EventArgs.Empty);
        }        
    }

    // Spending action points
    private void SpendActionPoints(int amount)
    {
        actionPoints -= amount;
        OnAnyActionPointsChanged?.Invoke(this, EventArgs.Empty);
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

    public BaseAction[] GetBaseActionArray()
    {
        return baseActionArray;
    }

    public int GetActionPoints()
    {
        return actionPoints;
    }

    public bool IsEnemy()
    {
        return isEnemy;
    }

    public void Damage()
    {
        Debug.Log(transform + " damaged");
    }

}
