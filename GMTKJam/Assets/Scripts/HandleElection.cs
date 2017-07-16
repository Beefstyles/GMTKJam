using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleElection : MonoBehaviour {

    ElectionUI electionUI;

    void Start()
    {
        electionUI = FindObjectOfType<ElectionUI>();
    }
	public void CheckElection()
    {
        electionUI.SetElectionScreen();
    }
}
