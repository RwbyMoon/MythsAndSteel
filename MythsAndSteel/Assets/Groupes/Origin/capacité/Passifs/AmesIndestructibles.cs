using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmesIndestructibles : MonoBehaviour
{
    #region Info
    [SerializeField] string _Capacity1Name = "";
    public string Capacity1Name => _Capacity1Name;
    [SerializeField] private Sprite render1;

    [TextArea]
    [SerializeField] string _Capacity1Description = "";
    public string Capacity1Description => _Capacity1Description;
    #endregion

    private void Start()
    {
        GetComponent<UnitScript>().HasOnlyOneDamage = true;
    }
}
