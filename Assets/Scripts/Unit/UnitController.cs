using System;
using UnityEditor;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    public UnitBaseState currentState;
    public LayerMask groundLayer;
    public LayerMask clickable;
    public bool cursorAttackIcon;
    public bool isCommandedToMove;
    public bool isCommandedToHoldPosition;
    public bool isCommandedToAttackMove;
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
        if (SelectionManager.Instance.SelectedUnits.Count > 0)
        {
            CheckMouseHoverOverEnemy();
        }
        if (SelectionManager.Instance.SelectedUnits.Contains(gameObject) && Input.GetKeyUp(KeyCode.A))
        {
            ToggleAttackMove();
        }
        if (SelectionManager.Instance.SelectedUnits.Contains(gameObject) && Input.GetKeyUp(KeyCode.H))
        {
            ToggleHoldPosition();
        }
        if (SelectionManager.Instance.SelectedUnits.Contains(gameObject) && Input.GetMouseButtonUp(1))
        {
            CancellAllUnitCommands();
        }

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
    public bool CheckMouseHoverOverEnemy()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, Mathf.Infinity, clickable))
        {
            cursorAttackIcon = true;
            return true;
        }
        cursorAttackIcon = false;
        return false;
    }

    public void ToggleAttackMove()
    {
        isCommandedToAttackMove = true;
        isCommandedToHoldPosition = false;
    }
    private void ToggleHoldPosition()
    {
        isCommandedToHoldPosition = true;
        isCommandedToAttackMove = false;
    }
    private void CancellAllUnitCommands()
    {
        isCommandedToHoldPosition = false;
        isCommandedToAttackMove = false;
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

    void OnDrawGizmos()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, groundLayer);

        if (Input.GetMouseButtonUp(1))
        {
            Gizmos.DrawWireSphere(hit.point, UnitMovement.stoppingRadius);
        }
    }

    public float CalculateDistanceFromEnemy(Vector3 enemyPosition)
    {
        return Vector3.Distance(enemyPosition, gameObject.transform.position);
    }
}
