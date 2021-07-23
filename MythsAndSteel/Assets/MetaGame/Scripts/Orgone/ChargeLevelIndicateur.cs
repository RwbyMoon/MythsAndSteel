using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeLevelIndicateur : MonoBehaviour
{
    [SerializeField] private int ThisOrgoneChargeObject; //0 ded, 1 allumé, 2 clignotant
    int lastOrgone;
    [SerializeField] private GameObject Gamanager;
    [SerializeField] private byte Possessor; //1 = joueur 1, 2 = joueur 2

    private void Update()
    {
        if (GetComponent<Animator>().GetInteger("Allumage") == 2 && Gamanager.GetComponent<GameManager>().ActualTurnPhase != MYthsAndSteel_Enum.PhaseDeJeu.OrgoneJ1 && Possessor == 1)
        {
            GetComponent<Animator>().SetInteger("Allumage", 1);
        }

        else if (GetComponent<Animator>().GetInteger("Allumage") == 2 && Gamanager.GetComponent<GameManager>().ActualTurnPhase != MYthsAndSteel_Enum.PhaseDeJeu.OrgoneJ2 && Possessor == 2)
        {
            GetComponent<Animator>().SetInteger("Allumage", 1);
        }
            
        if (Gamanager.GetComponent<GameManager>().ActualTurnPhase == MYthsAndSteel_Enum.PhaseDeJeu.OrgoneJ1 && GetComponent<Animator>().GetInteger("Allumage") == 1 && Possessor == 1)
        {
            Debug.Log("+1 Pour Gryffondor");
            GetComponent<Animator>().SetInteger("Allumage", 2);
        }
        else if (Gamanager.GetComponent<GameManager>().ActualTurnPhase == MYthsAndSteel_Enum.PhaseDeJeu.OrgoneJ2 && GetComponent<Animator>().GetInteger("Allumage") == 1 && Possessor == 2)
        {
            Debug.Log("+1 Pour Serpentard");
            GetComponent<Animator>().SetInteger("Allumage", 2);
        }
    }

    public void UpdateOrgoneCase(int CurrentOrgone)
    {
        lastOrgone = CurrentOrgone;

        if (CurrentOrgone >= ThisOrgoneChargeObject && Possessor == 1)
        {
            GetComponent<Animator>().SetInteger("Allumage", 1);
        }
        
        else if (CurrentOrgone >= ThisOrgoneChargeObject && Possessor == 2)
        {
            GetComponent<Animator>().SetInteger("Allumage", 1);
        }
        else
        {
            GetComponent<Animator>().SetInteger("Allumage", 0);
            //GetComponent<Image>().color = new Color32(255, 255, 255, 100);
        }
    }
}