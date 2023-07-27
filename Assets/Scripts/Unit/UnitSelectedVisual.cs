using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UnitSelectedVisual : MonoBehaviour
{
    // This unit
    [SerializeField] private Unit unit;

    private MeshRenderer meshRenderer;

    private void Awake()
    {
        meshRenderer = this.GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        UnitActionSystem.Instance.OnSelectedUnitChanged += UnitActionSystem_OnSelectedUnitChanged;
        // Update visuals right away.
        UpdateVisuals();
    }

    // Listening to on Selected Unit Chaged -> compare, whether the selected unit is this object
    private void UnitActionSystem_OnSelectedUnitChanged(object sender, EventArgs empty)
    {
        UpdateVisuals();
    }

    private void UpdateVisuals()
    {
        Unit selectedUnit = UnitActionSystem.Instance.GetSelectedUnit();
        // If this unit is selected - enable meshrenderer of the circle
        if (unit == selectedUnit)
        {
            meshRenderer.enabled = true;
        }
        else
        {
            meshRenderer.enabled = false;
        }
    }
}
