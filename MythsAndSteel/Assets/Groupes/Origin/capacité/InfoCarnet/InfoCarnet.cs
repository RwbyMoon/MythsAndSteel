using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InfoCarnet : MonoBehaviour
{
    [Header("--------------- CAPACITÉ 1 ---------------")]
    [SerializeField] string _Capacity1Name = "";
    public string Capacity1Name => _Capacity1Name;

    [SerializeField] string _Capacity1Type = "";
    public string Capacity1Type => _Capacity1Type;

    [SerializeField] int _Capacity1Cost;
    public int Capacity1Cost => _Capacity1Cost;

    [TextArea]
    [SerializeField] string _Capacity1Desc = "";
    public string Capacity1Desc => _Capacity1Desc;

    [SerializeField] Sprite _Capacity1Icon;
    public Sprite Capacity1Icon => _Capacity1Icon;

    [Header("--------------- CAPACITÉ 2 ---------------")]
    [SerializeField] string _Capacity2Name = "";
    public string Capacity2Name => _Capacity2Name;

    [SerializeField] string _Capacity2Type = "";
    public string Capacity2Type => _Capacity2Type;

    [SerializeField] int _Capacity2Cost;
    public int Capacity2Cost => _Capacity2Cost;

    [TextArea]
    [SerializeField] string _Capacity2Desc = "";
    public string Capacity2Desc => _Capacity2Desc;

    [SerializeField] Sprite _Capacity2Icon;
    public Sprite Capacity2Icon => _Capacity2Icon;

    public GameObject ReturnInfo(GameObject PrefabCapacity, int number)
    {

        switch (number)
        {
            case 0:
                {
                    PrefabCapacity.transform.GetChild(1).GetComponent<Image>().sprite = _Capacity1Icon;
                    PrefabCapacity.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = _Capacity1Name;
                    PrefabCapacity.transform.GetComponent<TextMeshProUGUI>().text = _Capacity1Desc;
                    if (_Capacity1Cost > 0)
                    {
                        PrefabCapacity.transform.GetChild(0).gameObject.SetActive(true);
                        PrefabCapacity.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = _Capacity1Cost.ToString();
                    }
                    else
                    {
                        PrefabCapacity.transform.GetChild(0).gameObject.SetActive(false);
                    }
                    int lengthTxt = _Capacity1Desc.Length;
                    float LengthLine = (float)lengthTxt / 21;
                    int truncateLine = (int)LengthLine;
                    PrefabCapacity.GetComponent<RectTransform>().sizeDelta = new Vector2(
                        PrefabCapacity.GetComponent<RectTransform>().sizeDelta.x,
                        130 + (20 * truncateLine));
                    break;
                }
            case 1:
                {
                    PrefabCapacity.transform.GetChild(1).GetComponent<Image>().sprite = _Capacity2Icon;
                    PrefabCapacity.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = _Capacity2Name;
                    PrefabCapacity.transform.GetComponent<TextMeshProUGUI>().text = _Capacity2Desc;
                    if (_Capacity2Cost > 0)
                    {
                         PrefabCapacity.transform.GetChild(0).gameObject.SetActive(true);
                         PrefabCapacity.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = _Capacity2Cost.ToString();
                    }
                    else
                    {
                        PrefabCapacity.transform.GetChild(0).gameObject.SetActive(false);
                    }
                    int lengthTxt = _Capacity1Desc.Length;
                    float LengthLine = (float)lengthTxt / 21;
                    int truncateLine = (int)LengthLine;
                    PrefabCapacity.GetComponent<RectTransform>().sizeDelta = new Vector2(
                    PrefabCapacity.GetComponent<RectTransform>().sizeDelta.x,
                    130 + (20 * truncateLine));
                    break;
                }
        }
        return PrefabCapacity;
    }
}
