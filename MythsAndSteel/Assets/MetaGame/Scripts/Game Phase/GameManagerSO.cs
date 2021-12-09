using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
[CreateAssetMenu(menuName = "META/Game Manager")]
public class GameManagerSO : ScriptableObject
{
    public bool azer = false;
    //Event pour quand le joueur clique sur un bouton pour passer � la phase en question
    public delegate void ClickButtonSwitchPhase();
    public event ClickButtonSwitchPhase GoToDebutPhase;
    public event ClickButtonSwitchPhase GoToActivationPhase;
    public event ClickButtonSwitchPhase GoToOrgoneJ1Phase;
    public event ClickButtonSwitchPhase GoToActionJ1Phase;
    public event ClickButtonSwitchPhase GoToOrgoneJ2Phase;
    public event ClickButtonSwitchPhase GoToActionJ2Phase;
    public event ClickButtonSwitchPhase GoToStrategyPhase;

    /// <summary>
    /// Aller � la phase de jeu renseigner en param�tre
    /// </summary>
    /// <param name="phaseToGoTo"></param>
    public void GoToPhase(MYthsAndSteel_Enum.PhaseDeJeu phase = MYthsAndSteel_Enum.PhaseDeJeu.Debut, bool randomPhase = false)
    {

        UIInstance.Instance.ActivateNextPhaseButton();
        
        int phaseSuivante = ((int)GameManager.Instance.ActualTurnPhase) + 1;
        MYthsAndSteel_Enum.PhaseDeJeu nextPhase = MYthsAndSteel_Enum.PhaseDeJeu.Debut;

        if (randomPhase)
        {
            nextPhase = phase;
        }
        else
        {
            phaseSuivante = ((int)GameManager.Instance.ActualTurnPhase) + 1;

            if (phaseSuivante > 6)
            {
                if (GameManager.Instance.ActualTurnNumber > 11)
                {
                    GameManager.Instance.VictoryForArmy(1);
                    return;
                }
                else
                {
                    phaseSuivante = 0;
                    GameManager.Instance.ActualTurnNumber++;
                }
            }

            nextPhase = (MYthsAndSteel_Enum.PhaseDeJeu)phaseSuivante;
        }

        gophase(nextPhase);
    }

    /// <summary>
    /// Aller � la phase de jeu renseigner en param�tre POUR LE BOUTON
    /// </summary>
    /// <param name="phaseToGoTo"></param>
    public void GoToPhase()
    {
       

        int phaseSuivante = ((int)GameManager.Instance.ActualTurnPhase) + 1;


        if (phaseSuivante > 6)
        {
            if (GameManager.Instance.ActualTurnNumber > 11)
            {
                GameManager.Instance.VictoryForArmy(1);
                return;
            }
            else
            {
                phaseSuivante = 0;
                GameManager.Instance.ActualTurnNumber++;
                GameManager.Instance.UpdateTurn();
            }
        }

        MYthsAndSteel_Enum.PhaseDeJeu nextPhase = (MYthsAndSteel_Enum.PhaseDeJeu)phaseSuivante;

        gophase(nextPhase);
    }

    /// <summary>
    /// Va a la phase
    /// </summary>
    /// <param name="nextPhase"></param>
    void gophase(MYthsAndSteel_Enum.PhaseDeJeu nextPhase)
    {
        //Selon la phase effectue certaines actions
        switch (nextPhase)
        {
            case MYthsAndSteel_Enum.PhaseDeJeu.Debut:
                SoundController.Instance.PlaySound(SoundController.Instance.AudioClips[5]);
                if (GoToDebutPhase != null)
                {
                    GameManager.Instance.GoPhase(MYthsAndSteel_Enum.PhaseDeJeu.Debut);
                    GoToDebutPhase();
                }
                else
                {
                    GameManager.Instance.GoPhase(MYthsAndSteel_Enum.PhaseDeJeu.Activation);
                    GoToActivationPhase();
                }
                break;
                
            case MYthsAndSteel_Enum.PhaseDeJeu.Activation:
                GameManager.Instance.GoPhase(MYthsAndSteel_Enum.PhaseDeJeu.Activation);
                GoToActivationPhase();
                break;

            case MYthsAndSteel_Enum.PhaseDeJeu.OrgoneJ1:
                GameManager.Instance.GoPhase(MYthsAndSteel_Enum.PhaseDeJeu.OrgoneJ1);

                UIInstance.Instance.ActiveOrgoneChargeButton();
                if(GameManager.Instance.IsPlayerRedStarting)
                {
                    PlayerScript.Instance.J1Infos.OrgonePowerLeft = 1;
                    PlayerScript.Instance.J1Infos.EventUseLeft = 1;
                }
                else
                {
                    PlayerScript.Instance.J2Infos.OrgonePowerLeft = 1;
                    PlayerScript.Instance.J2Infos.EventUseLeft = 1;
                }
                GoToOrgoneJ1Phase();
                break;

            case MYthsAndSteel_Enum.PhaseDeJeu.ActionJ1:
                GameManager.Instance.GoPhase(MYthsAndSteel_Enum.PhaseDeJeu.ActionJ1);
                foreach (GameObject unit in GameManager.Instance.IsPlayerRedTurn ? PlayerScript.Instance.UnitRef.UnitListRedPlayer : PlayerScript.Instance.UnitRef.UnitListBluePlayer)
                {
                    unit.GetComponent<UnitScript>().ResetTurn();
                }
                UIInstance.Instance.DesactiveOrgoneChargeButton();
                if (GameManager.Instance.IsPlayerRedTurn)
                {
                    UIInstance.Instance.ButtonRenfortJ1.GetComponent<Button>().interactable = true;
                    UIInstance.Instance.RedRenfortCount = 0;
                }
                else
                {
                    UIInstance.Instance.ButtonRenfortJ2.GetComponent<Button>().interactable = true;
                    UIInstance.Instance.BlueRenfortCount = 0;
                }
                if (GameManager.Instance.SabotageStat == 1 && !GameManager.Instance.IsPlayerRedTurn)
                {
                    PlayerScript.Instance.J2Infos.ActivationLeft--;
                    GameManager.Instance.SabotageStat = 3;

                }
                else if (GameManager.Instance.SabotageStat == 2 && GameManager.Instance.IsPlayerRedTurn)
                {
                    PlayerScript.Instance.J1Infos.ActivationLeft--;
                    GameManager.Instance.SabotageStat = 3;
                }
                UIInstance.Instance.UpdateActivationLeft();
                RaycastManager.Instance.ActualUnitSelected = null;
                RaycastManager.Instance.ActualTileSelected = null;
                GoToActionJ1Phase();
                break;

            case MYthsAndSteel_Enum.PhaseDeJeu.OrgoneJ2:

                GameManager.Instance.GoPhase(MYthsAndSteel_Enum.PhaseDeJeu.OrgoneJ2);
                RaycastManager.Instance.ActualUnitSelected = null;
                RaycastManager.Instance.ActualTileSelected = null;
                if (GameManager.Instance.IsPlayerRedTurn)
                {


                    UIInstance.Instance.ButtonRenfortJ1.GetComponent<Button>().interactable = false;
                }
                else
                {
                    UIInstance.Instance.ButtonRenfortJ2.GetComponent<Button>().interactable = false;
                }
                
                if (GameManager.Instance.possesion)
                {
                    foreach (GameObject unit in GameManager.Instance.IsPlayerRedTurn ? PlayerScript.Instance.UnitRef.UnitListBluePlayer : PlayerScript.Instance.UnitRef.UnitListRedPlayer)
                    {
                        GameManager.Instance.possesion = false;
                        unit.GetComponent<UnitScript>().ResetStatutPossesion();



                    }
                }
                foreach (GameObject TS in TilesManager.Instance.TileList)
                {
                    foreach (MYthsAndSteel_Enum.TerrainType T1 in TS.GetComponent<TileScript>().TerrainEffectList)
                    {
                        foreach (TerrainType Type in GameManager.Instance.Terrain.EffetDeTerrain)
                        {
                            foreach (MYthsAndSteel_Enum.TerrainType T2 in Type._eventType)
                            {
                                if (T1 == T2)
                                {
                                    if (Type.Child != null)
                                    {
                                        if (Type.MustBeInstantiate)
                                        {
                                            foreach (GameObject G in TS.GetComponent<TileScript>()._Child)
                                            {
                                                if (G.TryGetComponent<ChildEffect>(out ChildEffect Try2))
                                                {
                                                    if (Try2.Type == T1)
                                                    {
                                                        if (Try2.TryGetComponent<TerrainParent>(out TerrainParent Try3))
                                                        {
                                                            UnitScript u = null;
                                                            if (TS.GetComponent<TileScript>().Unit != null)
                                                            {
                                                                u = TS.GetComponent<TileScript>().Unit.GetComponent<UnitScript>();
                                                            }
                                                            Try3.EndPlayerTurnEffect(GameManager.Instance.IsPlayerRedTurn, u);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (Type.Child.TryGetComponent<TerrainParent>(out TerrainParent Try))
                                            {
                                                UnitScript u = null;
                                               if (TS.GetComponent<TileScript>().Unit != null)
                                                {
                                                   u = TS.GetComponent<TileScript>().Unit.GetComponent<UnitScript>();
                                                }

                                                Try.EndPlayerTurnEffect(GameManager.Instance.IsPlayerRedTurn, u);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }



                if (GameManager.Instance.armeEpidemelogiqueStat != 0)
                {
                    List<GameObject> refunit5 = new List<GameObject>();
                    if (GameManager.Instance.IsPlayerRedTurn && GameManager.Instance.armeEpidemelogiqueStat == 2)
                    {
                        refunit5 = PlayerScript.Instance.UnitRef.UnitListRedPlayer;
                    }
                    else if (!GameManager.Instance.IsPlayerRedTurn && GameManager.Instance.armeEpidemelogiqueStat == 1)
                    {
                        refunit5 = PlayerScript.Instance.UnitRef.UnitListBluePlayer;
                    }
                    foreach (GameObject unit in refunit5)
                    {

                        if (unit.GetComponent<UnitScript>().UnitStatuts.Contains(MYthsAndSteel_Enum.UnitStatut.ArmeEpidemiologique))
                        {

                            unit.GetComponent<UnitScript>().TakeDamage(1);
                            unit.GetComponent<UnitScript>().RemoveStatutToUnit(MYthsAndSteel_Enum.UnitStatut.ArmeEpidemiologique);
                            foreach (int i in PlayerStatic.GetNeighbourDiag(unit.GetComponent<UnitScript>().ActualTiledId, TilesManager.Instance.TileList[unit.GetComponent<UnitScript>().ActualTiledId].GetComponent<TileScript>().Line, false))
                            {

                                if (TilesManager.Instance.TileList[i].GetComponent<TileScript>().Unit != null)
                                {
                                    TilesManager.Instance.TileList[i].GetComponent<TileScript>().Unit.GetComponent<UnitScript>().TakeDamage(1);
                                }
                            }
                            
                            GameManager.Instance.armeEpidemelogiqueStat = 0;
                            refunit5 = null;

                        }
                    }

                }

                List<GameObject> refunit = new List<GameObject>();
                refunit.AddRange(PlayerScript.Instance.UnitRef.UnitListRedPlayer);
                refunit.AddRange(PlayerScript.Instance.UnitRef.UnitListBluePlayer);
                if (GameManager.Instance.IsPlayerRedTurn)
                {

                    foreach (GameObject unit in refunit)
                    {

                        if (unit.GetComponent<UnitScript>().ParalysieStat == 2)
                        {
                            unit.GetComponent<UnitScript>().RemoveStatutToUnit(MYthsAndSteel_Enum.UnitStatut.Paralysie);
                            unit.GetComponent<UnitScript>().ParalysieStat = 3;
                        }

                    }


                }
                else if (!GameManager.Instance.IsPlayerRedTurn)
                {

                    foreach (GameObject unit in refunit)
                    {

                        if (unit.GetComponent<UnitScript>().ParalysieStat == 1)
                        {
                            unit.GetComponent<UnitScript>().RemoveStatutToUnit(MYthsAndSteel_Enum.UnitStatut.Paralysie);
                            unit.GetComponent<UnitScript>().ParalysieStat = 3;
                        }

                    }


                }
                refunit.Clear();

                List<GameObject> refunit3 = new List<GameObject>();
                refunit3.AddRange(PlayerScript.Instance.UnitRef.UnitListRedPlayer);
                refunit3.AddRange(PlayerScript.Instance.UnitRef.UnitListBluePlayer);
                if (GameManager.Instance.IsPlayerRedTurn)
                {

                    foreach (GameObject unit in refunit3)
                    {

                        if (unit.GetComponent<UnitScript>().SilenceStat == 2)
                        {
                            unit.GetComponent<UnitScript>().RemoveStatutToUnit(MYthsAndSteel_Enum.UnitStatut.Silence);
                            unit.GetComponent<UnitScript>().SilenceStat = 3;
                        }

                    }


                }
                else if (!GameManager.Instance.IsPlayerRedTurn)
                {

                    foreach (GameObject unit in refunit3)
                    {

                        if (unit.GetComponent<UnitScript>().SilenceStat == 1)
                        {
                            unit.GetComponent<UnitScript>().RemoveStatutToUnit(MYthsAndSteel_Enum.UnitStatut.Silence);
                            unit.GetComponent<UnitScript>().SilenceStat = 3;
                        }

                    }


                }
                refunit3.Clear();

                if (GameManager.Instance.statetImmobilisation != 3)
                {
                    List<GameObject> refunit6 = new List<GameObject>();
                    refunit6.AddRange(PlayerScript.Instance.UnitRef.UnitListRedPlayer);
                    refunit6.AddRange(PlayerScript.Instance.UnitRef.UnitListBluePlayer);
                    if (GameManager.Instance.IsPlayerRedTurn && GameManager.Instance.statetImmobilisation == 2 || !GameManager.Instance.IsPlayerRedTurn && GameManager.Instance.statetImmobilisation == 1)
                    {

                        foreach (GameObject unit in refunit6)
                        {

                            if (unit.GetComponent<UnitScript>().UnitStatuts.Contains(MYthsAndSteel_Enum.UnitStatut.Immobilisation))
                            {
                                unit.GetComponent<UnitScript>().RemoveStatutToUnit(MYthsAndSteel_Enum.UnitStatut.Immobilisation);
                            }

                        }

                        GameManager.Instance.statetImmobilisation = 3;
                    }
                }
                    if (!GameManager.Instance.IsPlayerRedStarting)
                {
                    PlayerScript.Instance.J1Infos.OrgonePowerLeft = 1;
                    PlayerScript.Instance.J1Infos.EventUseLeft = 1;
                }
                else
                {
                    PlayerScript.Instance.J2Infos.OrgonePowerLeft = 1;
                    PlayerScript.Instance.J2Infos.EventUseLeft = 1;
                }

                UIInstance.Instance.ActiveOrgoneChargeButton();
                GoToOrgoneJ2Phase();
                break;

            case MYthsAndSteel_Enum.PhaseDeJeu.ActionJ2:
                GameManager.Instance.GoPhase(MYthsAndSteel_Enum.PhaseDeJeu.ActionJ2);
                foreach (GameObject unit in GameManager.Instance.IsPlayerRedTurn ? PlayerScript.Instance.UnitRef.UnitListRedPlayer : PlayerScript.Instance.UnitRef.UnitListBluePlayer)
                {
                    unit.GetComponent<UnitScript>().ResetTurn();
                }
                UIInstance.Instance.DesactiveOrgoneChargeButton();
                if(GameManager.Instance.IsPlayerRedTurn)
                {

                    UIInstance.Instance.RedRenfortCount = 0;
                UIInstance.Instance.ButtonRenfortJ1.GetComponent<Button>().interactable = true;
                }
                else
                {
                    UIInstance.Instance.BlueRenfortCount = 0;
                    UIInstance.Instance.ButtonRenfortJ2.GetComponent<Button>().interactable = true;
                }
                if (GameManager.Instance.SabotageStat == 1 && !GameManager.Instance.IsPlayerRedTurn)
                {
                    PlayerScript.Instance.J2Infos.ActivationLeft--;
                    GameManager.Instance.SabotageStat = 3;
                }
                else if (GameManager.Instance.SabotageStat == 2 && GameManager.Instance.IsPlayerRedTurn)
                {
                    PlayerScript.Instance.J1Infos.ActivationLeft--;
                    GameManager.Instance.SabotageStat = 3;
                }
                UIInstance.Instance.UpdateActivationLeft();
                GoToActionJ2Phase();
                break;

            case MYthsAndSteel_Enum.PhaseDeJeu.Strategie:
                RaycastManager.Instance.ActualUnitSelected = null;
                RaycastManager.Instance.ActualTileSelected = null;
                
                foreach (GameObject TS in TilesManager.Instance.TileList)
                {
                    foreach (MYthsAndSteel_Enum.TerrainType T1 in TS.GetComponent<TileScript>().TerrainEffectList)
                    {
                        foreach (TerrainType Type in GameManager.Instance.Terrain.EffetDeTerrain)
                        {
                            foreach (MYthsAndSteel_Enum.TerrainType T2 in Type._eventType)
                            {
                                if (T1 == T2)
                                {
                                    if (Type.Child != null)
                                    {
                                        if (Type.MustBeInstantiate)
                                        {
                                            foreach (GameObject G in TS.GetComponent<TileScript>()._Child)
                                            {
                                                if (G.TryGetComponent<ChildEffect>(out ChildEffect Try2))
                                                {
                                                    if (Try2.Type == T1)
                                                    {
                                                        if (Try2.TryGetComponent<TerrainParent>(out TerrainParent Try3))
                                                        {
                                                            UnitScript u = null;
                                                            if (TS.GetComponent<TileScript>().Unit != null)
                                                            {
                                                                u = TS.GetComponent<TileScript>().Unit.GetComponent<UnitScript>();
                                                            }
                                                            Try3.EndPlayerTurnEffect(GameManager.Instance.IsPlayerRedTurn, u);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (Type.Child.TryGetComponent<TerrainParent>(out TerrainParent Try))
                                            {
                                                UnitScript u = null;
                                                if (TS.GetComponent<TileScript>().Unit != null)
                                                {
                                                    u = TS.GetComponent<TileScript>().Unit.GetComponent<UnitScript>();
                                                }
                                                Try.EndPlayerTurnEffect(GameManager.Instance.IsPlayerRedTurn, u);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                foreach (GameObject unit in PlayerScript.Instance.UnitRef.UnitListRedPlayer)
                {

                    unit.GetComponent<UnitScript>().RemoveStatutToUnit(MYthsAndSteel_Enum.UnitStatut.PeutPasCombattre);
                    unit.GetComponent<UnitScript>().RemoveStatutToUnit(MYthsAndSteel_Enum.UnitStatut.Invincible);
                    unit.GetComponent<UnitScript>().RemoveStatutToUnit(MYthsAndSteel_Enum.UnitStatut.PeutPasPrendreDesObjectifs);

                }

                foreach (GameObject unit in PlayerScript.Instance.UnitRef.UnitListBluePlayer)
                {

                    unit.GetComponent<UnitScript>().RemoveStatutToUnit(MYthsAndSteel_Enum.UnitStatut.PeutPasCombattre);
                    unit.GetComponent<UnitScript>().RemoveStatutToUnit(MYthsAndSteel_Enum.UnitStatut.Invincible);
                    unit.GetComponent<UnitScript>().RemoveStatutToUnit(MYthsAndSteel_Enum.UnitStatut.PeutPasPrendreDesObjectifs);


                }
                GameManager.Instance.GoPhase(MYthsAndSteel_Enum.PhaseDeJeu.Strategie);
                if (GameManager.Instance.armeEpidemelogiqueStat != 0)
                {
                    List<GameObject> refunit5 = new List<GameObject>();
                    if (GameManager.Instance.IsPlayerRedTurn && GameManager.Instance.armeEpidemelogiqueStat == 2)
                    {
                        refunit5 = PlayerScript.Instance.UnitRef.UnitListRedPlayer;
                    }
                    else if (!GameManager.Instance.IsPlayerRedTurn && GameManager.Instance.armeEpidemelogiqueStat == 1)
                    {
                        refunit5 = PlayerScript.Instance.UnitRef.UnitListBluePlayer;
                    }
                    foreach (GameObject unit in refunit5)
                    {

                        if (unit.GetComponent<UnitScript>().UnitStatuts.Contains(MYthsAndSteel_Enum.UnitStatut.ArmeEpidemiologique))
                        {

                            unit.GetComponent<UnitScript>().TakeDamage(1);
                            unit.GetComponent<UnitScript>().RemoveStatutToUnit(MYthsAndSteel_Enum.UnitStatut.ArmeEpidemiologique);
                            foreach (int i in PlayerStatic.GetNeighbourDiag(unit.GetComponent<UnitScript>().ActualTiledId, TilesManager.Instance.TileList[unit.GetComponent<UnitScript>().ActualTiledId].GetComponent<TileScript>().Line, false))
                            {

                                if (TilesManager.Instance.TileList[i].GetComponent<TileScript>().Unit != null)
                                {
                                    TilesManager.Instance.TileList[i].GetComponent<TileScript>().Unit.GetComponent<UnitScript>().TakeDamage(1);
                                }
                            }
                            GameManager.Instance.armeEpidemelogiqueStat = 0;
                            refunit5 = null;
                        }
                    }
                }
                      
                    if (GameManager.Instance.possesion)
                    {
                        foreach (GameObject unit in GameManager.Instance.IsPlayerRedTurn ? PlayerScript.Instance.UnitRef.UnitListBluePlayer : PlayerScript.Instance.UnitRef.UnitListRedPlayer)
                        {
                           
                            GameManager.Instance.possesion = false;
                            unit.GetComponent<UnitScript>().ResetStatutPossesion();

                        }
                    }

                List<GameObject> refunit2 = new List<GameObject>();
                refunit2.AddRange(PlayerScript.Instance.UnitRef.UnitListRedPlayer);
                refunit2.AddRange(PlayerScript.Instance.UnitRef.UnitListBluePlayer);
                if (GameManager.Instance.IsPlayerRedTurn)
                {

                    foreach (GameObject unit in refunit2)
                    {

                        if (unit.GetComponent<UnitScript>().ParalysieStat == 2)
                        {
                            unit.GetComponent<UnitScript>().RemoveStatutToUnit(MYthsAndSteel_Enum.UnitStatut.Paralysie);
                            unit.GetComponent<UnitScript>().ParalysieStat = 3;
                        }

                    }


                }
                else if (!GameManager.Instance.IsPlayerRedTurn)
                {

                    foreach (GameObject unit in refunit2)
                    {

                        if (unit.GetComponent<UnitScript>().ParalysieStat == 1)
                        {
                            unit.GetComponent<UnitScript>().RemoveStatutToUnit(MYthsAndSteel_Enum.UnitStatut.Paralysie);
                            unit.GetComponent<UnitScript>().ParalysieStat = 3;
                        }

                    }


                }
                refunit2.Clear();

                List<GameObject> refunit4 = new List<GameObject>();
                refunit4.AddRange(PlayerScript.Instance.UnitRef.UnitListRedPlayer);
                refunit4.AddRange(PlayerScript.Instance.UnitRef.UnitListBluePlayer);
                if (GameManager.Instance.IsPlayerRedTurn)
                {

                    foreach (GameObject unit in refunit4)
                    {

                        if (unit.GetComponent<UnitScript>().SilenceStat == 2)
                        {
                            unit.GetComponent<UnitScript>().RemoveStatutToUnit(MYthsAndSteel_Enum.UnitStatut.Silence);
                            unit.GetComponent<UnitScript>().SilenceStat = 3;
                        }

                    }


                }
                else if (!GameManager.Instance.IsPlayerRedTurn)
                {

                    foreach (GameObject unit in refunit4)
                    {

                        if (unit.GetComponent<UnitScript>().SilenceStat == 1)
                        {
                            unit.GetComponent<UnitScript>().RemoveStatutToUnit(MYthsAndSteel_Enum.UnitStatut.Silence);
                            unit.GetComponent<UnitScript>().SilenceStat = 3;
                        }

                    }


                }
                refunit4.Clear();
                if (GameManager.Instance.statetImmobilisation != 3)
                {
                    List<GameObject> refunit6 = new List<GameObject>();
                    refunit6.AddRange(PlayerScript.Instance.UnitRef.UnitListRedPlayer);
                    refunit6.AddRange(PlayerScript.Instance.UnitRef.UnitListBluePlayer);
                    if (GameManager.Instance.IsPlayerRedTurn && GameManager.Instance.statetImmobilisation == 2 || !GameManager.Instance.IsPlayerRedTurn && GameManager.Instance.statetImmobilisation == 1)
                    {

                        foreach (GameObject unit in refunit6)
                        {

                            if (unit.GetComponent<UnitScript>().UnitStatuts.Contains(MYthsAndSteel_Enum.UnitStatut.Immobilisation))
                            {
                                unit.GetComponent<UnitScript>().RemoveStatutToUnit(MYthsAndSteel_Enum.UnitStatut.Immobilisation);
                            }

                        }

                        GameManager.Instance.statetImmobilisation = 3;
                    }
                }
               
               


                    UIInstance.Instance.ButtonRenfortJ1.GetComponent<Button>().interactable = false;
               UIInstance.Instance.ButtonRenfortJ2.GetComponent<Button>().interactable = false;
                
           
                PlayerScript.Instance.J1Infos.HasCreateUnit = false;
                PlayerScript.Instance.J2Infos.HasCreateUnit = false;
                if (GoToStrategyPhase != null) GoToStrategyPhase();
                        break;
        }
    }
        
    /// <summary>
    /// Est ce que l'event de d�but poss�de des fonctions � appeler
    /// </summary>
    /// <returns></returns>
    public bool GetDebutFunction()
    {
        if (GoToDebutPhase != null) return true;
        else return false;
    }
    public void LoadMainMenu()
    {
        Time.timeScale = 1;
        GoToDebutPhase = null;
        azer = false;
        GoToActivationPhase = null;
        GoToOrgoneJ1Phase = null;
        GoToActionJ1Phase = null;
        GoToOrgoneJ2Phase = null;
        GoToActionJ2Phase = null; 
        GoToStrategyPhase = null;
       GameManager.Instance.isGamePaused = false;
       SceneManager.LoadScene(1);
    }
    public void LoadScene(int sceneToLoad)
    {
        Time.timeScale = 1;
      
        GoToDebutPhase = null;
        azer = false;
        GoToActivationPhase = null;
        GoToOrgoneJ1Phase = null;
        GoToActionJ1Phase = null;
        GoToOrgoneJ2Phase = null;
        GoToActionJ2Phase = null;
        GoToStrategyPhase = null;
        GameManager.Instance.isGamePaused = false;
        SceneManager.LoadScene(sceneToLoad);
      
    }
}