using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Unit/Basic Unit")]
public class Unit_SO : ScriptableObject{
    [Header("Nom et Description")]
    [Tooltip("Nom de l'unité")]
    public string UnitName;
    [Tooltip("Decritpion de l'unité")]
    public string Description; 

    [Header("Type de l'unité")]
    [Tooltip("Type de l'unité")]
    public MYthsAndSteel_Enum.TypeUnite typeUnite;

    [Header("Stat de base de l'unité")]
    [Tooltip("Vie de base de l'unité")]
    public int LifeMax;
    [Tooltip("Bouclier Max de l'unité")]
    public int ShieldMax;
    [Tooltip("Range d'attaque de l'unité")]
    public int AttackRange;
    [Tooltip("Vitesse de déplacement de l'unité")]
    public int MoveSpeed;
    [Tooltip("Cout de création de l'unité")]
    public int CreationCost; 

    [Header("Attaque Minimum")]
    [Tooltip("nombre d'unité à sélectionner pour effectuer l'attaque")]
    public int numberOfUnitToAttack;
    [Header("Attaque Minimum")]
    [Tooltip("Dégats minimum infligé")]
    public int DamageMinimum;
    [Tooltip("Range des dés min")]
    public Vector2 NumberRangeMin;

    [Header("Attaque Maximum")]
    [Tooltip("Dégats maximum infligé")]
    public int DamageMaximum;
    [Tooltip("Range des dés max")]
    public Vector2 NumberRangeMax;

    [Header("Son de l'unité")]
    public AudioClip SonAttaque; //Son d'Attaque de l'unité
    public AudioClip SonDeplacement;//Son de déplacement de l'unité
    public AudioClip SonMort; //Son de mort de l'unité
    public AudioClip VoiceLine; 

    [Header("Sprite de l'unité")]
    [Tooltip("Sprite de base de l'unité")]
    public Sprite Sprite; 

    [Header("Infos Supplémentaire")]
    [Tooltip("Est ce que l'unité peut attaquer des cases vide?")]
    public bool CanAttackEmptyTile = false;
    [Tooltip("Est ce que si cette unité est tuée elle permet à l'adversaire de gagner?")]
    public bool UsefullToWin = false;
    [Tooltip("Est ce que cette unité fait parti de l'armée 1?")]
    public bool IsInRedArmy = true;
    public MYthsAndSteel_Enum.Attributs[] UnitAttributs = new MYthsAndSteel_Enum.Attributs[3];
}
