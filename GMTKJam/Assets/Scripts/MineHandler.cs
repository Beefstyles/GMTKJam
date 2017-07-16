using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineHandler : MonoBehaviour {

    private int NumberOfResourcesToAdd;
    GameController gc;
    HexCoordinates hexCoords;
    HexGrid hexGrid;

    void Start()
    {
        hexGrid = FindObjectOfType<HexGrid>();
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
