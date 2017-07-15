using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour {

    public Text ObjectName;
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
            if (ObjectName.text != hexGrid.SelectedUnit.name)
            {
                ObjectName.text = hexGrid.SelectedUnit.name;
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
}
