using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineHandler : MonoBehaviour {

    private int NumberOfResourcesToAdd;
    GameController gc;

    void Start()
    {
        gc = FindObjectOfType<GameController>();
        NumberOfResourcesToAdd = Random.Range(3, 10);
    }

	public void AddResourcesToPool()
    {
        gc.NumberOfResources += NumberOfResourcesToAdd;
    }
}
