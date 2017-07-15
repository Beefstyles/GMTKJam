﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    UnitBehaviour[] unitArray;

    private bool isBaseSelected;

    private int numberOfResources;

    public GameObject BaseObject, SoldierObject, SettlerObject, MinerObject;

    public Transform ObjectSpawnLocation;

    public bool IsBaseSelected
    {
        get
        {
            return isBaseSelected;
        }

        set
        {
            isBaseSelected = value;
        }
    }

    public int NumberOfResources
    {
        get
        {
            return numberOfResources;
        }

        set
        {
            numberOfResources = value;
        }
    }

    public bool GetIsObjectSelected()
    {
        return isObjectSelected;
    }

    public void SetIsObjectSelected(bool value)
    {
        isObjectSelected = value;
    }

    private bool isObjectSelected;

    void Start ()
    {
        StartCoroutine("RefreshUnitArray");
    }

    public void DeselectAllUnits()
    {
        if (unitArray.Length > 1)
        {
            foreach (var unit in unitArray)
            {
                unit.DeSelectUnit();
            }
        }
        
    }

    public IEnumerator RefreshUnitArray()
    {
        yield return new WaitForSeconds(0.001F);
        unitArray = FindObjectsOfType<UnitBehaviour>();
    }


    void StartGame()
    {
        
    }

    public void ObjectSpawner(GameObject objectToBeSpawned, Vector3 location)
    {
        GameObject go = Instantiate(objectToBeSpawned, location, Quaternion.identity);
    }

     
}
