using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TutorielMenu : MonoBehaviour
{
    public GameObject UnitesIcon;
    public GameObject PhasesDeJeuIcon;
    public GameObject MecaniquesIcon;

    public GameObject Menu1;
    public GameObject Menu2;
    public GameObject Menu3;
    public GameObject Menu4;
    public GameObject Menu5;
    public GameObject Menu6;
    public GameObject Menu7;
    public GameObject Menu8;
    public GameObject Menu9;
    public GameObject Menu10;
    public GameObject Menu11;
    public GameObject Menu12;
    public GameObject Menu13;
    public GameObject Menu14;
    public GameObject Menu15;
    public GameObject Menu16;
    public GameObject Menu17;
    public GameObject Menu18;
    public GameObject Menu19;
    public void TutorielOnClick(int TutorielIndex)
    {
        Menu1.SetActive(false);
        Menu2.SetActive(false);
        Menu3.SetActive(false);
        Menu4.SetActive(false);
        Menu5.SetActive(false);
        Menu6.SetActive(false);
        Menu7.SetActive(false);
        Menu8.SetActive(false);
        Menu9.SetActive(false);
        Menu10.SetActive(false);
        Menu11.SetActive(false);
        Menu12.SetActive(false);
        Menu13.SetActive(false);
        Menu14.SetActive(false);
        Menu15.SetActive(false);
        Menu16.SetActive(false);
        Menu17.SetActive(false);
        Menu18.SetActive(false);
        Menu19.SetActive(false);


        if (TutorielIndex < 8)
        {
            UnitesIcon.GetComponent<Image>().color = new Color(255, 190, 190, 255);
            PhasesDeJeuIcon.GetComponent<Image>().color = Color.white;
            MecaniquesIcon.GetComponent<Image>().color = Color.white;
        }
        if (TutorielIndex >= 8 && TutorielIndex < 14)
        {
            UnitesIcon.GetComponent<Image>().color = Color.white;
            PhasesDeJeuIcon.GetComponent<Image>().color = new Color(255, 190, 190, 255);
            MecaniquesIcon.GetComponent<Image>().color = Color.white;
        }
        if (TutorielIndex >= 14)
        {
            UnitesIcon.GetComponent<Image>().color = Color.white;
            PhasesDeJeuIcon.GetComponent<Image>().color = Color.white;
            MecaniquesIcon.GetComponent<Image>().color = new Color(255, 190, 190, 255);
        }

        switch (TutorielIndex)
        {
            case 1:
                Menu1.SetActive(true) ;
                break;
            case 2:
                Menu2.SetActive(true);
                break;
            case 3:
                Menu3.SetActive(true);
                break;
            case 4:
                Menu4.SetActive(true);
                break;
            case 5:
                Menu5.SetActive(true);
                break;
            case 6:
                Menu6.SetActive(true);
                break;
            case 7:
                Menu7.SetActive(true);
                break;
            case 8:
                Menu8.SetActive(true);
                break;
            case 9:
                Menu9.SetActive(true);
                break;
            case 10:
                Menu10.SetActive(true);
                break;
            case 11:
                Menu11.SetActive(true);
                break;
            case 12:
                Menu12.SetActive(true);
                break;
            case 13:
                Menu13.SetActive(true);
                break;
            case 14:
                Menu14.SetActive(true);
                break;
            case 15:
                Menu15.SetActive(true);
                break;
            case 16:
                Menu16.SetActive(true);
                break;
            case 17:
                Menu17.SetActive(true);
                break;
            case 18:
                Menu18.SetActive(true);
                break;
            case 19:
                Menu19.SetActive(true);
                break;

            default:
                break;

        }
    }
}
