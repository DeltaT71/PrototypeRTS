using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class UnitMovement : MonoBehaviour
{
    private Camera cam;
    public LayerMask ground;
    private NavMeshAgent agent;

    void Awake()
    {
        cam = Camera.main;
        agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        if (agent == null)
        {
            Debug.LogWarning($"Game Object {name} does not have NavMeshAgent Component.");
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ground))
            {
                agent.SetDestination(hit.point);
            }
        }
    }
}
