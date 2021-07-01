using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Ce Script gère l'affichage des points de vie et des point de bouclier sur les unités
 */

public class LifeHeartShieldUI : MonoSingleton<LifeHeartShieldUI>
{
    
    public GameObject ShieldPrefab;
   
 
    public GameObject LifeHeartPrefab;
   
    public   Sprite[] ShieldSprite;
 
    public Sprite[] LifeHeartSprite;
    /// <summary>
    /// Cette fonction va permettre de mettre un sprite d'une liste trouvé à partir d'un index à un objet. C'est la fonction qui met à jour l'affichage des boucliers et des points de vie. 
    /// </summary>
    public void UpdateLifeHeartShieldUI(Sprite[] listSprite, int LifeHeartShield, SpriteRenderer currentSprite)

    {
        currentSprite.sprite = listSprite[LifeHeartShield];
    }
}
