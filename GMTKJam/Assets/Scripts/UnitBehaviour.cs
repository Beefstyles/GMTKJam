﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBehaviour : MonoBehaviour {

    private UnitTypes ut;
    public int NumberOfMoves = 1;
    public int NumberOfDice = 1;
    public int NumberOfAttacks = 1;
    public bool IsSelected;
    MeshRenderer mr;
    public Material ObjectSelected, ObjectNotSelected;
    public HexCoordinates hexCoords;
    HexGrid hexGrid;
    GameController gc;
 
    void Start()
    {
        ut = GetComponent<ObjectInfo>().ut;
        hexGrid = FindObjectOfType<HexGrid>();
        gc = FindObjectOfType<GameController>();
        mr = GetComponent<MeshRenderer>();
        mr.material = ObjectNotSelected;
    }

	public void SelectUnit()
    {
        if (!IsSelected)
        {
            IsSelected = true;
            mr.material = ObjectSelected;
            FindCellLocation();
            gc.SetIsObjectSelected(true);
        }
        else
        {
            DeSelectUnit();
        }
    }

    public void DeSelectUnit()
    {
        if (IsSelected)
        {
            IsSelected = false;
            mr.material = ObjectNotSelected;
            RemoveFromHexGridSelection();
            gc.SetIsObjectSelected(false);
        }
    }

    public void FindCellLocation()
    {
        hexCoords = hexGrid.ReturnHexCoords(transform.position);
        hexGrid.SelectedUnitCoords = hexCoords;
        hexGrid.SelectedUnit = gameObject;
    }

    void RemoveFromHexGridSelection()
    {
        hexGrid.SelectedUnit = null;
    }


}
