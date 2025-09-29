using UnityEngine;

public class SelectableUnit : MonoBehaviour
{
    [SerializeField] private GameObject unitSelectionIndicator;

    void Start()
    {
        SelectionManager.Instance.AllSelectableUnits.Add(gameObject);
        SelectionManager.Instance.OnUnitSelectionChange += HandleSelectionChanged;
    }

    void OnDestroy()
    {
        SelectionManager.Instance.AllSelectableUnits.Remove(gameObject);
        SelectionManager.Instance.OnUnitSelectionChange -= HandleSelectionChanged;
    }
    private void HandleSelectionChanged(GameObject unit, bool showIndicator)
    {
        //Inportant check otherwise we will invoke the selection indicators of all units.
        if (unit == gameObject)
        {
            unitSelectionIndicator.SetActive(showIndicator);
        }
    }
}
