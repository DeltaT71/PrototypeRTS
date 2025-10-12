using UnityEngine;

public class UnitIdleState : UnitBaseState
{
    public override void EnterState(UnitController unit)
    {
        unit.movementCmp.isMoving = false;
        unit.movementCmp.StopMovement();
    }

    public override void UpdateState(UnitController unit)
    {
        if (IsUnitSelected(unit) && Input.GetMouseButtonDown(1))
        {
            unit.isCommandedToHoldPosition = false;
            if (unit.CheckMouseHoverOverEnemy())
            {
                unit.movementCmp.SetTargetToChase();
                unit.SwitchState(unit.chaseState);
                return;
            }
            else
            {
                unit.SwitchState(unit.movementState);
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
            unit.movementCmp.StopMovement();
            return;
        }

        if (IsTargetInChaseRange(unit))
        {
            unit.SwitchState(unit.chaseState);
            return;
        }

        if (unit.movementCmp.chaseTarget != null)
        {
            unit.SwitchState(unit.chaseState);
        }

        // if (unit.movementCmp.ReachedDestination())
        // {
        //     unit.isCommandedToMove = false;
        //     unit.movementCmp.StopMovement();
        //     return;
        // }
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
