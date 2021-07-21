using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TutorielMenu : MonoBehaviour
{
    public GameObject UnitesIcon;
    public GameObject PhasesDeJeuIcon;
    public GameObject MecaniquesIcon;
    public void TutorielOnClick(int TutorielIndex)
    {
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
    }
}
