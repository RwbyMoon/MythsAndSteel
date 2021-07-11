using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColereDuKhodok : MonoBehaviour
{
    #region Info
    [SerializeField] string _Capacity1Name = "";
    public string Capacity1Name => _Capacity1Name;
    [SerializeField] private Sprite render1;

    [TextArea]
    [SerializeField] string _Capacity1Description = "";
    public string Capacity1Description => _Capacity1Description;
    #endregion
    
    public int actualLife;
    bool SetLifeDone;
    public int actualBonus;

    private void Start()
    {
        StartCoroutine(SetFirstLife());
    }

    private void Update()
    {
        if(actualLife > GetComponent<UnitScript>()._life && SetLifeDone)
        {
            GetComponent<UnitScript>().DiceBonus += 2 * (actualLife - GetComponent<UnitScript>()._life);
            GetComponent<UnitScript>().PermaDiceBoost += 2 * (actualLife - GetComponent<UnitScript>()._life);
            actualBonus += 2 * (actualLife - GetComponent<UnitScript>()._life);
            actualLife = GetComponent<UnitScript>()._life;
        }
        if (GetComponent<UnitScript>().HasAttacked)
        {
            GetComponent<UnitScript>().DiceBonus -= actualBonus;
            GetComponent<UnitScript>().PermaDiceBoost -= actualBonus;
            actualBonus = 0;
        }
        if(actualLife < GetComponent<UnitScript>()._life)
        {
            actualLife = GetComponent<UnitScript>()._life;
        }
    }

    IEnumerator SetFirstLife()
    {
        actualLife = GetComponent<UnitScript>()._life;
        yield return new WaitForSeconds(1);
        SetLifeDone = true;
    }
}
