using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PluieMortelle : MonoBehaviour
{
    bool StartCapa = false;
    int CentralTile;
    private void Update()
    {
        if((GetComponent<UnitScript>().InflictMaximumDamages || GetComponent<UnitScript>().InflictMinimumDamages || GetComponent<UnitScript>().FailAttack) && !StartCapa)
        {
            StartCapa = true;
            InflictDamageNear();
        }
        CentralTile = Attaque.Instance.idTileCible;
    }

    void InflictDamageNear()
    {
        foreach(int T in PlayerStatic.GetNeighbourDiag(CentralTile, TilesManager.Instance.TileList[CentralTile].GetComponent<TileScript>().Line, false))
        {
            if(TilesManager.Instance.TileList[T].GetComponent<TileScript>().Unit != null)
            {
                TilesManager.Instance.TileList[T].GetComponent<TileScript>().Unit.GetComponent<UnitScript>().TakeDamage(1);
            }
        }
        StartCoroutine(DelayRestart());
    }

    IEnumerator DelayRestart()
    {
        yield return new WaitForSeconds(2);
        StartCapa = false;
    }
}
