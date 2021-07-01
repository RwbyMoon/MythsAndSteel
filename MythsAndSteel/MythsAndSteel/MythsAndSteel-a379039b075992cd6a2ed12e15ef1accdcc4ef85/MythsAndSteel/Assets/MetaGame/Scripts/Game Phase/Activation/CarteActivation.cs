using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
Cette Classe regroupe les informations suivantes pour une carte activation  :
- sa valeur (en string) 
- sa position en tant qu'enfant du Panel du joueur correspondant
- son KeyCode qui correspond à la touche pour choisir la carte. 
*/

[System.Serializable]
public class CarteActivation{
    public string valeurActivation;
    public int IndexCarteActivation;
    public KeyCode inputCarteActivation;

    public CarteActivation(string newvaleurActivation, KeyCode newinputCarteActivation ,int newIndexCarteActivation){
        valeurActivation = newvaleurActivation;
        inputCarteActivation = newinputCarteActivation;
        IndexCarteActivation = newIndexCarteActivation;
    }

}
