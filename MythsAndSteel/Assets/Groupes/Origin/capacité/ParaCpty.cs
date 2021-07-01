using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParaCpty : Capacity
{
    private List<GameObject> id = new List<GameObject>();
    [SerializeField] private int Range = 4;
    public void Highlight(int tileId, int Range, int lasttileId = 999)
    {
        if (Range > 0)
        {
            GameObject unit = TilesManager.Instance.TileList[tileId].GetComponent<TileScript>().Unit;
            if (unit != null && unit.GetComponent<UnitScript>().UnitSO.IsInRedArmy != GetComponent<UnitScript>().UnitSO.IsInRedArmy)
            {
                id.Add(TilesManager.Instance.TileList[tileId]);
            }
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
        int ressourcePlayer = GetComponent<UnitScript>().UnitSO.IsInRedArmy ? PlayerScript.Instance.RedPlayerInfos.Ressource : PlayerScript.Instance.BluePlayerInfos.Ressource;
        if (ressourcePlayer >= Capacity1Cost)
        {
            id = new List<GameObject>();
            Highlight(GetComponent<UnitScript>().ActualTiledId, Range);
            GameManager.Instance._eventCall += EndCpty;
            GameManager.Instance._eventCallCancel += StopCpty;
            GameManager.Instance.StartEventModeTiles(1, GetComponent<UnitScript>().UnitSO.IsInRedArmy, id, "Corruption des Troupes!", "Paralyse une unité ennemie. Voulez-vous vraiment effectuer cette action ?");
        }
    }

    public override void StopCpty()
    {
        GameManager.Instance.StopEventModeTile();
        GameManager.Instance.TileChooseList.Clear();
        GameManager.Instance.StopEventModeUnit(); GameManager.Instance.UnitChooseList.Clear();

        GetComponent<UnitScript>().StopCapacity(true);
        base.StopCpty();
    }

    public override void EndCpty()
    {
        if (GetComponent<UnitScript>().UnitSO.IsInRedArmy)
        {
            PlayerScript.Instance.RedPlayerInfos.Ressource -= Capacity1Cost;
        }
        else
        {
            PlayerScript.Instance.BluePlayerInfos.Ressource -= Capacity1Cost;
        }
        
        if(GameManager.Instance.TileChooseList.Count > 0)
        {
            GameManager.Instance.TileChooseList[0].GetComponent<TileScript>().Unit.GetComponent<UnitScript>().AddStatutToUnit(MYthsAndSteel_Enum.UnitStatut.Paralysie);
          
        }
        if (GameManager.Instance.IsPlayerRedTurn)
        {

            GameManager.Instance.TileChooseList[0].GetComponent<TileScript>().Unit.GetComponent<UnitScript>().ParalysieStat = 1;
        }
        else
        {
            GameManager.Instance.TileChooseList[0].GetComponent<TileScript>().Unit.GetComponent<UnitScript>().ParalysieStat = 2;
        }
        GameManager.Instance._eventCall -= EndCpty;
        GetComponent<UnitScript>().EndCapacity();
        GameManager.Instance.TileChooseList.Clear();
    }
}
