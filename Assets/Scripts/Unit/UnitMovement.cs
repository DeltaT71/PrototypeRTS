using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class UnitMovement : MonoBehaviour
{
    private Camera cam;
    public LayerMask groundLayer;
    public LayerMask clickable;
    private NavMeshAgent agent;
    public GameObject chaseTarget;
    public bool isMoving;
    public static float stoppingRadius;
    //public float stoppingDistance;
    public float stoppingRadiusMultiplier;
    public float initialStoppingRadius;

    void Awake()
    {
        cam = Camera.main;
        agent = GetComponent<NavMeshAgent>();
        isMoving = false;
    }

    void Start()
    {
        if (agent == null)
        {
            Debug.LogWarning($"Game Object {name} does not have NavMeshAgent Component.");
        }
    }

    public void MoveToPosition()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
        {
            agent.SetDestination(hit.point);
            isMoving = true;
        }

    }
    public void StopMovement()
    {
        agent.ResetPath();
    }
    public void SetTargetToChase()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, clickable))
        {
            chaseTarget = hit.transform.gameObject;
        }
    }

    public bool ReachedDestination()
    {
        //stoppingRadius = Mathf.Sqrt(SelectionManager.Instance.SelectedUnits.Count * stoppingDistance);
        // if (agent.pathPending) return false;
        // if (agent.remainingDistance >= agent.stoppingDistance) return false;
        // if (agent.hasPath || agent.velocity.sqrMagnitude != 0f) return false;
        // return true;
        print(agent.hasPath && agent.remainingDistance < stoppingRadius);
        return agent.hasPath && agent.remainingDistance < stoppingRadius;
    }
    public void StoppingRadiusMultiplier()
    {
        stoppingRadius += stoppingRadiusMultiplier;
    }
    public void ChaseTarget(Vector3 target)
    {
        agent.SetDestination(target);
    }
}
