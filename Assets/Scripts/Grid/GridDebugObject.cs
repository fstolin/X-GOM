using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GridDebugObject : MonoBehaviour
{
    [SerializeField] private TextMeshPro textField;

    private GridObject gridObject;

    private void Update()
    {
        Debug.Assert(gridObject != null);
        Debug.Assert(textField != null);

        UpdateGridObjectLabel();
    }

    public void SetGridObject(GridObject gridObject)
    {
        this.gridObject = gridObject;
    }

    private void UpdateGridObjectLabel()
    {
        textField.text = gridObject.ToString();
    }
}
