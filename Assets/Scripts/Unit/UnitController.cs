using System;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    public UnitBaseState currentState;
    public LayerMask groundLayer;
    public LayerMask clickable;
    public bool isCommandedToMove;
    public float attackRange;
    [NonSerialized] public Transform targetToAttack;
    public UnitIdleState idleState = new UnitIdleState();
    public UnitChaseState chaseState = new UnitChaseState();
    public UnitMoveState movementState = new UnitMoveState();
    public UnitAttackState attackState = new UnitAttackState();
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

    public bool CheckRightClickGround()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, Mathf.Infinity, groundLayer))
        {
            return true;
        }

        return false;
    }
    public bool CheckRightClickEnemy()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, Mathf.Infinity, clickable))
        {
            return true;
        }

        return false;
    }

    public void ChasteTargetCommand()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit, Mathf.Infinity);

        print(hit.transform.gameObject.name);

        if (hit.transform.gameObject.CompareTag("Enemy"))
        {
            targetToAttack = hit.transform;
            print(targetToAttack);
        }
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

    public float CalculateDistanceFromEnemy(Vector3 enemyPosition)
    {
        return Vector3.Distance(enemyPosition, gameObject.transform.position);
    }
}
