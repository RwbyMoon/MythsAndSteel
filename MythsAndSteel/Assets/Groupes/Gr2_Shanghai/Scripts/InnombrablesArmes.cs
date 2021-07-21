using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnombrablesArmes : MonoBehaviour
{
    bool StartCapa = false;
    int CentralTile;
    
    void Start()
    {
        GetComponent<UnitScript>().AttaqueEnLigne = true;
        GetComponent<UnitScript>().UnitSO.CanAttackEmptyTile = true;
    }

    
    void Update()
    {
        if (GetComponent<UnitScript>().InflictMinimumDamages && !StartCapa)
        {
            StartCapa = true;
            InflictDamageNear();
        }
        CentralTile = Attaque.Instance.idTileCible;
    }

    void InflictDamageNear()
    {
        foreach (int T in PlayerStatic.GetNeighbourDiag(CentralTile, TilesManager.Instance.TileList[CentralTile].GetComponent<TileScript>().Line, false))
        {
            if(-8 <= GetComponent<UnitScript>().ActualTiledId - CentralTile && GetComponent<UnitScript>().ActualTiledId - CentralTile <= 8)
            {
                if((T - CentralTile == 9 || T - CentralTile == -9) && TilesManager.Instance.TileList[T].GetComponent<TileScript>().Unit != null)
                {
                    TilesManager.Instance.TileList[T].GetComponent<TileScript>().Unit.GetComponent<UnitScript>().TakeDamage(GetComponent<UnitScript>().DamageMaximum + GetComponent<UnitScript>().DamageBonus);
                }
            }
            else
            {
                if ((T - CentralTile == 1 || T - CentralTile == -1) && TilesManager.Instance.TileList[T].GetComponent<TileScript>().Unit != null)
                {
                    TilesManager.Instance.TileList[T].GetComponent<TileScript>().Unit.GetComponent<UnitScript>().TakeDamage(GetComponent<UnitScript>().DamageMaximum + GetComponent<UnitScript>().DamageBonus);
                }
            }
        }
        StartCoroutine(DelayRestart());
    }

    IEnumerator DelayRestart()
    {
        yield return new WaitForSeconds(3);
        StartCapa = false;
    }
}
