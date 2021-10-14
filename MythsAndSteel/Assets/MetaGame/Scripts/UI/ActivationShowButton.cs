using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivationShowButton : MonoBehaviour
{
    public void SetOnActivationCanvasState()
    {
        UIInstance.Instance.CanvasActivation.SetActive(true);
        UIInstance.Instance.ButtonSetOnSwitch.SetActive(false);
        UIInstance.Instance.ButtonSetOffSwitch.SetActive(true);
    }
    public void SetOffActivationCanvasState()
    {
        UIInstance.Instance.CanvasActivation.SetActive(false);
        UIInstance.Instance.ButtonSetOnSwitch.SetActive(true);
        UIInstance.Instance.ButtonSetOffSwitch.SetActive(false);
    }
}