using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Ce script gère l'Affichage de Menu d'Action d'une Unité (déplacement, attaque, pouvoir) et tout ses vérifications.
/// </summary>
public class MenuActionUnite : MonoBehaviour
{
    [SerializeField] private MouseCommand _mousecommand;
    public MouseCommand MouseCommand => _mousecommand;
    
    // a rajoutter dans le playerscriptunite
    [SerializeField] bool pouvoiractif = true;
    [SerializeField] private GameObject UIMenuActionUnite;
    [SerializeField] private RectTransform _moveBtn;
    [SerializeField] private RectTransform _atkBtn;
    [SerializeField] private RectTransform _powerBtn;
    [SerializeField] private RectTransform _quitBtn;

    [SerializeField] private GameObject _movePanel = null;

    [SerializeField] private bool _isOpen = false;
    public bool IsOpen => _isOpen;

    private void Start(){
        _movePanel.SetActive(false);
       
    }

    /// <summary>
    /// Affiche le panneau avec les boutons
    /// </summary>
    public void ShowPanel(){
        UnitScript unit = RaycastManager.Instance.UnitInTile.GetComponent<UnitScript>();
        if((PlayerScript.Instance.RedPlayerInfos.ActivationLeft > 0 || (unit.MoveLeft + unit.MoveSpeedBonus > 0 && unit._hasStartMove) || (unit.IsMoveDone && !unit._isActionDone)) && GameManager.Instance.IsPlayerRedTurn && _isOpen == false){
            menuaffichage(RaycastManager.Instance.UnitInTile.GetComponent<UnitScript>(), pouvoiractif);
            //_isOpen = true;
        }
        else if((PlayerScript.Instance.BluePlayerInfos.ActivationLeft > 0 || (unit.MoveLeft + unit.MoveSpeedBonus > 0 && unit._hasStartMove) || (unit.IsMoveDone && !unit._isActionDone)) && !GameManager.Instance.IsPlayerRedTurn && _isOpen == false)
        {
            menuaffichage(RaycastManager.Instance.UnitInTile.GetComponent<UnitScript>(), pouvoiractif);
            //_isOpen = true;
        }
    }

    /// <summary>
    /// Ferme le panneau avec les boutons
    /// </summary>
    public void closePanel(){
        _isOpen = false;
        UIMenuActionUnite.SetActive(false);
    }

    /// <summary>
    /// Cette fonction gère l'affichage des différentes actions que le joueur va pouvoir effectuer sur l'unité contre un point activation.
    /// </summary>
    void menuaffichage(UnitScript unitscript, bool capaActive)
    {
        UIMenuActionUnite.SetActive(true);
    }

    /// <summary>
    /// Pour le bouton déplacement
    /// </summary>
    public void déplacement()
    {
        closePanel();
    }

    /// <summary>
    /// Pour le bouton attaque
    /// </summary>
    public void attaque()
    {
        closePanel();
    }

    /// <summary>
    /// Pour le bouton pouvoir
    /// </summary>
    public void capacité()
    {
      
        Debug.Log("pouvoir");
        closePanel();
    }
  
    /// <summary>
    /// Affiche le bouton pour se déplacer
    /// </summary>
    public void ShowMovementPanel(){
        _movePanel.SetActive(true);
    }

    /// <summary>
    /// Affiche le bouton pour se déplacer
    /// </summary>
    public void CloseMovementPanel(){
        _movePanel.SetActive(false);
    }
}
