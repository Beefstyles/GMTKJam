using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliticsTracker : MonoBehaviour {

    public int PercentApprovalWH, PercentApprovalPK, PercentApprovalBalance;
    public int PercentWH, PercentPK, PercentBalance;

    public int ElectionResult;
    public int PreviousElectionResult;
    public int currentElectionNumber = 0;

	public void AlterPercentApproval(int percentChange, PoliticsParty party)
    {
        switch (party)
        {
            case PoliticsParty.Warhawk:
                if(PercentApprovalWH <= 100 && PercentApprovalWH >= 0)
                {
                    PercentApprovalWH += percentChange;
                    PercentApprovalWH = Mathf.Clamp(PercentApprovalWH, 0, 100);
                }
                break;
            case PoliticsParty.Peacenik:
                if (PercentApprovalPK <= 100 && PercentApprovalPK >= 0)
                {
                    PercentApprovalPK += percentChange;
                    PercentApprovalPK = Mathf.Clamp(PercentApprovalPK, 0, 100);
                }
                break;
            case PoliticsParty.Balance:
                if (PercentApprovalBalance <= 100 && PercentApprovalBalance >= 0)
                {
                    PercentApprovalBalance += percentChange;
                    PercentApprovalBalance = Mathf.Clamp(PercentApprovalBalance, 0, 100);
                }
                break;
        }
    }
	public void ShuffleProportions ()
    {
        int WarhawkChange = Random.Range(-5, 5);
        Debug.Log("WarhawkChange" + WarhawkChange);
        int PeacenikChange = Random.Range(-5, 5);

        if (PercentWH >= 0 && PercentWH <= 100)
        {
            PercentWH += WarhawkChange;
            PercentWH = Mathf.Clamp(PercentWH, 5, 100);
        }

        if (PercentPK > 0 && PercentPK < 100)
        {
            PercentPK += PeacenikChange;
            PercentPK = Mathf.Clamp(PercentPK, 5, 100);
        }

        PercentBalance = 100 - PercentWH - PercentPK;
    }

    public void CalculateElectionResult()
    {
        ElectionResult = (PercentApprovalWH / 100 * PercentWH) + (PercentApprovalPK / 100 * PercentPK) + (PercentApprovalBalance / 100 * PercentBalance);
    }
}
