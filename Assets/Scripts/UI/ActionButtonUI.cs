using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ActionButtonUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshPro;
    [SerializeField] private Button button;
    [SerializeField] private Transform selectedUI;

    private BaseAction baseAction;

    // Sets the base action for the button
    // This action is kept for future reference
    public void SetBaseAction(BaseAction action)
    {
        textMeshPro.text = action.GetActionName();
        this.baseAction = action;
        button.onClick.AddListener(() =>
        {
            // Anonymous function code
            UnitActionSystem.Instance.SetSelectedAction(action);
        });
    }

    // Sets green visual around the action -> selected
    public void SetSelected()
    {
        selectedUI.gameObject.SetActive(true);
    }

    // Disables the green visual around the action -> deselected
    public void SetDeselected()
    {
        selectedUI.gameObject.SetActive(false);
    }

    public BaseAction GetButtonAction()
    {
        return baseAction;
    }
}
