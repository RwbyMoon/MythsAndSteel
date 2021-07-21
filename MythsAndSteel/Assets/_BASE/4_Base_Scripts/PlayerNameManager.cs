using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerNameManager : MonoBehaviour
{
    public TMP_InputField RedInputCampagne;
    public TMP_InputField BlueInputCampagne;
    public TMP_InputField RedInputMenu;
    public TMP_InputField BlueInputMenu;

    void Start()
    {
        if (!PlayerPrefs.HasKey("AlliesName"))
        {
            PlayerPrefs.SetString("AlliesName", "Joueur Alliés");
        }

        if (!PlayerPrefs.HasKey("AxeName"))
        {
            PlayerPrefs.SetString("AxeName", "Joueur Axe");
        }

        RedInputMenu.text = PlayerPrefs.GetString("AlliesName");
        BlueInputMenu.text = PlayerPrefs.GetString("AxeName");
        RedInputCampagne.text = PlayerPrefs.GetString("AlliesName");
        BlueInputCampagne.text = PlayerPrefs.GetString("AxeName");

        RedInputMenu.characterLimit = 15;
        RedInputCampagne.characterLimit = 15;
        BlueInputCampagne.characterLimit = 15;
        BlueInputMenu.characterLimit = 15;
    }


    public void SetRedNameCampagne()
    {
        if (!(RedInputCampagne.text == ""))
        {
            PlayerPrefs.SetString("AlliesName", RedInputCampagne.text);
        }
        else
        {
            PlayerPrefs.SetString("AlliesName", "Joueur Alliés");
        }

        RedInputMenu.text = PlayerPrefs.GetString("AlliesName");

        Debug.Log(PlayerPrefs.GetString("AlliesName"));
    }

    public void SetBlueNameCampagne()
    {
        if (!(BlueInputCampagne.text == ""))
        {
            PlayerPrefs.SetString("AxeName", BlueInputCampagne.text);
        }
        else
        {
            PlayerPrefs.SetString("AxeName", "Joueur Axe");
        }

        BlueInputMenu.text = PlayerPrefs.GetString("AxeName");

        Debug.Log(PlayerPrefs.GetString("AxeName"));
    }

    public void SetRedNameMenu()
    {
        if (!(RedInputMenu.text == ""))
        {
            PlayerPrefs.SetString("AlliesName", RedInputMenu.text);
        }
        else
        {
            PlayerPrefs.SetString("AlliesName", "Joueur Alliés");
        }
        

        RedInputCampagne.text = PlayerPrefs.GetString("AlliesName");

        Debug.Log(PlayerPrefs.GetString("AlliesName"));
    }

    public void SetBlueNameMenu()
    {
        if (!(BlueInputMenu.text == ""))
        {
            PlayerPrefs.SetString("AxeName", BlueInputMenu.text);
        }
        else
        {
            PlayerPrefs.SetString("AxeName", "Joueur Axe");
        }

        BlueInputCampagne.text = PlayerPrefs.GetString("AxeName");

        Debug.Log(PlayerPrefs.GetString("AxeName"));
    }

}

