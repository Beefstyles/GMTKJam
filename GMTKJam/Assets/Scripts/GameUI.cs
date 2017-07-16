using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour {

    public Text ObjectType, MessageText, ActionsRemaining, CostToUpgrade, CostForAction, TurnNumber, NumberResources, 
        PercentApprovalWH, PercentApprovalPK, PercentApprovalBalance,
        PercentWH, PercentPK, PercentBalance,
        MineResourceAbility, MineUpgradeCost;

    public GameObject BuildingInfo, UnitInfo, MineInfo, OutpostInfo;
    public GameObject SelectedObjectWindow;
    GameController gc;
    PoliticsTracker pt;

	void Start ()
    {
        gc = FindObjectOfType<GameController>();
        pt = FindObjectOfType<PoliticsTracker>();
    }
	
	void Update ()
    {
        UpdateSelectedUnitUI();
        UpdateTurnNumberText();
        UpdateResourceText();
        UpdatePoliticsNumbers();
        if (gc.GetIsObjectSelected())
        {
            SetObjectDetails();
        }
        else
        {
            if (SelectedObjectWindow.activeSelf)
            {
                SelectedObjectWindow.SetActive(false);
            }
        }
    }

    public IEnumerator SetMessage(string message)
    {
        MessageText.text = message;
        yield return new WaitForSeconds(5F);
        MessageText.text = "";
    }

    private void SetObjectDetails()
    {
            if (!SelectedObjectWindow.activeSelf)
            {
                SelectedObjectWindow.SetActive(true);
            }

            if (gc.IsBaseSelected)
            {
                if (!BuildingInfo.activeSelf)
                {
                    BuildingInfo.SetActive(true);
                    UnitInfo.SetActive(false);
                }
            }
            else
            {
                if (!UnitInfo.activeSelf)
                {
                    BuildingInfo.SetActive(false);
                    UnitInfo.SetActive(true);
                }
            }
    }

    private void UpdateSelectedUnitUI()
    {
        if (gc.SelectedObject != null)
        {
            if (ObjectType.text != gc.SelectedObject.GetComponent<ObjectInfo>().ut.ToString())
            {
                ObjectType.text = gc.SelectedObject.GetComponent<ObjectInfo>().ut.ToString();
            }
            if (gc.SelectedObject.GetComponent<ObjectInfo>().ut == UnitTypes.Base)
            {
                if (ActionsRemaining.text != gc.SelectedObject.GetComponent<BaseController>().NumberOfActionsRemaining.ToString())
                {
                    ActionsRemaining.text = gc.SelectedObject.GetComponent<BaseController>().NumberOfActionsRemaining.ToString();
                }
            }
            else if (gc.SelectedObject.GetComponent<ObjectInfo>().ut == UnitTypes.Mine)
            {
                if (ActionsRemaining.text != gc.SelectedObject.GetComponent<MineHandler>().NumberOfActionsRemaining.ToString())
                {
                    ActionsRemaining.text = gc.SelectedObject.GetComponent<MineHandler>().NumberOfActionsRemaining.ToString();
                }
            }
            else if (gc.SelectedObject.GetComponent<ObjectInfo>().ut == UnitTypes.Outpost)
            {
                if (ActionsRemaining.text != gc.SelectedObject.GetComponent<OutpostHandler>().NumberOfActionsRemaining.ToString())
                {
                    ActionsRemaining.text = gc.SelectedObject.GetComponent<OutpostHandler>().NumberOfActionsRemaining.ToString();
                }
            }
            else
            {
                if (ActionsRemaining.text != gc.SelectedObject.GetComponent<UnitBehaviour>().NumberOfActions.ToString())
                {
                    ActionsRemaining.text = gc.SelectedObject.GetComponent<UnitBehaviour>().NumberOfActions.ToString();
                }
                if (CostToUpgrade.text != gc.SelectedObject.GetComponent<UnitBehaviour>().CostToUpgrade.ToString())
                {
                    CostToUpgrade.text = gc.SelectedObject.GetComponent<UnitBehaviour>().CostToUpgrade.ToString();
                }
                if (CostForAction.text != gc.SelectedObject.GetComponent<UnitBehaviour>().CostForAction.ToString())
                {
                    CostForAction.text = gc.SelectedObject.GetComponent<UnitBehaviour>().CostForAction.ToString();
                }
            }
        }
    }

    private void UpdateTurnNumberText()
    {
        if (TurnNumber.text != gc.TurnNumber.ToString())
        {
            TurnNumber.text = gc.TurnNumber.ToString();
        }
    }

    private void UpdateResourceText()
    {
        if (NumberResources.text != gc.NumberOfResources.ToString())
        {
            NumberResources.text = gc.NumberOfResources.ToString();
        }
    }

    private void UpdatePoliticsNumbers()
    {
        if (PercentApprovalWH.text != pt.PercentApprovalWH.ToString())
        {
            PercentApprovalWH.text = pt.PercentApprovalWH.ToString() + "%";
        }
        if (PercentApprovalPK.text != pt.PercentApprovalPK.ToString())
        {
            PercentApprovalPK.text = pt.PercentApprovalPK.ToString() + "%";
        }
        if (PercentApprovalBalance.text != pt.PercentApprovalBalance.ToString())
        {
            PercentApprovalBalance.text = pt.PercentApprovalBalance.ToString() + "%";
        }
        if (PercentWH.text != pt.PercentWH.ToString())
        {
            PercentWH.text = pt.PercentWH.ToString() + "%";
        }
        if (PercentPK.text != pt.PercentPK.ToString())
        {
            PercentPK.text = pt.PercentPK.ToString() + "%";
        }
        if (PercentBalance.text != pt.PercentBalance.ToString())
        {
            PercentBalance.text = pt.PercentBalance.ToString() + "%";
        }
    }

    public void DisplayMineInfo(int numberOfResourcesAdded, int upgradeCost)
    {
        MineInfo.SetActive(true);
        BuildingInfo.SetActive(false);
        UnitInfo.SetActive(false);
        OutpostInfo.SetActive(false);
        NumberResources.text = numberOfResourcesAdded.ToString();
        MineUpgradeCost.text = upgradeCost.ToString();
    }

    public void DisplayOutpostInfo()
    {
        MineInfo.SetActive(false);
        BuildingInfo.SetActive(false);
        UnitInfo.SetActive(false);
        OutpostInfo.SetActive(true);
    }
}
