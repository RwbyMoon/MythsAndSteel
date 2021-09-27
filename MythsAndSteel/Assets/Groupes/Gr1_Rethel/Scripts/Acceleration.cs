using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acceleration : MonoBehaviour
{
    bool BoostLaunch;
    bool BoostAct;

    void Update()
    {
        if(GetComponent<UnitScript>().NewTurnHasStart && !BoostLaunch)
        {
            BoostLaunch = true;
            foreach (int i in PlayerStatic.GetNeighbourDiag(GetComponent<UnitScript>().ActualTiledId, TilesManager.Instance.TileList[gameObject.GetComponent<UnitScript>().ActualTiledId].GetComponent<TileScript>().Line, false))
            {
                if((TilesManager.Instance.TileList[i].GetComponent<TileScript>().TerrainEffectList.Contains(MYthsAndSteel_Enum.TerrainType.Feu) || TilesManager.Instance.TileList[i].GetComponent<TileScript>().TerrainEffectList.Contains(MYthsAndSteel_Enum.TerrainType.Brasier) || TilesManager.Instance.TileList[i].GetComponent<TileScript>().TerrainEffectList.Contains(MYthsAndSteel_Enum.TerrainType.Fumigène)) && !BoostAct)
                {
                    BoostAct = true;
                    GetComponent<UnitScript>().MoveSpeedBonus++;
                }
            }
        }
        if(GameManager.Instance.IsNextPhaseDone && BoostLaunch)
        {
            BoostLaunch = false;
            BoostAct = false;
        }
    }
}
