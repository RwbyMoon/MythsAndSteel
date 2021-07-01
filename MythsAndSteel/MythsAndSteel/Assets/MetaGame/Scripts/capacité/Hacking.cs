using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hacking : Capacity
{




    public override void StartCpty()
    {
        int ressourcePlayer = GetComponent<UnitScript>().UnitSO.IsInRedArmy ? PlayerScript.Instance.RedPlayerInfos.Ressource : PlayerScript.Instance.BluePlayerInfos.Ressource;
        if (ressourcePlayer >= Capacity1Cost)
        {
            List<GameObject> unit = new List<GameObject>();
           
            foreach (GameObject T in gameObject.GetComponent<UnitScript>().UnitSO.IsInRedArmy ? PlayerScript.Instance.UnitRef.UnitListBluePlayer : PlayerScript.Instance.UnitRef.UnitListRedPlayer)
            {
                if (T.GetComponent<UnitScript>().UnitSO.typeUnite == MYthsAndSteel_Enum.TypeUnite.Mecha || T.GetComponent<UnitScript>().UnitSO.typeUnite == MYthsAndSteel_Enum.TypeUnite.Vehicule)
                {
                    if(!T.GetComponent<UnitScript>().UnitStatuts.Contains(MYthsAndSteel_Enum.UnitStatut.Possédé))
                    {
                    unit.Add(T);
                    }
                }
            }


            GameManager.Instance._eventCall += EndCpty;
            GameManager.Instance._eventCallCancel += StopCpty;
            GameManager.Instance.StartEventModeUnit(1, GetComponent<UnitScript>().UnitSO.IsInRedArmy, unit, "Hacking", "Voulez vous activer une unité adverse de type Char ou Mécha durant un tour en échange d'une activation.?");

        }

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
        if (GetComponent<UnitScript>().UnitSO.IsInRedArmy)
        {
            PlayerScript.Instance.RedPlayerInfos.Ressource -= Capacity1Cost;
        }
        else
        {
            PlayerScript.Instance.BluePlayerInfos.Ressource -= Capacity1Cost;
        }

        GetComponent<UnitScript>().EndCapacity();
        foreach (GameObject unit in GameManager.Instance.UnitChooseList)
        {
            unit.GetComponent<UnitScript>().AddStatutToUnit(MYthsAndSteel_Enum.UnitStatut.Possédé);
        }
        GameManager.Instance.possesion = true;
        base.EndCpty();
        GameManager.Instance.UnitChooseList.Clear();
        GameManager.Instance._eventCall -= EndCpty;
    }
}
