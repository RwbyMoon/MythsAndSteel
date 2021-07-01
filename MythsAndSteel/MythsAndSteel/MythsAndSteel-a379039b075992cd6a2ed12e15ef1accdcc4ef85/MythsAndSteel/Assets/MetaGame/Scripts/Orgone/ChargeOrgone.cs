using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ChargeOrgone : MonoBehaviour
{
  public bool DoingOrgoneCharge = false;
public void EndOrgoneUpdateRed(int cost)
    {
        PlayerScript.Instance.UseOrgone(cost, 1);
        PlayerScript.Instance.RedPlayerInfos.OrgonePowerLeft--;
        Attaque.Instance.PanelBlockant1.SetActive(false);
        Attaque.Instance.PanelBlockant2.SetActive(false);

    }

    public void EndOrgoneUpdateBlue(int cost)
    {
        PlayerScript.Instance.UseOrgone(cost, 2);
        Attaque.Instance.PanelBlockant1.SetActive(false);
        Attaque.Instance.PanelBlockant2.SetActive(false);
            PlayerScript.Instance.BluePlayerInfos.OrgonePowerLeft--;
        

    }

}
