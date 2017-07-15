using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    UnitBehaviour[] unitArray;

    private bool isBaseSelected;

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

    IEnumerator RefreshUnitArray()
    {
        yield return new WaitForSeconds(0.001F);
        unitArray = FindObjectsOfType<UnitBehaviour>();
    }
	
}
