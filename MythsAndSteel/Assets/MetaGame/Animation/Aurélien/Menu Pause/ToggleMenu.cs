using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleMenu : MonoBehaviour
{
    [SerializeField] private GameObject Grille;
    public void Toggle_Change(bool Value)
    {
        if (Value == false)
        {
            Grille.SetActive(false);
        }
        if (Value == true)
        {
            Grille.SetActive(true);        
        }
    }
}
