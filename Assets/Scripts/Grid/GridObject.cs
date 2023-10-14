using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject
{
    
    private GridSystem gridSystem;
    private GridPosition gridPosition;
    private List<Unit> unitList;

    public GridObject(GridPosition gridPosition, GridSystem gridSystem)
    {
        this.gridPosition = gridPosition;
        this.gridSystem = gridSystem;
        unitList = new List<Unit>();
    }

    public void AddUnit(Unit unit)
    {
        unitList.Add(unit);
    }

    public void RemoveUnit(Unit unit)
    {
        unitList.Remove(unit);
    }

    public List<Unit> GetUnitList()
    {
        return unitList;
    }

    public bool HasAnyUnit()
    {
        return unitList.Count > 0;
    }

    public Unit GetUnit()
    {
        if (HasAnyUnit())
        {
            Debug.Assert(GetUnitList().Count == 1, "Assertion failed! There are more than 1 units on 1 tile.");
            return GetUnitList()[0];
        } else
        {
            return null;
        }
    }

    public bool IsGridTileEmpty()
    {
        return unitList.Count == 0;
    }

    public override string ToString()
    {
        //// Empty unit list
        //if (unitList.Count == 0) return gridPosition.ToString();
        // There are units on the gridObject
        string unitString = "";
        foreach (Unit unit in unitList)
        {
            unitString += unit.name + "\n";
        }
        return gridPosition.ToString() + "\n" + unitString;        
    }


}
