using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElectionUI : MonoBehaviour {

    public Text ElectionResult, ElectionSwing;
    PoliticsTracker pt;

    void Start()
    {
        pt = FindObjectOfType<PoliticsTracker>();
    }
	public void SetElectionScreen()
    {
        ElectionResult.text = pt.ElectionResult.ToString();
        ElectionSwing.text = pt.ElectionResult.ToString();
    }
}
