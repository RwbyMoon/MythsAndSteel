using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LameTempete : MonoBehaviour
{
    List<GameObject> TilesInRange = new List<GameObject>();
    bool startCapa;
    int damageInflicted;
    bool knockStart;

    void Start()
    {
        GetComponent<UnitScript>().AttaqueEnLigne = true;
    }

    void Update()
    {
        if((GetComponent<UnitScript>().InflictMaximumDamages || GetComponent<UnitScript>().InflictMinimumDamages) && !startCapa)
        {
            startCapa = true;
            if (GetComponent<UnitScript>().InflictMaximumDamages) damageInflicted = GetComponent<UnitScript>().DamageMaximum + GetComponent<UnitScript>().DamageBonus;
            else damageInflicted = GetComponent<UnitScript>().DamageMinimum + GetComponent<UnitScript>().DamageBonus;
            
            if(Attaque.Instance.idTileCible - GetComponent<UnitScript>().ActualTiledId >= 1 && Attaque.Instance.idTileCible - GetComponent<UnitScript>().ActualTiledId <= 8)
            {
                ListTilesInRange(GetComponent<UnitScript>().ActualTiledId, GetComponent<UnitScript>().ActualTiledId, GetComponent<UnitScript>().AttackRange + GetComponent<UnitScript>().AttackRangeBonus, 2);
            }
            else if(Attaque.Instance.idTileCible - GetComponent<UnitScript>().ActualTiledId >= -8 && Attaque.Instance.idTileCible - GetComponent<UnitScript>().ActualTiledId <= -1)
            {
                ListTilesInRange(GetComponent<UnitScript>().ActualTiledId, GetComponent<UnitScript>().ActualTiledId, GetComponent<UnitScript>().AttackRange + GetComponent<UnitScript>().AttackRangeBonus, 4);
            }
            else if (Attaque.Instance.idTileCible - GetComponent<UnitScript>().ActualTiledId >= 9)
            {
                ListTilesInRange(GetComponent<UnitScript>().ActualTiledId, GetComponent<UnitScript>().ActualTiledId, GetComponent<UnitScript>().AttackRange + GetComponent<UnitScript>().AttackRangeBonus, 1);
            }
            else if (Attaque.Instance.idTileCible - GetComponent<UnitScript>().ActualTiledId <= -9)
            {
                ListTilesInRange(GetComponent<UnitScript>().ActualTiledId, GetComponent<UnitScript>().ActualTiledId, GetComponent<UnitScript>().AttackRange + GetComponent<UnitScript>().AttackRangeBonus, 3);
            }
        }
    }

    void ListTilesInRange(int tileId, int currentID, int Range, int direction, int InfoLigneDroite = 1)
    {
        if(Range > 0)
        {
            foreach (int ID in PlayerStatic.GetNeighbourDiag(tileId, TilesManager.Instance.TileList[tileId].GetComponent<TileScript>().Line, false))
            {
                TileScript TileSc = TilesManager.Instance.TileList[ID].GetComponent<TileScript>();
                bool i = false;

                if (ID == currentID)
                {
                    i = true;
                }

                if (!i)
                {
                    if (!TilesInRange.Contains(TilesManager.Instance.TileList[ID]))
                    {
                        if (((ID - currentID) / InfoLigneDroite == 9 && direction == 1)|| ((ID - currentID) / InfoLigneDroite == -9 && direction == 3)|| ((ID - currentID) / InfoLigneDroite == 1 && direction == 2)|| ((ID - currentID) / InfoLigneDroite == -1 && direction == 4)) 
                        {
                            if(TilesManager.Instance.TileList[ID].GetComponent<TileScript>().Unit != null) TilesInRange.Add(TilesManager.Instance.TileList[ID].GetComponent<TileScript>().Unit);
                        }
                    }
                    ListTilesInRange(ID, currentID, Range - 1, direction,  InfoLigneDroite + 1);
                }
            }
        }
        else
        {
            InflictDamage(direction, Range);
        }
    }

    void InflictDamage(int direction, int Range)
    {
        foreach(GameObject target in TilesInRange)
        {
            if(target != TilesManager.Instance.TileList[Attaque.Instance.idTileCible].GetComponent<TileScript>().Unit)
            {
                target.GetComponent<UnitScript>().TakeDamage(damageInflicted);
            }
        }
        Knockback(direction, Range);
    }

    void Knockback(int direction, int Range)
    {
        /*
        if(TilesInRange.Count > 0 && !knockStart)
        {
            knockStart = true;
            Debug.Log(TilesInRange.Count);
            Debug.Log(TilesInRange[TilesInRange.Count - 1]);
            
            foreach(int T in PlayerStatic.GetNeighbourDiag(TilesInRange[TilesInRange.Count - 1].GetComponent<UnitScript>().ActualTiledId, TilesManager.Instance.TileList[TilesInRange[TilesInRange.Count - 1].GetComponent<UnitScript>().ActualTiledId].GetComponent<TileScript>().Line, false))
            {
                if(((T - TilesInRange[TilesInRange.Count - 1].GetComponent<UnitScript>().ActualTiledId) == 9 && direction == 1) || ((T - TilesInRange[TilesInRange.Count - 1].GetComponent<UnitScript>().ActualTiledId) == 1 && direction == 2) || ((T - TilesInRange[TilesInRange.Count - 1].GetComponent<UnitScript>().ActualTiledId) == -9 && direction == 3) || ((T - TilesInRange[TilesInRange.Count - 1].GetComponent<UnitScript>().ActualTiledId) == -9 && direction == 4))
                {
                    if(TilesManager.Instance.TileList[T].GetComponent<TileScript>().Unit != null)
                    {
                        while (TilesInRange[TilesInRange.Count - 1].transform.position != TilesManager.Instance.TileList[T].transform.position)
                        {
                            TilesInRange[TilesInRange.Count - 1].transform.position = Vector3.MoveTowards(TilesInRange[TilesInRange.Count - 1].transform.position, TilesManager.Instance.TileList[T].transform.position, .7f);
                            GameManager.Instance.WaitToMove(.025f);
                            return;
                        }
                        TilesManager.Instance.TileList[TilesInRange[TilesInRange.Count - 1].GetComponent<UnitScript>().ActualTiledId].GetComponent<TileScript>().RemoveUnitFromTile();
                        TilesManager.Instance.TileList[T].GetComponent<TileScript>().AddUnitToTile(TilesInRange[TilesInRange.Count - 1]);
                    }
                }
            }
            TilesInRange.Remove(TilesInRange[TilesInRange.Count - 1]);
            knockStart = false;
            Knockback(direction, Range - 1);
        }
        else
        {
            StartCoroutine(ResetCapa());
        }
        */
    }

    IEnumerator ResetCapa()
    {
        yield return new WaitForSeconds(3);
        startCapa = false;
    }
}
