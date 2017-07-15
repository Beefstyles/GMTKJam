using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour {

    public Text ObjectType, MessageText, ActionsRemaining, CostToUpgrade, TurnNumber, NumberResources, 
        PercentApprovalWH, PercentApprovalPK, PercentApprovalBalance,
        PercentWH, PercentPK, PercentBalance;
    HexGrid hexGrid;
    public GameObject BuildingInfo, UnitInfo;
    public GameObject SelectedObjectWindow;
    GameController gc;
    PoliticsTracker pt;

	void Start ()
    {
        hexGrid = FindObjectOfType<HexGrid>();
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
        if (hexGrid.SelectedUnit != null)
        {
            if (ObjectType.text != hexGrid.SelectedUnit.GetComponent<ObjectInfo>().ut.ToString())
            {
                ObjectType.text = hexGrid.SelectedUnit.GetComponent<ObjectInfo>().ut.ToString();
            }
            if (hexGrid.SelectedUnit.GetComponent<ObjectInfo>().ut == UnitTypes.Base)
            {
                if (ActionsRemaining.text != hexGrid.SelectedUnit.GetComponent<BaseController>().NumberOfActionsRemaining.ToString())
                {
                    ObjectType.text = hexGrid.SelectedUnit.GetComponent<BaseController>().NumberOfActionsRemaining.ToString();
                }
            }
            else
            {
                if (ActionsRemaining.text != hexGrid.SelectedUnit.GetComponent<UnitBehaviour>().NumberOfActions.ToString())
                {
                    ObjectType.text = hexGrid.SelectedUnit.GetComponent<UnitBehaviour>().NumberOfActions.ToString();
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
}
