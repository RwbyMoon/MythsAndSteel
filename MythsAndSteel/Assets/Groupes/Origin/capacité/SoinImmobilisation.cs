using System.Collections.Generic;
using UnityEngine;

public class SoinImmobilisation : Capacity
{
    private List<GameObject> id = new List<GameObject>();
    [SerializeField] private int Range = 2;
    public void Highlight(int tileId, int Range, int lasttileId = 999)
    {
        if (Range > 0)
        {
            id.Add(TilesManager.Instance.TileList[tileId]);
            foreach (int ID in PlayerStatic.GetNeighbourDiag(tileId, TilesManager.Instance.TileList[tileId].GetComponent<TileScript>().Line, false))
            {
                if (lasttileId != tileId)
                {
                    Highlight(ID, Range - 1, tileId);
                }
            }
        }
    }
    public override void StartCpty()
    {
        Range = GetComponent<UnitScript>().AttackRange + GetComponent<UnitScript>().AttackRangeBonus;
        id = new List<GameObject>();
        Highlight(GetComponent<UnitScript>().ActualTiledId, Range);

        GameManager.Instance._eventCall += EndCpty;
        GameManager.Instance._eventCallCancel += StopCpty;
        GameManager.Instance.StartEventModeTiles(1, false, id, "Soin/Immobilisation", "Voulez-vous vraiment soigner/immobiliser cette unitée ?");
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
