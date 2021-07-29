using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hacking : Capacity
{




    public override void StartCpty()
    {
        int ressourcePlayer = GetComponent<UnitScript>().UnitSO.IsInJ1Army ? PlayerScript.Instance.J1Infos.Ressource : PlayerScript.Instance.J2Infos.Ressource;
        if (ressourcePlayer >= Capacity1Cost)
        {
            List<GameObject> unit = new List<GameObject>();
           
            foreach (GameObject T in gameObject.GetComponent<UnitScript>().UnitSO.IsInJ1Army ? PlayerScript.Instance.UnitRef.UnitListBluePlayer : PlayerScript.Instance.UnitRef.UnitListRedPlayer)
            {
                if (T.GetComponent<UnitScript>().UnitSO.typeUnite == MYthsAndSteel_Enum.TypeUnite.Mecha || T.GetComponent<UnitScript>().UnitSO.typeUnite == MYthsAndSteel_Enum.TypeUnite.Vehicule)
                {
                    if(!T.GetComponent<UnitScript>().UnitStatuts.Contains(MYthsAndSteel_Enum.UnitStatut.Poss�d�))
                    {
                    unit.Add(T);
                    }
                }
            }


            GameManager.Instance._eventCall += EndCpty;
            GameManager.Instance._eventCallCancel += StopCpty;
            GameManager.Instance.StartEventModeUnit(1, GetComponent<UnitScript>().UnitSO.IsInJ1Army, unit, "Hacking", "Voulez vous activer une unit� adverse de type Char ou M�cha durant un tour en �change d'une activation.?");

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
        if (GetComponent<UnitScript>().UnitSO.IsInJ1Army)
        {
            PlayerScript.Instance.J1Infos.Ressource -= Capacity1Cost;
        }
        else
        {
            PlayerScript.Instance.J2Infos.Ressource -= Capacity1Cost;
        }

        GetComponent<UnitScript>().EndCapacity();
        foreach (GameObject unit in GameManager.Instance.UnitChooseList)
        {
            unit.GetComponent<UnitScript>().AddStatutToUnit(MYthsAndSteel_Enum.UnitStatut.Poss�d�);
        }
        GameManager.Instance.possesion = true;
        base.EndCpty();
        GameManager.Instance.UnitChooseList.Clear();
        GameManager.Instance._eventCall -= EndCpty;
    }
}
