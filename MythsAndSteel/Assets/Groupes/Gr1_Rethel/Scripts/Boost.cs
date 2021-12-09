using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boost : Capacity
{
    public override void StartCpty()
    {
        GetComponent<UnitScript>().IsActifNotConsumeAction = true;
        int tileId = RaycastManager.Instance.ActualUnitSelected.GetComponent<UnitScript>().ActualTiledId;
        List<GameObject> unit = new List<GameObject>();

        if (GetComponent<UnitScript>().ActifUsedThisTurn == false)
        {
            unit.Add(TilesManager.Instance.TileList[GetComponent<UnitScript>().ActualTiledId]);

            foreach (int i in PlayerStatic.GetNeighbourDiag(tileId, TilesManager.Instance.TileList[gameObject.GetComponent<UnitScript>().ActualTiledId].GetComponent<TileScript>().Line, false))
            {
                GameObject Unit = TilesManager.Instance.TileList[i].GetComponent<TileScript>().Unit;
                if (Unit != null)
                {
                    if (RaycastManager.Instance.ActualUnitSelected.GetComponent<UnitScript>().UnitSO.IsInRedArmy == Unit.GetComponent<UnitScript>().UnitSO.IsInRedArmy)
                    {
                        unit.Add(TilesManager.Instance.TileList[Unit.GetComponent<UnitScript>().ActualTiledId]);

                        
                    }
                }
            }
            foreach (int i in PlayerStatic.GetNeighbourDiag(tileId + 1, TilesManager.Instance.TileList[tileId + 1].GetComponent<TileScript>().Line, false))
            {
                GameObject Unit = TilesManager.Instance.TileList[i].GetComponent<TileScript>().Unit;
                if (Unit != null)
                {
                    if (RaycastManager.Instance.ActualUnitSelected.GetComponent<UnitScript>().UnitSO.IsInRedArmy == Unit.GetComponent<UnitScript>().UnitSO.IsInRedArmy)
                    {
                        unit.Add(TilesManager.Instance.TileList[Unit.GetComponent<UnitScript>().ActualTiledId]);


                    }
                }
            }

            foreach (int i in PlayerStatic.GetNeighbourDiag(tileId - 1, TilesManager.Instance.TileList[tileId - 1].GetComponent<TileScript>().Line, false))
            {
                GameObject Unit = TilesManager.Instance.TileList[i].GetComponent<TileScript>().Unit;
                if (Unit != null)
                {
                    if (RaycastManager.Instance.ActualUnitSelected.GetComponent<UnitScript>().UnitSO.IsInRedArmy == Unit.GetComponent<UnitScript>().UnitSO.IsInRedArmy)
                    {
                        unit.Add(TilesManager.Instance.TileList[Unit.GetComponent<UnitScript>().ActualTiledId]);


                    }
                }
            }

            foreach (int i in PlayerStatic.GetNeighbourDiag(tileId - 9, TilesManager.Instance.TileList[tileId - 9].GetComponent<TileScript>().Line, false))
            {
                GameObject Unit = TilesManager.Instance.TileList[i].GetComponent<TileScript>().Unit;
                if (Unit != null)
                {
                    if (RaycastManager.Instance.ActualUnitSelected.GetComponent<UnitScript>().UnitSO.IsInRedArmy == Unit.GetComponent<UnitScript>().UnitSO.IsInRedArmy)
                    {
                        unit.Add(TilesManager.Instance.TileList[Unit.GetComponent<UnitScript>().ActualTiledId]);


                    }
                }
            }

            foreach (int i in PlayerStatic.GetNeighbourDiag(tileId + 9, TilesManager.Instance.TileList[tileId + 9].GetComponent<TileScript>().Line, false))
            {
                GameObject Unit = TilesManager.Instance.TileList[i].GetComponent<TileScript>().Unit;
                if (Unit != null)
                {
                    if (RaycastManager.Instance.ActualUnitSelected.GetComponent<UnitScript>().UnitSO.IsInRedArmy == Unit.GetComponent<UnitScript>().UnitSO.IsInRedArmy)
                    {
                        unit.Add(TilesManager.Instance.TileList[Unit.GetComponent<UnitScript>().ActualTiledId]);


                    }
                }
            }
        }
        GameManager.Instance._eventCall += EndCpty;
        GameManager.Instance._eventCallCancel += StopCpty;
        GameManager.Instance.StartEventModeTiles(1, GetComponent<UnitScript>().UnitSO.IsInRedArmy, unit, "Boost", "Voulez-vous vraiment utiliser cette capacité?");

        base.StartCpty();
    }
    public override void StopCpty()
    {
        GetComponent<UnitScript>().IsActifNotConsumeAction = false;
        GameManager.Instance.StopEventModeTile();
        GameManager.Instance.TileChooseList.Clear();
        GetComponent<UnitScript>().StopCapacity(true);
        base.StopCpty();
    }

    public override void EndCpty()
    {


        if (GameManager.Instance.TileChooseList[0].GetComponent<TileScript>().Unit.GetComponent<UnitScript>().Life > 1)
        {
            GameManager.Instance.TileChooseList[0].GetComponent<TileScript>().Unit.GetComponent<UnitScript>().TakeDamage(1);
            GameManager.Instance.TileChooseList[0].GetComponent<TileScript>().Unit.GetComponent<UnitScript>().MoveSpeedBonus += 2;
        }
        else
        {
        }
        GetComponent<UnitScript>().ActifUsedThisTurn = true;
        GameManager.Instance._eventCall -= EndCpty;
        GetComponent<UnitScript>().EndCapacity();
        base.EndCpty();
        GameManager.Instance.TileChooseList.Clear();
        GetComponent<UnitScript>().IsActifNotConsumeAction = false;
    }
}
