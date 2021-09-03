using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrecisionStable : MonoBehaviour
{
    void Update()
    {
        if(GetComponent<UnitScript>()._diceBonus != 0)
        {
            GetComponent<UnitScript>()._diceBonus = 0;
        }
        
        if(GetComponent<UnitScript>().PermaDiceBoost != 0)
        {
            GetComponent<UnitScript>().PermaDiceBoost = 0;
        }
    }
}
