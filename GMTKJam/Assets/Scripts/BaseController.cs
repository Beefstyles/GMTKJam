using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour {

    public int NumberOfActionsRemaining;
    private UnitTypes ut;
    public string Name;
    public HexCoordinates hexCoords;
    HexGrid hexGrid;

    void Start()
    {
        ut = GetComponent<ObjectInfo>().ut;
        hexGrid = FindObjectOfType<HexGrid>();
        SetHexCoords();
        OverallHexCoordsDict.GameDictionary.Add(hexCoords, ut);
    }

    private void SetHexCoords()
    {
        hexCoords = hexGrid.ReturnHexCoords(transform.position);
    }

}
