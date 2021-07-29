using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TirDouble : Capacity
{
    public override void StartCpty()
    {
        GetComponent<UnitScript>().IsActifNotConsumeAction = true;
        int tileId = RaycastManager.Instance.ActualUnitSelected.GetComponent<UnitScript>().ActualTiledId;
        List<GameObject> tile = new List<GameObject>();
        int ressourcePlayer = GetComponent<UnitScript>().UnitSO.IsInJ1Army ? PlayerScript.Instance.J1Infos.Ressource : PlayerScript.Instance.J2Infos.Ressource;
        if (ressourcePlayer >= Capacity1Cost && GetComponent<UnitScript>().ActifUsedThisTurn == false)
        {
            tile.Add(TilesManager.Instance.TileList[GetComponent<UnitScript>().ActualTiledId]);
            GameManager.Instance._eventCall += EndCpty;
            GameManager.Instance._eventCallCancel += StopCpty;
            GameManager.Instance.StartEventModeTiles(1, GetComponent<UnitScript>().UnitSO.IsInJ1Army, tile, "Tir Double", "Voulez-vous vraiment utiliser cette capacité?");
        }
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
        if (GetComponent<UnitScript>().UnitSO.IsInJ1Army)
        {
            PlayerScript.Instance.J1Infos.Ressource -= Capacity1Cost;
        }
        else
        {
            PlayerScript.Instance.J2Infos.Ressource -= Capacity1Cost;
        }
        GetComponent<UnitScript>().ActifUsedThisTurn = true;
        GetComponent<UnitScript>().NbAttaqueParTour++;
        GameManager.Instance._eventCall -= EndCpty;
        GetComponent<UnitScript>().EndCapacity();
        base.EndCpty();
        GameManager.Instance.TileChooseList.Clear();
        GetComponent<UnitScript>().IsActifNotConsumeAction = false;
    }
}
