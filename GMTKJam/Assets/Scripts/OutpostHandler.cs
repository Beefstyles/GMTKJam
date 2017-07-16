using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutpostHandler : MonoBehaviour {

    GameController gc;
    HexCoordinates hexCoords;
    HexGrid hexGrid;

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
}
