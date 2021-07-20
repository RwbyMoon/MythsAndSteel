using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceEtTanto : Capacity
{
    //Cette capacité appartient à l'unité "Impérial" de l'armée Japonaise sur le plateau de Shanghai
    private int attackMiss;
    List<GameObject> newNeighbourId = new List<GameObject>();
    [SerializeField] private int Range = 3;
    void Highlight(int tileId, int currentID, int Range)
    {
        if (Range > 0)
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
                    if (!newNeighbourId.Contains(TilesManager.Instance.TileList[ID]) && TilesManager.Instance.TileList[ID].GetComponent<TileScript>().Unit != null)
                    {
                        if(TilesManager.Instance.TileList[ID].GetComponent<TileScript>().Unit.GetComponent<UnitScript>().UnitSO.IsInRedArmy != GetComponent<UnitScript>().UnitSO.IsInRedArmy)
                        newNeighbourId.Add(TilesManager.Instance.TileList[ID]);
                    }
                    Highlight(ID, currentID, Range - 1); ;
                }
            }
        }
    }
    public override void StartCpty()
    {
        newNeighbourId = new List<GameObject>();
        Highlight(GetComponent<UnitScript>().ActualTiledId, GetComponent<UnitScript>().ActualTiledId, Range);

        GameManager.Instance._eventCall += EndCpty;
        GameManager.Instance._eventCallCancel += StopCpty;
        GameManager.Instance.StartEventModeTiles(1, GetComponent<UnitScript>().UnitSO.IsInRedArmy, newNeighbourId, "Distance et Tanto", "Voulez-vous vraiment utiliser cette capacité?");
        base.StartCpty();
    }

    public override void EndCpty()
    {
        attackMiss = Random.Range(1, 37);
        if(attackMiss > 3)
        {
            SoundController.Instance.PlaySound(GetComponent<UnitScript>().SonAttaque);
            GameManager.Instance.TileChooseList[0].GetComponent<TileScript>().Unit.GetComponent<UnitScript>().TakeDamage(GetComponent<UnitScript>().DamageMinimum + GetComponent<UnitScript>()._damageBonus);
            GameManager.Instance._eventCall -= EndCpty;
            GetComponent<UnitScript>().EndCapacity();
            base.EndCpty();
            GameManager.Instance.TileChooseList.Clear();
        }
    }

    public override void StopCpty()
    {
        GameManager.Instance.StopEventModeTile();
        GameManager.Instance.TileChooseList.Clear();
        GetComponent<UnitScript>().StopCapacity(true);
        base.StopCpty();
    }
}
