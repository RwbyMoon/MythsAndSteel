using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MélodieSinistre : Capacity
{




    public override void StartCpty()
    {
   
        
            List<GameObject> unit = new List<GameObject>();
        foreach (int i in PlayerStatic.GetNeighbourDiag(gameObject.GetComponent<UnitScript>().ActualTiledId, TilesManager.Instance.TileList[gameObject.GetComponent<UnitScript>().ActualTiledId].GetComponent<TileScript>().Line, false))
        {
          GameObject  Unit = TilesManager.Instance.TileList[i].GetComponent<TileScript>().Unit;
          if (  Unit != null)
                {
                if (Unit.GetComponent<UnitScript>().UnitSO.typeUnite == MYthsAndSteel_Enum.TypeUnite.Infanterie || Unit.GetComponent<UnitScript>().UnitSO.typeUnite == MYthsAndSteel_Enum.TypeUnite.Leader || Unit.GetComponent<UnitScript>().UnitSO.typeUnite == MYthsAndSteel_Enum.TypeUnite.Artillerie)
                {
                if (!Unit.GetComponent<UnitScript>().UnitStatuts.Contains(MYthsAndSteel_Enum.UnitStatut.Possédé))
                {
                    unit.Add(Unit);
                }
            }
        }
       
            
        }


            GameManager.Instance._eventCall += EndCpty;
            GameManager.Instance._eventCallCancel += StopCpty;
            GameManager.Instance.StartEventModeUnit(1, GetComponent<UnitScript>().UnitSO.IsInRedArmy, unit, "Mélodie Sinistre", "Voulez vous activer une unité adverse de type Leader, Infanterie ou Artillerie durant un tour ?");

        

        base.StartCpty();
    }

    public override void StopCpty()
    {
        GameManager.Instance.StopEventModeUnit();
        GameManager.Instance.UnitChooseList.Clear();
        GetComponent<UnitScript>().StopCapacity(true);
        base.StopCpty();
    }

    public override void EndCpty()
    {

        GetComponent<UnitScript>().EndCapacity();
        foreach (GameObject unit in GameManager.Instance.UnitChooseList)
        {
            unit.GetComponent<UnitScript>().AddStatutToUnit(MYthsAndSteel_Enum.UnitStatut.Possédé);
            unit.GetComponent<UnitScript>().MélodieSinistre = true;
        }
        GameManager.Instance.possesion = true;
  
        base.EndCpty();
        GameManager.Instance.UnitChooseList.Clear();
        GameManager.Instance._eventCall -= EndCpty;
    }
}
