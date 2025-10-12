using UnityEngine;

public class UnitMoveState : UnitBaseState
{
    private static float unitReachedDestinationCounter;
    public override void EnterState(UnitController unit)
    {
        if (IsUnitSelected(unit) && unit.CheckRightClickGround() && Input.GetMouseButtonDown(1))
        {
            unit.movementCmp.MoveToPosition();
            UnitMovement.stoppingRadius = unit.movementCmp.initialStoppingRadius;
            unitReachedDestinationCounter = 0;
        }
    }

    public override void UpdateState(UnitController unit)
    {
        if (IsUnitSelected(unit) && unit.CheckRightClickGround() && Input.GetMouseButtonDown(1))
        {
            UnitMovement.stoppingRadius = unit.movementCmp.initialStoppingRadius;
            unitReachedDestinationCounter = 0;

            if (unit.CheckMouseHoverOverEnemy())
            {
                unit.movementCmp.SetTargetToChase();
                unit.SwitchState(unit.chaseState);
                return;
            }
            else
            {
                unit.movementCmp.MoveToPosition();
                return;
            }

        }

        if (IsUnitSelected(unit) && unit.isCommandedToAttackMove && Input.GetMouseButtonUp(0))
        {

            unit.movementCmp.MoveToPosition();

            if (IsTargetInChaseRange(unit))
            {
                unit.SwitchState(unit.chaseState);
                return;
            }

            unit.isCommandedToAttackMove = false;
            return;
        }

        if (unit.isCommandedToHoldPosition)
        {
            unit.SwitchState(unit.idleState);
            return;
        }

        if (unit.movementCmp.ReachedDestination())
        {
            Debug.Log(unitReachedDestinationCounter);
            unitReachedDestinationCounter += unit.movementCmp.stoppingRadiusMultiplier;
            unit.isCommandedToMove = false;
            UnitMovement.stoppingRadius += unitReachedDestinationCounter;
            unit.SwitchState(unit.idleState);
            return;
        }
    }

    private bool IsTargetInChaseRange(UnitController unit)
    {
        return unit.targetToAttack != null && unit.isCommandedToMove == false;
    }
    private bool IsUnitSelected(UnitController unit)
    {
        return SelectionManager.Instance.SelectedUnits.Contains(unit.gameObject);
    }
}


