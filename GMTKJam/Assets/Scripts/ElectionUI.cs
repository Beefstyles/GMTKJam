using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElectionUI : MonoBehaviour {

    public Text ElectionText, ElectionNumber, ElectionResult, ElectionSwing;
    PoliticsTracker pt;
    public GameObject ElectionScreen, GameOnScreen, GameOnObjects;
    public Button ContinueOrRestartBtn;
    private bool electionWon;
    private decimal electionSwing;


    void Start()
    {
        pt = FindObjectOfType<PoliticsTracker>();
    }
	public void SetElectionScreen()
    {
        ElectionScreen.SetActive(true);
        GameOnObjects.SetActive(false);
        GameOnScreen.SetActive(false);
        ElectionResult.text = Math.Round(pt.ElectionResult,2).ToString() + "%";
        electionSwing = pt.ElectionResult - pt.PreviousElectionResult;
        ElectionSwing.text = Math.Round(electionSwing, 2).ToString() + "%";
        ElectionNumber.text = pt.currentElectionNumber.ToString();
        if (pt.ElectionResult >= 50)
        {
            ElectionText.text = "You survive to rule for another election cycle";
            electionWon = true;
        }
        else
        {
            ElectionText.text = "You lost your supporter base and will not be re-elected. You lasted for: " + pt.currentElectionNumber.ToString() + " elections";
            electionWon = false;
        }
        SetContinueOrRestart();
    }

    private void SetContinueOrRestart()
    {
        if (electionWon)
        {
            ContinueOrRestartBtn.GetComponentInChildren<Text>().text = "Continue";
        }
        else
        {
            ContinueOrRestartBtn.GetComponentInChildren<Text>().text = "Restart";
        }
    }

    public void ContinueOrRestart()
    {
        if (electionWon)
        {
            ElectionScreen.SetActive(false);
            GameOnScreen.SetActive(true);
            GameOnObjects.SetActive(true);
            pt.currentElectionNumber++;
            pt.AlterPercentApproval(UnityEngine.Random.Range(-3, 2), PoliticsParty.Balance);
            pt.AlterPercentApproval(UnityEngine.Random.Range(-3, 2), PoliticsParty.Peacenik);
            pt.AlterPercentApproval(UnityEngine.Random.Range(-3, 2), PoliticsParty.Warhawk);
            pt.PreviousElectionResult = pt.ElectionResult;
        }
        else
        {
            Debug.Log("Restart");
        }
    }
}
