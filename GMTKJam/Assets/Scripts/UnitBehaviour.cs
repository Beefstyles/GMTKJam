using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBehaviour : MonoBehaviour {

    public int NumberOfMoves = 1;
    public int NumberOfDice = 1;
    public int NumberOfAttacks = 1;
    public bool IsSelected;
    MeshRenderer mr;
    public Material ObjectSelected, ObjectNotSelected;

    void Start()
    {
        mr = GetComponent<MeshRenderer>();
        mr.material = ObjectNotSelected;
    }

	public void SelectUnit()
    {
        if (!IsSelected)
        {
            IsSelected = true;
            mr.material = ObjectSelected;
        }
    }

    public void DeSelectUnit()
    {
        if (IsSelected)
        {
            IsSelected = false;
            mr.material = ObjectNotSelected;
        }
    }

    
}
