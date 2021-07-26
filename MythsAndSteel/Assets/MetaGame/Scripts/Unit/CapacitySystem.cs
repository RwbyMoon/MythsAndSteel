using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CapacitySystem : MonoSingleton<CapacitySystem>
{
    public bool capacityTryStart = false;
    [SerializeField] private Sprite attacklaunchspritebutton;
    [SerializeField] private Sprite attackcancelspritebutton;
    [SerializeField]
    public GameObject PanelBlockant1;
    [SerializeField]
    public GameObject PanelBlockant2;
    [SerializeField] public bool CapacityRunning = false;

    [SerializeField] private GameObject ButtonLaunchCapacity;
    [SerializeField] private GameObject ButtonLaunchCapacity2;

    public void Updatebutton()
    {
        GameObject Unit = RaycastManager.Instance.ActualUnitSelected;

        if (Unit != null)
        {
            Player player;
            if (GameManager.Instance.IsPlayerRedTurn)
            {
                player = PlayerScript.Instance.J1Infos;
            }
            else
            {
                player = PlayerScript.Instance.J2Infos;
            }
            if ((!Attaque.Instance.attackselected && !Unit.GetComponent<UnitScript>()._isActionDone
                && !Mouvement.Instance.mvmtrunning && player.ActivationLeft > 0) || (!Attaque.Instance.attackselected && !Unit.GetComponent<UnitScript>()._isActionDone && Unit.GetComponent<UnitScript>()._hasStartMove && player.ActivationLeft == 0))
            {

                ButtonLaunchCapacity2.SetActive(false);
                ButtonLaunchCapacity.SetActive(true);

                Capacity[] CapacityList = Unit.GetComponents<Capacity>();
                if (CapacityList.Length > 1)
                {
                    ButtonLaunchCapacity2.SetActive(true);
                }

                if (Unit.GetComponent<UnitScript>().GotCapacity())
                {

                    if (Unit.GetComponent<UnitScript>().RunningCapacity)
                    {

                        ButtonLaunchCapacity.GetComponent<Image>().sprite = attackcancelspritebutton;
                        ButtonLaunchCapacity2.GetComponent<Image>().sprite = attacklaunchspritebutton;
                        UIInstance.Instance.DesactivateNextPhaseButton();

                    }
                    else if (Unit.GetComponent<UnitScript>().RunningCapacity2)
                    {

                        ButtonLaunchCapacity.GetComponent<Image>().sprite = attacklaunchspritebutton;
                        ButtonLaunchCapacity2.GetComponent<Image>().sprite = attackcancelspritebutton;

                    }
                    else
                    {

                        ButtonLaunchCapacity.GetComponent<Image>().sprite = attacklaunchspritebutton;
                        ButtonLaunchCapacity2.GetComponent<Image>().sprite = attacklaunchspritebutton;

                    }
                }
                else
                {
                    ButtonLaunchCapacity.SetActive(false);
                    ButtonLaunchCapacity2.SetActive(false);
                    UIInstance.Instance.ActivateNextPhaseButton();
                }
            }

            else
            {
                ButtonLaunchCapacity.SetActive(false);
                ButtonLaunchCapacity2.SetActive(false);
                UIInstance.Instance.ActivateNextPhaseButton();
            }
        }
    }


    public void CapacityButton()
    {
        GameObject Unit = RaycastManager.Instance.ActualUnitSelected;

        if (Unit != null)
        {
            if (!Unit.GetComponent<UnitScript>().IsActivationDone)
            {
                if (!Unit.GetComponent<UnitScript>().RunningCapacity)
                {
                    Unit.GetComponent<UnitScript>().StopCapacity();
                    Unit.GetComponent<UnitScript>().RunningCapacity2 = false;

                    capacityTryStart = true;
                    Attaque.Instance.StopAttack();
                    Mouvement.Instance.StopMouvement(true);

                    Unit.GetComponent<UnitScript>().StartCapacityV2();
                    Unit.GetComponent<UnitScript>().RunningCapacity = true;
                    PanelBlockant1.SetActive(true);
                    PanelBlockant2.SetActive(true);
                    Updatebutton();
                    capacityTryStart = false;
                }
                else if (Unit.GetComponent<UnitScript>().RunningCapacity)
                {

                    Unit.GetComponent<UnitScript>().StopCapacity();
                    Unit.GetComponent<UnitScript>().RunningCapacity = false;
                    PanelBlockant1.SetActive(false);
                    PanelBlockant2.SetActive(false);
                    Updatebutton();
                }
            }
        }
    }

    public void Capacity2Button()
    {
        GameObject Unit = RaycastManager.Instance.ActualUnitSelected;

        if (Unit != null)
        {
            if (!Unit.GetComponent<UnitScript>().IsActivationDone)
            {
                if (!Unit.GetComponent<UnitScript>().RunningCapacity2)
                {
                    Unit.GetComponent<UnitScript>().StopCapacity();
                    Unit.GetComponent<UnitScript>().RunningCapacity = false;

                    capacityTryStart = true;
                    Attaque.Instance.StopAttack();
                    Mouvement.Instance.StopMouvement(true);

                    Unit.GetComponent<UnitScript>().StartCapacityV2(2);
                    Unit.GetComponent<UnitScript>().RunningCapacity2 = true;
                    PanelBlockant1.SetActive(true);
                    PanelBlockant2.SetActive(true);
                    Updatebutton();
                    capacityTryStart = false;
                }
                else if (Unit.GetComponent<UnitScript>().RunningCapacity2)
                {

                    Unit.GetComponent<UnitScript>().StopCapacity();
                    Unit.GetComponent<UnitScript>().RunningCapacity2 = false;
                    PanelBlockant1.SetActive(false);
                    PanelBlockant2.SetActive(false);
                    Updatebutton();
                }
            }
        }
    }


}
