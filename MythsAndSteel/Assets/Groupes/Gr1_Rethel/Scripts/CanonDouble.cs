using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonDouble : MonoBehaviour
{
    void Start()
    {
        GetComponent<UnitScript>().NbAttaqueParTour = 2;
        GetComponent<UnitScript>().NbAtkTurn = 2;
    }
}
