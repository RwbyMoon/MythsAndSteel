using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Montgomery : Capacity
{
    [SerializeField] private int PowerUseLeft = 2;
    override public void EndCpty()
    {
        UIInstance.Instance.ActivateNextPhaseButton();

        int firstTileId = GameManager.Instance.UnitChooseList[0].GetComponent<UnitScript>().ActualTiledId;

        TilesManager.Instance.TileList[GameManager.Instance.UnitChooseList[1].GetComponent<UnitScript>().ActualTiledId].GetComponent<TileScript>().AddUnitToTile(GameManager.Instance.UnitChooseList[0].gameObject);
        TilesManager.Instance.TileList[firstTileId].GetComponent<TileScript>().AddUnitToTile(GameManager.Instance.UnitChooseList[1].gameObject);

        GameManager.Instance.UnitChooseList.Clear();
        GameManager.Instance.IllusionStratégique = false;
        GetComponent<UnitScript>().EndCapacity();
        PowerUseLeft--;
        if (PowerUseLeft == 0)
        {
            Destroy(GetComponent<Montgomery>());
        }

    }

    override public void StartCpty()
    {
        List<GameObject> unitList = new List<GameObject>();
        unitList.AddRange(GameManager.Instance.IsPlayerRedTurn ? PlayerScript.Instance.UnitRef.UnitListRedPlayer : PlayerScript.Instance.UnitRef.UnitListBluePlayer);
        GameManager.Instance.StartEventModeUnit(2, GameManager.Instance.IsPlayerRedTurn ? true : false, unitList, Capacity1Name, Capacity1Description + "Utilsiation Restantes: " + PowerUseLeft);
        GameManager.Instance._eventCall += EndCpty;

    }
    override public void StopCpty()
    {

    }

}