using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class UnitActionSystem : MonoBehaviour
{
    // Unit selected event
    public event System.EventHandler OnSelectedUnitChanged;
    public event System.EventHandler OnSelectedActionChanged;
    public event System.EventHandler OnActionStarted;
    // Singleton pattern
    public static UnitActionSystem Instance { get; private set; }

    [SerializeField] private LayerMask unitLayer;

    // Selected unit
    private Unit selectedUnit;
    private BaseAction selectedAction;
    private bool isBusy;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There's modre than UnitActionSystem! " + this.transform + " - " + Instance);
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (isBusy)
        {
            return;
        }

        // UI check
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        // Handle the selection of units, should the unit be selected,
        // do not move it in the same frame
        if (TryHandleUnitSelection()) return;

        HandleSelectedAction();

    }

    // Handles unit actions
    private void HandleSelectedAction()
    {

        if (Input.GetMouseButtonDown(1))
        {
            GridPosition mouseGridPosition = LevelGrid.Instance.GetGridPosition(MouseWorld.GetMouseWorldPosition());

            // If the selected grid position is valid for the action and the unit
            if (!selectedAction.IsValidActionGridPosition(mouseGridPosition))
            {
                return;
            }
            // If we can spend enough action points. If yes, actionPoints will be spend.
            if (!selectedUnit.TrySpendActionPointsToTakeAction(selectedAction))
            {
                return;
            }

            OnActionStarted?.Invoke(this, EventArgs.Empty);
            selectedAction.TakeAction(mouseGridPosition, ClearBusy);
            
        
        }
    }

    private void SetBusy()
    {
        isBusy = true;
    }

    private void ClearBusy()
    {
        isBusy = false;
    }

    // Tries to select a Unit and get it's unit component on mous click
    private bool TryHandleUnitSelection()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Get mouse button input
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, float.MaxValue, unitLayer))
            {
                // Try get the Unit component, if we found it, select the unit
                if (hitInfo.transform.TryGetComponent<Unit>(out Unit unit))
                {
                    if (unit == selectedUnit)
                    {
                        // Unit is already selected
                        return false;
                    }
                    SetSelectedUnit(unit);
                    return true;
                }
            }
            // Selection was unsuccessful - Select null unit to fire events
            // OnSelectedUnitChanged?.Invoke(this, EventArgs.Empty);
        }
        return false;
    }

    // Sets the selected unit & fires selection events
    private void SetSelectedUnit(Unit unit)
    {
        selectedUnit = unit;
        if (selectedUnit == null) return;

        SetSelectedAction(unit.GetMoveAction());
        // Check whether EventHandler has listeners (otherwise returns null)
        OnSelectedUnitChanged?.Invoke(this, EventArgs.Empty);
    }

    public Unit GetSelectedUnit()
    {
        return selectedUnit;
    }

    public BaseAction getSelectedAction()
    {
        return selectedAction;
    }

    public void SetSelectedAction(BaseAction action)
    {
        selectedAction = action;
        //GridSystemVisual.Instance.EnableRenderVisuals();
        // Fire selected action events
        OnSelectedActionChanged?.Invoke(this, EventArgs.Empty);
    }
}
