using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveAction : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float stoppingDistance = .05f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private int maxMoveDistance = 4;
    [SerializeField] private Animator unitAnimator;

    private Vector3 targetPosition;
    private Unit unit;
    private bool isActive;

    private void Awake()
    {
        targetPosition = transform.position;
        unit = GetComponent<Unit>();
    }

    private void Update()
    {
        // Only move, when the move action is active.
        if (!isActive) return;

        // We calculate normalized direction & then move the unit in that direction
        Vector3 moveDirection = (targetPosition - transform.position).normalized;

        // Running - Prevent the unit from moving after it's almost at the position
        if (Vector3.Distance(transform.position, targetPosition) > stoppingDistance)
        {            
            transform.position += moveDirection * Time.deltaTime * moveSpeed;
            // Animation
            unitAnimator.SetBool("isRunning", true);
        }
        // Stopped
        else
        {
            unitAnimator.SetBool("isRunning", false);
            this.isActive = false;
        }
        // Rotation
        transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * rotationSpeed);
    }

    public void Move(GridPosition gridPosition)
    {
        this.targetPosition = LevelGrid.Instance.GetWorldPosition(gridPosition);
        this.isActive = true;
    }

    // Checks whether a position is valid for move action
    public bool IsValidMoveActionPosition(GridPosition gridPosition)
    {
        return GetValidActionGridPositionList().Contains(gridPosition);
    }

    // Returns a list of valid grid positions for momvement.
    public List<GridPosition> GetValidActionGridPositionList()
    {
        List<GridPosition> validGridPositionList = new List<GridPosition>();
        if (unit == null) return validGridPositionList;

        // Unit grid position
        GridPosition unitGridPosition = unit.GetGridPosition();

        // Cycle offsets
        for (int x = -maxMoveDistance; x <= maxMoveDistance; x++)
        {
            for (int z = -maxMoveDistance; z <= maxMoveDistance; z++)
            {
                GridPosition offsetGridPosition = new GridPosition(x, z);
                GridPosition inRangeGridPosition = unitGridPosition + offsetGridPosition;
                // Do nothing if position is invalid
                if (!LevelGrid.Instance.IsValidGridPosition(inRangeGridPosition))
                {
                    continue;
                }
                // Do nothing if inRangeGridPosition is same as unit position
                if (unitGridPosition == inRangeGridPosition)
                {
                    continue;
                }
                // Occupied slots
                if (LevelGrid.Instance.HasAnyUnitsOnGridPosition(inRangeGridPosition))
                {
                    continue;
                }

                // Add it to list if it's valid
                validGridPositionList.Add(inRangeGridPosition);
            }
        }

        return validGridPositionList;
    }

}
