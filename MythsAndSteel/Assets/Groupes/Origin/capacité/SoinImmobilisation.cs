using System.Collections.Generic;
using UnityEngine;

public class SoinImmobilisation : Capacity
{
    List<GameObject> newNeighbourId = new List<GameObject>();
    [SerializeField] private int Range;
    void Highlight(int tileId, int currentID, int Range)
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
                    if (!newNeighbourId.Contains(TilesManager.Instance.TileList[ID]) && TilesManager.Instance.TileList[ID].GetComponent<TileScript>().Unit != null)
                    {
                        newNeighbourId.Add(TilesManager.Instance.TileList[ID]);
                    }
                    Highlight(ID, currentID, Range - 1); ;
                }
            }
        }
    }
    public override void StartCpty()
    {
        Range = GetComponent<UnitScript>().AttackRange + GetComponent<UnitScript>().AttackRangeBonus;
        newNeighbourId = new List<GameObject>();
        Highlight(GetComponent<UnitScript>().ActualTiledId, GetComponent<UnitScript>().ActualTiledId, Range);

        GameManager.Instance._eventCall += EndCpty;
        GameManager.Instance._eventCallCancel += StopCpty;
        GameManager.Instance.StartEventModeTiles(1, false, newNeighbourId, "Soin/Immobilisation", "Voulez-vous vraiment soigner/immobiliser cette unitée ?");
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
        if (GameManager.Instance.TileChooseList[0].GetComponent<TileScript>().Unit.GetComponent<UnitScript>().UnitSO.IsInRedArmy != GameManager.Instance.IsPlayerRedTurn)
        {
            GameManager.Instance.TileChooseList[0].GetComponent<TileScript>().Unit.GetComponent<UnitScript>().AddStatutToUnit(MYthsAndSteel_Enum.UnitStatut.Immobilisation);
           if(GameManager.Instance.IsPlayerRedTurn)
           {
                GameManager.Instance.statetImmobilisation = 1;
           }
           else
           {
                GameManager.Instance.statetImmobilisation = 2;
           }
        }
        else if (GameManager.Instance.TileChooseList[0].GetComponent<TileScript>().Unit.GetComponent<UnitScript>().UnitSO.IsInRedArmy == GameManager.Instance.IsPlayerRedTurn)   
        {
            GameManager.Instance.TileChooseList[0].GetComponent<TileScript>().Unit.GetComponent<UnitScript>().GiveLife(2);
        }
        
        GameManager.Instance._eventCall -= EndCpty;
        GetComponent<UnitScript>().EndCapacity();
        base.EndCpty();
        GameManager.Instance.TileChooseList.Clear();
        
    }
}
