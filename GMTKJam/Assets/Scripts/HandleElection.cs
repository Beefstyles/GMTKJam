using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleElection : MonoBehaviour {

    ElectionUI electionUI;
    HexCell[] hexCells;

    void Start()
    {
        electionUI = FindObjectOfType<ElectionUI>();
    }
	public void CheckElection()
    {
        electionUI.SetElectionScreen();
        hexCells = FindObjectsOfType<HexCell>();
        foreach (var hexcell in hexCells)
        {
            hexcell.SpawnRandomEnemy();
        }
    }
}
