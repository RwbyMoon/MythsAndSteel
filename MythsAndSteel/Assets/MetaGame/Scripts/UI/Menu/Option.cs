using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Option : MonoBehaviour
{
    #region Variables
    //---------Audio--------------
    public AudioMixer audioMixer; //Variable qui défin
    [SerializeField] GameObject EffectVolumeSlider;
    [SerializeField] GameObject MusicVolumeSlider;
    [SerializeField] GameObject Toggle;
    [SerializeField] GameObject toggleAvertissement;
   

    //--------Résolution----------
    public Dropdown ResolutionDropdown;//Variable qui défini quel dropdown on modifie
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

        resolutions = Screen.resolutions; //Récupère toute les résoluttion possible
        ResolutionDropdown.ClearOptions();//Enlève les options de bases du Dropdown


        //--------Convertie les résolution en String pour les afficher dans le Dropdown---------
        List<string> options = new List<string>();


        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && //Cherche la résolution actuelle de l'écran
               resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        //----------------------------------------------------------------------------------------

        ResolutionDropdown.AddOptions(options); //Ajoute toutes les résolution possible au Dropdown (en string)
        ResolutionDropdown.value = currentResolutionIndex; //Assigne la résolution actuelle au Dropdown
        ResolutionDropdown.RefreshShownValue();//Actualise le Dropdown pour afficher la résolution actuelle
    }
    private void Awake()
    {
        audioMixer.SetFloat("Effect", PlayerPrefs.GetFloat("EffectVolume"));

        Debug.Log(PlayerPrefs.GetInt("Avertissement"));
        audioMixer.SetFloat("Music", PlayerPrefs.GetFloat("MusicVolume"));
        EffectVolumeSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("EffectVolume");
        MusicVolumeSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("MusicVolume");
        
        Debug.Log("Volume set : Effect " + PlayerPrefs.GetFloat("EffectVolume") + ", Music " + PlayerPrefs.GetFloat("MusicVolume"));
        if (PlayerPrefs.GetInt("Volume") == 1) Toggle.GetComponent<Toggle>().isOn = true;
        else Toggle.GetComponent<Toggle>().isOn = false;

        if (PlayerPrefs.GetInt("Avertissement") == 1)
        { toggleAvertissement.GetComponent<Toggle>().isOn = true;
            Debug.Log(PlayerPrefs.GetInt("Avertissement"));
        }
        else if (PlayerPrefs.GetInt("Avertissement") == 0)
       
        {

            toggleAvertissement.GetComponent<Toggle>().isOn = false;
            PlayerPrefs.SetInt("Avertissement", 0);
        Debug.Log(PlayerPrefs.GetInt("Avertissement"));
        }
   
    }

    #region Résolution

    public void SetResolution(int resolutionIndex) //
    {

        OldResolution = resolutions[currentResolutionIndex];
        Oldindex = currentResolutionIndex;

        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        currentResolutionIndex = resolutionIndex;
        Debug.Log("rr");
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
        Debug.Log(OldResolution);
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
