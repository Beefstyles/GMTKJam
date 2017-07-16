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
                if(PercentApprovalWH <= 100 && PercentApprovalWH >= 0)
                {
                    if((PercentApprovalWH + percentChange) > 100)
                    {
                        PercentApprovalWH = 100;
                    }
                    else if ((PercentApprovalWH + percentChange) < 0)
                    {
                        PercentApprovalWH = 0;
                    }
                    else
                    {
                        PercentApprovalWH += percentChange;
                    }
                    
                }
                break;
            case PoliticsParty.Peacenik:
                if (PercentApprovalPK <= 100 && PercentApprovalPK >= 0)
                {
                    if ((PercentApprovalPK + percentChange) > 100)
                    {
                        PercentApprovalPK = 100;
                    }
                    else if ((PercentApprovalPK + percentChange) < 0)
                    {
                        PercentApprovalPK = 0;
                    }
                    else
                    {
                        PercentApprovalPK += percentChange;
                    }
                }
                break;
            case PoliticsParty.Balance:
                if (PercentApprovalBalance <= 100 && PercentApprovalBalance >= 0)
                {
                    if ((PercentApprovalBalance + percentChange) > 100)
                    {
                        PercentApprovalBalance = 100;
                    }
                    else if ((PercentApprovalBalance + percentChange) < 0)
                    {
                        PercentApprovalBalance = 0;
                    }
                    else
                    {
                        PercentApprovalBalance += percentChange;
                    }
                }
                break;
        }
    }
	public void ShuffleProportions ()
    {
        int WarhawkChange = Random.Range(-5, 5);
        Debug.Log("WarhawkChange" + WarhawkChange);
        int PeacenikChange = Random.Range(-5, 5);

        if (PercentWH > 0 && PercentWH < 100)
        {
            PercentWH += WarhawkChange;
            Debug.Log("Actually does something");
        }

        if (PercentPK > 0 && PercentPK < 100)
        {
            PercentPK += PeacenikChange;
        }

        PercentBalance = 100 - PercentWH - PercentPK;

        
    }
}
