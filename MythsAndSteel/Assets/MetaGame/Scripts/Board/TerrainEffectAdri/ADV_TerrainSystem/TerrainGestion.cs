using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGestion : MonoSingleton<TerrainGestion>
{
    public void UnitModification(TerrainParent pt, TileScript ts)
    {
        if(ts.LastUnit != ts.Unit && ts.Unit != null)
        {
            // Nouvelle unité.
            UnitScript us = ts.Unit.GetComponent<UnitScript>();
            pt.OnUnityAdd(us);
            if (us.LastTileId != 999)
            {
                if (us.ActualTiledId - us.LastTileId == 1)
                {
                    pt.ComingFromLeft(us);
                }
                else if (us.ActualTiledId - us.LastTileId == -1)
                {
                    pt.ComingFromRight(us);
                }
                else if (us.ActualTiledId - us.LastTileId == 9)
                {
                    pt.ComingFromDown(us);
                }
                else if (us.ActualTiledId - us.LastTileId == -9)
                {
                    pt.ComingFromUp(us);
                }
            }
            else
            {
                Debug.LogError("Coming from unknown place");
            }
        }
        if(ts.LastUnit == ts.Unit)
        {
            Debug.Log("Same unit.");
        }
        if(ts.Unit == null)
        {
            UnitScript us = ts.LastUnit.GetComponent<UnitScript>();
            if (us.ActualTiledId - us.LastTileId == 1)
            {
                pt.QuitToRight(us);
            }
            else if (us.ActualTiledId - us.LastTileId == -1)
            {
                pt.QuitToLeft(us);
            }
            else if (us.ActualTiledId - us.LastTileId == 9)
            {
                pt.QuitToUp(us);
            }
            else if (us.ActualTiledId - us.LastTileId == -9)
            {
                pt.QuitToDown(us);
            }
        }
    }
    public void EndTurn(TerrainParent pt, TileScript ts)
    {
        if(ts.Unit != null)
        {
            UnitScript us = ts.Unit.GetComponent<UnitScript>();       
            pt.EndTurnEffect(ts, us);
        }
        else if(ts.Unit == null)
        {
            pt.EndTurnEffect(ts);
        }
    }
}

