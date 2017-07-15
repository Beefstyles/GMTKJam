using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseActionController : MonoBehaviour {

    GameController gc;
    BaseController bc;
    public int CostOfSoldier, CostOfMiner, CostOfSettler;
    GameUI gameUI;

    void Start()
    {
        gc = FindObjectOfType<GameController>();
        bc = FindObjectOfType<BaseController>();
        gameUI = FindObjectOfType<GameUI>();
    }

	public void SpawnSoldierUnit()
    {
        if (bc.NumberOfActionsRemaining > 0)
        {
            if (CostOfSoldier >= gc.NumberOfResources)
            {
                gc.NumberOfResources -= CostOfSoldier;
                gc.ObjectSpawner(gc.SoldierObject, gc.ObjectSpawnLocation.position);
            }
            else
            {
                StartCoroutine(gameUI.SetMessage("Cannot afford soldier"));
            }
        }
        else
        {
            StartCoroutine(gameUI.SetMessage("No actions remaining"));
        }
        
    }
    public void SpawnMinerUnit()
    {
        if (bc.NumberOfActionsRemaining > 0)
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
        else
        {
            StartCoroutine(gameUI.SetMessage("No actions remaining"));
        }
    }
    public void SpawnSettlerUnit()
    {
        if (bc.NumberOfActionsRemaining > 0)
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
        else
        {
            StartCoroutine(gameUI.SetMessage("No actions remaining"));
        }
    }
}
