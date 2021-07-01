using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitReference : MonoBehaviour{
    [Header("LISTE DES UNITES EN JEU")]
    [SerializeField] List<GameObject> _unitListRedPlayer = new List<GameObject>();
    public List<GameObject> UnitListRedPlayer => _unitListRedPlayer;

    [SerializeField] List<GameObject> _unitListBluePlayer = new List<GameObject>();
    public List<GameObject> UnitListBluePlayer => _unitListBluePlayer;

    [Header("LISTE DES UNITES")]
    [SerializeField] List<Unit_SO> _unitClassListRedPlayer = new List<Unit_SO>();
    public List<Unit_SO> UnitClassListRedPlayer => _unitClassListRedPlayer;

    [SerializeField] List<Unit_SO> _unitClassListBluePlayer = new List<Unit_SO>();
    public List<Unit_SO> UnitClassListBluePlayer => _unitClassListBluePlayer;

    [Header("LISTE DES UNITES CREABLES")]
    [SerializeField] List<GameObject> _unitClassCreableListRedPlayer = new List<GameObject>();
   

    [SerializeField] List<GameObject> _unitClassCreableListBluePlayer = new List<GameObject>();
    public List<GameObject> UnitClassCreableListBluePlayer => _unitClassCreableListBluePlayer;
    public List<GameObject> UnitClassCreableListRedPlayer
    {
        get => _unitClassCreableListRedPlayer;
        set => _unitClassCreableListRedPlayer = value;
    }
}
