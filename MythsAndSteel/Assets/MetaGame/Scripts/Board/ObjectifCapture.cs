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
        
        if(UnitOnTile && OtherPlayerHasPlayed && GameManager.Instance.IsNextPhaseDone && !LaunchCapture)
        {
            LaunchCapture = true;
            ChangeOwner();
        }

        if(ObjectifNeutre && GetComponent<TileScript>().Unit != null)
        {
            if(!UnitOnTile && ((GetComponent<UnitScript>().UnitSO.IsInRedArmy && Possesseur != 1) || (!GetComponent<UnitScript>().UnitSO.IsInRedArmy && Possesseur != 2)))
            {
                UnitOnTile = true;
            }
        }
        else if(ObjectifNeutre && (GetComponent<TileScript>().Unit == null || (GetComponent<TileScript>().Unit.GetComponent<UnitScript>().UnitSO.IsInRedArmy && Possesseur == 1) || (!GetComponent<TileScript>().Unit.GetComponent<UnitScript>().UnitSO.IsInRedArmy && Possesseur == 2)) && UnitOnTile)
        {
            UnitOnTile = false;
            OtherPlayerHasPlayed = false;
        }

        if( UnitOnTile && !OtherPlayerHasPlayed && ((!GameManager.Instance.IsPlayerRedTurn && GetComponent<UnitScript>().UnitSO.IsInRedArmy) || (GameManager.Instance.IsPlayerRedTurn && !GetComponent<UnitScript>().UnitSO.IsInRedArmy)))
        {
            OtherPlayerHasPlayed = true;
        }

        if(ObjectifNeutre && UnitOnTile && OtherPlayerHasPlayed && !LaunchCapture)
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
            UnitOnTile = false;
            OtherPlayerHasPlayed = false;
            StartCoroutine(ResetLaunch());
        }
        else if(!ObjectifNeutre && PrisParAdversaire)
        {
            PrisParAdversaire = false;
            UnitOnTile = false;
            OtherPlayerHasPlayed = false;
            StartCoroutine(ResetLaunch());
        }

        if(ObjectifNeutre && !PrisParAdversaire)
        {
            PrisParAdversaire = true;
            if (GetComponent<UnitScript>().UnitSO.IsInRedArmy)
            {
                Possesseur = 1;
            }
            else
            {
                Possesseur = 2;
            }
            UnitOnTile = false;
            OtherPlayerHasPlayed = false;
            StartCoroutine(ResetLaunch());
        }
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
