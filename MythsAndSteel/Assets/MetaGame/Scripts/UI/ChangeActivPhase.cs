using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChangeActivPhase : MonoBehaviour
{
    [Header("PhaseObj")]
    [SerializeField] private GameObject _debutGam;

    [SerializeField] private GameObject _activationGam;
    [SerializeField] private GameObject _orgoneJ1Gam;
    [SerializeField] private GameObject _actionJ1Gam;
    [SerializeField] private GameObject _orgoneJ2Gam;
    [SerializeField] private GameObject _actionJ2Gam;
    [SerializeField] private GameObject _strategyGam;

    [Header("Phase Sp� Joueur")]
    [SerializeField] private Image _orgoneJ1Img;
    [SerializeField] private Image _actionJ1Img;
    [SerializeField] private Image _orgoneJ2Img;
    [SerializeField] private Image _actionJ2Img;
    [SerializeField] private Sprite _refOrgoneJ1Img;
    [SerializeField] private Sprite _refActionJ1Img;
    [SerializeField] private Sprite _refOrgoneJ2Img;
    [SerializeField] private Sprite _refActionJ2Img;

    [Header("Texte Phase Sp�")]
    [SerializeField] private TextMeshProUGUI _orgoneJ1Txt;
    [SerializeField] private TextMeshProUGUI _actionJ1Txt;
    [SerializeField] private TextMeshProUGUI _orgoneJ2Txt;
    [SerializeField] private TextMeshProUGUI _actionJ2Txt;

    [Header("Couleurs")]
    [SerializeField] private Color _redPlayerColor;
    [SerializeField] private Color _bluePlayerColor;

    private void Start()
    {
        GameManager.Instance.ManagerSO.GoToOrgoneJ1Phase += ChangeColorTextPhase;
        
        ChangeActivObj();
    }
  public  void ChangeSpriteButtonPhase()
    {
        if(GameManager.Instance.IsPlayerRedStarting)
        {
            gameObject.transform.GetChild(2).GetComponent<Image>().sprite = _refOrgoneJ1Img;
            gameObject.transform.GetChild(3).GetComponent<Image>().sprite = _refActionJ1Img;
            gameObject.transform.GetChild(4).GetComponent<Image>().sprite = _refOrgoneJ2Img;
            gameObject.transform.GetChild(5).GetComponent<Image>().sprite = _refActionJ2Img;


        }
        else
        {
            gameObject.transform.GetChild(2).GetComponent<Image>().sprite = _refOrgoneJ2Img;
            gameObject.transform.GetChild(3).GetComponent<Image>().sprite = _refActionJ2Img;
            gameObject.transform.GetChild(4).GetComponent<Image>().sprite = _refOrgoneJ1Img;
            gameObject.transform.GetChild(5).GetComponent<Image>().sprite = _refActionJ1Img;
        }
    }
        
    void ChangeColorTextPhase(){
        if(GameManager.Instance.IsPlayerRedStarting){
            _orgoneJ1Img.color = _redPlayerColor;
            _orgoneJ2Img.color = _bluePlayerColor;
            _actionJ1Img.color = _redPlayerColor;
            _actionJ2Img.color = _bluePlayerColor;

            _actionJ1Txt.text = $"Phase de jeu du joueur {PlayerScript.Instance.J1Infos.ArmyNameNomMasc}";
            _actionJ2Txt.text = $"Phase de jeu du joueur {PlayerScript.Instance.J2Infos.ArmyNameNomMasc}";
            _orgoneJ1Txt.text = $"Phase orgonique du joueur {PlayerScript.Instance.J1Infos.ArmyNameNomMasc}";
            _orgoneJ2Txt.text = $"Phase orgonique du joueur {PlayerScript.Instance.J2Infos.ArmyNameNomMasc}";
        }
        else
        {
            _orgoneJ1Img.color = _bluePlayerColor;
            _orgoneJ2Img.color = _redPlayerColor;
            _actionJ1Img.color = _bluePlayerColor;
            _actionJ2Img.color = _redPlayerColor;

            _actionJ2Txt.text = $"Phase de jeu du joueur {PlayerScript.Instance.J1Infos.ArmyNameNomMasc}";
            _actionJ1Txt.text = $"Phase de jeu du joueur {PlayerScript.Instance.J2Infos.ArmyNameNomMasc}";
            _orgoneJ2Txt.text = $"Phase orgonique du joueur {PlayerScript.Instance.J1Infos.ArmyNameNomMasc}";
            _orgoneJ1Txt.text = $"Phase orgonique du joueur {PlayerScript.Instance.J2Infos.ArmyNameNomMasc}";
        }
    }

    public void ChangeActivObj(){
        switch(GameManager.Instance.ActualTurnPhase)
        {
            case MYthsAndSteel_Enum.PhaseDeJeu.Debut:
                ActivObj(_debutGam);
                break;
            case MYthsAndSteel_Enum.PhaseDeJeu.Activation:
                ActivObj(_activationGam);
                break;
            case MYthsAndSteel_Enum.PhaseDeJeu.OrgoneJ1:
                ActivObj(_orgoneJ1Gam);
                break;
            case MYthsAndSteel_Enum.PhaseDeJeu.ActionJ1:
                ActivObj(_actionJ1Gam);
                break;
            case MYthsAndSteel_Enum.PhaseDeJeu.OrgoneJ2:
                ActivObj(_orgoneJ2Gam);
                break;
            case MYthsAndSteel_Enum.PhaseDeJeu.ActionJ2:
                ActivObj(_actionJ2Gam);
                break;
            case MYthsAndSteel_Enum.PhaseDeJeu.Strategie:
                ActivObj(_strategyGam);
                break;
        }
    }

    private void ActivObj(GameObject activGam)
    {
        _debutGam.SetActive(false);
        _activationGam.SetActive(false);
        _orgoneJ1Gam.SetActive(false);
        _actionJ1Gam.SetActive(false);
        _orgoneJ2Gam.SetActive(false);
        _actionJ2Gam.SetActive(false);
        _strategyGam.SetActive(false);

        activGam.SetActive(true);
    }
}
