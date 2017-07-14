using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    UnitBehaviour[] unitArray;

    void Start ()
    {
        StartCoroutine("DelayUnitFinding");
    }

    public void DeselectAllUnits()
    {
        foreach (var unit in unitArray)
        {
            unit.DeSelectUnit();
        }
    }

    IEnumerator DelayUnitFinding()
    {
        yield return new WaitForSeconds(0.001F);
        unitArray = FindObjectsOfType<UnitBehaviour>();
        Debug.Log(unitArray.Length);
    }
	
}
