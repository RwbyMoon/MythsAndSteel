using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AutoCompContainer : MonoBehaviour
{
    public GameObject TitleText;
    public GameObject TextDetails;

    public void Apply(TextMeshProUGUI txt)
    {
        ConsoleManager.Instance.AutoCompButton(txt);
    }
}
