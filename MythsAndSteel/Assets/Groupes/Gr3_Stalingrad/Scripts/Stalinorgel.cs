using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stalinorgel : Capacity
{
    int Range = 2;
    List<GameObject> newNeighbourId = new List<GameObject>();
    List<GameObject> ZoneImpact = new List<GameObject>();
    List<GameObject> DoubleImpact = new List<GameObject>();

    public override void StartCpty()
    {
        int ressourcePlayer = GetComponent<UnitScript>().UnitSO.IsInRedArmy ? PlayerScript.Instance.J1Infos.Ressource : PlayerScript.Instance.J2Infos.Ressource;
        if(ressourcePlayer >= Capacity1Cost)
        {
            RangeSelect(GetComponent<UnitScript>().ActualTiledId, GetComponent<UnitScript>().ActualTiledId, Range);
        }
        GameManager.Instance._eventCall += EndCpty;
        GameManager.Instance._eventCallCancel += StopCpty;
        GameManager.Instance.StartEventModeTiles(1, GetComponent<UnitScript>().UnitSO.IsInRedArmy, newNeighbourId, "Stalinorgel", "Voulez-vous vraiment utiliser cette capacité?");
    }

    void RangeSelect(int tileId, int currentID, int Range)
    {
        if (Range > 0)
        {
            foreach (int ID in PlayerStatic.GetNeighbourDiag(tileId, TilesManager.Instance.TileList[tileId].GetComponent<TileScript>().Line, false))
            {
                TileScript TileSc = TilesManager.Instance.TileList[ID].GetComponent<TileScript>();
                bool i = false;

                if (ID == currentID)
                {
                    i = true;

                }

                if (!i)
                {
                    if (!newNeighbourId.Contains(TilesManager.Instance.TileList[ID]))
                    {
                            newNeighbourId.Add(TilesManager.Instance.TileList[ID]);
                    }
                    RangeSelect(ID, currentID, Range - 1); ;
                }
            }
        }
        if(Range == 0)
        {
            foreach(int ID in PlayerStatic.GetNeighbourDiag(GetComponent<UnitScript>().ActualTiledId, TilesManager.Instance.TileList[GetComponent<UnitScript>().ActualTiledId].GetComponent<TileScript>().Line, true))
            {
                if (newNeighbourId.Contains(TilesManager.Instance.TileList[ID]))
                {
                    newNeighbourId.Remove(TilesManager.Instance.TileList[ID]);
                }
            }
        }
    }

    public override void StopCpty()
    {
        GameManager.Instance.StopEventModeTile();
        GameManager.Instance.TileChooseList.Clear();
        GetComponent<UnitScript>().StopCapacity(true);
        newNeighbourId.Clear();
        ZoneImpact.Clear();
        DoubleImpact.Clear();
    }

    public override void EndCpty()
    {
        ZoneImpact.Add(GameManager.Instance.TileChooseList[0]);
        foreach(int i in PlayerStatic.GetNeighbourDiag(GameManager.Instance.TileChooseList[0].GetComponent<TileScript>().TileId, TilesManager.Instance.TileList[GameManager.Instance.TileChooseList[0].GetComponent<TileScript>().TileId].GetComponent<TileScript>().Line, true))
        {
            ZoneImpact.Add(TilesManager.Instance.TileList[i]);
        }
        int DoubleShot = 0;
        DoubleShot = Random.Range(0, ZoneImpact.Count);
        DoubleImpact.Add(ZoneImpact[DoubleShot]);
        ZoneImpact.Remove(ZoneImpact[DoubleShot]);
        DoubleShot = Random.Range(0, ZoneImpact.Count);
        DoubleImpact.Add(ZoneImpact[DoubleShot]);
        ZoneImpact.Remove(ZoneImpact[DoubleShot]);
        DoubleShot = Random.Range(0, ZoneImpact.Count);
        DoubleImpact.Add(ZoneImpact[DoubleShot]);
        ZoneImpact.Remove(ZoneImpact[DoubleShot]);
        foreach (GameObject tile in ZoneImpact)
        {
            tile.GetComponent<TileScript>().Unit.GetComponent<UnitScript>().TakeDamage(1);
        }
        foreach(GameObject gam in DoubleImpact)
        {
            gam.GetComponent<TileScript>().Unit.GetComponent<UnitScript>().TakeDamage(2);
        }
        GameManager.Instance.TileChooseList.Clear();
        GameManager.Instance._eventCall -= EndCpty;
        base.EndCpty();
        GetComponent<UnitScript>().EndCapacity();
        newNeighbourId.Clear();
        ZoneImpact.Clear();
        DoubleImpact.Clear();
        GameManager.Instance.StopEventModeTile();
    }
}
