using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boue : TerrainParent
{
    public override void EndTurnEffect(TileScript ts, UnitScript Unit = null)
    {
        if(Unit != null)
        {
            if (!Unit._hasStartMove)
            {
                Unit.TakeDamage(1);
            }
        }
        base.EndTurnEffect(ts, Unit);
    }
}