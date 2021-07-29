using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boue : TerrainParent
{
 
    public override void EndPlayerTurnEffect(bool IsInRedArmy, UnitScript Unit)
    {
        if (Unit != null)
        {
           
            if(Unit.UnitSO.IsInJ1Army == IsInRedArmy && !Unit._hasStartMove && !Unit.GetComponent<UnitScript>().ToutTerrain && !Unit.GetComponent<UnitScript>().Volant)
            {
                Unit.TakeDamage(1);

            }
        }
        
        base.EndPlayerTurnEffect(IsInRedArmy);
    }
}