using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElectionUI : MonoBehaviour {

    public Text ElectionText, ElectionNumber, ElectionResult, ElectionSwing;
    PoliticsTracker pt;
    public GameObject ElectionScreen, GameOnScreen;
    public Button ContinueOrRestartBtn;
    private bool electionWon;


    void Start()
    {
        pt = FindObjectOfType<PoliticsTracker>();
    }
	public void SetElectionScreen()
    {
        ElectionScreen.SetActive(true);
        GameOnScreen.SetActive(false);
        ElectionResult.text = pt.ElectionResult.ToString();
        ElectionSwing.text = pt.ElectionResult.ToString();
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
            pt.currentElectionNumber++;
        }
        else
        {
            Debug.Log("Restart");
        }
    }
}
