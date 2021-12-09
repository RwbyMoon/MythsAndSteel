using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusLittoral : MonoBehaviour
{
    bool hasBonus;

    void Update()
    {
        if (TilesManager.Instance.TileList[GetComponent<UnitScript>().ActualTiledId].GetComponent<TileScript>().TerrainEffectList.Contains(MYthsAndSteel_Enum.TerrainType.Plage) && !hasBonus)
        {
            hasBonus = true;
            GetComponent<UnitScript>().PermaDiceBoost += 2;
            GetComponent<UnitScript>().DiceBonus += 2;
            GetComponent<UnitScript>().PermaSpeedBoost++;
            GetComponent<UnitScript>().MoveSpeedBonus++;
        }
        if (!TilesManager.Instance.TileList[GetComponent<UnitScript>().ActualTiledId].GetComponent<TileScript>().TerrainEffectList.Contains(MYthsAndSteel_Enum.TerrainType.Plage) && hasBonus)
        {
            hasBonus = false;
            GetComponent<UnitScript>().PermaDiceBoost -= 2;
            GetComponent<UnitScript>().PermaSpeedBoost--;
            GetComponent<UnitScript>().DiceBonus -= 2;
        }
    }
}
