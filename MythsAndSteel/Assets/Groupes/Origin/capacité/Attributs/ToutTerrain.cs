using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToutTerrain : MonoBehaviour
{
    private void Start()
    {
        GetComponent<UnitScript>().IgnoreTerrainEffect = true;
    }
}
