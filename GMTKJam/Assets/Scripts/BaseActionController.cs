using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseActionController : MonoBehaviour {

    GameController gc;
    public int CostOfSoldier, CostOfMiner, CostOfSettler;
    GameUI gameUI;

    void Start()
    {
        gc = FindObjectOfType<GameController>();
        gameUI = FindObjectOfType<GameUI>();
    }

	public void SpawnSoldierUnit()
    {
        if(CostOfSoldier >= gc.NumberOfResources)
        {
            gc.NumberOfResources -= CostOfSoldier;
            gc.ObjectSpawner(gc.SoldierObject, gc.ObjectSpawnLocation.position);
        }
        else
        {
            StartCoroutine(gameUI.SetMessage("Cannot afford solider"));
        }
    }
    public void SpawnMinerUnit()
    {
        if (CostOfMiner >= gc.NumberOfResources)
        {
            gc.NumberOfResources -= CostOfMiner;
            gc.ObjectSpawner(gc.MinerObject, gc.ObjectSpawnLocation.position);
        }
        else
        {
            StartCoroutine(gameUI.SetMessage("Cannot afford miner"));
        }
    }
    public void SpawnSettlerUnit()
    {

        if (CostOfSettler >= gc.NumberOfResources)
        {
            gc.NumberOfResources -= CostOfSettler;
            gc.ObjectSpawner(gc.SettlerObject, gc.ObjectSpawnLocation.position);
        }
        else
        {
            StartCoroutine(gameUI.SetMessage("Cannot afford settler"));
        }
    }
}
