using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ShootAction : BaseAction
{

    [SerializeField] private int maxShootDistance = 7;
    [SerializeField] private float shootingStateTime = 0.1f;
    [SerializeField] private float aimingStateTime = 1.0f;
    [SerializeField] private float cooloffStateTime = 0.5f;
    // Shooting states 
    private enum State
    {
        Aiming,
        Shooting,
        Cooloff,
    }
    private State state;
    private float stateTimer;
    private Unit targetUnit;
    private Quaternion unitQuaternion;
    private Quaternion targetQuaternion;
    private bool canShootBullet;
    private bool isAimingFinished;
    private float timeFloat = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        if (!isActive) return;

        stateTimer-= Time.deltaTime;

        if (!isAimingFinished) LookAtTarget();

        if (state == State.Shooting && canShootBullet)
        {
            Shoot();
            canShootBullet = false;
        }

        if (stateTimer <= 0f)
        {
            NextState();
        }
    }

    private void NextState()
    {
        switch (state)
        {
            case State.Aiming:
                state = State.Shooting;
                stateTimer = shootingStateTime;
                break;
            case State.Shooting:
                state = State.Cooloff;
                stateTimer = cooloffStateTime;
                break;
            case State.Cooloff:
                ActionComplete();
                break;
        }
    }

    private void Shoot()
    {
        targetUnit.Damage();
    }

    public override string GetActionName()
    {
        return "SHOOT";
    }

    public override List<GridPosition> GetValidActionGridPositionList()
    {
        List<GridPosition> validGridPositionList = new List<GridPosition>();
        if (unit == null) return validGridPositionList;

        // Unit grid position
        GridPosition unitGridPosition = unit.GetGridPosition();

        // Cycle offsets
        for (int x = -maxShootDistance; x <= maxShootDistance; x++)
        {
            for (int z = -maxShootDistance; z <= maxShootDistance; z++)
            {
                GridPosition offsetGridPosition = new GridPosition(x, z);
                GridPosition inRangeGridPosition = unitGridPosition + offsetGridPosition;
                // Do nothing if position is invalid
                if (!LevelGrid.Instance.IsValidGridPosition(inRangeGridPosition))
                {
                    continue;
                }

                // Distance check - circular range
                int testDistance = Mathf.Abs(x) + Mathf.Abs(z);
                if (testDistance > maxShootDistance)
                {
                    continue;
                }

                // Occupied slots = valid
                if (!LevelGrid.Instance.HasAnyUnitsOnGridPosition(inRangeGridPosition))
                {
                    continue;
                }
       
                Unit targetUnit = LevelGrid.Instance.GetUnitAtGridPosition(inRangeGridPosition);
                // Do not add friendly units - compare this unit with target unit
                if (targetUnit.IsEnemy() == unit.IsEnemy())
                {
                    continue;
                }

                // Add it to list if it's valid
                validGridPositionList.Add(inRangeGridPosition);
            }
        }

        return validGridPositionList;
    }

    public override void TakeAction(GridPosition gridPosition, Action onActionComplete)
    {
        // assign spinComplete reference to this Spin Action
        ActionStart(onActionComplete);

        targetUnit = LevelGrid.Instance.GetUnitAtGridPosition(gridPosition);
        targetQuaternion = Quaternion.LookRotation(targetUnit.transform.position - unit.transform.position);
        unitQuaternion = unit.transform.rotation;


        state = State.Aiming;
        stateTimer = aimingStateTime;
        canShootBullet = true;
    }

    private void LookAtTarget()
    {
        float rotationTime = 0.35f;
        unit.transform.rotation = Quaternion.Slerp(unitQuaternion, targetQuaternion, timeFloat);
        timeFloat += Time.deltaTime * (1 / rotationTime);
        if (timeFloat > 1) isAimingFinished = true;
    }
}
