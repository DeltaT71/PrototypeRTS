using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] private RectTransform selectionBox;
    [SerializeField] private LayerMask clickable;
    [SerializeField] private LayerMask ground;
    private Vector2 startMousePosition;

    void Update()
    {
        HandleSelectInput();
    }

    private void HandleSelectInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            selectionBox.sizeDelta = Vector2.zero;
            selectionBox.gameObject.SetActive(true);
            startMousePosition = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0) && Vector2.Distance(Input.mousePosition, startMousePosition) > 10f)
        {
            HandleBoxSelection();

        }
        else if (Input.GetMouseButtonUp(0))
        {
            selectionBox.sizeDelta = Vector2.zero;
            selectionBox.gameObject.SetActive(false);

            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            //Checks if we left clicked a unit.
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, clickable))
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    SelectionManager.Instance.AddUnitToSelection(hit.transform.parent.gameObject);
                }
                else
                {
                    SelectionManager.Instance.DeselectAllUnits();
                    SelectionManager.Instance.SelectUnit(hit.transform.parent.gameObject);
                }
            }
            else if (Physics.Raycast(ray, Mathf.Infinity, ground) && Vector2.Distance(Input.mousePosition, startMousePosition) < 10f && !Input.GetKey(KeyCode.LeftShift))
            {
                SelectionManager.Instance.DeselectAllUnits();
            }
        }
    }

    private void HandleBoxSelection()
    {
        DrawSelectionBox();
        Bounds bounds = new Bounds(selectionBox.anchoredPosition, selectionBox.sizeDelta);

        for (int i = 0; i < SelectionManager.Instance.AllSelectableUnits.Count; i++)
        {
            if (UnitInSelectionBox(camera.WorldToScreenPoint(SelectionManager.Instance.AllSelectableUnits[i].transform.position), bounds))
            {
                SelectionManager.Instance.SelectUnit(SelectionManager.Instance.AllSelectableUnits[i].transform.gameObject);
            }
            else
            {
                SelectionManager.Instance.DeselectUnit(SelectionManager.Instance.AllSelectableUnits[i].transform.gameObject);
            }
        }
    }

    private bool UnitInSelectionBox(Vector3 position, Bounds bounds)
    {
        return position.x > bounds.min.x && position.x < bounds.max.x
            && position.y > bounds.min.y && position.y < bounds.max.y;
    }

    private void DrawSelectionBox()
    {
        float width = Input.mousePosition.x - startMousePosition.x;
        float height = Input.mousePosition.y - startMousePosition.y;

        selectionBox.anchoredPosition = startMousePosition + new Vector2(width / 2, height / 2);
        selectionBox.sizeDelta = new Vector2(Mathf.Abs(width), Mathf.Abs(height));
    }
}


