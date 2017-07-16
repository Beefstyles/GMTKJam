using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerInput : MonoBehaviour
{

    UnitBehaviour ub;
    GameController gc;
    MineHandler mh;
    OutpostHandler oh;
    private bool helpPressed;
    public GameObject HelpScreen, GameOnScreen;

    void Start()
    {
        gc = FindObjectOfType<GameController>();
    }
    void Update()
    {
        Help();
        HandleInput();
        CheckForExit();
    }

    void Help()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            if (helpPressed)
            {
                helpPressed = false;
                HelpScreen.SetActive(false);
                GameOnScreen.SetActive(true);
            }
            else
            {
                helpPressed = true;
                HelpScreen.SetActive(true);
                GameOnScreen.SetActive(false);
            }

        }
    }

    void CheckForExit()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
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
                        gc.SelectedObject = null;
                    }
                    else
                    {
                        gc.IsBaseSelected = true;
                        gc.SetIsObjectSelected(true);
                        gc.SelectedObject = hit.collider.gameObject;
                        gc.DeselectAllUnits();
                    }

                }
                if (hit.collider.tag == "Unit")
                {
                    ub = hit.collider.GetComponent<UnitBehaviour>();
                    if (ub != null)
                    {
                        if (hit.collider.GetComponent<ObjectInfo>().ut != UnitTypes.Enemy)
                        {
                            gc.DeselectAllUnits();
                            ub.SelectUnit();
                            gc.IsBaseSelected = false;
                        }
                    }
                }

                if (hit.collider.tag == "AncBuilding")
                {
                    gc.SelectedObject = hit.collider.gameObject;
                    mh = hit.collider.GetComponent<MineHandler>();
                    oh = hit.collider.GetComponent<OutpostHandler>();
                }
            }
        }
    }
}

