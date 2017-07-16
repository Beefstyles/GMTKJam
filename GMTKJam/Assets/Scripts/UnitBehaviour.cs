using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBehaviour : MonoBehaviour {

    private UnitTypes ut;
    public int NumberOfActions = 1;
    private int maxActions = 1;
    public int CostToUpgrade;
    public int CostForAction;
    public bool IsSelected;
    MeshRenderer mr;
    public Material ObjectSelected, ObjectNotSelected;
    public HexCoordinates hexCoords;
    HexGrid hexGrid;
    GameController gc;
    GameUI gameUI;
    PoliticsTracker pt;
    public GameObject MineObject, OutpostObject;
 
    void Start()
    {
        ut = GetComponent<ObjectInfo>().ut;
        hexGrid = FindObjectOfType<HexGrid>();
        gameUI = FindObjectOfType<GameUI>();
        pt = FindObjectOfType<PoliticsTracker>();

        gc = FindObjectOfType<GameController>();
        mr = GetComponent<MeshRenderer>();
        mr.material = ObjectNotSelected;
        SetHexCoords();
        AddLocationToDict();
        DetermineCostForAction();
        DetermineCostForUpgrade();
    }

    void DetermineCostForAction()
    {
        switch (ut)
        {
            case UnitTypes.Soldier:
                break;
            case UnitTypes.Miner:
                CostForAction = 15;
                break;
            case UnitTypes.Settler:
                CostForAction = 10;
                break;
            case UnitTypes.Base:
                break;
            case UnitTypes.Enemy:
                break;
            case UnitTypes.Outpost:
                break;
            case UnitTypes.Mine:
                break;
            default:
                break;
        }
    }

    void DetermineCostForUpgrade()
    {
        switch (ut)
        {
            case UnitTypes.Soldier:
                CostToUpgrade = 10;
                break;
            case UnitTypes.Miner:
                CostToUpgrade = 5;
                break;
            case UnitTypes.Settler:
                CostToUpgrade = 5;
                break;
            case UnitTypes.Base:
                CostToUpgrade = 20;
                break;
            case UnitTypes.Enemy:
                break;
            case UnitTypes.Outpost:
                break;
            case UnitTypes.Mine:
                break;
            default:
                break;
        }
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
        HandleEnemyMovement();
    }

    public void HandleUpgrade()
    {
        if(gc.NumberOfResources >= CostToUpgrade)
        {
            gc.NumberOfResources -= CostToUpgrade;
            switch (gc.SelectedObject.GetComponent<ObjectInfo>().ut)
            {
                case (UnitTypes.Miner):
                    pt.AlterPercentApproval(Random.Range(-2, 2), PoliticsParty.Warhawk);
                    pt.AlterPercentApproval(Random.Range(1, 3), PoliticsParty.Peacenik);
                    pt.AlterPercentApproval(Random.Range(-3, 3), PoliticsParty.Balance);
                    break;
                case (UnitTypes.Soldier):
                    pt.AlterPercentApproval(Random.Range(1, 3), PoliticsParty.Warhawk);
                    pt.AlterPercentApproval(Random.Range(-1, -3), PoliticsParty.Peacenik);
                    pt.AlterPercentApproval(Random.Range(-3, 3), PoliticsParty.Balance);
                    break;
                case (UnitTypes.Settler):
                    pt.AlterPercentApproval(Random.Range(1, 3), PoliticsParty.Warhawk);
                    pt.AlterPercentApproval(Random.Range(-1, -3), PoliticsParty.Peacenik);
                    pt.AlterPercentApproval(Random.Range(-3, 3), PoliticsParty.Balance);
                    break;
            }
            StartCoroutine(gameUI.SetMessage("Upgraded: " + gc.SelectedObject.GetComponent<ObjectInfo>().ut.ToString()));
            NumberOfActions++;
            maxActions++;
        }
    }

    public void HandleAction()
    {
        if (gc.NumberOfResources >= CostForAction)
        {
            gc.NumberOfResources -= CostForAction;
            switch (gc.SelectedObject.GetComponent<ObjectInfo>().ut)
            {
                case (UnitTypes.Miner):
                    pt.AlterPercentApproval(Random.Range(-2, 2), PoliticsParty.Warhawk);
                    pt.AlterPercentApproval(Random.Range(2, 5), PoliticsParty.Peacenik);
                    pt.AlterPercentApproval(Random.Range(0, 3), PoliticsParty.Balance);
                    HandleObjectSpawn();
                    break;
                case (UnitTypes.Soldier):
                    break;
                case (UnitTypes.Settler):
                    pt.AlterPercentApproval(Random.Range(0, 2), PoliticsParty.Warhawk);
                    pt.AlterPercentApproval(Random.Range(-2, 3), PoliticsParty.Peacenik);
                    pt.AlterPercentApproval(Random.Range(0, 5), PoliticsParty.Balance);
                    HandleObjectSpawn();
                    break;
            }
            StartCoroutine(gameUI.SetMessage("Actioned : " + gc.SelectedObject.GetComponent<ObjectInfo>().ut.ToString()));
        }
    }

    private void HandleObjectSpawn()
    {
        RemoveLocationFromDict();
        if(ut == UnitTypes.Miner)
        {
            Instantiate(MineObject, transform.position, Quaternion.identity);
            StartCoroutine(gameUI.SetMessage("Created mine at : " + hexCoords.ToString()));
        }
        else if (ut == UnitTypes.Settler)
        {
            Instantiate(OutpostObject, transform.position, Quaternion.identity);
            StartCoroutine(gameUI.SetMessage("Created outpost at : " + hexCoords.ToString()));
        }
        Destroy(this.gameObject);
    }

}
