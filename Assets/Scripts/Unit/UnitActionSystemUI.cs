using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UnitActionSystemUI : MonoBehaviour
{

    [SerializeField] private Transform actionButtonPrefab;
    [SerializeField] private Transform actionButtonContainerTransform;

    private void Start()
    {
        UnitActionSystem.Instance.OnSelectedUnitChanged += UnitActionSystem_OnSelectedUnitChanged;
        CreateUnitActionButtons();
    }

    
    // Create UI buttons for each of the actions
    private void CreateUnitActionButtons()
    {
        DestroyCurrentButtons();
        Unit selectedUnit = UnitActionSystem.Instance.getSelectedUnit();
        if (selectedUnit == null) {
            return;
        }

        foreach(BaseAction baseAction in selectedUnit.GetBaseActionArray())
        {
            Transform button = Instantiate(actionButtonPrefab, actionButtonContainerTransform);
            // Assign the action to the button
            button.GetComponent<ActionButtonUI>().SetBaseAction(baseAction);
        }
    }

    private void DestroyCurrentButtons()
    {
        foreach(Transform buttonTransform in actionButtonContainerTransform)
        {
            Destroy(buttonTransform.gameObject);
        }
    }

    private void UnitActionSystem_OnSelectedUnitChanged(object sender,  EventArgs e)
    {
        CreateUnitActionButtons();
    }

}
