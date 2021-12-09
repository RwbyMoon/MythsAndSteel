using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Capacity : MonoBehaviour
{
    //----------------------------Capacit� 1--------------------------------------
    [SerializeField] string _Capacity1Name = "";
    public string Capacity1Name => _Capacity1Name;
    [SerializeField] private Sprite render1;

    [TextArea]
    [SerializeField] string _Capacity1Description = "";
    public string Capacity1Description => _Capacity1Description;

    [SerializeField] int _Capacity1Cost;
    public int Capacity1Cost => _Capacity1Cost;

    public bool SecondCapacity = false;

    virtual public void EndCpty()
    {

    }

    virtual public void StartCpty()
    {

    }
    virtual public void StopCpty()
    {

    }

    /// <summary>
    /// Retourne le pr�fab pour l'UI de l'unit�.
    /// </summary>
    /// <param name="PrefabCapacity"></param>
    /// <param name="number">Capacit� 0 ou capacit� 1</param>
    /// <returns></returns>
    /// 
    public void EventUnit(int numberUnit, bool redPlayer, List<GameObject> _unitSelectable, string title, string description, bool multiplesUnit = false)
    {
        GameManager.Instance.StartEventModeUnit(numberUnit, redPlayer, _unitSelectable, title, description, multiplesUnit);
    }
    public void EventTile(int numberUnit, bool redPlayer, List<GameObject> _unitSelectable, string title, string description, bool multiplesUnit = false)
    {
        GameManager.Instance.StartEventModeTiles(numberUnit, redPlayer, _unitSelectable, title, description, multiplesUnit);
    }
    public GameObject ReturnInfo(GameObject PrefabCapacity, int number = 0)
    {

        switch (number)
        {
            case 0:
                {
                    PrefabCapacity.transform.GetChild(1).GetComponent<Image>().sprite = render1;
                    PrefabCapacity.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = _Capacity1Name;
                    PrefabCapacity.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = _Capacity1Description;
                    if (_Capacity1Cost > 0)
                    {
                        PrefabCapacity.transform.GetChild(0).gameObject.SetActive(true);
                        PrefabCapacity.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = _Capacity1Cost.ToString();
                    }
                    else
                    {
                        PrefabCapacity.transform.GetChild(3).gameObject.SetActive(false);
                    }
                    int lengthTxt = _Capacity1Description.Length;
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