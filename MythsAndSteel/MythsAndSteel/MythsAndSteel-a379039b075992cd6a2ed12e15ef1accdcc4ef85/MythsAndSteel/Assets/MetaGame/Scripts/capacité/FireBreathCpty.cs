using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBreathCpty : Capacity
{
    [SerializeField] private int LineCount = 2;


    public override void StartCpty()
    {
        int ressourcePlayer = GetComponent<UnitScript>().UnitSO.IsInRedArmy ? PlayerScript.Instance.RedPlayerInfos.Ressource : PlayerScript.Instance.BluePlayerInfos.Ressource;
        if (ressourcePlayer >= Capacity1Cost)
        {
            List<GameObject> tile = new List<GameObject>();
            foreach (int T in PlayerStatic.GetNeighbourDiag(GetComponent<UnitScript>().ActualTiledId, TilesManager.Instance.TileList[GetComponent<UnitScript>().ActualTiledId].GetComponent<TileScript>().Line, false))
            {
                tile.Add(TilesManager.Instance.TileList[T]);
            }
            GameManager.Instance._eventCall += EndCpty;
            GameManager.Instance._eventCallCancel += StopCpty;
            GameManager.Instance.StartEventModeTiles(1, GetComponent<UnitScript>().UnitSO.IsInRedArmy, tile, "Souffle de feu!", "Inflige 2 points de dégâts sur 3 cases en ligne droite. Voulez-vous vraiment effectuer cette action ?");
        }
    }

    public override void StopCpty()
    {
        GameManager.Instance.StopEventModeTile();
        GameManager.Instance.TileChooseList.Clear();
        GetComponent<UnitScript>().StopCapacity(true);
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
        
        GameManager.Instance._eventCall -= EndCpty;
        GetComponent<UnitScript>().EndCapacity();

        MYthsAndSteel_Enum.Direction Direct = PlayerStatic.CheckDirection(GetComponent<UnitScript>().ActualTiledId, GameManager.Instance.TileChooseList[GameManager.Instance.TileChooseList.Count - 1].GetComponent<TileScript>().TileId);

        for (int i = 0; i <= LineCount; i++)
        {
            TileScript LastTile = GameManager.Instance.TileChooseList[GameManager.Instance.TileChooseList.Count - 1].GetComponent<TileScript>();
            bool check = false;
            foreach (int D in PlayerStatic.GetNeighbourDiag(LastTile.TileId, LastTile.Line, false))
            {
                if (PlayerStatic.CheckDirection(LastTile.TileId, TilesManager.Instance.TileList[D].GetComponent<TileScript>().TileId) == Direct)
                {
                    check = true;
                    GameManager.Instance.TileChooseList.Add(TilesManager.Instance.TileList[D]);
                }
            }
            if (check == false)
            {
                break;
            }
        }
        
        if (GameManager.Instance.TileChooseList.Count > 0)
        {
            foreach(GameObject id in GameManager.Instance.TileChooseList)
            {
                GameObject U = TilesManager.Instance.TileList[id.GetComponent<TileScript>().TileId].GetComponent<TileScript>().Unit;
                if (U != null)
                {
                    U.GetComponent<UnitScript>().TakeDamage(2);
                }
            }            
        }
        GameManager.Instance.StopEventModeTile();

        GameManager.Instance.TileChooseList.Clear();
    }
}
