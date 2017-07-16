using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliticsTracker : MonoBehaviour {

    public int PercentApprovalWH, PercentApprovalPK, PercentApprovalBalance;
    public int PercentWH, PercentPK, PercentBalance;

	public void AlterPercentApproval(int percentChange, PoliticsParty party)
    {
        switch (party)
        {
            case PoliticsParty.Warhawk:
                if(PercentApprovalWH <= 100)
                {
                    PercentApprovalWH += percentChange;
                }
                break;
            case PoliticsParty.Peacenik:
                if (PercentApprovalPK <= 100)
                {
                    PercentApprovalPK += percentChange;
                }
                break;
            case PoliticsParty.Balance:
                if (PercentApprovalBalance <= 100)
                {
                    PercentApprovalBalance += percentChange;
                }
                break;
        }
    }
	void Update () {
		
	}
}
