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
                    gc.IsBaseSelected = true;
                    Debug.Log("Hit player base");
                }
                if (hit.collider.tag == "Unit")
                {
                    ub = hit.collider.GetComponent<UnitBehaviour>();
                    if(ub != null)
                    {
                        gc.DeselectAllUnits();
                        ub.SelectUnit();
                    }
                    gc.IsBaseSelected = false;
                }
            }

        }
    }
}
