using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class UnitActionSystemUI : MonoBehaviour
{
    // Singleton pattern
    public static UnitActionSystemUI Instance { get; private set; }

    // Canvas - contains all UI
    private GameObject UnitUIcanvas;

    [SerializeField] private Transform actionButtonPrefab;
    [SerializeField] private Transform actionButtonContainerTransform;
    [SerializeField] private TextMeshProUGUI actionPointsText;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There's modre than UnitActionSystemUI! " + this.transform + " - " + Instance);
            Destroy(this.gameObject);
            return;
        }
        Instance = this;

        UnitUIcanvas = this.transform.parent.gameObject;
        Assert.IsNotNull(UnitUIcanvas);
    }

    private void Start()
    {
        UnitActionSystem.Instance.OnSelectedUnitChanged += UnitActionSystem_OnSelectedUnitChanged;
        UnitActionSystem.Instance.OnSelectedActionChanged += UnitActionSystem_OnSelectedActionChanged;
        UnitActionSystem.Instance.OnActionStarted += UnitActionSystem_OnActionStarted;
        TurnSystem.Instance.OnNextTurnHappening += TurnSystem_OnNextTurnHappening;
        
        UpdateActionPoints();
        CreateUnitActionButtons();
        UpdateSelectedActionVisual();
        // Start with unselected actionPointtext
        actionPointsText.gameObject.SetActive(false);
    }

    // Create UI buttons for each of the actions
    private void CreateUnitActionButtons()
    {
        DestroyCurrentButtons();
        Unit selectedUnit = UnitActionSystem.Instance.GetSelectedUnit();
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

    // Removes current buttons
    private void DestroyCurrentButtons()
    {
        foreach(Transform buttonTransform in actionButtonContainerTransform)
        {
            Destroy(buttonTransform.gameObject);
        }
    }

    // Listen to next turn happening
    private void TurnSystem_OnNextTurnHappening(object sender, EventArgs e)
    {
        UpdateActionPoints();
    }

    // On selected unit changed
    private void UnitActionSystem_OnSelectedUnitChanged(object sender,  EventArgs e)
    {
            UpdateActionPoints();
            CreateUnitActionButtons();
            UpdateSelectedActionVisual();
    }

    // On action changed
    private void UnitActionSystem_OnSelectedActionChanged(object sender, EventArgs e)
    {
        UpdateSelectedActionVisual();
    }

    // On taking an action
    private void UnitActionSystem_OnActionStarted(object sender, EventArgs e)
    {
        UpdateActionPoints();
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
        bool isUIActive = UnitUIcanvas.activeSelf;
        UnitUIcanvas.SetActive(!isUIActive);
    }

    private void HideUnitUI()
    {
        UnitUIcanvas.SetActive(false);
        Debug.Log("Hidning UI: " +  UnitUIcanvas);
    }

    private void ShowUnitUI()
    {
        UnitUIcanvas.SetActive(true);
    }

    // Updates UI text for number of actions
    private void UpdateActionPoints()
    {
        actionPointsText.gameObject.SetActive(true);

        Unit selectedUnit = UnitActionSystem.Instance.GetSelectedUnit();
        //Debug.Log("Unit has: " + selectedUnit.GetActionPoints() + " action points.");
        if (selectedUnit == null) return;

        actionPointsText.text = "Action Points: " + selectedUnit.GetActionPoints();
    }

}
