using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineHandler : MonoBehaviour {

    private int NumberOfResourcesToAdd;
    private int UpgradeCost;
    GameController gc;
    HexCoordinates hexCoords;
    HexGrid hexGrid;
    GameUI gameUI;

    void Start()
    {
        hexGrid = FindObjectOfType<HexGrid>();
        gameUI = FindObjectOfType<GameUI>();
        SetHexCoords();
        gc = FindObjectOfType<GameController>();
        NumberOfResourcesToAdd = Random.Range(3, 10);
    }

	public void AddResourcesToPool()
    {
        gc.NumberOfResources += NumberOfResourcesToAdd;
    }

    private void SetHexCoords()
    {
        hexCoords = hexGrid.ReturnHexCoords(transform.position);
    }

    public void DisplayValues()
    {
        gameUI.DisplayMineInfo(NumberOfResourcesToAdd, UpgradeCost);
    }
}
