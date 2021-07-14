using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoCarnet : MonoBehaviour
{
    [Header("--------------ATTRIBUTS-----------")]
    [SerializeField] string _ListeAttributs = "";
    public string ListeAttributs => _ListeAttributs;
    [Header("--------------Info Capacité 1-----------")]
    [SerializeField] string _Capacity1Name = "";
    public string Capacity1Name => _Capacity1Name;
    [SerializeField] bool _IsPassive1;
    public bool IsPassive1 => _IsPassive1;
    [SerializeField] bool _IsBonus1;
    public bool IsBonus1 => _IsBonus1;
    [SerializeField] private Sprite _render1capaOne;
    public Sprite Render1CapaOne => _render1capaOne;
    [TextArea]
    [SerializeField] string _Capacity1Description = "";
    public string Capacity1Description => _Capacity1Description;

    [SerializeField] int _Capacity1Cost;
    public int Capacity1Cost => _Capacity1Cost;
    [Header("--------------Info Capacité 2-----------")]
    [SerializeField] string _Capacity2Name = "";
    public string Capacity2Name => _Capacity2Name;
    [SerializeField] bool _IsPassive2;
    public bool IsPassive2 => _IsPassive2;
    [SerializeField] bool _IsBonus2;
    public bool IsBonus2 => _IsBonus2;
    [SerializeField] private Sprite _render2capaOne;
    public Sprite Render2CapaOne => _render2capaOne;
    [TextArea]
    [SerializeField] string _Capacity2Description = "";
    public string Capacity2Description => _Capacity2Description;

    [SerializeField] int _Capacity2Cost;
    public int Capacity2Cost => _Capacity2Cost;
}
