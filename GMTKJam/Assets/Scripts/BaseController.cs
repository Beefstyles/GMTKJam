using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour {

    public int NumberOfActionsRemaining;
    private UnitTypes ut;
    public string Name;

    void Start()
    {
        ut = GetComponent<ObjectInfo>().ut;
    }


}
