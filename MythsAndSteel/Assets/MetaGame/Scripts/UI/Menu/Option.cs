using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Option : MonoBehaviour
{
    #region Variables
    //---------Audio--------------
    public AudioMixer audioMixer; //Variable qui d�fin
    [SerializeField] GameObject EffectVolumeSlider;
    [SerializeField] GameObject MusicVolumeSlider;
    [SerializeField] GameObject Toggle;
    [SerializeField] GameObject toggleAvertissement;
   

    //--------R�solution----------
    public Dropdown ResolutionDropdown;//Variable qui d�fini quel dropdown on modifie
    Resolution[] resolutions;//Array de toute 
    int currentResolutionIndex = 2;
    Resolution OldResolution;
    int Oldindex;

    bool FirstTime = true;

    [SerializeField] GameObject ValidationPanel;

    //-------Avertissement--------
    int isAvertissement;
    #endregion 

    private void Start()
    {

        resolutions = Screen.resolutions; //R�cup�re toute les r�soluttion possible
        ResolutionDropdown.ClearOptions();//Enl�ve les options de bases du Dropdown


        //--------Convertie les r�solution en String pour les afficher dans le Dropdown---------
        List<string> options = new List<string>();


        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && //Cherche la r�solution actuelle de l'�cran
               resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        //----------------------------------------------------------------------------------------

        ResolutionDropdown.AddOptions(options); //Ajoute toutes les r�solution possible au Dropdown (en string)
        ResolutionDropdown.value = currentResolutionIndex; //Assigne la r�solution actuelle au Dropdown
        ResolutionDropdown.RefreshShownValue();//Actualise le Dropdown pour afficher la r�solution actuelle
    }
    private void Awake()
    {
        audioMixer.SetFloat("Effect", PlayerPrefs.GetFloat("EffectVolume"));

        audioMixer.SetFloat("Music", PlayerPrefs.GetFloat("MusicVolume"));
        EffectVolumeSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("EffectVolume");
        MusicVolumeSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("MusicVolume");
        
        if (PlayerPrefs.GetInt("Volume") == 1) Toggle.GetComponent<Toggle>().isOn = true;
        else Toggle.GetComponent<Toggle>().isOn = false;

        if (PlayerPrefs.GetInt("Avertissement") == 1)
        { toggleAvertissement.GetComponent<Toggle>().isOn = true;
        }
        else if (PlayerPrefs.GetInt("Avertissement") == 0)
       
        {

            toggleAvertissement.GetComponent<Toggle>().isOn = false;
            PlayerPrefs.SetInt("Avertissement", 0);
        }
   
    }

    #region R�solution

    public void SetResolution(int resolutionIndex) //
    {

        OldResolution = resolutions[currentResolutionIndex];
        Oldindex = currentResolutionIndex;

        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        currentResolutionIndex = resolutionIndex;
        if (!FirstTime)
        {
            StartCoroutine(Validation());
        }
        else
        {
            FirstTime = false;
        }
    }

    public void ResetResolution()
    {
        Screen.SetResolution(OldResolution.width, OldResolution.height, Screen.fullScreen);

        ResolutionDropdown.value = Oldindex;
        currentResolutionIndex = Oldindex;
        ResolutionDropdown.RefreshShownValue();

        ValidationPanel.SetActive(false);

        
    }

    public IEnumerator Validation()
    {
        ValidationPanel.SetActive(true);
        yield return new WaitForSeconds(5);

        ResetResolution();
    }
    #endregion

    #region Audio
    public void ActiveVolume(bool isVolumeOn)
    {
        if (isVolumeOn)
        {
            audioMixer.SetFloat("Master", 0);
            PlayerPrefs.SetInt("Volume", 1);
        }
        else
        {
            audioMixer.SetFloat("Master", -80);
            PlayerPrefs.SetInt("Volume", 0);
        }

    }
    public void SetEffectVolume (float volume)
    {
        audioMixer.SetFloat("Effect", volume);
        PlayerPrefs.SetFloat("EffectVolume", volume);

    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("Music", volume);
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }
    #endregion

    #region Avertissement
    public void Avertissement()
    {
       if( PlayerPrefs.GetInt("Avertissement")  == 1)
        {
            PlayerPrefs.SetInt("Avertissement", 0); 
        }
       else
        {
            PlayerPrefs.SetInt("Avertissement", 1);
        }
        
    }
    #endregion

    

}
