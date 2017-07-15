using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour {

    public Text ObjectName;
    HexGrid hexGrid;

	void Start ()
    {
        hexGrid = FindObjectOfType<HexGrid>();
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
	}
}
