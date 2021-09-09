using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
Ce Script va gérer l'utilisation des charges d'orgones. Tous les scripts des charges d'orgone dérive de ce dernier. 
C'est pour cela qu'il ne faut surtout pas le MODIFIER !! 
 */ 
public class OrgoneManager : MonoSingleton<OrgoneManager>
{

    public bool DoingOrgoneCharge = false;
    #region Variables
    [Header("PARENT JAUGE D'ORGONE")]
    //Jauge d'orgone joueur rouge
    [SerializeField] private GameObject _j1PanelOrgone = null;
    public GameObject J1PanelOrgone => _j1PanelOrgone;
    public GameObject FxOrgoneGauche;
    public GameObject FxOrgoneDroit;
    public GameObject ForceFieldDroit;
    public GameObject ForceFieldGauche;
    public GameObject BasOrgoneRed;
    public GameObject BasOrgoneBlue;

    //Jauge d'orgone joueur bleu
    [SerializeField] private GameObject _j2PanelOrgone = null;
    public GameObject J2PanelOrgone => _j2PanelOrgone;

    [Space]
    //Liste des enfants de la jauge d'orgone du joueur rouge
    [SerializeField] private List<Animator> _j1Charge = new List<Animator>();
    public List<Animator> J1Charge => _j1Charge;

    [SerializeField] private Animator Explodered;

    //Liste des enfants de la jauge d'orgone du joueur bleu
    [SerializeField] private List<Animator> _j2Charge = new List<Animator>();
    public List<Animator> J2Charge => _j2Charge;

    [SerializeField] private Animator Explodeblue;

    [SerializeField] private List<Animator> _j1Indicateur = new List<Animator>();
    public List<Animator> J1Indicateur => _j1Indicateur;

    //Liste des enfants de la jauge d'orgone du joueur bleu
    [SerializeField] private List<Animator> _j2Indicateur = new List<Animator>();
    public List<Animator> J2Indicateur => _j2Indicateur;

    [Header("ZONE ORGONE")]
    //Est ce qu'une jauge d'orgone est sélectionnée
    [SerializeField] private bool _selected = false;
    public bool Selected => _selected;

    //Zone d'orgone joueur rouge
    [SerializeField] private GameObject _j1Zone = null;
    public GameObject J1Zone => _j1Zone;

    //Zone d'orgone joueur bleu
    [SerializeField] private GameObject _j2Zone = null;
    public GameObject J2Zone => _j2Zone;

    public bool OrgoneHasBoom;

    #endregion Variables

    public void CheckZoneOrgone()
    {
        if(_j2Zone.GetComponent<ZoneOrgone>()._centerOrgoneArea == _j1Zone.GetComponent<ZoneOrgone>()._centerOrgoneArea)
        {
            _j2Zone.GetComponent<Animator>().SetBool("SAME", true);
            _j1Zone.GetComponent<Animator>().SetBool("SAME", true);
        }
        else
        {
            _j2Zone.GetComponent<Animator>().SetBool("SAME", false);
            _j1Zone.GetComponent<Animator>().SetBool("SAME", false);
        }
    }


    private void Start(){
        GameManager.Instance.ManagerSO.GoToOrgoneJ1Phase += ActivateOrgoneArea;
        GameManager.Instance.ManagerSO.GoToOrgoneJ2Phase += ActivateOrgoneArea;
    }
    private void Update()
    {
       
        ForceFieldGauche.transform.GetChild(0).position = Camera.main.ScreenToWorldPoint(BasOrgoneRed.transform.position);
        ForceFieldDroit.transform.GetChild(0).position = Camera.main.ScreenToWorldPoint(BasOrgoneBlue.transform.position);
 

    }
    public void ReleaseZone(){
        if(GameManager.Instance.IsPlayerRedTurn){
            _j1Zone.GetComponent<ZoneOrgone>().ReleaseZone();
        }
        else{
            _j2Zone.GetComponent<ZoneOrgone>().ReleaseZone();
        }

        _selected = false;
    }

    public void StartToMoveZone(){
        if(GameManager.Instance.IsPlayerRedTurn && !_j1Zone.GetComponent<ZoneOrgone>().HasMoveOrgoneArea && !_j1Zone.GetComponent<ZoneOrgone>().IsInValidation)
        {
            _j1Zone.GetComponent<ZoneOrgone>().AddOrgoneAtRange();
            SoundController.Instance.PlaySound(SoundController.Instance.AudioClips[13]);
            _selected = true;
        }
        else if(!GameManager.Instance.IsPlayerRedTurn && !_j2Zone.GetComponent<ZoneOrgone>().HasMoveOrgoneArea && !_j2Zone.GetComponent<ZoneOrgone>().IsInValidation)
        {
            _j2Zone.GetComponent<ZoneOrgone>().AddOrgoneAtRange();
            SoundController.Instance.PlaySound(SoundController.Instance.AudioClips[13]);
            _selected = true;
        }
    }

    /// <summary>
    /// Permet de connaitre la nouvelle valeur de la jauge d'orgone en fonction d'un variation positif ou négatif
    /// </summary>
    public int ChangeOrgone(int currentValue, int fluctuation){
        int newValue = currentValue + fluctuation;
        return newValue;
    }


    private bool OrgoneRunning1 = false; private bool OrgoneRunning2 = false;
    /// <summary>
    /// Détermine et applique l'animation de l'UI de la jauge d'orgone. A DÉTERMINER !
    /// </summary>

    public void StartOrgoneAnimation(int Player, int LastOrgoneValue, int ActualOrgoneValue)
    {
        StartCoroutine(UpdateOrgoneUI(Player, LastOrgoneValue, ActualOrgoneValue));
        SoundController.Instance.PlaySound(SoundController.Instance.AudioClips[3]);
        if(LastOrgoneValue == 4) SoundController.Instance.PlaySound(SoundController.Instance.AudioClips[4]);
    }
    
    public IEnumerator UpdateOrgoneUI(int Player, int LastOrgoneValue, int ActualOrgoneValue)
    {
        if (Player == 1)
        {
            while (OrgoneRunning1)
            {
                yield return new WaitForSeconds(0.1f);
            }
            StartCoroutine(AnimationOrgone(LastOrgoneValue, ActualOrgoneValue, 1));
        }
        else if (Player == 2)
        {
            while (OrgoneRunning2)
            {
                yield return new WaitForSeconds(0.1f);
            }
            StartCoroutine(AnimationOrgone(LastOrgoneValue, ActualOrgoneValue, 2));
        }
    }

    public void ExplosionOrgone(int Player)
    {
        if(Player == 1)
        {
           Explodered.SetTrigger("explode");
            SoundController.Instance.PlaySound(SoundController.Instance.AudioClips[2]);
            Debug.Log("exoplsionn");
        }
        else
        {
           Explodeblue.SetTrigger("explode");
            SoundController.Instance.PlaySound(SoundController.Instance.AudioClips[2]);
            Debug.Log("exoplsionn");
        }
        StartCoroutine(UpdateOrgoneUI(Player, 4, 0));
        StartCoroutine(OrgonBoom());
    }

    IEnumerator AnimationOrgone(int LastOrgoneValue, int ActualOrgoneValue, int Player)
    {            
        if (Player == 1)
        {        
            OrgoneRunning1 = true;
            int w = ActualOrgoneValue - LastOrgoneValue;
            if(w > 0)
            {
                for (int i = LastOrgoneValue; i < ActualOrgoneValue; i++)
                {
                    if (i < 0 || i > 4)
                    {
                        // Explosion.
                        if(i > 4)
                        {
                            ExplosionOrgone(1);
                        }
                        OrgoneRunning1 = false;
                        break;
                    }
                    for (int u = 0; u <= i; u++)
                    {
                        if (!OrgoneManager.Instance.J1Charge[u].GetBool("Increase"))
                        {
                            OrgoneManager.Instance.J1Charge[u].SetBool("Increase", true);
                            OrgoneManager.Instance.J1Indicateur[0].GetComponent<ChargeLevelIndicateur>().UpdateOrgoneCase(ActualOrgoneValue);
                            OrgoneManager.Instance.J1Indicateur[1].GetComponent<ChargeLevelIndicateur>().UpdateOrgoneCase(ActualOrgoneValue);
                            OrgoneManager.Instance.J1Indicateur[2].GetComponent<ChargeLevelIndicateur>().UpdateOrgoneCase(ActualOrgoneValue);
                            OrgoneManager.Instance.J1Indicateur[3].GetComponent<ChargeLevelIndicateur>().UpdateOrgoneCase(ActualOrgoneValue);
                            OrgoneManager.Instance.J1Indicateur[4].GetComponent<ChargeLevelIndicateur>().UpdateOrgoneCase(ActualOrgoneValue);
                            SoundController.Instance.PlaySound(SoundController.Instance.AudioClips[3]);
                            yield return new WaitForSeconds(.75f);
                        }
                    }
                    OrgoneManager.Instance.J1Charge[i].SetBool("Increase", true);
                    OrgoneManager.Instance.J1Indicateur[0].GetComponent<ChargeLevelIndicateur>().UpdateOrgoneCase(ActualOrgoneValue);
                    OrgoneManager.Instance.J1Indicateur[1].GetComponent<ChargeLevelIndicateur>().UpdateOrgoneCase(ActualOrgoneValue);
                    OrgoneManager.Instance.J1Indicateur[2].GetComponent<ChargeLevelIndicateur>().UpdateOrgoneCase(ActualOrgoneValue);
                    OrgoneManager.Instance.J1Indicateur[3].GetComponent<ChargeLevelIndicateur>().UpdateOrgoneCase(ActualOrgoneValue);
                    OrgoneManager.Instance.J1Indicateur[4].GetComponent<ChargeLevelIndicateur>().UpdateOrgoneCase(ActualOrgoneValue);
                    yield return new WaitForSeconds(.75f);
                }
            }
            if(w < 0)
            {
                for (int i = LastOrgoneValue; i > ActualOrgoneValue; i--)
                {
                    for (int u = 4; u >= i; u--)
                    {

                        if (i < 0 || i > 4)
                        {
                            Debug.Log("je suis là");
                            OrgoneRunning1 = false;
                            break;
                        }
                        if (OrgoneManager.Instance.J1Charge[u].GetBool("Increase"))
                        {
                            OrgoneManager.Instance.J1Charge[u].SetBool("Increase", false);
                            OrgoneManager.Instance.J1Indicateur[0].GetComponent<ChargeLevelIndicateur>().UpdateOrgoneCase(ActualOrgoneValue);
                            OrgoneManager.Instance.J1Indicateur[1].GetComponent<ChargeLevelIndicateur>().UpdateOrgoneCase(ActualOrgoneValue);
                            OrgoneManager.Instance.J1Indicateur[2].GetComponent<ChargeLevelIndicateur>().UpdateOrgoneCase(ActualOrgoneValue);
                            OrgoneManager.Instance.J1Indicateur[3].GetComponent<ChargeLevelIndicateur>().UpdateOrgoneCase(ActualOrgoneValue);
                            OrgoneManager.Instance.J1Indicateur[4].GetComponent<ChargeLevelIndicateur>().UpdateOrgoneCase(ActualOrgoneValue);
                            yield return new WaitForSeconds(.5f);
                        }

                    }
                    if (i - 1 >= 0)
                    {
                        OrgoneManager.Instance.J1Charge[i - 1].SetBool("Increase", false);
                        OrgoneManager.Instance.J1Indicateur[0].GetComponent<ChargeLevelIndicateur>().UpdateOrgoneCase(ActualOrgoneValue);
                        OrgoneManager.Instance.J1Indicateur[1].GetComponent<ChargeLevelIndicateur>().UpdateOrgoneCase(ActualOrgoneValue);
                        OrgoneManager.Instance.J1Indicateur[2].GetComponent<ChargeLevelIndicateur>().UpdateOrgoneCase(ActualOrgoneValue);
                        OrgoneManager.Instance.J1Indicateur[3].GetComponent<ChargeLevelIndicateur>().UpdateOrgoneCase(ActualOrgoneValue);
                        OrgoneManager.Instance.J1Indicateur[4].GetComponent<ChargeLevelIndicateur>().UpdateOrgoneCase(ActualOrgoneValue);
                        yield return new WaitForSeconds(.5f);
                    }
                 
                }
            }
            else if (w == 0)
            {
                Debug.Log("Same value.");
            }
            OrgoneRunning1 = false;
        }
        else if (Player == 2)
        {
            OrgoneRunning2 = true;
            int w = ActualOrgoneValue - LastOrgoneValue;
            if (w > 0)
            {
                for (int i = LastOrgoneValue; i < ActualOrgoneValue; i++)
                {
                    if (i < 0 || i > 4)
                    {
                        if (i > 4)
                        {
                            ExplosionOrgone(2);
                        }
                        // Explosion.
                        OrgoneRunning2 = false;
                        break;
                    }
                    for (int u = 0; u <= i; u++)
                    {
                        if (!OrgoneManager.Instance.J2Charge[u].GetBool("Increase"))
                        {
                            OrgoneManager.Instance.J2Charge[u].SetBool("Increase", true);
                            OrgoneManager.Instance.J2Indicateur[0].GetComponent<ChargeLevelIndicateur>().UpdateOrgoneCase(ActualOrgoneValue);
                            OrgoneManager.Instance.J2Indicateur[1].GetComponent<ChargeLevelIndicateur>().UpdateOrgoneCase(ActualOrgoneValue);
                            OrgoneManager.Instance.J2Indicateur[2].GetComponent<ChargeLevelIndicateur>().UpdateOrgoneCase(ActualOrgoneValue);
                            OrgoneManager.Instance.J2Indicateur[3].GetComponent<ChargeLevelIndicateur>().UpdateOrgoneCase(ActualOrgoneValue);
                            OrgoneManager.Instance.J2Indicateur[4].GetComponent<ChargeLevelIndicateur>().UpdateOrgoneCase(ActualOrgoneValue);
                            yield return new WaitForSeconds(.75f);
                        }
                    }
                    OrgoneManager.Instance.J2Charge[i].SetBool("Increase", true);
                    OrgoneManager.Instance.J2Indicateur[0].GetComponent<ChargeLevelIndicateur>().UpdateOrgoneCase(ActualOrgoneValue);
                    OrgoneManager.Instance.J2Indicateur[1].GetComponent<ChargeLevelIndicateur>().UpdateOrgoneCase(ActualOrgoneValue);
                    OrgoneManager.Instance.J2Indicateur[2].GetComponent<ChargeLevelIndicateur>().UpdateOrgoneCase(ActualOrgoneValue);
                    OrgoneManager.Instance.J2Indicateur[3].GetComponent<ChargeLevelIndicateur>().UpdateOrgoneCase(ActualOrgoneValue);
                    OrgoneManager.Instance.J2Indicateur[4].GetComponent<ChargeLevelIndicateur>().UpdateOrgoneCase(ActualOrgoneValue);
                    yield return new WaitForSeconds(.75f);
                }
            }
            if (w < 0)
            {
                for (int i = LastOrgoneValue; i > ActualOrgoneValue; i--)
                {
                    for (int u = 4; u >= i; u--)
                    {

                        if (i < 0 || i > 4)
                        {
                            OrgoneRunning2 = false;
                            break;
                        }
                        if (OrgoneManager.Instance.J2Charge[u].GetBool("Increase"))
                        {
                            OrgoneManager.Instance.J2Charge[u].SetBool("Increase", false);
                            OrgoneManager.Instance.J2Indicateur[0].GetComponent<ChargeLevelIndicateur>().UpdateOrgoneCase(ActualOrgoneValue);
                            OrgoneManager.Instance.J2Indicateur[1].GetComponent<ChargeLevelIndicateur>().UpdateOrgoneCase(ActualOrgoneValue);
                            OrgoneManager.Instance.J2Indicateur[2].GetComponent<ChargeLevelIndicateur>().UpdateOrgoneCase(ActualOrgoneValue);
                            OrgoneManager.Instance.J2Indicateur[3].GetComponent<ChargeLevelIndicateur>().UpdateOrgoneCase(ActualOrgoneValue);
                            OrgoneManager.Instance.J2Indicateur[4].GetComponent<ChargeLevelIndicateur>().UpdateOrgoneCase(ActualOrgoneValue);

                            yield return new WaitForSeconds(.5f);
                        }

                    }
                    if (i - 1 >= 0)
                    {
                        OrgoneManager.Instance.J2Charge[i - 1].SetBool("Increase", false);
                        OrgoneManager.Instance.J2Indicateur[0].GetComponent<ChargeLevelIndicateur>().UpdateOrgoneCase(ActualOrgoneValue);
                        OrgoneManager.Instance.J2Indicateur[1].GetComponent<ChargeLevelIndicateur>().UpdateOrgoneCase(ActualOrgoneValue);
                        OrgoneManager.Instance.J2Indicateur[2].GetComponent<ChargeLevelIndicateur>().UpdateOrgoneCase(ActualOrgoneValue);
                        OrgoneManager.Instance.J2Indicateur[3].GetComponent<ChargeLevelIndicateur>().UpdateOrgoneCase(ActualOrgoneValue);
                        OrgoneManager.Instance.J2Indicateur[4].GetComponent<ChargeLevelIndicateur>().UpdateOrgoneCase(ActualOrgoneValue);
                        yield return new WaitForSeconds(.5f);
                    }

                }
            }
         

        
            else if (w == 0)
            {
                Debug.Log("Same value.");
            }
            OrgoneRunning2 = false;
        }
    }

    public void ActivateOrgoneArea(){
        if(GameManager.Instance.IsPlayerRedTurn){
            _j1Zone.GetComponent<ZoneOrgone>().ActivationArea();
        }
        else{
            _j2Zone.GetComponent<ZoneOrgone>().ActivationArea();
        }
    }

    IEnumerator OrgonBoom()
    {
        OrgoneHasBoom = true;
        yield return new WaitForSeconds(2);
        OrgoneHasBoom = false;
    }
}

namespace MythsAndSteel.Orgone{
    public static class OrgoneCheck{

        /// <summary>
        /// Determine si le joueur peut utiliser un pouvoir d'orgone
        /// </summary>
        /// <param name="cost"></param>
        /// <param name="player"></param>
        /// <returns></returns>
        public static bool CanUseOrgonePower(int cost, int player){
            bool canUse = false;
          
            if(GameManager.Instance.ActualTurnPhase == MYthsAndSteel_Enum.PhaseDeJeu.OrgoneJ1 || GameManager.Instance.ActualTurnPhase == MYthsAndSteel_Enum.PhaseDeJeu.OrgoneJ2){
              
                if(GameManager.Instance.IsPlayerRedTurn == (player == 1? true : false)){
           
                    if (player == 1){
                       
                        if (PlayerScript.Instance.J1Infos.OrgonePowerLeft > 0 && PlayerScript.Instance.J1Infos.OrgoneValue >= cost){

                            canUse = true;
                        }
                    }
                    else{
                        
                        if (PlayerScript.Instance.J2Infos.OrgonePowerLeft > 0 && PlayerScript.Instance.J2Infos.OrgoneValue >= cost){
                            canUse = true;
                            
                        }
                    }
                }
            }
            if (!canUse) SoundController.Instance.PlaySound(SoundController.Instance.AudioClips[14]);
            return canUse;
        }
    }
}
