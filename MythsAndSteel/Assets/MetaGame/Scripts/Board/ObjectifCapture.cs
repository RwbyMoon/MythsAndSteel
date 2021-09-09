using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectifCapture : MonoBehaviour
{
    //Défini les paramètres de base de l'objectif
    public bool ObjectifNeutre;
    public bool RecapturePossible;
    public bool AppartientAuxRouge;

    bool PrisParAdversaire;

    //0 = Personne, 1 = Rouge, 2 = Bleu
    int Possesseur = 0;
    
    //Gates des conditions de prise de l'objectif
    bool UnitOnTile;
    bool OtherPlayerHasPlayed;
    bool LaunchCapture;

    private void Update()
    {
        if(GetComponent<TileScript>().Unit != null && !ObjectifNeutre)
        {
            if(((GetComponent<TileScript>().Unit.GetComponent<UnitScript>().UnitSO.IsInRedArmy && !AppartientAuxRouge && !PrisParAdversaire) || (!GetComponent<TileScript>().Unit.GetComponent<UnitScript>().UnitSO.IsInRedArmy && AppartientAuxRouge && !PrisParAdversaire) || (GetComponent<TileScript>().Unit.GetComponent<UnitScript>().UnitSO.IsInRedArmy && AppartientAuxRouge && PrisParAdversaire && RecapturePossible) || (!GetComponent<TileScript>().Unit.GetComponent<UnitScript>().UnitSO.IsInRedArmy && !AppartientAuxRouge && PrisParAdversaire && RecapturePossible)) && !UnitOnTile)
            {
                UnitOnTile = true;
            }
        }
        else if(!ObjectifNeutre && (GetComponent<TileScript>().Unit == null || (GetComponent<TileScript>().Unit.GetComponent<UnitScript>().UnitSO.IsInRedArmy && AppartientAuxRouge) || (!GetComponent<TileScript>().Unit.GetComponent<UnitScript>().UnitSO.IsInRedArmy && !AppartientAuxRouge)) && UnitOnTile)
        {
            UnitOnTile = false;
            OtherPlayerHasPlayed = false;
        }

        if(!OtherPlayerHasPlayed && UnitOnTile && !ObjectifNeutre && ((AppartientAuxRouge && GameManager.Instance.IsPlayerRedTurn && !PrisParAdversaire) || (!AppartientAuxRouge && !GameManager.Instance.IsPlayerRedTurn && !PrisParAdversaire) || (AppartientAuxRouge && !GameManager.Instance.IsPlayerRedTurn && PrisParAdversaire) || (!AppartientAuxRouge && GameManager.Instance.IsPlayerRedTurn && PrisParAdversaire)))
        {
            OtherPlayerHasPlayed = true;
        }
        
        if(UnitOnTile && OtherPlayerHasPlayed && GameManager.Instance.IsNextPhaseDone && !LaunchCapture && GameManager.Instance.ActualTurnPhase == MYthsAndSteel_Enum.PhaseDeJeu.Strategie)
        {
            LaunchCapture = true;
            ChangeOwner();
        }

        if(ObjectifNeutre && GetComponent<TileScript>().Unit != null)
        {
            if(!UnitOnTile && ((GetComponent<TileScript>().Unit.GetComponent<UnitScript>().UnitSO.IsInRedArmy && Possesseur != 1) || (!GetComponent<TileScript>().Unit.GetComponent<UnitScript>().UnitSO.IsInRedArmy && Possesseur != 2)))
            {
                UnitOnTile = true;
            }
        }
        else if(ObjectifNeutre && (GetComponent<TileScript>().Unit == null || (GetComponent<TileScript>().Unit.GetComponent<UnitScript>().UnitSO.IsInRedArmy && Possesseur == 1) || (!GetComponent<TileScript>().Unit.GetComponent<UnitScript>().UnitSO.IsInRedArmy && Possesseur == 2)) && UnitOnTile)
        {
            UnitOnTile = false;
            OtherPlayerHasPlayed = false;
        }

        if( UnitOnTile && !OtherPlayerHasPlayed && ((!GameManager.Instance.IsPlayerRedTurn && GetComponent<TileScript>().Unit.GetComponent<UnitScript>().UnitSO.IsInRedArmy) || (GameManager.Instance.IsPlayerRedTurn && !GetComponent<TileScript>().Unit.GetComponent<UnitScript>().UnitSO.IsInRedArmy)))
        {
            OtherPlayerHasPlayed = true;
        }

        if(ObjectifNeutre && UnitOnTile && OtherPlayerHasPlayed && !LaunchCapture && GameManager.Instance.ActualTurnPhase == MYthsAndSteel_Enum.PhaseDeJeu.Strategie)
        {
            LaunchCapture = true;
            ChangeOwner();
        }
    }

    void ChangeOwner()
    {
        if (!ObjectifNeutre && !PrisParAdversaire)
        {
            PrisParAdversaire = true;
            if (AppartientAuxRouge)
            {
                VictoryConditions.Instance.ObjectiveBlue++;
            }
            if (!AppartientAuxRouge)
            {
                VictoryConditions.Instance.ObjectiveRed++;
            }
            UnitOnTile = false;
            OtherPlayerHasPlayed = false;
            StartCoroutine(ResetLaunch());
        }
        else if(!ObjectifNeutre && PrisParAdversaire)
        {
            PrisParAdversaire = false;
            if (AppartientAuxRouge)
            {
                VictoryConditions.Instance.ObjectiveBlue--;
            }
            if (!AppartientAuxRouge)
            {
                VictoryConditions.Instance.ObjectiveRed--;
            }
            UnitOnTile = false;
            OtherPlayerHasPlayed = false;
            StartCoroutine(ResetLaunch());
        }

        if(ObjectifNeutre && !PrisParAdversaire)
        {
            PrisParAdversaire = true;
            if (GetComponent<UnitScript>().UnitSO.IsInRedArmy)
            {
                if (Possesseur == 2)
                {
                    VictoryConditions.Instance.ObjectiveBlue--;
                }
                Possesseur = 1;
                VictoryConditions.Instance.ObjectiveRed++;
            }
            else
            {
                if (Possesseur == 1)
                {
                    VictoryConditions.Instance.ObjectiveRed--;
                }
                Possesseur = 2;
                VictoryConditions.Instance.ObjectiveBlue++;
            }
            UnitOnTile = false;
            OtherPlayerHasPlayed = false;
            StartCoroutine(ResetLaunch());
        }
        VictoryConditions.Instance.CheckVictoryConditions();
    }

    IEnumerator ResetLaunch()
    {
        yield return new WaitForSeconds(5);
        LaunchCapture = false;
        if (ObjectifNeutre)
        {
            PrisParAdversaire = false;
        }
    }
}
