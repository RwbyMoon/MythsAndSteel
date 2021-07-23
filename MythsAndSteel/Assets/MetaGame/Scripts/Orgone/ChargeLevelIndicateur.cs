using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeLevelIndicateur : MonoBehaviour
{
    [SerializeField] private int ThisOrgoneChargeObject; //0 ded, 1 allumé, 2 clignotant
    int lastOrgone;
    [SerializeField] private GameObject Gamanager;
    [SerializeField] private byte Possessor; //1 = joueur 1, 2 = joueur 2

    public void UpdateOrgoneCase(int CurrentOrgone = 0)
    {
        lastOrgone = CurrentOrgone;

        if (CurrentOrgone >= ThisOrgoneChargeObject && Possessor == 1)
        {
            GetComponent<Animator>().SetInteger("Allumage", 1);

            if (Gamanager.GetComponent<GameManager>().ActualTurnPhase == MYthsAndSteel_Enum.PhaseDeJeu.OrgoneJ1)
            {
                GetComponent<Animator>().SetInteger("Allumage", 2);
            }
        }
        
        else if (CurrentOrgone >= ThisOrgoneChargeObject && Possessor == 2)
        {
            GetComponent<Animator>().SetInteger("Allumage", 1);
            if (Gamanager.GetComponent<GameManager>().ActualTurnPhase == MYthsAndSteel_Enum.PhaseDeJeu.OrgoneJ2)
            {
                GetComponent<Animator>().SetInteger("Allumage", 2);
            }
        }
        else
        {
            GetComponent<Animator>().SetInteger("Allumage", 0);
            //GetComponent<Image>().color = new Color32(255, 255, 255, 100);
        }
    }
}