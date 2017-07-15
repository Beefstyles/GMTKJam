using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {

    UnitBehaviour ub;
    GameController gc;

    void Start()
    {
        gc = FindObjectOfType<GameController>();
    }
	void Update ()
    {
        HandleInput();
    }

    void HandleInput()
    {
        Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(inputRay, out hit))
            {
                if (hit.collider.tag == "PlayerBase")
                {
                    if (gc.IsBaseSelected)
                    {
                        gc.IsBaseSelected = false;
                        gc.SetIsObjectSelected(false);
                    }
                    else
                    {
                        gc.IsBaseSelected = true;
                        gc.SetIsObjectSelected(true);
                    }
                    
                }
                if (hit.collider.tag == "Unit")
                {
                    ub = hit.collider.GetComponent<UnitBehaviour>();
                    if(ub != null)
                    {
                        if(hit.collider.GetComponent<ObjectInfo>().ut != UnitTypes.Enemy)
                        {
                            gc.DeselectAllUnits();
                            ub.SelectUnit();
                            gc.IsBaseSelected = false;
                        }
                        
                    }
                    
                }
            }

        }
    }
}
