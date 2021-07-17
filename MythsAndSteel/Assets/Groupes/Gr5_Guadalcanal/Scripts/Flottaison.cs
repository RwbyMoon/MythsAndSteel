using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flottaison : Capacity
{
    public override void StartCpty()
    {
        GetComponent<UnitScript>().IsActifNotConsumeAction = true;
        List<GameObject> tile = new List<GameObject>();
        if (GetComponent<UnitScript>().ActifUsedThisTurn == false)
        {
            foreach (GameObject gam in TilesManager.Instance.TileList)
            {
                if(gam.GetComponent<TileScript>().Unit != null)
                {
                    tile.Add(gam);
                }
            }
        }
        GameManager.Instance._eventCall += EndCpty;
        GameManager.Instance._eventCallCancel += StopCpty;
        GameManager.Instance.StartEventModeTiles(1, GetComponent<UnitScript>().UnitSO.IsInRedArmy, tile, "Flottaison", "Voulez-vous vraiment rendre cette unité insensible à l'eau?");
    }

    public override void StopCpty()
    {
        GameManager.Instance.TileChooseList.Clear();
        GetComponent<UnitScript>().IsActifNotConsumeAction = false;
        GameManager.Instance.StopEventModeTile();
        GameManager.Instance.TileChooseList.Clear();
        GetComponent<UnitScript>().StopCapacity(true);
    }

    public override void EndCpty()
    {
        GameManager.Instance.TileChooseList[0].GetComponent<TileScript>().Unit.GetComponent<UnitScript>().Amphibie = true;
        GameManager.Instance.TileChooseList[0].GetComponent<TileScript>().Unit.GetComponent<UnitScript>().Submersible = true;
        GameManager.Instance._eventCall -= EndCpty;
        GetComponent<UnitScript>().EndCapacity();
        GetComponent<UnitScript>().ActifUsedThisTurn = true;
        GameManager.Instance.TileChooseList.Clear();
        GetComponent<UnitScript>().IsActifNotConsumeAction = false;
    }
}
