using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour {

    public Text ObjectType, MessageText, ActionsRemaining, CostToUpgrade, TurnNumber, NumberResources, 
        PerecentApprovalWH, PercentApprovalPK, PercentApprovalBalance,
        PerecentWH, PercentPK, PercentBalance;
    HexGrid hexGrid;
    public GameObject BuildingInfo, UnitInfo;
    public GameObject SelectedObjectWindow;
    GameController gc;

	void Start ()
    {
        hexGrid = FindObjectOfType<HexGrid>();
        gc = FindObjectOfType<GameController>();
    }
	
	void Update ()
    {
        if (hexGrid.SelectedUnit != null)
        {
            if (ObjectType.text != hexGrid.SelectedUnit.GetComponent<ObjectInfo>().ut.ToString())
            {
                ObjectType.text = hexGrid.SelectedUnit.GetComponent<ObjectInfo>().ut.ToString();
            }
        }
        if (gc.GetIsObjectSelected())
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
}
