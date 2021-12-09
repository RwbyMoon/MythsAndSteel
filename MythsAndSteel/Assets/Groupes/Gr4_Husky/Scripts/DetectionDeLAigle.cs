using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionDeLAigle : MonoBehaviour
{
    void Update()
    {
        if(RaycastManager.Instance.UnitInTile != null && RaycastManager.Instance.UnitInTile.GetComponent<UnitScript>().UnitSO.typeUnite == MYthsAndSteel_Enum.TypeUnite.Artillerie)
        {
            GetComponent<UnitScript>().OtherCanTargetNear = true;
        }
        else
        {
            GetComponent<UnitScript>().OtherCanTargetNear = false;
        }
    }
}
