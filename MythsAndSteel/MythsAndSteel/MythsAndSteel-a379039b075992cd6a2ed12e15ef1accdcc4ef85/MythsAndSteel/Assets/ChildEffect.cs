using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildEffect : MonoBehaviour
{
    [SerializeField] private MYthsAndSteel_Enum.TerrainType _Type;
    public MYthsAndSteel_Enum.TerrainType Type
    {
        get
        {
            return _Type;
        }
        set
        {
            _Type = value;
        }
    }
}
