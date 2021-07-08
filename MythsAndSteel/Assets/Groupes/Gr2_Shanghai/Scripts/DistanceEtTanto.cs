using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceEtTanto : Capacity
{
    //Cette capacité appartient à l'unité "Impérial" de l'armée Japonaise sur le plateau de Shanghai
    private int attackMiss;
    public override void StartCpty()
    {
        int tileId = RaycastManager.Instance.ActualUnitSelected.GetComponent<UnitScript>().ActualTiledId;
        List<GameObject> tile = new List<GameObject>();

        foreach (int T in PlayerStatic.GetNeighbourDiag(tileId, TilesManager.Instance.TileList[tileId].GetComponent<TileScript>().Line, false))
        {
            if (TilesManager.Instance.TileList[T] != null)
            {
                if (TilesManager.Instance.TileList[T].GetComponent<TileScript>().Unit != RaycastManager.Instance.ActualUnitSelected && TilesManager.Instance.TileList[T].GetComponent<TileScript>().Unit != null && TilesManager.Instance.TileList[T].GetComponent<TileScript>().Unit.GetComponent<UnitScript>().UnitSO.IsInRedArmy != RaycastManager.Instance.ActualUnitSelected.GetComponent<UnitScript>().UnitSO.IsInRedArmy)
                {
                    tile.Add(TilesManager.Instance.TileList[T]);
                }
            }
        }

        foreach (int T in PlayerStatic.GetNeighbourDiag(tileId + 1, TilesManager.Instance.TileList[tileId + 1].GetComponent<TileScript>().Line, false))
        {
            if (TilesManager.Instance.TileList[T] != null)
            {
                if (TilesManager.Instance.TileList[T].GetComponent<TileScript>().Unit != RaycastManager.Instance.ActualUnitSelected && TilesManager.Instance.TileList[T].GetComponent<TileScript>().Unit != null && TilesManager.Instance.TileList[T].GetComponent<TileScript>().Unit.GetComponent<UnitScript>().UnitSO.IsInRedArmy != RaycastManager.Instance.ActualUnitSelected.GetComponent<UnitScript>().UnitSO.IsInRedArmy)
                {
                    tile.Add(TilesManager.Instance.TileList[T]);
                }
            }
        }

        foreach (int T in PlayerStatic.GetNeighbourDiag(tileId - 1, TilesManager.Instance.TileList[tileId - 1].GetComponent<TileScript>().Line, false))
        {
            if (TilesManager.Instance.TileList[T] != null)
            {
                if (TilesManager.Instance.TileList[T].GetComponent<TileScript>().Unit != RaycastManager.Instance.ActualUnitSelected && TilesManager.Instance.TileList[T].GetComponent<TileScript>().Unit != null && TilesManager.Instance.TileList[T].GetComponent<TileScript>().Unit.GetComponent<UnitScript>().UnitSO.IsInRedArmy != RaycastManager.Instance.ActualUnitSelected.GetComponent<UnitScript>().UnitSO.IsInRedArmy)
                {
                    tile.Add(TilesManager.Instance.TileList[T]);
                }
            }
        }

        foreach (int T in PlayerStatic.GetNeighbourDiag(tileId - 9, TilesManager.Instance.TileList[tileId - 9].GetComponent<TileScript>().Line, false))
        {
            if (TilesManager.Instance.TileList[T] != null)
            {
                if (TilesManager.Instance.TileList[T].GetComponent<TileScript>().Unit != RaycastManager.Instance.ActualUnitSelected && TilesManager.Instance.TileList[T].GetComponent<TileScript>().Unit != null && TilesManager.Instance.TileList[T].GetComponent<TileScript>().Unit.GetComponent<UnitScript>().UnitSO.IsInRedArmy != RaycastManager.Instance.ActualUnitSelected.GetComponent<UnitScript>().UnitSO.IsInRedArmy)
                {
                    tile.Add(TilesManager.Instance.TileList[T]);
                }
            }
        }

        foreach (int T in PlayerStatic.GetNeighbourDiag(tileId + 9, TilesManager.Instance.TileList[tileId + 9].GetComponent<TileScript>().Line, false))
        {
            if (TilesManager.Instance.TileList[T] != null)
            {
                if (TilesManager.Instance.TileList[T].GetComponent<TileScript>().Unit != RaycastManager.Instance.ActualUnitSelected && TilesManager.Instance.TileList[T].GetComponent<TileScript>().Unit != null && TilesManager.Instance.TileList[T].GetComponent<TileScript>().Unit.GetComponent<UnitScript>().UnitSO.IsInRedArmy != RaycastManager.Instance.ActualUnitSelected.GetComponent<UnitScript>().UnitSO.IsInRedArmy)
                {
                    tile.Add(TilesManager.Instance.TileList[T]);
                }
            }
        }

        tile.Remove(TilesManager.Instance.TileList[GetComponent<UnitScript>().ActualTiledId].GetComponent<GameObject>());

        GameManager.Instance._eventCall += EndCpty;
        GameManager.Instance._eventCallCancel += StopCpty;
        GameManager.Instance.StartEventModeTiles(1, GetComponent<UnitScript>().UnitSO.IsInRedArmy, tile, "Distance et Tanto", "Voulez-vous vraiment utiliser cette capacité?");
        base.StartCpty();
    }

    public override void EndCpty()
    {
        attackMiss = Random.Range(1, 37);
        if(attackMiss > 3)
        {
            SoundController.Instance.PlaySound(GetComponent<UnitScript>().SonAttaque);
            GameManager.Instance.TileChooseList[0].GetComponent<TileScript>().Unit.GetComponent<UnitScript>().TakeDamage(GetComponent<UnitScript>().DamageMinimum + GetComponent<UnitScript>()._damageBonus);
            GameManager.Instance._eventCall -= EndCpty;
            GetComponent<UnitScript>().EndCapacity();
            base.EndCpty();
            GameManager.Instance.TileChooseList.Clear();
        }
    }

    public override void StopCpty()
    {
        GameManager.Instance.StopEventModeTile();
        GameManager.Instance.TileChooseList.Clear();
        GetComponent<UnitScript>().StopCapacity(true);
        base.StopCpty();
    }
}
