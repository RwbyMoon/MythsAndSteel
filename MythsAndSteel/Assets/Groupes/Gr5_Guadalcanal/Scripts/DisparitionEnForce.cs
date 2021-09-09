using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparitionEnForce : MonoBehaviour
{
    bool WaitToExplode;
    void Update()
    {
        if (OrgoneManager.Instance.OrgoneHasBoom && !WaitToExplode)
        {
            WaitToExplode = true;
            foreach(int i in PlayerStatic.GetNeighbourDiag(GetComponent<UnitScript>().ActualTiledId, TilesManager.Instance.TileList[GetComponent<UnitScript>().ActualTiledId].GetComponent<TileScript>().Line, false))
            {
                if(TilesManager.Instance.TileList[i].GetComponent<TileScript>().Unit != null)
                {
                    TilesManager.Instance.TileList[i].GetComponent<TileScript>().Unit.GetComponent<UnitScript>().TakeDamage(1);
                }
            }
            GetComponent<UnitScript>().Death();
        }
    }
}
