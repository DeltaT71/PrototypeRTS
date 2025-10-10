using UnityEngine;
using UnityEngine.EventSystems;

public class CameraMovement : MonoBehaviour
{
    [Header("General Settings")]
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float cameraSpeed;
    [SerializeField] private float cameraMovementSmoothing;
    [SerializeField] private float cameraRotationSmoothing;
    [SerializeField] private float mouseEdgeMovementThreshhold;

    [Header("Toggle Movement Systems")]
    [SerializeField] private bool toggleArrowKeysMovement;
    [SerializeField] private bool toggleWASDKeysMovement;
    [SerializeField] private bool toggleMouseEdgeMovement;
    [SerializeField] private bool toggleCameraDragMovement;
    [SerializeField] private bool toggleCameraRotation;

    private Vector3 newPosition;
    private bool isCursorSet;
    private ChangeCursor changeCursorCmp;
    private Vector3 startingMouseDragPosition;
    private Vector3 currentMouseDragPosition;
    private Quaternion newRotation;

    void Awake()
    {
        newPosition = transform.position;
        newRotation = transform.rotation;
        changeCursorCmp = GetComponent<ChangeCursor>();
    }

    void Update()
    {
        if (toggleMouseEdgeMovement) HandleMouseEdgeMovement();
        if (toggleArrowKeysMovement) HandleArrowKeysMovement();
        if (toggleWASDKeysMovement) HandleWASDKeysMovement();
        if (toggleCameraRotation) HandleCameraRotation();
        if (toggleCameraDragMovement) HandleMouseDragMovement();

        cameraTransform.position = Vector3.Lerp(cameraTransform.position, newPosition, Time.deltaTime * cameraMovementSmoothing);
        cameraTransform.rotation = Quaternion.Slerp(cameraTransform.rotation, newRotation, Time.deltaTime * cameraRotationSmoothing);

        Cursor.lockState = CursorLockMode.Confined;
    }

    private void HandleMouseEdgeMovement()
    {
        if (Input.mousePosition.x > Screen.width - mouseEdgeMovementThreshhold)
        {
            newPosition += cameraTransform.right * cameraSpeed * Time.deltaTime;
            changeCursorCmp.ChangeMouseCursor(CursorArrow.RIGHT);
            isCursorSet = true;
        }
        if (Input.mousePosition.x < mouseEdgeMovementThreshhold)
        {
            newPosition += cameraTransform.right * -cameraSpeed * Time.deltaTime;
            changeCursorCmp.ChangeMouseCursor(CursorArrow.LEFT);
            isCursorSet = true;
        }
        if (Input.mousePosition.y > Screen.height - mouseEdgeMovementThreshhold)
        {
            newPosition += cameraTransform.forward * cameraSpeed * Time.deltaTime;
            changeCursorCmp.ChangeMouseCursor(CursorArrow.UP);
            isCursorSet = true;
        }
        if (Input.mousePosition.y < mouseEdgeMovementThreshhold)
        {
            newPosition += cameraTransform.forward * -cameraSpeed * Time.deltaTime;
            changeCursorCmp.ChangeMouseCursor(CursorArrow.DOWN);
            isCursorSet = true;
        }
        if (CheckIfMouseIsNotOnEdge() && isCursorSet)
        {
            changeCursorCmp.ChangeMouseCursor(CursorArrow.DEFAULT);
            isCursorSet = false;
        }
    }

    private bool CheckIfMouseIsNotOnEdge()
    {
        // Checks if the cursor is not on one of the 4 Screen Edges.

        return Input.mousePosition.x < Screen.width - mouseEdgeMovementThreshhold &&
               Input.mousePosition.x > mouseEdgeMovementThreshhold &&
               Input.mousePosition.y < Screen.height - mouseEdgeMovementThreshhold &&
               Input.mousePosition.y > mouseEdgeMovementThreshhold;
    }

    private void HandleArrowKeysMovement()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            newPosition += cameraTransform.right * cameraSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            newPosition += cameraTransform.right * -cameraSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            newPosition += cameraTransform.forward * cameraSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            newPosition += cameraTransform.forward * -cameraSpeed * Time.deltaTime;
        }
    }
    private void HandleWASDKeysMovement()
    {
        if (Input.GetKey(KeyCode.D))
        {
            newPosition += cameraTransform.right * cameraSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            newPosition += cameraTransform.right * -cameraSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.W))
        {
            newPosition += cameraTransform.forward * cameraSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            newPosition += cameraTransform.forward * -cameraSpeed * Time.deltaTime;
        }
    }

    private void HandleCameraRotation()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            newRotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
        else if (Input.GetKey(KeyCode.E))
        {
            newRotation = Quaternion.Euler(new Vector3(0, -90, 0));
        }
        else if (newRotation != Quaternion.Euler(new Vector3(0, -45, 0)) && !Input.GetKey(KeyCode.E) && !Input.GetKey(KeyCode.Q))
        {
            newRotation = Quaternion.Euler(new Vector3(0, -45, 0));
        }
    }

    private void HandleMouseDragMovement()
    {
        if (Input.GetMouseButtonDown(2) && !EventSystem.current.IsPointerOverGameObject())
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            float entry;

            if (plane.Raycast(ray, out entry))
            {
                startingMouseDragPosition = ray.GetPoint(entry);
            }
        }
        if (Input.GetMouseButton(2) && !EventSystem.current.IsPointerOverGameObject())
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            float entry;

            if (plane.Raycast(ray, out entry))
            {
                currentMouseDragPosition = ray.GetPoint(entry);

                newPosition = transform.position + startingMouseDragPosition - currentMouseDragPosition;
            }
        }
    }
}
