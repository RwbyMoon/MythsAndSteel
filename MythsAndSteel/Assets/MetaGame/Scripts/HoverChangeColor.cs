using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HoverChangeColor : MonoBehaviour
{
    [SerializeField]private Color startColor = new Color(255, 255, 255, 255);
    [SerializeField] private Color mouseOverColor = new Color(221, 221, 221, 255);
    [SerializeField] private Color pressedColor = new Color(160, 160, 160, 255);
    [SerializeField] private GameObject changedTarget;
    bool mouseOver = false;
    bool isClicked = false;

    public void OnMouseEnter()
    {
        if (isClicked == false)
        {
            mouseOver = true;
            changedTarget.GetComponent<Image>().color = mouseOverColor;
        }
    }
    public void OnMouseExit()
    {
        if (isClicked == false)
        {
            mouseOver = false;
            changedTarget.GetComponent<Image>().color = startColor;
        }
        
    }
    public void OnMouseDown()
    {
        isClicked = true;
        changedTarget.GetComponent<Image>().color = pressedColor;
    }

    public void OnMouseUp()
    {
        isClicked = false;
        changedTarget.GetComponent<Image>().color = startColor;
    }
}
