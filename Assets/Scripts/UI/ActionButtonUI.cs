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

    private string actionName;

    public void SetBaseAction(BaseAction action)
    {
        textMeshPro.text = action.GetActionName();
        actionName = textMeshPro.text;
        button.onClick.AddListener(() =>
        {
            // Anonymous function code
            UnitActionSystem.Instance.SetSelectedAction(action);
        });
    }

    public void SetSelected()
    {
        selectedUI.gameObject.SetActive(true);
    }

    public void Deselect()
    {
        selectedUI.gameObject.SetActive(false);
    }

    public string GetName()
    {
        return actionName;
    }
}
