using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boue : TerrainParent
{
 
    public override void EndPlayerTurnEffect(bool IsInRedArmy, UnitScript Unit)
    {
        if (Unit != null)
        {
           
            if(Unit.UnitSO.IsInRedArmy == IsInRedArmy && !Unit._hasStartMove && !Unit.GetComponent<UnitScript>().ToutTerrain && !RaycastManager.Instance.ActualUnitSelected.GetComponent<UnitScript>().Volant)
            {
                Unit.TakeDamage(1);

            }
        }
        
        base.EndPlayerTurnEffect(IsInRedArmy);
    }
}