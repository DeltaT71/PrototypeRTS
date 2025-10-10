using UnityEngine;

public class UnitMoveState : UnitBaseState
{
    public override void EnterState(UnitController unit)
    {
        if (IsUnitSelected(unit) && unit.CheckRightClickGround() && Input.GetMouseButtonDown(1))
        {
            unit.movementCmp.MoveToPosition();
        }
    }

    public override void UpdateState(UnitController unit)
    {
        if (IsUnitSelected(unit) && unit.CheckRightClickGround() && Input.GetMouseButtonDown(1))
        {
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


