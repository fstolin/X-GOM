using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UnitActionSystemUI : MonoBehaviour
{
    // Singleton pattern
    public static UnitActionSystemUI Instance { get; private set; }

    [SerializeField] private Transform actionButtonPrefab;
    [SerializeField] private Transform actionButtonContainerTransform;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There's modre than UnitActionSystemUI! " + this.transform + " - " + Instance);
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        UnitActionSystem.Instance.OnSelectedUnitChanged += UnitActionSystem_OnSelectedUnitChanged;
        UnitActionSystem.Instance.OnSelectedActionChanged += UnitActionSystem_OnSelectedActionChanged;
        CreateUnitActionButtons();
        UpdateSelectedActionVisual();
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
        UpdateSelectedActionVisual();
    }

    private void UnitActionSystem_OnSelectedActionChanged(object sender, EventArgs e)
    {
        UpdateSelectedActionVisual();
    }

    // Updates the visual of the selected action to be green
    private void UpdateSelectedActionVisual()
    {
        BaseAction selectedAction = UnitActionSystem.Instance.getSelectedAction();

        foreach(Transform buttonTransform in actionButtonContainerTransform)
        {
            ActionButtonUI ui = buttonTransform.gameObject.GetComponent<ActionButtonUI>();
            if (selectedAction == null)
            {
                ui.SetDeselected();
            }
            else if (ui.GetButtonAction() == selectedAction)
            {
                ui.SetSelected();
            }
            else
            {
                ui.SetDeselected();
            }
        }
    }

    // Toggles the UI to the busy state. In my case -> no UI
    public void ToggleBusyUI()
    {
        bool isUIActive = actionButtonContainerTransform.gameObject.activeSelf;
        actionButtonContainerTransform.gameObject.SetActive(!isUIActive);
    }

}
