using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineHandler : MonoBehaviour {

    public int NumberOfResourcesToAdd;
    public int UpgradeCost;
    public int NumberOfActionsRemaining;
    private int maxActions;
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

}
