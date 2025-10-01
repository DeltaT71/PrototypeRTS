using System;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    public Action<GameObject, bool> OnUnitSelectionChange;
    public Action<bool> ToggleGroundMarkerVisability;
    public static SelectionManager Instance { get; set; }
    public List<GameObject> AllSelectableUnits = new List<GameObject>();
    public HashSet<GameObject> SelectedUnits = new HashSet<GameObject>();

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void AddUnitToSelection(GameObject unit)
    {
        if (SelectedUnits.Contains(unit))
        {
            SelectedUnits.Remove(unit);
            SetUnitSelectionState(unit, false);
        }
        else
        {
            SelectedUnits.Add(unit);
            SetUnitSelectionState(unit, true);
        }
    }
    public void DeselectAllUnits()
    {
        foreach (var unit in SelectedUnits)
        {
            SetUnitSelectionState(unit, false);
        }
        ToggleGroundMarkerVisability?.Invoke(false);
        SelectedUnits.Clear();
    }

    public void SelectUnit(GameObject unit)
    {
        SelectedUnits.Add(unit);
        SetUnitSelectionState(unit, true);
    }
    public void DeselectUnit(GameObject unit)
    {
        SelectedUnits.Remove(unit);
        SetUnitSelectionState(unit, false);
    }

    private void ToggleUnitMovement(GameObject unit, bool canMove)
    {
        unit.GetComponent<UnitMovement>().enabled = canMove;
    }

    private void ToggleUnitSelectedIndicator(GameObject unit, bool showIndicator)
    {
        OnUnitSelectionChange?.Invoke(unit, showIndicator);
    }

    private void SetUnitSelectionState(GameObject unit, bool isSelected)
    {
        ToggleUnitMovement(unit, isSelected);
        ToggleUnitSelectedIndicator(unit, isSelected);
    }
}