using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltParentCopyState : MonoBehaviour
{
    [SerializeField] GameObject ParentObject;
    [SerializeField] GameObject ThisObject;
    void Update()
    {
        if (ParentObject.activeSelf == true)
        {
            ThisObject.SetActive(true);
        }
        else
        {
            ThisObject.SetActive(false);
        }
    }
}
