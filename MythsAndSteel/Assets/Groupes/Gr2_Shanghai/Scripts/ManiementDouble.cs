using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManiementDouble : MonoBehaviour
{
    bool hasBenUsedThisTurn;

    void Update()
    {
        if(!hasBenUsedThisTurn && GameManager.Instance.lastKiller == GetComponent<UnitScript>().UnitSO.UnitName)
        {
            hasBenUsedThisTurn = true;
            if (GetComponent<UnitScript>().UnitSO.IsInRedArmy) PlayerScript.Instance.J1Infos.ActivationLeft++;
            else PlayerScript.Instance.J2Infos.ActivationLeft++;
            UIInstance.Instance.UpdateActivationLeft();
            GetComponent<UnitScript>().ResetTurn(true);
        }

        if (GetComponent<UnitScript>().NewTurnHasStart)
        {
            hasBenUsedThisTurn = false;
        }
    }
}
