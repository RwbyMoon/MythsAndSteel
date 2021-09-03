using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*
    Ce script est le Game Manager. Il va g�rer toutes les phases du jeu, les diff�rents tours de jeu, ...
    Il sera aussi la pour changer de phase, changer de tour, ...
    Il est indispensable au jeu!!!

    NE LE SUPPRIMEZ PAS DE VOTRE SCENE!!!
*/

public class GameManager : MonoSingleton<GameManager>
{


    #region Variables
    public int statetImmobilisation = 3;
    [SerializeField] private SaveData saveData;
    public GameObject d�tonationPrefab;
    public VictoryScreen victoryScreen;
   
    [Header("INFO TOUR ACTUEL")]
    //Correspond � la valeur du tour actuel
  public  int armeEpidemelogiqueStat = 0;
    public bool filBbarbel�s = false;
    public int VolDeRavitaillementStat = 3;
    public bool possesion = false;
    public int SabotageStat = 3;

    [SerializeField]
    GameObject pauseMenu;
    /// <param name="sceneId"></param>
   public  bool menuOptionOuvert = false;
    [SerializeField]
    GameObject backgroundActivation;
    public bool isGamePaused = false;
    [SerializeField]
    GameObject BackgroundPaused;
    [SerializeField] private int _actualTurnNumber = 0;

    public int ActualTurnNumber
    {
        get
        {
            return _actualTurnNumber;
        }
        set
        {
            _actualTurnNumber = value;
        }
    }
  
    [SerializeField] TextMeshProUGUI _TurnNumber;

    //Permet de savoir si c'est le joueur 1 (TRUE) ou le joueur 2 (FALSE) qui commence durant ce tour
    [SerializeField] private bool _isPlayerRedStarting = false;
    public bool IsPlayerRedStarting => _isPlayerRedStarting;

    //Permet de savoir si c'est le joueur 1 (TRUE) ou le joueur 2 (FALSE) qui joue actuellement
    [SerializeField] private bool _isPlayerRedTurn = false;
    public bool IsPlayerRedTurn => _isPlayerRedTurn;

    //Est ce que les joueurs sont actuellement dans un tour de jeu?
    [SerializeField] private bool _isInTurn = false;
    public bool IsInTurn => _isInTurn;


    [Header("INFO PHASE DE JEU ACTUEL")]
    //Correspond � la phase actuelle durant le tour
    [SerializeField] private MYthsAndSteel_Enum.PhaseDeJeu _actualTurnPhase = MYthsAndSteel_Enum.PhaseDeJeu.Debut;
    public MYthsAndSteel_Enum.PhaseDeJeu ActualTurnPhase => _actualTurnPhase;

    [SerializeField] private ChangeActivPhase _changeActivPhase = null;

    [Header("REFERENCES DES SCRIPTABLE")]
    //Event Manager
    [SerializeField] private EventCardClass _eventCardSO = null;
    public EventCardClass EventCardSO => _eventCardSO;

    //Game Manager avec tous les event
    [SerializeField] private GameManagerSO _managerSO = null;
    public GameManagerSO ManagerSO => _managerSO;

    //Option manager pour ouvrir le menu d'option
    [SerializeField] private MenuTransition _optionSO = null;
    public MenuTransition OptionSO => _optionSO;

    [Header("RENFORT PHASE SCRIPT")]
    [SerializeField] RenfortPhase _renfortPhase = null;
    public RenfortPhase RenfortPhase => _renfortPhase;

    [Header("SELECTION UNITE")]
    [Header("MODE EVENEMENT")]
    [Space]
    //Est ce que le joueur est en train de choisir des unit�s
    [SerializeField] private bool _chooseUnitForEvent = false;
    public bool ChooseUnitForEvent => _chooseUnitForEvent;

    //Liste des unit�s s�lectionnables par le joueur
    [SerializeField] private List<GameObject> _selectableUnit = new List<GameObject>();
    public List<GameObject> SelectableUnit => _selectableUnit;

    List<GameObject> _saveselectableUnit = new List<GameObject>();

    //Liste des unit�s choisies
    [SerializeField] private List<GameObject> _unitChooseList = new List<GameObject>();
    public List<GameObject> UnitChooseList => _unitChooseList;



    //Nombre d'unit� � choisir
    [SerializeField] int _numberOfUnitToChoose = 0;

    [Header("SELECTION CASE")]
    //Est ce que le joueur est en train de choisir des unit�s
    [SerializeField] private bool _chooseTileForEvent = false;
    public bool ChooseTileForEvent => _chooseTileForEvent;

    //Liste des cases s�lectionnables
    public List<GameObject> _selectableTiles = new List<GameObject>();

    //Nombre d'unit� � choisir
    [SerializeField] int _numberOfTilesToChoose = 0;

    //Liste des unit�s choisies
    [SerializeField] private List<GameObject> _tileChooseList = new List<GameObject>();
    public List<GameObject> TileChooseList => _tileChooseList;


    [Header("SELECTION VARIABLE COMMUNE")]
    //Est ce que c'est le joueur rouge qui a utilis� les cartes events
    [SerializeField] bool _redPlayerUseEvent = false;
    public bool RedPlayerUseEvent => _redPlayerUseEvent;

    [SerializeField] private Sprite _selectedTileSprite = null;
    public Sprite _normalEventSprite = null;

    [HideInInspector] public bool IllusionStrat�gique = false;

    string _titleValidation = "";
    string _descriptionValidation = "";
    bool _canSelectMultiples = false;

    //Fonctions � appeler apr�s que le joueur ait choisit les unit�s
    public delegate void EventToCallAfterChoose();
    public EventToCallAfterChoose _eventCall;
    public EventToCallAfterChoose _eventCallCancel;
    public EventToCallAfterChoose _waitEvent;

    [Header("ACTIVATION")]
    //Est ce que le joueur est en train de choisir des unit�s
    [SerializeField] private PhaseActivation _activationPhase = null;
    public PhaseActivation ActivationPhase => _activationPhase;
    public bool activationDone = false;
    float deltaTimeX = 0f;

    // Scriptable terrain.
    [SerializeField] TerrainTypeClass _Terrain;
    public TerrainTypeClass Terrain => _Terrain;

    //Valeur lue lors d'un changement de phase
    public bool IsNextPhaseDone = false;

    #region CheckOrgone
    //Check l'orgone pour �viter l'override
    public bool IsCheckingOrgone = false;
    public bool DoingEpxlosionOrgone = false;
    public int DeathByOrgone = 0;



    //Event qui permet d'attendre pour donner de l'orgone � un joueur
    public delegate void Checkorgone();
    public Checkorgone _waitToCheckOrgone;

    //Quel joueur attend de recevoir son orgone
    private int _playerOrgone = 0;
    public int PlayerOrgone => _playerOrgone;

    //Quelle est la valeur a donner au joueur
    private int _valueOrgone = 0;
    public int ValueOrgone => _valueOrgone;
    #endregion CheckOrgone
  
    #endregion Variables

    /// <summary>
    /// Permet d'initialiser le script
    /// </summary>
    private void Start()
    {
        _managerSO.GoToOrgoneJ1Phase += DetermineWhichPlayerplay;
        _managerSO.GoToOrgoneJ2Phase += DetermineWhichPlayerplay;
        _isInTurn = true;
        activationDone = false;
    }

    private void Update()
    {

    }

    /// <summary>
    /// Quand le joueur clic pour passer � la phase suivante
    /// </summary>
    public void CliCToChangePhase()
    {

        _eventCallCancel += CancelSkipPhase;
        _eventCall += ChangePhase;
        _eventCall += SoundController.Instance.nextPhaseSound;

        if (PlayerPrefs.GetInt("Avertissement") == 0)
        {
            _eventCall();

        }
        UIInstance.Instance.ShowValidationPanel("Passer � la phase suivante", "�tes-vous sur de vouloir passer � la phase suivante? En passant la phase vous n'aurez pas la possibilit� de revenir en arri�re.");
    }

    /// <summary>
    /// Quand le joueur annule le fait de passer une phase
    /// </summary>
    void CancelSkipPhase()
    {
        _eventCall = null;
        _eventCallCancel = null;
        UIInstance.Instance.ActivateNextPhaseButton();
    }

    /// <summary>
    /// Aller � la phase de jeu renseigner en param�tre
    /// </summary>
    /// <param name="phaseToGoTo"></param>
    public void ChangePhase()
    {
        //Affiche le panneau de transition d'UI
        _isInTurn = false;
        _eventCall = null;
        _eventCallCancel = null;
        IsNextPhaseDone = true;
        StartCoroutine(ChangePhaseDone());
        OnclickedEvent();
    }

    /// <summary>
    /// Donne la victoire � une arm�e
    /// </summary>
    /// <param name="armeeGagnante"></param>
    public void VictoryForArmy(int armeeGagnante)
    {
        Debug.Log($"Arm�e {armeeGagnante} a gagn�");
        if (armeeGagnante == 1)
        {
            UIInstance.Instance.VictoryScreen.SetActive(true);
            victoryScreen.IsVictoryScreenActive = true;
            victoryScreen.RedWin = true;
            saveData.redPlayerVictories += 1;
            PlayerPrefs.SetInt("RedPlayerVictories", saveData.redPlayerVictories);
            Debug.Log(PlayerPrefs.GetInt("RedPlayerVictories"));
            saveData.unlockCampaign += 1;
            PlayerPrefs.SetInt("UnlockCampaign", saveData.unlockCampaign);
            Debug.Log(PlayerPrefs.GetInt("UnlockCampaign"));
            Debug.Log("Red win.");
        }
        else if (armeeGagnante == 2)
        {
            UIInstance.Instance.VictoryScreen.SetActive(true);
            victoryScreen.IsVictoryScreenActive = true;
            victoryScreen.BlueWin = true;
            saveData.bluePlayerVictories += 1;
            PlayerPrefs.SetInt("BluePlayerVictories", saveData.bluePlayerVictories);
            saveData.unlockCampaign += 1;
            PlayerPrefs.SetInt("UnlockCampaign", saveData.unlockCampaign);
            Debug.Log("Blue win.");
        }
    }

    /// <summary>
    /// Determine quel joueur est actuellement en train de jouer
    /// </summary>
    void DetermineWhichPlayerplay()
    {
        if (_actualTurnPhase == MYthsAndSteel_Enum.PhaseDeJeu.OrgoneJ1)
        {
            if (_isPlayerRedStarting)
            {
                _isPlayerRedTurn = true;
            }
            else
            {
                _isPlayerRedTurn = false;
            }
        }
        else if (_actualTurnPhase == MYthsAndSteel_Enum.PhaseDeJeu.OrgoneJ2)
        {
            if (_isPlayerRedStarting)
            {
                _isPlayerRedTurn = false;
            }
            else
            {
                _isPlayerRedTurn = true;
            }
        }
    }

    /// <summary>
    /// Fonction qui est appell�e lorsque l'event est appell� (event lors du clic sur le bouton pour passer � la phase suivante)
    /// </summary>
    void OnclickedEvent()
    {
        SwitchPhaseObjectUI();
        if (ActualTurnPhase == MYthsAndSteel_Enum.PhaseDeJeu.Debut)
        {
            _isInTurn = false;
        }
        else
        {
            _isInTurn = true;
        }
    }

    public void GoPhase(MYthsAndSteel_Enum.PhaseDeJeu phase)
    {
        _actualTurnPhase = phase;
        _changeActivPhase.ChangeActivObj();
    }

    #region UIFunction
    /// <summary>
    /// Affiche le panneau d'indication de changement de phase. Les joueurs doivent cliquer sur un bouton pour passer la phase
    /// </summary>
    void SwitchPhaseObjectUI()
    {

        int nextPhase = (int)_actualTurnPhase + 1 > 6 ? 0 : (int)_actualTurnPhase + 1;
        if ((MYthsAndSteel_Enum.PhaseDeJeu)nextPhase != MYthsAndSteel_Enum.PhaseDeJeu.Debut)
        {
            createPanel(1);
        }
        else if ((MYthsAndSteel_Enum.PhaseDeJeu)nextPhase == MYthsAndSteel_Enum.PhaseDeJeu.Debut && !ManagerSO.GetDebutFunction())
        {
            createPanel(2);
        }
        else
        {
            createPanel(1);
        }



        if (ActualTurnPhase == MYthsAndSteel_Enum.PhaseDeJeu.Debut)
        {
            StartCoroutine(waitToChange());
        }
        else if (ActualTurnPhase == MYthsAndSteel_Enum.PhaseDeJeu.Strategie && !ManagerSO.GetDebutFunction())
        {
            StartCoroutine(waitToChange());
        }
        else
        {
            ManagerSO.GoToPhase();
        }
    }

    void createPanel(int i)
    {
        //Ajoute le menu o� il faut cliquer
        //Instantie le panneau de transition entre deux phases et le garde en m�moire
        UIInstance.Instance.DesactivateNextPhaseButton();
        GameObject phaseObj = Instantiate(UIInstance.Instance.SwitchPhaseObject, UIInstance.Instance.CanvasTurnPhase.transform.position,
                                          Quaternion.identity, UIInstance.Instance.CanvasTurnPhase.transform);

        //Variable qui permet d'avoir le texte � afficher au d�but de la phase
        int nextPhase = (int)ActualTurnPhase + i > 6 ? 0 + i - 1 : (int)ActualTurnPhase + 1;
        string textForSwitch = "Phase " + ((MYthsAndSteel_Enum.PhaseDeJeu)nextPhase).ToString();

        phaseObj.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = textForSwitch;

        Destroy(phaseObj, 1.25f);
        StartCoroutine(ButtonDesactivateWhenAnimation());

    }
    IEnumerator ButtonDesactivateWhenAnimation()
    {
        yield return new WaitForSeconds(1.25f);
        UIInstance.Instance.ActivateNextPhaseButton();
    }
    #endregion UIFunction

    /// <summary>
    /// Permet d'avoir en r�f�rence si c'est le joueur 1 qui commence ou le joueur 2
    /// </summary>
    /// <param name="player1"></param>
    public void SetPlayerStart(bool player1)
    {
        _isPlayerRedStarting = player1;

    }

    private void FixedUpdate()
    {
        
    }
    #region EventMode
    /// <summary>
    /// Commence le mode event pour choisir une unit�
    /// </summary>
    /// <param name="numberUnit"></param>
    /// <param name="opponentUnit"></param>
    /// <param name="armyUnit"></param>
    public void StartEventModeUnit(int numberUnit, bool redPlayer, List<GameObject> _unitSelectable, string title, string description, bool multiplesUnit = false)
    {
        if (IsPlayerRedTurn && ActualTurnPhase == MYthsAndSteel_Enum.PhaseDeJeu.ActionJ1 || ActualTurnPhase == MYthsAndSteel_Enum.PhaseDeJeu.ActionJ2)
        {

            UIInstance.Instance.ButtonRenfortJ1.GetComponent<Button>().interactable = false;

        }
        else if (!IsPlayerRedTurn && ActualTurnPhase == MYthsAndSteel_Enum.PhaseDeJeu.ActionJ1 || ActualTurnPhase == MYthsAndSteel_Enum.PhaseDeJeu.ActionJ2)
        {

            UIInstance.Instance.ButtonRenfortJ2.GetComponent<Button>().interactable = false;
        }

        UIInstance.Instance.DesactivateNextPhaseButton();
        _titleValidation = title;
        _descriptionValidation = description;
        Attaque.Instance.PanelBlockant1.SetActive(true);
        Attaque.Instance.PanelBlockant2.SetActive(true);
        Attaque.Instance.PanelBlockantOrgone1.SetActive(true);
        Attaque.Instance.PanelBlockantOrgone2.SetActive(true);
        _numberOfUnitToChoose = numberUnit;
        _chooseUnitForEvent = true;
        _selectableUnit.AddRange(_unitSelectable);
        _redPlayerUseEvent = redPlayer;
        _canSelectMultiples = multiplesUnit;
        if(!DoingEpxlosionOrgone)
        {

        foreach (GameObject gam in _selectableUnit)
        {
            TilesManager.Instance.TileList[gam.GetComponent<UnitScript>().ActualTiledId].GetComponent<TileScript>().ActiveChildObj(MYthsAndSteel_Enum.ChildTileType.EventSelect);
        }
        }
        else
        {
            foreach (GameObject gam in _selectableUnit)
            {
                if (gam.GetComponent<UnitScript>()._hasStartMove && Mouvement.Instance._selectedTileId.Count > 0)
                {
                    TilesManager.Instance.TileList[Mouvement.Instance._selectedTileId[Mouvement.Instance._selectedTileId.Count - 1]].GetComponent<TileScript>().ActiveChildObj(MYthsAndSteel_Enum.ChildTileType.EventSelect);

                }
                else
                {

                     TilesManager.Instance.TileList[gam.GetComponent<UnitScript>().ActualTiledId].GetComponent<TileScript>().ActiveChildObj(MYthsAndSteel_Enum.ChildTileType.EventSelect);
                }
            }
        }
       if(!DoingEpxlosionOrgone && PlayerPrefs.GetInt("Avertissement") == 0 || PlayerPrefs.GetInt("Avertissement") == 1)
        {

        _eventCall += StopEventModeUnit;
          
        }
       

        

    }

    /// <summary>
    /// Arrete le choix d'unit�
    /// </summary>
 public   void StopEventModeUnit()
    {
        if (GameManager.Instance.IsPlayerRedTurn)
        {
            if (UIInstance.Instance.RedRenfortCount == 0)
            {

            UIInstance.Instance.ButtonRenfortJ1.GetComponent<Button>().interactable = true;
            }
        }
        else
        {
            if (UIInstance.Instance.BlueRenfortCount == 0)
            {

            UIInstance.Instance.ButtonRenfortJ2.GetComponent<Button>().interactable = true;
            }
        }
        _titleValidation = "";
        _descriptionValidation = "";

        _numberOfUnitToChoose = 0;

        _chooseUnitForEvent = false;
        _redPlayerUseEvent = false;
        IllusionStrat�gique = false;
        _canSelectMultiples = false;
        Attaque.Instance.PanelBlockant1.SetActive(false);
        Attaque.Instance.PanelBlockant2.SetActive(false);
        Attaque.Instance.PanelBlockantOrgone1.SetActive(false);
        Attaque.Instance.PanelBlockantOrgone2.SetActive(false);
        _eventCall = null;

        foreach (GameObject gam in _selectableUnit)
        {
            //D�truit l'enfant avec le tag selectable tile
            GameObject tile = TilesManager.Instance.TileList[gam.GetComponent<UnitScript>().ActualTiledId];
            if (tile != null)
            {
                tile.GetComponent<TileScript>().ActiveChildObj(MYthsAndSteel_Enum.ChildTileType.EventSelect, _normalEventSprite);
                tile.GetComponent<TileScript>().DesActiveChildObj(MYthsAndSteel_Enum.ChildTileType.EventSelect);
            }
        }

        _selectableUnit.Clear();
    }

    /// <summary>
    /// Ajoute une unit� � la liste des unit�s s�lectionn�es
    /// </summary>
    /// <param name="unit"></param>
    public void AddUnitToList(GameObject unit)
    {
        if (unit != null)
        {
            if (_canSelectMultiples)
            {
                if (DoingEpxlosionOrgone)
                {


                    Debug.Log("Do explosion");
                    int TimeChoosen = 1;
                    for (int i = 0; i < _unitChooseList.Count; i++)
                    {
                        if (_unitChooseList[i] == unit)
                        {
                            TimeChoosen++;
                        }

                    }

                    if (TimeChoosen == unit.GetComponent<UnitScript>().Life)
                    {
                        SelectableUnit.Remove(unit);
                        unit.GetComponent<UnitScript>().DieByOrgone();
                    }

                    _unitChooseList.Add(unit);
                    TilesManager.Instance.TileList[unit.GetComponent<UnitScript>().ActualTiledId].GetComponent<TileScript>().ActiveChildObj(MYthsAndSteel_Enum.ChildTileType.EventSelect, _selectedTileSprite);
                    SoundController.Instance.PlaySound(SoundController.Instance.AudioClips[10]);

                }
                else
                {

                    _unitChooseList.Add(unit);
                    TilesManager.Instance.TileList[unit.GetComponent<UnitScript>().ActualTiledId].GetComponent<TileScript>().ActiveChildObj(MYthsAndSteel_Enum.ChildTileType.EventSelect, _selectedTileSprite);
                    SoundController.Instance.PlaySound(SoundController.Instance.AudioClips[10]);
                }
            }
            else if (!_canSelectMultiples && !_unitChooseList.Contains(unit))
            {
                _unitChooseList.Add(unit);
                TilesManager.Instance.TileList[unit.GetComponent<UnitScript>().ActualTiledId].GetComponent<TileScript>().ActiveChildObj(MYthsAndSteel_Enum.ChildTileType.EventSelect, _selectedTileSprite);
                SoundController.Instance.PlaySound(SoundController.Instance.AudioClips[10]);
            }
            //Pour la carte �v�nement illusion strat�gique
            if (IllusionStrat�gique)
            {
                foreach (GameObject gam in _selectableUnit)
                {
                    if (gam.GetComponent<UnitScript>().UnitSO.IsInRedArmy != unit.GetComponent<UnitScript>().UnitSO.IsInRedArmy)
                    {
                        GameObject tile = TilesManager.Instance.TileList[gam.GetComponent<UnitScript>().ActualTiledId].gameObject;
                        tile.GetComponent<TileScript>().DesActiveChildObj(MYthsAndSteel_Enum.ChildTileType.EventSelect);
                    }
                }
            }

            if (_unitChooseList.Count == _numberOfUnitToChoose)
            {
                _chooseUnitForEvent = false;
                if (PlayerPrefs.GetInt("Avertissement") == 0)
                {
                    if(DoingEpxlosionOrgone)
                    {
                        _eventCall += StopEventModeUnit;

                       DoingEpxlosionOrgone = false;
                    }
                    _eventCall();
         
                }
               
                UIInstance.Instance.ShowValidationPanel(_titleValidation, _descriptionValidation);
            }
        }
    }

    /// <summary>
    /// Enleve une unit� de la liste
    /// </summary>
    /// <param name="unit"></param>
    public void RemoveUnitToList(GameObject unit)
    {
        _unitChooseList.Remove(unit);
        if (!_unitChooseList.Contains(unit))
        {
            SoundController.Instance.PlaySound(SoundController.Instance.AudioClips[10]);
            TilesManager.Instance.TileList[unit.GetComponent<UnitScript>().ActualTiledId].GetComponent<TileScript>().ActiveChildObj(MYthsAndSteel_Enum.ChildTileType.EventSelect, _normalEventSprite);
        }

        if (IllusionStrat�gique)
        {
            foreach (GameObject gam in _selectableUnit)
            {
                SoundController.Instance.PlaySound(SoundController.Instance.AudioClips[10]);
                GameObject tile = TilesManager.Instance.TileList[gam.GetComponent<UnitScript>().ActualTiledId].gameObject;
                tile.GetComponent<TileScript>().ActiveChildObj(MYthsAndSteel_Enum.ChildTileType.EventSelect, _normalEventSprite);
            }
        }
    }

    /// <summary>
    /// Commence le mode event pour choisir une case du plateau
    /// </summary>
    /// <param name="numberOfTile"></param>
    /// <param name="redPlayer"></param>
    /// <param name="_tileSelectable"></param>
    public void StartEventModeTiles(int numberOfTile, bool redPlayer, List<GameObject> _tileSelectable, string title, string description, bool multiplesTile = false)
    {
        if (GameManager.Instance.IsPlayerRedTurn)
        {
            UIInstance.Instance.ButtonRenfortJ1.GetComponent<Button>().interactable = false;

        }
        else
        {

            UIInstance.Instance.ButtonRenfortJ2.GetComponent<Button>().interactable = false;
        }
        UIInstance.Instance.DesactivateNextPhaseButton();
        Attaque.Instance.PanelBlockant1.SetActive(true);
        Attaque.Instance.PanelBlockant2.SetActive(true);
        Attaque.Instance.PanelBlockantOrgone1.SetActive(true);
        Attaque.Instance.PanelBlockantOrgone2.SetActive(true);
        _titleValidation = title;
        _descriptionValidation = description;

        _chooseTileForEvent = true;
        _redPlayerUseEvent = redPlayer;
        _numberOfTilesToChoose = numberOfTile;
        _selectableTiles.AddRange(_tileSelectable);
        _canSelectMultiples = multiplesTile;

        foreach (GameObject gam in _selectableTiles)
        {
            gam.GetComponent<TileScript>().ActiveChildObj(MYthsAndSteel_Enum.ChildTileType.EventSelect);
        }

        _eventCall += StopEventModeTile;
    }

    /// <summary>
    /// Arrete le choix de case
    /// </summary>
    public void StopEventModeTile()
    {
        if (IsPlayerRedTurn && ActualTurnPhase == MYthsAndSteel_Enum.PhaseDeJeu.ActionJ1 || ActualTurnPhase == MYthsAndSteel_Enum.PhaseDeJeu.ActionJ2)
        {
            if (UIInstance.Instance.RedRenfortCount == 0)
            {

            UIInstance.Instance.ButtonRenfortJ1.GetComponent<Button>().interactable = true;
            }

        }
        else if(!IsPlayerRedTurn && ActualTurnPhase == MYthsAndSteel_Enum.PhaseDeJeu.ActionJ1 || ActualTurnPhase == MYthsAndSteel_Enum.PhaseDeJeu.ActionJ2)
        {
            if (UIInstance.Instance.BlueRenfortCount == 0)
            {

            UIInstance.Instance.ButtonRenfortJ2.GetComponent<Button>().interactable = true;
            }

        }
        _titleValidation = "";
        _descriptionValidation = "";

        _numberOfTilesToChoose = 0;

        _chooseTileForEvent = false;
        _redPlayerUseEvent = false;
        IllusionStrat�gique = false;
        _canSelectMultiples = false;
        Attaque.Instance.PanelBlockant1.SetActive(false);
        Attaque.Instance.PanelBlockant2.SetActive(false);
        Attaque.Instance.PanelBlockantOrgone1.SetActive(false);
        Attaque.Instance.PanelBlockantOrgone2.SetActive(false);
        _eventCall = null;

        foreach (GameObject gam in _selectableTiles)
        {
            gam.GetComponent<TileScript>().ActiveChildObj(MYthsAndSteel_Enum.ChildTileType.EventSelect, _normalEventSprite);
            gam.GetComponent<TileScript>().DesActiveChildObj(MYthsAndSteel_Enum.ChildTileType.EventSelect);
        }

        _selectableTiles.Clear();
    }

    /// <summary>
    /// Ajoute la case � la liste
    /// </summary>
    /// <param name="tile"></param>
    public void AddTileToList(GameObject tile)
    {
        if (tile != null)
        {
            if (_selectableTiles.Contains(tile))
            {
                if (_canSelectMultiples)
                {
                    _tileChooseList.Add(tile);
                    SoundController.Instance.PlaySound(SoundController.Instance.AudioClips[10]);
                    tile.GetComponent<TileScript>().ActiveChildObj(MYthsAndSteel_Enum.ChildTileType.EventSelect, _selectedTileSprite);
                }
                else if (!_tileChooseList.Contains(tile) && !_canSelectMultiples)
                {
                    _tileChooseList.Add(tile);
                    SoundController.Instance.PlaySound(SoundController.Instance.AudioClips[10]);
                    tile.GetComponent<TileScript>().ActiveChildObj(MYthsAndSteel_Enum.ChildTileType.EventSelect, _selectedTileSprite);
                }

                if (_tileChooseList.Count == _numberOfTilesToChoose)
                {
                    _chooseTileForEvent = false;
                    if (PlayerPrefs.GetInt("Avertissement") == 0)
                    {
                        _eventCall();
                    UIInstance.Instance.ShowValidationPanel(_titleValidation, _descriptionValidation);
         
                       
     
                    }
                    else
                    {
                        if(OrgoneManager.Instance.DoingOrgoneCharge)
                        {
                            _eventCall();
                        }
                        else
                        {
                            UIInstance.Instance.ShowValidationPanel(_titleValidation, _descriptionValidation);
                        }
                    }
                }
            }
            if (filBbarbel�s && _tileChooseList.Count >= 1)
            {
                if (_tileChooseList.Count == 1)
                {
                    SoundController.Instance.PlaySound(SoundController.Instance.AudioClips[10]);
                    _tileChooseList[0].GetComponent<TileScript>().ActiveChildObj(MYthsAndSteel_Enum.ChildTileType.EventSelect, _normalEventSprite);
                    _selectableTiles.Clear();

                    foreach (int element in PlayerStatic.GetNeighbourDiag(_tileChooseList[0].GetComponent<TileScript>().TileId, _tileChooseList[0].GetComponent<TileScript>().Line, false))
                    {
                        TilesManager.Instance.TileList[element].GetComponent<TileScript>().ActiveChildObj(MYthsAndSteel_Enum.ChildTileType.EventSelect, _selectedTileSprite);
                        _selectableTiles.Add(TilesManager.Instance.TileList[element]);

                    }

                }

                else if (_tileChooseList.Count == 2)
                {
                    SoundController.Instance.PlaySound(SoundController.Instance.AudioClips[10]);
                    foreach (GameObject element in _selectableTiles)
                    {
                        element.GetComponent<TileScript>().ActiveChildObj(MYthsAndSteel_Enum.ChildTileType.EventSelect, _normalEventSprite);
                    }
                }
                
            }
        }
    }

    /// <summary>
    /// Enleve une case � la liste des cases s�lectionn�es
    /// </summary>
    /// <param name="tile"></param>
    public void RemoveTileToList(GameObject tile)
    {

        if(!filBbarbel�s)
        {
         
            _tileChooseList.Remove(tile);
        if (!_tileChooseList.Contains(tile))
        {
                SoundController.Instance.PlaySound(SoundController.Instance.AudioClips[10]);
                tile.GetComponent<TileScript>().ActiveChildObj(MYthsAndSteel_Enum.ChildTileType.EventSelect, _normalEventSprite);
        }
        }

        if (filBbarbel�s)
        {
            if (_tileChooseList.Contains(tile))
            {
            SoundController.Instance.PlaySound(SoundController.Instance.AudioClips[10]);
                _selectableTiles.Clear();
                _selectableTiles.AddRange(TilesManager.Instance.TileList);
            Debug.Log("test3");
            foreach (int element in PlayerStatic.GetNeighbourDiag(_tileChooseList[0].GetComponent<TileScript>().TileId, _tileChooseList[0].GetComponent<TileScript>().Line, false))
            {
               
          TilesManager.Instance.TileList[element].GetComponent<TileScript>().ActiveChildObj(MYthsAndSteel_Enum.ChildTileType.EventSelect, _normalEventSprite);
                Debug.Log("test4");
                _tileChooseList.Remove(tile);
            }
            }

            
        }
    }

    /// <summary>
    /// Fonction qui permet d'attendre avant de relancer une autre fonction
    /// </summary>
    /// <param name="t"></param>
    public void WaitToMove(float t)
    {
        StartCoroutine(waitToCall(t));
    }
    IEnumerator waitToCall(float t)
    {
        yield return new WaitForSeconds(t);
        if (_waitEvent != null)
        {
            _waitEvent();
        }
    }

    /// <summary>
    /// Call the event of validation panel
    /// </summary>
    public void CallEvent()
    {
        if (DoingEpxlosionOrgone)
        {
            if (_unitChooseList.Count != _selectableUnit.Count)
                foreach (GameObject gam in _unitChooseList)
                {
                    if (!_selectableUnit.Contains(gam)) _selectableUnit.Add(gam);
                }
        }
        _eventCall();
    
        DoingEpxlosionOrgone = false;
    }

    /// <summary>
    /// Call the event cancel on the validation panel
    /// </summary>
    public void CancelEvent()
    {
        foreach (GameObject gam in _unitChooseList)
        {
            if (!_selectableUnit.Contains(gam)) _selectableUnit.Add(gam);
        }
        UIInstance.Instance.ActivateNextPhaseButton();
        StopEventModeTile();
        StopEventModeUnit();


        TileChooseList.Clear();
        UnitChooseList.Clear();

        if (_eventCallCancel != null) _eventCallCancel();
    }
    #endregion EventMode

    IEnumerator waitToChange()
    {
        yield return new WaitForSeconds(1.35f);

        ManagerSO.GoToPhase();
        _isInTurn = true;
    }

    public void UpdateTurn()
    {
        _TurnNumber.text = _actualTurnNumber.ToString();
        
        victoryScreen.turnCounter = _actualTurnNumber;
   
    }

    public void Paused()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        BackgroundPaused.SetActive(true);
        if (ActualTurnPhase == MYthsAndSteel_Enum.PhaseDeJeu.Activation)
        {
            backgroundActivation.SetActive(false);

        }
        isGamePaused = true;
    }
    public void StopPaused()
    {


        Time.timeScale = 1;
        isGamePaused = false;

        pauseMenu.SetActive(false);
        if (ActualTurnPhase == MYthsAndSteel_Enum.PhaseDeJeu.Activation)
        {
            backgroundActivation.SetActive(true);

        }
        BackgroundPaused.SetActive(false);

    }

    IEnumerator ChangePhaseDone()
    {
        yield return new WaitForSeconds(2);
        IsNextPhaseDone = false;
    }
}
