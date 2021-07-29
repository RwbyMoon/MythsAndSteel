using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Overclock : Capacity
{
    public override void StartCpty()
    {
        int tileId = RaycastManager.Instance.ActualUnitSelected.GetComponent<UnitScript>().ActualTiledId;
        List<GameObject> tile = new List<GameObject>();
        tile.Add(TilesManager.Instance.TileList[GetComponent<UnitScript>().ActualTiledId]);

        GameManager.Instance._eventCall += EndCpty;
        GameManager.Instance._eventCallCancel += StopCpty;
        GameManager.Instance.StartEventModeTiles(1, GetComponent<UnitScript>().UnitSO.IsInJ1Army, tile, "Overclock", "Voulez-vous booster les unités adjacentes?");
        base.StartCpty();
    }

    public override void StopCpty()
    {
        GameManager.Instance.StopEventModeTile();
        GameManager.Instance.TileChooseList.Clear();
        GetComponent<UnitScript>().StopCapacity(true);
        base.StopCpty();
    }

    public override void EndCpty()
    {
        int tileId = GetComponent<UnitScript>().ActualTiledId;
        List<GameObject> UnitToBoost = new List<GameObject>();
        foreach (int T in PlayerStatic.GetNeighbourDiag(tileId, TilesManager.Instance.TileList[tileId].GetComponent<TileScript>().Line, false))
        {
            if(TilesManager.Instance.TileList[T].GetComponent<TileScript>().Unit != null && TilesManager.Instance.TileList[T].GetComponent<TileScript>().Unit.GetComponent<UnitScript>().UnitSO.IsInJ1Army == GetComponent<UnitScript>().UnitSO.IsInJ1Army)
            {
                TilesManager.Instance.TileList[T].GetComponent<TileScript>().Unit.GetComponent<UnitScript>().DiceBonus += 2;
                TilesManager.Instance.TileList[T].GetComponent<TileScript>().Unit.GetComponent<UnitScript>().AttackRangeBonus+=1;
            }
        }
        GameManager.Instance._eventCall -= EndCpty;
        GetComponent<UnitScript>().EndCapacity();
        base.EndCpty();
        GameManager.Instance.TileChooseList.Clear();
    }
}
