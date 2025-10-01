using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class UnitMovement : MonoBehaviour
{
    private Camera cam;
    public LayerMask ground;
    private NavMeshAgent agent;
    public bool isMoving;

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

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, ground))
        {
            agent.SetDestination(hit.point);
            isMoving = true;
        }
    }

    public bool ReachedDestination()
    {
        if (agent.pathPending) return false;
        if (agent.remainingDistance > agent.stoppingDistance) return false;
        //if (agent.hasPath || agent.velocity.sqrMagnitude != 0f) return false;

        return true;
    }
}
