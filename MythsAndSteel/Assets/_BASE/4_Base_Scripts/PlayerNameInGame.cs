using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerNameInGame : MonoBehaviour
{
    public TMP_Text Player1name;
    public TMP_Text Player2name;

    // Start is called before the first frame update
    void Start()
    {
        Player1name.text = PlayerPrefs.GetString("AlliesName");
        Player2name.text = PlayerPrefs.GetString("AxeName");
    }


}
