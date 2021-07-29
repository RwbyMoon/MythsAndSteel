using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtocoleDeProtection : Capacity
{
    public override void StartCpty()
    {
        GetComponent<UnitScript>().IsActifNotConsumeAction = true;
        int tileId = RaycastManager.Instance.ActualUnitSelected.GetComponent<UnitScript>().ActualTiledId;
        List<GameObject> tile = new List<GameObject>();
        if(GetComponent<UnitScript>().ActifUsedThisTurn == false)
        {
            foreach (int T in PlayerStatic.GetNeighbourDiag(tileId, TilesManager.Instance.TileList[tileId].GetComponent<TileScript>().Line, false))
            {
                if (TilesManager.Instance.TileList[T] != null)
                {
                    if (TilesManager.Instance.TileList[T].GetComponent<TileScript>().Unit != RaycastManager.Instance.ActualUnitSelected && TilesManager.Instance.TileList[T].GetComponent<TileScript>().Unit != null && TilesManager.Instance.TileList[T].GetComponent<TileScript>().Unit.GetComponent<UnitScript>().UnitSO.IsInJ1Army == RaycastManager.Instance.ActualUnitSelected.GetComponent<UnitScript>().UnitSO.IsInJ1Army && TilesManager.Instance.TileList[T].GetComponent<TileScript>().Unit.GetComponent<UnitScript>().UnitSO.typeUnite == MYthsAndSteel_Enum.TypeUnite.Infanterie)
                    {
                        tile.Add(TilesManager.Instance.TileList[T]);
                    }
                }
            }
            GameManager.Instance._eventCall += EndCpty;
            GameManager.Instance._eventCallCancel += StopCpty;
            GameManager.Instance.StartEventModeTiles(1, GetComponent<UnitScript>().UnitSO.IsInJ1Army, tile, "Protocole de Protection", "Voulez-vous vraiment utiliser cette capacité?");
        }
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
        GameManager.Instance.TileChooseList[0].GetComponent<TileScript>().Unit.GetComponent<UnitScript>()._shield++;
        GameManager.Instance._eventCall -= EndCpty;
        GetComponent<UnitScript>().EndCapacity();
        base.EndCpty();
        GetComponent<UnitScript>().ActifUsedThisTurn = true;
        if (GameManager.Instance.TileChooseList[0].GetComponent<TileScript>().Unit.GetComponent<UnitScript>().UnitSO.IsInJ1Army)
        {
            GameManager.Instance.TileChooseList[0].GetComponent<TileScript>().Unit.GetComponent<UnitScript>().UpdateLifeHeartShieldUI(UIInstance.Instance.J1HeartShieldSprite, GameManager.Instance.TileChooseList[0].GetComponent<TileScript>().Unit.GetComponent<UnitScript>()._life + GameManager.Instance.TileChooseList[0].GetComponent<TileScript>().Unit.GetComponent<UnitScript>()._shield);
        }
        else
        {
            GameManager.Instance.TileChooseList[0].GetComponent<TileScript>().Unit.GetComponent<UnitScript>().UpdateLifeHeartShieldUI(UIInstance.Instance.J2HeartShieldSprite, GameManager.Instance.TileChooseList[0].GetComponent<TileScript>().Unit.GetComponent<UnitScript>()._life + GameManager.Instance.TileChooseList[0].GetComponent<TileScript>().Unit.GetComponent<UnitScript>()._shield);
        }
        GameManager.Instance.TileChooseList.Clear();
        GetComponent<UnitScript>().IsActifNotConsumeAction = false;
    }
}
