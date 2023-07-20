using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ActionButtonUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshPro;
    [SerializeField] private Button button;

    public void SetBaseAction(BaseAction action)
    {
        textMeshPro.text = action.GetActionName();
        button.onClick.AddListener(() =>
        {
            // Anonymous function code
            UnitActionSystem.Instance.SetSelectedAction(action);
        });
    }
}
