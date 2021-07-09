using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EfficaciteMecanique : MonoBehaviour
{
    public int HasBoost;
    bool randomHasStart;
    private void Start()
    {
        randomHasStart = false;
    }
    private void Update()
    {
        if(GetComponent<UnitScript>().NewTurnHasStart == true && randomHasStart == false)
        {
            randomHasStart = true;
            HasBoost = Random.Range(1, 3);
            if(HasBoost == 1)
            {
                GetComponent<UnitScript>()._MoveSpeedBonus = 1;
                GetComponent<UnitScript>()._diceBonus = 2;
            }
            else
            {
                GetComponent<UnitScript>()._MoveSpeedBonus = 0;
                GetComponent<UnitScript>()._diceBonus = 0;
            }
            StartCoroutine(ResetVariables());
        }
    }

    IEnumerator ResetVariables()
    {
        yield return new WaitForSeconds(5);
        HasBoost = 0;
        randomHasStart = false;
    }
}
