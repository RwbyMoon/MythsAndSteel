using UnityEngine;
using TMPro;

public class SaveData : MonoBehaviour
{
    public int unlockCampaign;
    public int redPlayerVictories;
    public int bluePlayerVictories;
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
    }
    public void ResetData()
    {
        PlayerPrefs.SetInt("UnlockCampaign", 0);
        PlayerPrefs.SetInt("RedPlayerVictories", 0);
        PlayerPrefs.SetInt("BluePlayerVictories", 0);
    }
}
