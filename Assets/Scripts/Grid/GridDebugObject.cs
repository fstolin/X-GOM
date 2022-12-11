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
        if (gridObject == null)
        {
            Debug.Log("GridObejct is null; " + transform.parent);
        } else
        {
            Debug.Log("GridObject GOOD" + transform.parent);
        }

        if (textField == null)
        {
            Debug.Log("Text is null;" + transform.parent);
        }
        else
        {
            Debug.Log("Text GOOD" + transform.parent);
        }

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
