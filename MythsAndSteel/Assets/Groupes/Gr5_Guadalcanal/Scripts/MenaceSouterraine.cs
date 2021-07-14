using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenaceSouterraine : Capacity
{
    int actualTile = 0;
    public override void StartCpty()
    {
        actualTile = GetComponent<UnitScript>().ActualTiledId;
        GetComponent<UnitScript>().IsActifNotConsumeAction = true;
        if (TilesManager.Instance.TileList[GetComponent<UnitScript>().ActualTiledId].GetComponent<TileScript>().TerrainEffectList.Contains(MYthsAndSteel_Enum.TerrainType.Tunnel))
        {
            List<GameObject> tile = new List<GameObject>();
            foreach (GameObject gam in TilesManager.Instance.TileList)
            {
                if (gam.GetComponent<TileScript>().TerrainEffectList.Contains(MYthsAndSteel_Enum.TerrainType.Tunnel) && gam.GetComponent<TileScript>().Unit == null)
                {
                    tile.Add(gam);
                }
            }
            GameManager.Instance._eventCall += EndCpty;
            GameManager.Instance._eventCallCancel += StopCpty;
            GameManager.Instance.StartEventModeTiles(1, GetComponent<UnitScript>().UnitSO.IsInRedArmy, tile, "Menace Souterraine", "Voulez-vous vraiment déplacer l'unité sur cette case?");
        }
        base.StartCpty();
    }

    public override void StopCpty()
    {
        GameManager.Instance.TileChooseList.Clear();
        GetComponent<UnitScript>().IsActifNotConsumeAction = false;
        GameManager.Instance.StopEventModeTile();
        GameManager.Instance.TileChooseList.Clear();
        GetComponent<UnitScript>().StopCapacity(true);
        base.StopCpty();
    }

    public override void EndCpty()
    {
        TilesManager.Instance.TileList[actualTile].GetComponent<TileScript>().RemoveUnitFromTile();
        GameManager.Instance.TileChooseList[0].GetComponent<TileScript>().AddUnitToTile(RaycastManager.Instance.ActualUnitSelected);
        GameManager.Instance._eventCall -= EndCpty;
        GetComponent<UnitScript>().EndCapacity();
        base.EndCpty();
        GameManager.Instance.TileChooseList.Clear();
        GetComponent<UnitScript>().IsActifNotConsumeAction = false;
    }
}
