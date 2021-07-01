using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAnimShortCut : MonoBehaviour
{
    public void Effector()
    {
        GetComponentInParent<Fire>().CheckingEffector();
    }
}
