using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Encyclopedie_Unit : MonoBehaviour
{
    public Unit_SO AssociatedUnit;

    void Start()
    {
        GetComponent<Image>().sprite = AssociatedUnit.Sprite;
    }



}
