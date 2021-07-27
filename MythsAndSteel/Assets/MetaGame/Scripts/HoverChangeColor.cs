using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HoverChangeColor : MonoBehaviour
{
    [SerializeField]private Color startColor;
    [SerializeField] private Color mouseOverColor;
    [SerializeField] private GameObject changedTarget;
    bool mouseOver = false;

    public void OnMouseEnter()
    {
        mouseOver = true;
        changedTarget.GetComponent<Image>().color = mouseOverColor;
    }
    public void OnMouseExit()
    {
        mouseOver = false;
        changedTarget.GetComponent<Image>().color = startColor;
    }
}
