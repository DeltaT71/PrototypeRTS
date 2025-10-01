using System;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    public UnitBaseState currentState;
    public UnitIdleState idleState = new UnitIdleState();
    public UnitMoveState movementState = new UnitMoveState();
    [NonSerialized] public UnitMovement movementCmp;

    void Awake()
    {
        movementCmp = GetComponent<UnitMovement>();
        currentState = idleState;

    }
    void Start()
    {
        currentState.EnterState(this);
    }
    void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(UnitBaseState newState)
    {
        currentState = newState;
        currentState.EnterState(this);
    }
}
