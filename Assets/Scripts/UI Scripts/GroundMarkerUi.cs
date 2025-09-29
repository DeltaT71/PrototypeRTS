using UnityEngine;

public class GroundMarkerUi : MonoBehaviour
{
    [SerializeField] private LayerMask ground;
    [SerializeField] private GameObject marker;

    void Start()
    {
        SelectionManager.Instance.ToggleGroundMarkerVisability += ToggleGroundMarkerVisability;
    }

    void Update()
    {
        SetGroundMarkerPosition();
    }

    void OnDestroy()
    {
        SelectionManager.Instance.ToggleGroundMarkerVisability -= ToggleGroundMarkerVisability;
    }

    private void ToggleGroundMarkerVisability(bool isActive)
    {
        marker.SetActive(isActive);
    }
    private void SetGroundMarkerPosition()
    {
        if (Input.GetMouseButtonUp(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            //Checks if we right clicked on the ground and we have selected units.
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ground) && SelectionManager.Instance.SelectedUnits.Count > 0)
            {
                gameObject.transform.position = hit.point;

                marker.SetActive(false);
                marker.SetActive(true);
            }
        }
    }
}
