using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    UnitBehaviour[] unitArray;


    private bool isBaseSelected;

    private int numberOfResources;

    public GameObject BaseObject, SoldierObject, SettlerObject, MinerObject;

    public int TurnNumber;
    public Transform ObjectSpawnLocation;
    HexGrid hexGrid;
    HexCoordinates spawnTargetCoords;
    GameUI gameUI;
    BaseController bc;

    public GameObject SelectedObject;

    public bool IsBaseSelected
    {
        get
        {
            return isBaseSelected;
        }

        set
        {
            isBaseSelected = value;
        }
    }

    public int NumberOfResources
    {
        get
        {
            return numberOfResources;
        }

        set
        {
            numberOfResources = value;
        }
    }

    public bool GetIsObjectSelected()
    {
        return isObjectSelected;
    }

    public void SetIsObjectSelected(bool value)
    {
        isObjectSelected = value;
    }

    private bool isObjectSelected;

    void Start ()
    {
        StartCoroutine("RefreshUnitArray");
        hexGrid = FindObjectOfType<HexGrid>();
        gameUI = FindObjectOfType<GameUI>();
        bc = FindObjectOfType<BaseController>();
    }

    public void DeselectAllUnits()
    {
        if (unitArray.Length > 1)
        {
            foreach (var unit in unitArray)
            {
                unit.DeSelectUnit();
            }
        }
        
    }

    public IEnumerator RefreshUnitArray()
    {
        yield return new WaitForSeconds(0.001F);
        unitArray = FindObjectsOfType<UnitBehaviour>();
    }


    void StartGame()
    {
        
    }

    public void ObjectSpawner(GameObject objectToBeSpawned, Vector3 location)
    {
        spawnTargetCoords = hexGrid.ReturnHexCoords(location);
        UnitTypes spawnUt;
        if (OverallHexCoordsDict.GameDictionary.TryGetValue(spawnTargetCoords, out spawnUt))
        {
            switch (spawnUt)
            {
                default:
                    StartCoroutine(gameUI.SetMessage("Move Out unit from spawn Zone"));
                    break;
            }
        }
        else
        {
            Instantiate(objectToBeSpawned, location, Quaternion.identity);
            bc.NumberOfActionsRemaining--;
        }
        
    }

    public void CheckDeathCoords(HexCoordinates victimCoords)
    {
        foreach (var unit in unitArray)
        {
            unit.DestroySelf(victimCoords);
        }
    }


    public void NextTurn()
    {
        TurnNumber++;
        bc.ResetOnTurn();
        foreach (var unit in unitArray)
        {
            unit.ResetOnTurn();
        }
    }

    public void UpgradeSelectedUnit()
    {
        if(SelectedObject != null)
        {
            SelectedObject.GetComponent<UnitBehaviour>().HandleUpgrade();
        }
    }

    public void PerformActionSelectedUnit()
    {
        if (SelectedObject != null)
        {
            SelectedObject.GetComponent<UnitBehaviour>().HandleAction();
        }
    }

}
