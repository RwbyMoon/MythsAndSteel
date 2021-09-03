using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtDuDepouillement : Capacity
{
    //Cette capacité appartient à Robin Hood

    public override void StartCpty()
    {
        int tileId = RaycastManager.Instance.ActualUnitSelected.GetComponent<UnitScript>().ActualTiledId;
        List<GameObject> tile = new List<GameObject>();

        foreach (int T in PlayerStatic.GetNeighbourDiag(tileId, TilesManager.Instance.TileList[tileId].GetComponent<TileScript>().Line, false))
        {
            if (TilesManager.Instance.TileList[T] != null)
            {
                if (TilesManager.Instance.TileList[T].GetComponent<TileScript>().Unit != RaycastManager.Instance.ActualUnitSelected && TilesManager.Instance.TileList[T].GetComponent<TileScript>().Unit != null && TilesManager.Instance.TileList[T].GetComponent<TileScript>().Unit.GetComponent<UnitScript>().UnitSO.IsInRedArmy != RaycastManager.Instance.ActualUnitSelected.GetComponent<UnitScript>().UnitSO.IsInRedArmy)
                {
                    tile.Add(TilesManager.Instance.TileList[T]);
                }
            }
        }

        tile.Remove(TilesManager.Instance.TileList[GetComponent<UnitScript>().ActualTiledId].GetComponent<GameObject>());

        GameManager.Instance._eventCall += EndCpty;
        GameManager.Instance._eventCallCancel += StopCpty;
        GameManager.Instance.StartEventModeTiles(1, GetComponent<UnitScript>().UnitSO.IsInRedArmy, tile, "Art du Dépouillement", "Voulez-vous vraiment utiliser cette capacité?");
        base.StartCpty();
    }

    public override void EndCpty()
    {
        if(GetComponent<UnitScript>().UnitSO.IsInRedArmy == true && PlayerScript.Instance.BluePlayerInfos.Ressource >0)
        {
            PlayerScript.Instance.RedPlayerInfos.Ressource++;
            PlayerScript.Instance.BluePlayerInfos.Ressource--;
        }
        else if(GetComponent<UnitScript>().UnitSO.IsInRedArmy == false && PlayerScript.Instance.RedPlayerInfos.Ressource >0)
        {
            PlayerScript.Instance.RedPlayerInfos.Ressource--;
            PlayerScript.Instance.BluePlayerInfos.Ressource++;
        }
        if(GameManager.Instance.TileChooseList[0].GetComponent<TileScript>().TileId - GetComponent<UnitScript>().ActualTiledId == -1 || GameManager.Instance.TileChooseList[0].GetComponent<TileScript>().TileId - GetComponent<UnitScript>().ActualTiledId == 1)
        {
            GetComponent<Animator>().SetInteger("CapaSens", 1);
            if(!GetComponent<SpriteRenderer>().flipX && GameManager.Instance.TileChooseList[0].GetComponent<TileScript>().TileId - GetComponent<UnitScript>().ActualTiledId == -1)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
            if (GetComponent<SpriteRenderer>().flipX && GameManager.Instance.TileChooseList[0].GetComponent<TileScript>().TileId - GetComponent<UnitScript>().ActualTiledId == 1)
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
        }
        else if(GameManager.Instance.TileChooseList[0].GetComponent<TileScript>().TileId - GetComponent<UnitScript>().ActualTiledId == 9)
        {
            GetComponent<Animator>().SetInteger("CapaSens", 2);
        }
        else if(GameManager.Instance.TileChooseList[0].GetComponent<TileScript>().TileId - GetComponent<UnitScript>().ActualTiledId == -9)
        {
            GetComponent<Animator>().SetInteger("CapaSens", 3);
        }
        StartCoroutine(WaitEndAnim());
    }

    public override void StopCpty()
    {
        GameManager.Instance.StopEventModeTile();
        GameManager.Instance.TileChooseList.Clear();
        GetComponent<UnitScript>().StopCapacity(true);
        base.StopCpty();
    }

    IEnumerator WaitEndAnim()
    {
        yield return new WaitForSeconds(1.5f);
        GetComponent<Animator>().SetInteger("CapaSens", 0);
        GameManager.Instance._eventCall -= EndCpty;
        GetComponent<UnitScript>().EndCapacity();
        GameManager.Instance.TileChooseList.Clear();
        base.EndCpty();
    }
}
