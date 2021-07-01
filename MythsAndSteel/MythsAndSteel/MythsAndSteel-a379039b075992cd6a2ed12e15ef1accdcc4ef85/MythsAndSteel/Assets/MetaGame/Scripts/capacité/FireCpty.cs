using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCpty : Capacity
{
    [SerializeField] private FireGestion fr;
    private List<GameObject> id = new List<GameObject>();
    [SerializeField] private int Range = 4;
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
        int ressourcePlayer = GetComponent<UnitScript>().UnitSO.IsInRedArmy ? PlayerScript.Instance.RedPlayerInfos.Ressource : PlayerScript.Instance.BluePlayerInfos.Ressource;
        if (ressourcePlayer >= Capacity1Cost)
        {
            id = new List<GameObject>();
            Highlight(GetComponent<UnitScript>().ActualTiledId, Range);
            GameManager.Instance._eventCall += EndCpty;
            GameManager.Instance._eventCallCancel += StopCpty;
            GameManager.Instance.StartEventModeTiles(1, GetComponent<UnitScript>().UnitSO.IsInRedArmy, id, "Embrasement!", "Embrase les cases en forme de croix à partir de la selection centrale. Voulez-vous vraiment effectuer cette action ?");
        }
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
            foreach(int id in PlayerStatic.GetNeighbourDiag(GameManager.Instance.TileChooseList[0].GetComponent<TileScript>().TileId, GameManager.Instance.TileChooseList[0].GetComponent<TileScript>().Line, false))
            {
                fr.CreateFire(id);
            }
            fr.CreateFire(GameManager.Instance.TileChooseList[0].GetComponent<TileScript>().TileId);
            
        }        
        GameManager.Instance._eventCall -= EndCpty;
        GetComponent<UnitScript>().EndCapacity();
        GameManager.Instance.TileChooseList.Clear();
    }
}
