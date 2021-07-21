using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealNear : Capacity
{
    [SerializeField] private int HealValue = 1;


    public override void StartCpty()
    {
        int ressourcePlayer = GetComponent<UnitScript>().UnitSO.IsInRedArmy ? PlayerScript.Instance.J1Infos.Ressource : PlayerScript.Instance.J2Infos.Ressource;
        if (ressourcePlayer >= Capacity1Cost)
        {
            List<GameObject> tile = new List<GameObject>();
            foreach (int T in PlayerStatic.GetNeighbourDiag(GetComponent<UnitScript>().ActualTiledId, TilesManager.Instance.TileList[GetComponent<UnitScript>().ActualTiledId].GetComponent<TileScript>().Line, false))
            {
                if(TilesManager.Instance.TileList[T].GetComponent<TileScript>().Unit != null)

                {
                    Unit_SO unit = TilesManager.Instance.TileList[T].GetComponent<TileScript>().Unit.GetComponent<UnitScript>().UnitSO;
                    if (unit.typeUnite == MYthsAndSteel_Enum.TypeUnite.Mecha || unit.typeUnite == MYthsAndSteel_Enum.TypeUnite.Artillerie || unit.typeUnite == MYthsAndSteel_Enum.TypeUnite.Vehicule)
                    {

                        if(unit.IsInRedArmy == GameManager.Instance.IsPlayerRedTurn)
                        {

                    tile.Add(TilesManager.Instance.TileList[T]);
                        }
                    }
                }
            }
            GameManager.Instance._eventCall += EndCpty;
            GameManager.Instance._eventCallCancel += StopCpty;
            GameManager.Instance.StartEventModeTiles(1, GetComponent<UnitScript>().UnitSO.IsInRedArmy, tile, "Equipement optimis�!", "Donne " + HealValue + " PV une unit� adjacente. Voulez-vous vraiment effectuer cette action ?");
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
            PlayerScript.Instance.J1Infos.Ressource -= Capacity1Cost;
        }
        else
        {
            PlayerScript.Instance.J2Infos.Ressource -= Capacity1Cost;
        }
        
        GameManager.Instance._eventCall -= EndCpty;
        GetComponent<UnitScript>().EndCapacity();
        
        if (GameManager.Instance.TileChooseList.Count > 0)
        {
            foreach(GameObject id in GameManager.Instance.TileChooseList)
            {
                GameObject U = TilesManager.Instance.TileList[id.GetComponent<TileScript>().TileId].GetComponent<TileScript>().Unit;
                if (U != null)
                {
                    U.GetComponent<UnitScript>().GiveLife(HealValue);
                }
            }            
        }
        GameManager.Instance.StopEventModeTile();

        GameManager.Instance.TileChooseList.Clear();
    }
}
