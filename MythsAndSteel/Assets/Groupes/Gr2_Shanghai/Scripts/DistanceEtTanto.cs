using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceEtTanto : Capacity
{
    //Cette capacité appartient à l'unité "Impérial" de l'armée Japonaise sur le plateau de Shanghai
    private int attackMiss;
    private List<GameObject> id = new List<GameObject>();
    [SerializeField] private int Range = 2;
    public void Highlight(int tileId, int Range, int lasttileId = 999)
    {
        if (Range > 0)
        {
            id.Add(TilesManager.Instance.TileList[tileId]);
            foreach (int ID in PlayerStatic.GetNeighbourDiag(tileId, TilesManager.Instance.TileList[tileId].GetComponent<TileScript>().Line, false))
            {
                if (lasttileId != tileId)
                {
                    Highlight(ID, Range - 1, tileId);
                }
            }
        }
    }
    public override void StartCpty()
    {
        id = new List<GameObject>();
        Highlight(GetComponent<UnitScript>().ActualTiledId, Range);

        GameManager.Instance._eventCall += EndCpty;
        GameManager.Instance._eventCallCancel += StopCpty;
        GameManager.Instance.StartEventModeTiles(1, GetComponent<UnitScript>().UnitSO.IsInRedArmy, id, "Distance et Tanto", "Voulez-vous vraiment utiliser cette capacité?");
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
