using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeineMaximale : MonoBehaviour
{
    List<GameObject> ApplyBonus = new List<GameObject>();
    List<GameObject> HasBonus = new List<GameObject>();
    List<GameObject> TilesWithUnit = new List<GameObject>();
    bool LaunchAura = false;
    bool WaitBonusEnd = false;
    int ActualTile;
    bool ResetAura;

    private void Start()
    {
        ActualTile = GetComponent<UnitScript>().ActualTiledId;
    }

    private void Update()
    {
        if(GetComponent<UnitScript>()._hasStartMove || GetComponent<UnitScript>().IsActivationDone)
        {
            int tileId = GetComponent<UnitScript>().ActualTiledId;
            if (ActualTile != GetComponent<UnitScript>().ActualTiledId && LaunchAura == false && !Mouvement.Instance.IsInMouvement)
            {
                LaunchAura = true;
                ActualTile = GetComponent<UnitScript>().ActualTiledId;
                TilesWithUnit.Clear();
                StartCoroutine(DelayBonus());
            }
            if (ActualTile == GetComponent<UnitScript>().ActualTiledId && !Mouvement.Instance.IsInMouvement)
            {
                LaunchAura = false;
            }
            foreach (int T in PlayerStatic.GetNeighbourDiag(tileId, TilesManager.Instance.TileList[tileId].GetComponent<TileScript>().Line, false))
            {
                if (TilesManager.Instance.TileList[T].GetComponent<TileScript>().Unit != null && !TilesWithUnit.Contains(TilesManager.Instance.TileList[T]) && !Mouvement.Instance.IsInMouvement && ActualTile == GetComponent<UnitScript>().ActualTiledId && !WaitBonusEnd)
                {
                    TilesWithUnit.Add(TilesManager.Instance.TileList[T]);
                    StartCoroutine(DelayBonus());
                }
                if (TilesManager.Instance.TileList[T].GetComponent<TileScript>().Unit == null && TilesWithUnit.Contains(TilesManager.Instance.TileList[T]) && !Mouvement.Instance.IsInMouvement && ActualTile == GetComponent<UnitScript>().ActualTiledId && !WaitBonusEnd)
                {
                    TilesWithUnit.Remove(TilesManager.Instance.TileList[T]);
                    StartCoroutine(DelayBonus());
                }
            }
        }
        if(!GetComponent<UnitScript>()._hasStartMove && !GetComponent<UnitScript>().IsActivationDone && !ResetAura)
        {
            ResetAura = true;
            RemoveBonus();
        }
    }
    private void AddBonus()
    {
        ApplyBonus.Clear();
        int tileId = GetComponent<UnitScript>().ActualTiledId;

        foreach (int T in PlayerStatic.GetNeighbourDiag(tileId, TilesManager.Instance.TileList[tileId].GetComponent<TileScript>().Line, false))
        {
            if (TilesManager.Instance.TileList[T] != null)
            {
                if (TilesManager.Instance.TileList[T].GetComponent<TileScript>().Unit != this && TilesManager.Instance.TileList[T].GetComponent<TileScript>().Unit != null && TilesManager.Instance.TileList[T].GetComponent<TileScript>().Unit.GetComponent<UnitScript>().UnitSO.IsInRedArmy == GetComponent<UnitScript>().UnitSO.IsInRedArmy)
                {
                    TilesWithUnit.Add(TilesManager.Instance.TileList[T]);
                    ApplyBonus.Add(TilesManager.Instance.TileList[T]);
                }
            }
        }

        if (ApplyBonus.Count > 0)
        {
            foreach (GameObject id in ApplyBonus)
            {
                GameObject U = TilesManager.Instance.TileList[id.GetComponent<TileScript>().TileId].GetComponent<TileScript>().Unit;
                if (U != null)
                {
                    HasBonus.Add(U);
                    U.GetComponent<UnitScript>().DiceBonus += 42;
                }
            }
        }
        WaitBonusEnd = false;
    }

    void RemoveBonus()
    {
        WaitBonusEnd = true;
        foreach (GameObject U in HasBonus)
        {
            U.GetComponent<UnitScript>().DiceBonus -= 42;
        }
        StartCoroutine(ClearHasBonus());
    }

    IEnumerator ClearHasBonus()
    {
        yield return new WaitForSeconds(1);
        HasBonus.Clear();
        ResetAura = false;
    }

    IEnumerator DelayBonus()
    {
        RemoveBonus();
        yield return new WaitForSeconds(1);
        AddBonus();
    }
}