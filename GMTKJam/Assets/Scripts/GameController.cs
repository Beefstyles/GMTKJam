using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    UnitBehaviour[] unitArray;


    private bool isBaseSelected;

    private int numberOfResources;

    public GameObject BaseObject, SoldierObject, SettlerObject, MinerObject;
    BaseActionController baseActionController;

    public int TurnNumber;
    public Transform ObjectSpawnLocation;
    HexGrid hexGrid;
    HexCoordinates spawnTargetCoords;
    GameUI gameUI;
    BaseController bc;
    PoliticsTracker pt;
    public GameObject SelectedObject;
    HandleElection electionHandler;

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
        pt = FindObjectOfType<PoliticsTracker>();
        baseActionController = FindObjectOfType<BaseActionController>();
        StartCoroutine("RefreshUnitArray");
        hexGrid = FindObjectOfType<HexGrid>();
        gameUI = FindObjectOfType<GameUI>();
        bc = FindObjectOfType<BaseController>();
        electionHandler = FindObjectOfType<HandleElection>();
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
                    StartCoroutine(gameUI.SetMessage("Move out unit from spawn Zone"));
                    break;
            }
        }
        else
        {
            switch (objectToBeSpawned.GetComponent<ObjectInfo>().ut)
            {
                case (UnitTypes.Miner):
                    NumberOfResources -= baseActionController.CostOfMiner;
                    pt.AlterPercentApproval(Random.Range(-2, 2), PoliticsParty.Warhawk);
                    pt.AlterPercentApproval(Random.Range(2, 5), PoliticsParty.Peacenik);
                    pt.AlterPercentApproval(Random.Range(0, 1), PoliticsParty.Balance);
                    break;
                case (UnitTypes.Settler):
                    pt.AlterPercentApproval(Random.Range(-2, 2), PoliticsParty.Warhawk);
                    pt.AlterPercentApproval(Random.Range(2, 5), PoliticsParty.Peacenik);
                    pt.AlterPercentApproval(Random.Range(0, 2), PoliticsParty.Balance);
                    NumberOfResources -= baseActionController.CostOfSettler;
                    break;
                case (UnitTypes.Soldier):
                    pt.AlterPercentApproval(Random.Range(2, 5), PoliticsParty.Warhawk);
                    pt.AlterPercentApproval(Random.Range(-4, -2), PoliticsParty.Peacenik);
                    pt.AlterPercentApproval(Random.Range(-2, 2), PoliticsParty.Balance);
                    NumberOfResources -= baseActionController.CostOfSoldier;
                    break;
            }
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
        if(TurnNumber % 10 == 0)
        {
            pt.CalculateElectionResult();
            electionHandler.CheckElection();
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
