using System;
using NUnit.Framework;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    public UnitBaseState currentState;
    public bool isCommandedToMove;
    [NonSerialized] public Transform targetToAttack;
    public UnitIdleState idleState = new UnitIdleState();
    public UnitChaseState chaseState = new UnitChaseState();
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

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && other.transform.parent != transform && targetToAttack == null)
        {
            targetToAttack = other.transform;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy") && other.transform.parent != transform && targetToAttack != null)
        {
            targetToAttack = null;
        }
    }
}
