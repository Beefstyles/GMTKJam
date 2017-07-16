using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutpostHandler : MonoBehaviour {

    GameController gc;
    HexCoordinates hexCoords;
    HexGrid hexGrid;
    public int NumberOfActionsRemaining;
    private int maxActions;
    public Transform SpawnLocation;
    public int CostOfMiner;
    public GameObject Miner;

    void Start()
    {
        hexGrid = FindObjectOfType<HexGrid>();
        SetHexCoords();
        gc = FindObjectOfType<GameController>();
    }

    private void SetHexCoords()
    {
        hexCoords = hexGrid.ReturnHexCoords(transform.position);
    }

    public void SpawnMiner()
    {
        if(gc.NumberOfResources >= CostOfMiner)
        {
            Instantiate(Miner, SpawnLocation.position, Quaternion.identity);
        }
    }
}
