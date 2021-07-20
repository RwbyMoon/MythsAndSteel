using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttaqueJumelle : MonoBehaviour
{
    void Start()
    {
        GetComponent<UnitScript>().NbAtkTurn = 2;
        GetComponent<UnitScript>().NbAttaqueParTour = 2;
    }

    void Update()
    {
        if (GetComponent<UnitScript>().HasAttackedOneTime)
        {
            GetComponent<UnitScript>().MoveLeft = 0;
            GetComponent<UnitScript>().MoveSpeedBonus = 0;
        }
    }
}
