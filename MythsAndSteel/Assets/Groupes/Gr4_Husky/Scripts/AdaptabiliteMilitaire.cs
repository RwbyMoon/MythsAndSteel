using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdaptabiliteMilitaire : MonoBehaviour
{
    bool BonusApplied;
    void Update()
    {
        foreach(int T in Attaque.Instance._selectedTiles)
        {
            if (!TilesManager.Instance.TileList[T].GetComponent<TileScript>().TerrainEffectList.Contains(MYthsAndSteel_Enum.TerrainType.Sol) && !BonusApplied)
            {
                BonusApplied = true;
                GetComponent<UnitScript>()._damageBonus++;
            }
            if (TilesManager.Instance.TileList[T].GetComponent<TileScript>().TerrainEffectList.Contains(MYthsAndSteel_Enum.TerrainType.Sol) && BonusApplied)
            {
                GetComponent<UnitScript>()._damageBonus--;
                BonusApplied = false;
            }
        }
    }
}
