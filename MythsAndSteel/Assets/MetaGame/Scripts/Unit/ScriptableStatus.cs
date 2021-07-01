using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


[CreateAssetMenu(menuName ="META/Status")]
public class ScriptableStatus : ScriptableObject
{
    public List<Data> Data;

    public GameObject ReturnInfo(GameObject PrefabStatus, MYthsAndSteel_Enum.UnitStatut ST)
    {
        Data Saved = FindEffect(ST);
        if (Saved != null)
        {
            if (Saved.Icon != null)
            {
                PrefabStatus.transform.GetChild(0).GetComponent<Image>().sprite = Saved.Icon;
            }
            PrefabStatus.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = Saved.Status.ToString();
            PrefabStatus.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = Saved._description;
            return PrefabStatus;
        }
        Debug.Log("null");
        return null;
    }

    protected Data FindEffect(MYthsAndSteel_Enum.UnitStatut Type)
    {
        foreach (Data tr in Data)
        {
            if (tr.Status == Type)
            {
                Debug.Log("returned");
                return tr;
            }
        }
        return null;
    }
}

[System.Serializable]
public class Data
{
    public MYthsAndSteel_Enum.UnitStatut Status;
    public RuntimeAnimatorController Animation;
    public Sprite Icon;
    [TextArea] public string _description = "";
}
