using UnityEngine;
using TMPro;

public class SaveData : MonoBehaviour
{
    public int unlockCampaign;
    public int redPlayerVictories;
    public int bluePlayerVictories;
    public string AlliesName;
    public string AxeName;
    void Start()
    {
        if (PlayerPrefs.HasKey("UnlockCampaign"))
        {
            unlockCampaign = PlayerPrefs.GetInt("UnlockCampaign");
        }
        if (PlayerPrefs.HasKey("RedPlayerVictories"))
        {
            redPlayerVictories = PlayerPrefs.GetInt("RedPlayerVictories");
        }
        if (PlayerPrefs.HasKey("BluePlayerVictories"))
        {
            bluePlayerVictories = PlayerPrefs.GetInt("BluePlayerVictories");
        }

        if (PlayerPrefs.HasKey("AlliesName"))
        {
            AlliesName = PlayerPrefs.GetString("AlliesName");
        }
        else
        {
            PlayerPrefs.SetString("AlliesName", "Joueur Alliés");
        }
        if (PlayerPrefs.HasKey("AxeName"))
        {
            AxeName = PlayerPrefs.GetString("AxeName");
        }
        else
        {
            PlayerPrefs.SetString("AxeName", "Joueur Axe");
        }

    }
    public void ResetData()
    {
        PlayerPrefs.SetInt("UnlockCampaign", 0);
        PlayerPrefs.SetInt("RedPlayerVictories", 0);
        PlayerPrefs.SetInt("BluePlayerVictories", 0);
        PlayerPrefs.SetString("AlliesName", "Joueur Alliés");
        PlayerPrefs.SetString("AxeName", "Joueur Axe");

    }
}
