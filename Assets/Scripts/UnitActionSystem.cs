using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UnitActionSystem : MonoBehaviour
{

    public event System.EventHandler OnSelectedUnitChanged;

    [SerializeField] private LayerMask unitLayer;

    private Unit selectedUnit;

    public Unit getSelectedUnit()
    {
        return selectedUnit;
    }

    // Update is called once per frame
    private void Update()
    {
        // Handle the selection of units, should the unit be selected,
        // do not move it in the same frame
        if (Input.GetMouseButtonDown(0))
        {
            if (TryHandleUnitSelection()) return;
        }
        // Move selectedUnit to a new place after clicking the mouse
        if (Input.GetMouseButtonDown(1) && selectedUnit != null)
        {
            selectedUnit.Move(MouseWorld.GetMouseWorldPosition());
        }
    }

    // Tries to select a Unit and get it's unit component on mous click
    private bool TryHandleUnitSelection()
    {
        // Get mouse button input
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, float.MaxValue, unitLayer))
        {
            // Try get the Unit component, if we found it, select the unit
            if (hitInfo.transform.TryGetComponent<Unit>(out Unit unit)) { 
                SetSelectedUnit(unit); 
                return true;
            }
        } 
        // Selection was unsuccessful
        return false;
    }

    private void SetSelectedUnit(Unit unit)
    {
        selectedUnit = unit;
        // Check whether EventHandler has listeners (otherwise returns null)
        OnSelectedUnitChanged?.Invoke(this, EventArgs.Empty);
    }
}
