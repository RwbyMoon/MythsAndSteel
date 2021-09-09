using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demolition : MonoBehaviour
{
    void Update()
    {
        if (TilesManager.Instance.TileList[GetComponent<UnitScript>().ActualTiledId].GetComponent<TileScript>().TerrainEffectList.Contains(MYthsAndSteel_Enum.TerrainType.Maison))
        {
            TilesManager.Instance.TileList[GetComponent<UnitScript>().ActualTiledId].GetComponent<TileScript>().RemoveEffect(MYthsAndSteel_Enum.TerrainType.Maison);
            TilesManager.Instance.TileList[GetComponent<UnitScript>().ActualTiledId].GetComponent<TileScript>().CreateEffect(MYthsAndSteel_Enum.TerrainType.Ruines);
        }
        if (TilesManager.Instance.TileList[GetComponent<UnitScript>().ActualTiledId].GetComponent<TileScript>().TerrainEffectList.Contains(MYthsAndSteel_Enum.TerrainType.Immeuble))
        {
            TilesManager.Instance.TileList[GetComponent<UnitScript>().ActualTiledId].GetComponent<TileScript>().RemoveEffect(MYthsAndSteel_Enum.TerrainType.Immeuble);
            TilesManager.Instance.TileList[GetComponent<UnitScript>().ActualTiledId].GetComponent<TileScript>().CreateEffect(MYthsAndSteel_Enum.TerrainType.Ruines);
        }
    }
}
