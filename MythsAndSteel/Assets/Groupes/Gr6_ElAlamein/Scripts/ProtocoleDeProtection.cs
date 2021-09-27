using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtocoleDeProtection : Capacity
{
    GameObject Bonus;
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
                    if (TilesManager.Instance.TileList[T].GetComponent<TileScript>().Unit != RaycastManager.Instance.ActualUnitSelected && TilesManager.Instance.TileList[T].GetComponent<TileScript>().Unit != null && TilesManager.Instance.TileList[T].GetComponent<TileScript>().Unit.GetComponent<UnitScript>().UnitSO.IsInRedArmy == RaycastManager.Instance.ActualUnitSelected.GetComponent<UnitScript>().UnitSO.IsInRedArmy && TilesManager.Instance.TileList[T].GetComponent<TileScript>().Unit.GetComponent<UnitScript>().UnitSO.typeUnite == MYthsAndSteel_Enum.TypeUnite.Infanterie)
                    {
                        tile.Add(TilesManager.Instance.TileList[T]);
                    }
                }
            }
            GameManager.Instance._eventCall += EndCpty;
            GameManager.Instance._eventCallCancel += StopCpty;
            GameManager.Instance.StartEventModeTiles(1, GetComponent<UnitScript>().UnitSO.IsInRedArmy, tile, "Protocole de Protection", "Voulez-vous vraiment utiliser cette capacité?");
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
        GameManager.Instance.TileChooseList[0].GetComponent<TileScript>().Unit.transform.GetChild(2).GetComponent<Animator>().SetBool("Shield", true);
        Bonus = GameManager.Instance.TileChooseList[0];
        StartCoroutine(WaitEndAnim());
        GameManager.Instance._eventCall -= EndCpty;
        GetComponent<UnitScript>().EndCapacity();
        base.EndCpty();
        GetComponent<UnitScript>().ActifUsedThisTurn = true;
        if (GameManager.Instance.TileChooseList[0].GetComponent<TileScript>().Unit.GetComponent<UnitScript>().UnitSO.IsInRedArmy)
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

    IEnumerator WaitEndAnim()
    {
        
        yield return new WaitForSeconds(GameManager.Instance.TileChooseList[0].GetComponent<TileScript>().Unit.transform.GetChild(2).GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + 1.5f);
        Bonus.GetComponent<TileScript>().Unit.transform.GetChild(2).GetComponent<Animator>().SetBool("Shield", false);
    }
}
