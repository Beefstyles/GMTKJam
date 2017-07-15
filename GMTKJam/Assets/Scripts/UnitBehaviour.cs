using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBehaviour : MonoBehaviour {

    private UnitTypes ut;
    public int NumberOfActions = 1;
    private int maxActions = 1;
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
        SetHexCoords();
        AddLocationToDict();
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
        RemoveLocationFromDict();
        SetHexCoords();
        hexGrid.SelectedUnitCoords = hexCoords;
        gc.SelectedObject = gameObject;
        AddLocationToDict();
    }

    private void SetHexCoords()
    {
        hexCoords = hexGrid.ReturnHexCoords(transform.position);
    }

    private void AddLocationToDict()
    {
        OverallHexCoordsDict.GameDictionary.Add(hexCoords, ut);
    }

    public void RemoveLocationFromDict()
    {
        OverallHexCoordsDict.GameDictionary.Remove(hexCoords);
    }

    void RemoveFromHexGridSelection()
    {
        gc.SelectedObject = null;
    }

    public void DestroySelf(HexCoordinates victimCoords)
    {
        if(victimCoords.ToString() == hexCoords.ToString())
        {
            RemoveLocationFromDict();
            Destroy(this.gameObject);
        }
    }

    public void HandleEnemyMovement()
    {
        if(ut == UnitTypes.Enemy)
        {
            StartCoroutine("EnemyControl");
        }
    }

    IEnumerator EnemyControl()
    {
        yield return new WaitForSeconds(0.1F);
    }

    public void ResetOnTurn()
    {
        NumberOfActions = maxActions;
    }

}
