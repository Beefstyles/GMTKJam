using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutpostHandlerController : MonoBehaviour {

    GameController gc;

    void Start()
    {
        gc = FindObjectOfType<GameController>();
    }
    public void TryAndSpawnMiner()
    {
        if (gc.SelectedObject != null)
        {
            gc.SelectedObject.GetComponent<OutpostHandler>().SpawnMiner();
        }
    }
}
