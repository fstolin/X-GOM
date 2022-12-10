using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitActionSystem : MonoBehaviour
{

    [SerializeField] private Unit selectedUnit;
    [SerializeField] private LayerMask unitLayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Handle the selection of units
        HandleUnitSelection();  

        // Move selectedUnit to a new place after clicking the mouse
        if (Input.GetMouseButtonDown(1))
        {
            selectedUnit    .Move(MouseWorld.GetMouseWorldPosition());
        }
    }

    // Tries to select a Unit and get it's unit component on mous click
    private void HandleUnitSelection()
    {
        // Get mouse button input
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, float.MaxValue, unitLayer))
            {
                // Try get the Unit component, if we found it, select the unit
                if (hitInfo.transform.TryGetComponent<Unit>(out Unit unit)) { 
                    selectedUnit = unit;
                }
            }
        }
    }
}
