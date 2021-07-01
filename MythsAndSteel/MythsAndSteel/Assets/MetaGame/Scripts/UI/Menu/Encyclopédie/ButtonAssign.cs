using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonAssign : MonoBehaviour
{
    [SerializeField] bool isArmy1;
    [SerializeField] GameObject Army1;
    [SerializeField] GameObject Army2;
    [SerializeField] GameObject informationPanel;

    public void showArmy1()
    {
        if (!isArmy1)
        {
            Army1.SetActive(true);
            Army2.SetActive(false);
        }
    }

    public void showArmy2()
    {
        if (isArmy1)
        {
            Army1.SetActive(false);
            Army2.SetActive(true);
        }
    }

    public void UnitButton()
    {
        if (informationPanel != null)
        {
            informationPanel.SetActive(false);
        }
        informationPanel = EventSystem.current.currentSelectedGameObject.transform.GetChild(1).gameObject;
        informationPanel.SetActive(true);
    }
}
