using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "META/Event Scriptable")]
public class EventCardClass : ScriptableObject{
    //Nombre de cartes events

    [SerializeField] private int _numberOfEventCard = 0;
    public int NumberOfEventCard => _numberOfEventCard;

    //Liste des cartes events
    [SerializeField] private List<EventCard> _eventCardList = new List<EventCard>();
    public List<EventCard> EventCardList => _eventCardList;

    [SerializeField] private float _spaceBetweenTwoEvents = 0f;
    [SerializeField] private float multiplier = 0f;
    [SerializeField] private Vector2 _baseResolution = new Vector2(1920, 1080);

    int _redPlayerPos = 0;
    int _bluePlayerPos = 0;

    private void OnValidate(){
        int number = 0;
        foreach(EventCard card in _eventCardList){
            if(card._isEventInFinalGame){
                number++;
            }
        }

        _numberOfEventCard = number;
    }

    #region RemoveEvent
    /// <summary>
    /// Eneleve la carte event d'un joueur
    /// </summary>
    /// <param name="ev"></param>
    void RemoveEvents(MYthsAndSteel_Enum.EventCard ev){
        if(PlayerScript.Instance.EventCardList._eventCardJ1.Contains(ev)){
            PlayerScript.Instance.EventCardList._eventCardJ1.Remove(ev);
            
            foreach(GameObject gam in PlayerScript.Instance.EventCardList._eventGamJ1){
                if(gam.GetComponent<EventCardContainer>().EventCardInfo._eventType == ev){
                    RemoveEventGam(gam, 1);
                    break;
                }
            }
        }
        else{
            PlayerScript.Instance.EventCardList._eventCardBluePlayer.Remove(ev);

            foreach(GameObject gam in PlayerScript.Instance.EventCardList._eventGamJ2){
                if(gam.GetComponent<EventCardContainer>().EventCardInfo._eventType == ev){
                    RemoveEventGam(gam, 2);
                    break;
                }
            }
        }
    }

    /// <summary>
    /// d�truit une carte event
    /// </summary>
    /// <param name="gam"></param>
    void RemoveEventGam(GameObject gam, int player){
        if(player == 1){
            PlayerScript.Instance.EventCardList._eventGamJ1.Remove(gam);
            Destroy(gam);
           GameManager.Instance.victoryScreen.redEventUsed += 1;
        }
        else if(player == 2){
            PlayerScript.Instance.EventCardList._eventGamJ2.Remove(gam);
            Destroy(gam);
          GameManager.Instance.victoryScreen.blueEventUsed += 1;
        }
        else{
            Debug.LogError("Vous essayez d'enlever une carte event a un joueur qui n'existe pas");
        }

        UpdateVisualUI(PlayerScript.Instance.EventCardList._eventGamJ1, 1);
        UpdateVisualUI(PlayerScript.Instance.EventCardList._eventGamJ2, 2);
    }
    #endregion RemoveEvent

    #region UpdateEventUI
    /// <summary>
    /// Met a jour la position des cartes events dans l'interface
    /// </summary>
    /// <param name="gam"></param>
    public void UpdateVisualUI(List<GameObject> gam, int player){
        if(player == 1){
            if(PlayerScript.Instance.EventCardList._eventCardJ1.Count <= 3){
                ResetEventParentPos(1);

                UpdateEventList(gam, player);
                UpdateButtonPlayer(UIInstance.Instance.ButtonEventRedPlayer._upButton, UIInstance.Instance.ButtonEventRedPlayer._downButton, false, PlayerScript.Instance.EventCardList._eventGamJ1.Count, _redPlayerPos, player);
            }
            else{
                UpdateEventList(gam, player);
                UpdateButtonPlayer(UIInstance.Instance.ButtonEventRedPlayer._upButton, UIInstance.Instance.ButtonEventRedPlayer._downButton, true, PlayerScript.Instance.EventCardList._eventGamJ1.Count, _redPlayerPos, player);
            }
        }
        else if(player == 2)
        {
            if(PlayerScript.Instance.EventCardList._eventCardBluePlayer.Count <= 3){
                ResetEventParentPos(2);

                UpdateEventList(gam, player);
                UpdateButtonPlayer(UIInstance.Instance.ButtonEventBluePlayer._upButton, UIInstance.Instance.ButtonEventBluePlayer._downButton, false, PlayerScript.Instance.EventCardList._eventGamJ2.Count, _bluePlayerPos, player);
            }
            else{
                UpdateEventList(gam, player);
                UpdateButtonPlayer(UIInstance.Instance.ButtonEventBluePlayer._upButton, UIInstance.Instance.ButtonEventBluePlayer._downButton, true, PlayerScript.Instance.EventCardList._eventGamJ2.Count, _bluePlayerPos, player);
            }
        }
        else{
            Debug.LogError("Vous essayez d'd'update l'ui d'un joueur qui n'existe pas");
        }
    }
    
    /// <summary>
    /// Update la position des events dans la liste
    /// </summary>
    /// <param name="gam"></param>
    /// <param name="player"></param>
    void UpdateEventList(List<GameObject> gam, int player)
    {
        if(player == 1){
            if(_redPlayerPos == 0){
                //D�place les events � leurs bonnes positions
                gam[0].transform.position = UIInstance.Instance.RedEventDowntrans.position;

                if(gam.Count > 1){
                    for(int i = 1; i < gam.Count; i++){
                        gam[i].transform.position = new Vector3(gam[i - 1].transform.position.x,
                                                                gam[i - 1].transform.position.y - _spaceBetweenTwoEvents * (Screen.height / _baseResolution.y),
                                                                gam[i - 1].transform.position.z);
                    }
                }
            }
            else{
                if(gam.Count > 1){
                    for(int i = 1; i < gam.Count; i++){
                        gam[i].transform.position = new Vector3(gam[i - 1].transform.position.x,
                                                                gam[i - 1].transform.position.y - _spaceBetweenTwoEvents * (Screen.height / _baseResolution.y),
                                                                gam[i - 1].transform.position.z);
                    }
                }
            }
        }

        else if(player == 2){
            if(_bluePlayerPos == 0){
                //D�place les events � leurs bonnes positions
                gam[0].transform.position = UIInstance.Instance.BlueEventDowntrans.position;

                if(gam.Count > 1){
                    for(int i = 1; i < gam.Count; i++){ 
                        gam[i].transform.position = new Vector3(gam[i - 1].transform.position.x,
                                                                gam[i - 1].transform.position.y - _spaceBetweenTwoEvents * (Screen.height / _baseResolution.y),
                                                                gam[i - 1].transform.position.z);
                    }
                }
            }
            else{
                if(gam.Count > 1){
                    for(int i = 1; i < gam.Count; i++){
                        gam[i].transform.position = new Vector3(gam[i - 1].transform.position.x,
                                                                gam[i - 1].transform.position.y - _spaceBetweenTwoEvents * (Screen.height / _baseResolution.y),
                                                                gam[i - 1].transform.position.z);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Met a jour le visuel des boutons et leur interaction
    /// </summary>
    /// <param name="upButton"></param>
    /// <param name="downButton"></param>
    /// <param name="active"></param>
    void UpdateButtonPlayer(GameObject upButton, GameObject downButton, bool active, int numberOfCard, int pos, int player){
        downButton.GetComponent<Image>().sprite = active ? (pos == numberOfCard - 3?  UIInstance.Instance.FlecheSpriteRef._grisArrowDown : (player == 1 ? UIInstance.Instance.FlecheSpriteRef._redArrowDown : UIInstance.Instance.FlecheSpriteRef._blueArrowDown)) : UIInstance.Instance.FlecheSpriteRef._grisArrowDown;
        downButton.GetComponent<Button>().interactable = pos == numberOfCard - 3 ? false : active;
        upButton.GetComponent<Image>().sprite = active ? (pos == 0 ? UIInstance.Instance.FlecheSpriteRef._grisArrowUp : (player == 1 ? UIInstance.Instance.FlecheSpriteRef._redArrowUp : UIInstance.Instance.FlecheSpriteRef._blueArrowUp)) : UIInstance.Instance.FlecheSpriteRef._grisArrowUp;
        upButton.GetComponent<Button>().interactable = pos == 0 ? false : active;
    }

    /// <summary>
    /// Update la position du parent des cartes events d'un joueur
    /// </summary>
    void UpdateEventsParentPos(int player)
    {
        if(player == 1)
        {
            UIInstance.Instance.RedPlayerEventtransf.GetChild(0).localPosition = new Vector3(UIInstance.Instance.RedPlayerEventtransf.GetChild(0).localPosition.x, +_spaceBetweenTwoEvents * _redPlayerPos / multiplier, UIInstance.Instance.RedPlayerEventtransf.GetChild(0).localPosition.z);
        }
        else if(player == 2)
        {
            UIInstance.Instance.BluePlayerEventtransf.GetChild(0).localPosition = new Vector3(UIInstance.Instance.BluePlayerEventtransf.GetChild(0).localPosition.x, +_spaceBetweenTwoEvents * _bluePlayerPos / multiplier, UIInstance.Instance.BluePlayerEventtransf.GetChild(0).localPosition.z);
        }
        else
        {
            Debug.LogError("vous essayez de d�placer les cartes events d'un joueur qui n'existe pas");
        }
    }

    /// <summary>
    /// Reset la position du parent des cartes events d'un joueur
    /// </summary>
    /// <param name="player"></param>
 public   void ResetEventParentPos(int player)
    {
        if(player == 1)
        {
            _redPlayerPos = 0;
            UIInstance.Instance.RedPlayerEventtransf.GetChild(0).localPosition = Vector3.zero;
        }
        else if(player == 2)
        {
            _bluePlayerPos = 0;
            UIInstance.Instance.BluePlayerEventtransf.GetChild(0).localPosition = Vector3.zero;
        }
        else
        {
            Debug.LogError("vous essayez de d�placer les cartes events d'un joueur qui n'existe pas");
        }
    }
    #endregion UpdateEventUI

    #region ButtonInput
    /// <summary>
    /// Quand le joueur appuie sur le bouton pour monter dans la liste des cartes events
    /// </summary>
    /// <param name="player"></param>
    public void GoUp(int player){
        if(player == 1){
            _redPlayerPos--;
            UpdateEventsParentPos(1);
            UpdateButtonPlayer(UIInstance.Instance.ButtonEventRedPlayer._upButton, UIInstance.Instance.ButtonEventRedPlayer._downButton, true, PlayerScript.Instance.EventCardList._eventGamJ1.Count, _redPlayerPos, player);
        }
        else if(player == 2){
            _bluePlayerPos--;
            UpdateEventsParentPos(2);
            UpdateButtonPlayer(UIInstance.Instance.ButtonEventBluePlayer._upButton, UIInstance.Instance.ButtonEventBluePlayer._downButton, true, PlayerScript.Instance.EventCardList._eventGamJ2.Count, _bluePlayerPos, player);
        }
        else{
            Debug.LogError("vous essayez de d�placer les cartes events d'un joueur qui n'existe pas");
        }
    }

    /// <summary>
    /// Quand le joueur appuie pour descendre dans la liste des cartes events
    /// </summary>
    /// <param name="player"></param>
    public void GoDown(int player){
        if(player == 1){
            _redPlayerPos++;
            UpdateEventsParentPos(1);
            UpdateButtonPlayer(UIInstance.Instance.ButtonEventRedPlayer._upButton, UIInstance.Instance.ButtonEventRedPlayer._downButton, true, PlayerScript.Instance.EventCardList._eventGamJ1.Count, _redPlayerPos, player);
        }
        else if(player == 2){
            _bluePlayerPos++;
            UpdateEventsParentPos(2);
            UpdateButtonPlayer(UIInstance.Instance.ButtonEventBluePlayer._upButton, UIInstance.Instance.ButtonEventBluePlayer._downButton, true, PlayerScript.Instance.EventCardList._eventGamJ2.Count, _bluePlayerPos, player);
        }
        else{
            Debug.LogError("vous essayez de d�placer les cartes events d'un joueur qui n'existe pas");
        }
    }
    #endregion ButtonInput

    #region Evenement

    //A finir d�s qu'on peut ajouter une unit�
    #region D�ploiement Acc�l�r�
    public void D�ploiementAcc�l�r�()
    {
        UIInstance.Instance.ActivateNextPhaseButton();
        OrgoneManager.Instance.DoingOrgoneCharge = true;
        RenfortPhase.Instance.idCreate = 0;
        int player = DeterminArmy(MYthsAndSteel_Enum.EventCard.D�ploiement_acc�l�r�);
           if (player == 1) RenfortPhase.Instance.redPlayerCreation = true; 
           else if (player == 2) RenfortPhase.Instance.redPlayerCreation = false;
        RenfortPhase.Instance.CreateNewUnit();
        foreach (GameObject tile in GameManager.Instance.TileChooseList){
            Debug.Log("Le jeu ajoute une infante");
        }

        GameManager.Instance.TileChooseList.Clear();
        RemovePlayerRessource(MYthsAndSteel_Enum.EventCard.D�ploiement_acc�l�r�);
        RemoveEvents(MYthsAndSteel_Enum.EventCard.D�ploiement_acc�l�r�);
        EventCardUse();
    }

    public void LaunchD�ploiementAcc�l�r�(){

        UIInstance.Instance.ActivateNextPhaseButton();

        int player = DeterminArmy(MYthsAndSteel_Enum.EventCard.D�ploiement_acc�l�r�);
        List<GameObject> gamList = new List<GameObject>();
        
        //obtien les cases voisines pour chaque unit� de l'arm�e
        foreach(GameObject unit in player == 1? PlayerScript.Instance.UnitRef.UnitListRedPlayer : PlayerScript.Instance.UnitRef.UnitListBluePlayer){
            List<int> neighTile = PlayerStatic.GetNeighbourDiag(unit.GetComponent<UnitScript>().ActualTiledId, TilesManager.Instance.TileList[unit.GetComponent<UnitScript>().ActualTiledId].GetComponent<TileScript>().Line, false);

            //Check les effets de terrain pour voir si il doit ajouter la tile � la liste
            foreach (int i in neighTile)
            {
                //Obtient la direction de la case par rapport � l'unit�
                MYthsAndSteel_Enum.Direction dir = PlayerStatic.CheckDirection(unit.GetComponent<UnitScript>().ActualTiledId, i);
                if (PlayerScript.Instance.J1Infos.Ressource >= 1 && player == 1 && PlayerScript.Instance.J1Infos.EventUseLeft > 0 || PlayerScript.Instance.J2Infos.Ressource >= 1 && player == 2 && PlayerScript.Instance.J2Infos.EventUseLeft > 0)
                {


                    if (TilesManager.Instance.TileList[i].GetComponent<TileScript>().TerrainEffectList.Contains(MYthsAndSteel_Enum.TerrainType.Ravin) || TilesManager.Instance.TileList[i].GetComponent<TileScript>().TerrainEffectList.Contains(MYthsAndSteel_Enum.TerrainType.Eau))
                    {
                        //La tile n'est pas ajout�e
                    }
                    else
                    {
                        switch (dir)
                        {
                            case MYthsAndSteel_Enum.Direction.Nord:
                                if (!TilesManager.Instance.TileList[i].GetComponent<TileScript>().TerrainEffectList.Contains(MYthsAndSteel_Enum.TerrainType.Rivi�re_Sud) && TilesManager.Instance.TileList[i].GetComponent<TileScript>().Unit ==null)
                                {
                                    gamList.Add(TilesManager.Instance.TileList[i]);
                                }
                                break;
                            case MYthsAndSteel_Enum.Direction.Sud:
                                if (!TilesManager.Instance.TileList[i].GetComponent<TileScript>().TerrainEffectList.Contains(MYthsAndSteel_Enum.TerrainType.Rivi�re_Nord)&& TilesManager.Instance.TileList[i].GetComponent<TileScript>().Unit == null)
                                {
                                    gamList.Add(TilesManager.Instance.TileList[i]);
                                }
                                break;
                            case MYthsAndSteel_Enum.Direction.Est:
                                if (!TilesManager.Instance.TileList[i].GetComponent<TileScript>().TerrainEffectList.Contains(MYthsAndSteel_Enum.TerrainType.Rivi�re_Ouest) && TilesManager.Instance.TileList[i].GetComponent<TileScript>().Unit == null)
                                {
                                    gamList.Add(TilesManager.Instance.TileList[i]);
                                }
                                break;
                            case MYthsAndSteel_Enum.Direction.Ouest:
                                if (!TilesManager.Instance.TileList[i].GetComponent<TileScript>().TerrainEffectList.Contains(MYthsAndSteel_Enum.TerrainType.Rivi�re_Est)&& TilesManager.Instance.TileList[i].GetComponent<TileScript>().Unit == null)
                                {
                                    gamList.Add(TilesManager.Instance.TileList[i]);
                                }
                                break;
                        }
                    }
                    
                }
             
            }
        }

        if((GameManager.Instance.ActualTurnPhase == MYthsAndSteel_Enum.PhaseDeJeu.ActionJ1 || GameManager.Instance.ActualTurnPhase == MYthsAndSteel_Enum.PhaseDeJeu.ActionJ2) && 
            ((player == 1 && GameManager.Instance.IsJ1Turn && PlayerScript.Instance.J1Infos.EventUseLeft > 0) && (PlayerScript.Instance.J1Infos.Ressource >= 1)|| (player == 2 && !GameManager.Instance.IsJ1Turn && PlayerScript.Instance.J2Infos.Ressource >= 1&& PlayerScript.Instance.J2Infos.EventUseLeft > 0))){

        
            LaunchEventTile(1, player == 1 ? true : false, gamList, "D�ploiement acc�l�r�", "�tes-vous sur de vouloir cr�er une unit� d'infanterie sur cette case?", false);
            GameManager.Instance._eventCall += D�ploiementAcc�l�r�;
        }
        else
        {
            SoundController.Instance.PlaySound(SoundController.Instance.AudioClips[14]);
          
        }
    }
    #endregion Reprogrammation

    #region IllusionStrat�gique
    public void IllusionStrat�gique()
    {
        UIInstance.Instance.ActivateNextPhaseButton();

        int firstTileId = GameManager.Instance.UnitChooseList[0].GetComponent<UnitScript>().ActualTiledId;

        TilesManager.Instance.TileList[GameManager.Instance.UnitChooseList[1].GetComponent<UnitScript>().ActualTiledId].GetComponent<TileScript>().AddUnitToTile(GameManager.Instance.UnitChooseList[0].gameObject);
        TilesManager.Instance.TileList[firstTileId].GetComponent<TileScript>().AddUnitToTile(GameManager.Instance.UnitChooseList[1].gameObject);

        GameManager.Instance.UnitChooseList.Clear();
        GameManager.Instance.IllusionStrat�gique = false;

        //Remove la carte event chez le bon joueur
        RemoveEvents(MYthsAndSteel_Enum.EventCard.Illusion_strat�gique);
        EventCardUse();
    }

    public void LaunchIllusionStrat�gique()
    {
        int player = DeterminArmy(MYthsAndSteel_Enum.EventCard.Illusion_strat�gique);
        List<GameObject> unitList = new List<GameObject>();

        if(player == 1){
            foreach(GameObject gam in PlayerScript.Instance.UnitRef.UnitListRedPlayer)
            {
                if(gam.GetComponent<UnitScript>().IsActivationDone == true)
                {
                    unitList.Add(gam);
                }
            }

            unitList.AddRange(PlayerScript.Instance.UnitRef.UnitListBluePlayer);
        }
        else{
            foreach(GameObject gam in PlayerScript.Instance.UnitRef.UnitListBluePlayer)
            {
                if(gam.GetComponent<UnitScript>().IsActivationDone == true)
                {
                    unitList.Add(gam);
                }
            }

            unitList.AddRange(PlayerScript.Instance.UnitRef.UnitListRedPlayer);
        }


        GameManager.Instance.IllusionStrat�gique = true;

        if((GameManager.Instance.ActualTurnPhase == MYthsAndSteel_Enum.PhaseDeJeu.ActionJ1 || GameManager.Instance.ActualTurnPhase == MYthsAndSteel_Enum.PhaseDeJeu.ActionJ2) &&
            ((player == 1 && GameManager.Instance.IsJ1Turn && PlayerScript.Instance.J1Infos.EventUseLeft > 0) || (player == 2 && !GameManager.Instance.IsJ1Turn && PlayerScript.Instance.J2Infos.EventUseLeft > 0)))
        {
            LaunchEventUnit(2, player == 1 ? true : false, unitList, "Illusion Strat�gique", "�tes-vous sur de vouloir �changer la position de ces deux unit�s sur le plateau?");
            GameManager.Instance._eventCall += IllusionStrat�gique;
        }
        else
        {
            SoundController.Instance.PlaySound(SoundController.Instance.AudioClips[14]);
        }
        unitList.Clear();
      
    }
    #endregion IllusionStrat�gique

    #region OptimisationOrgone
    public void OptimisationOrgone()
    {
        UIInstance.Instance.ActivateNextPhaseButton();

        int player = DeterminArmy(MYthsAndSteel_Enum.EventCard.Optimisation_de_l_orgone);
        if(player == 1){
            PlayerScript.Instance.J1Infos.OrgonePowerLeft++;
        }
        else{
            PlayerScript.Instance.J2Infos.OrgonePowerLeft++;
        }

        //Remove la carte event chez le bon joueur
        RemoveEvents(MYthsAndSteel_Enum.EventCard.Optimisation_de_l_orgone);
        EventCardUse();
    }

    public void LaunchOptimisationOrgone()
    {
        int player = DeterminArmy(MYthsAndSteel_Enum.EventCard.Optimisation_de_l_orgone);

        if((GameManager.Instance.ActualTurnPhase == MYthsAndSteel_Enum.PhaseDeJeu.OrgoneJ1 || GameManager.Instance.ActualTurnPhase == MYthsAndSteel_Enum.PhaseDeJeu.OrgoneJ2) &&
            ((player == 1 && GameManager.Instance.IsJ1Turn && PlayerScript.Instance.J1Infos.EventUseLeft > 0) || (player == 2 && !GameManager.Instance.IsJ1Turn && PlayerScript.Instance.J2Infos.EventUseLeft > 0)))
        {
                GameManager.Instance._eventCall += OptimisationOrgone;
            if (PlayerPrefs.GetInt("Avertissement") == 0)
            {

                GameManager.Instance._eventCall();
            }

            UIInstance.Instance.ShowValidationPanel("Optimisation de l'orgone", "�tes-vous sur de vouloir augmenter votre nombre d'utilisation de charge d'orgones de ");
        }
        else
        {
            SoundController.Instance.PlaySound(SoundController.Instance.AudioClips[14]);
        }
    }
    #endregion OptimisationOrgone

    #region PillageOrgone
    public void PillageOrgone()
    {
        UIInstance.Instance.ActivateNextPhaseButton();

        int player = DeterminArmy(MYthsAndSteel_Enum.EventCard.Pillage_orgone);

        foreach(GameObject gam in GameManager.Instance.TileChooseList){
            gam.GetComponent<TileScript>().RemoveRessources(1, player);
        }

        GameManager.Instance.TileChooseList.Clear();

        //Remove la carte event chez le bon joueur
        RemoveEvents(MYthsAndSteel_Enum.EventCard.Pillage_orgone);
        EventCardUse();
    }

    public void LaunchPillageOrgone()
    {
        int player = DeterminArmy(MYthsAndSteel_Enum.EventCard.Pillage_orgone);
        
        List<GameObject> tileList = new List<GameObject>();
        tileList.AddRange(TilesManager.Instance.ResourcesList);

        if((GameManager.Instance.ActualTurnPhase == MYthsAndSteel_Enum.PhaseDeJeu.ActionJ1 || GameManager.Instance.ActualTurnPhase == MYthsAndSteel_Enum.PhaseDeJeu.ActionJ2) &&
            ((player == 1 && GameManager.Instance.IsJ1Turn && PlayerScript.Instance.J1Infos.EventUseLeft > 0) || (player == 2 && !GameManager.Instance.IsJ1Turn && PlayerScript.Instance.J2Infos.EventUseLeft > 0)))
        {
            LaunchEventTile(2, player == 1 ? true : false, tileList, "Pillage d'orgone", "�tes-vous sur de vouloir voler deux Ressources sur ces cases?", true);
            GameManager.Instance._eventCall += PillageOrgone;
        }
        else
        {
            SoundController.Instance.PlaySound(SoundController.Instance.AudioClips[14]);
        }
        tileList.Clear();
    }

    #endregion PillageOrgone
    #region Fils Barbel�s
    public void Fils_Barbel�s()
    {
        UIInstance.Instance.ActivateNextPhaseButton();


foreach (GameObject element in TilesManager.Instance.TileList)
        {
         
            element.GetComponent<TileScript>().DesActiveChildObj(MYthsAndSteel_Enum.ChildTileType.EventSelect);

        }
    MYthsAndSteel_Enum.Direction  barbelposition =  PlayerStatic.CheckDirection(GameManager.Instance.TileChooseList[0].GetComponent<TileScript>().TileId, GameManager.Instance.TileChooseList[1].GetComponent<TileScript>().TileId);
        BarbelGestion.Instance.CreateBarbel(GameManager.Instance.TileChooseList[0].GetComponent<TileScript>().TileId, barbelposition);

        RemovePlayerRessource(MYthsAndSteel_Enum.EventCard.Fil_barbel�);
        //Remove la carte event chez le bon joueur
        GameManager.Instance.TileChooseList.Clear();
        GameManager.Instance.filBbarbel�s = false;
        barbelposition =MYthsAndSteel_Enum.Direction.None;
        RemoveEvents(MYthsAndSteel_Enum.EventCard.Fil_barbel�);
        EventCardUse();
    }

    public void LaunchFils_Barbel�s()
    {
        int player = DeterminArmy(MYthsAndSteel_Enum.EventCard.Fil_barbel�);

        List<GameObject> tileList = new List<GameObject>();
        tileList.AddRange(TilesManager.Instance.TileList);

        if ((GameManager.Instance.ActualTurnPhase == MYthsAndSteel_Enum.PhaseDeJeu.ActionJ1 || GameManager.Instance.ActualTurnPhase == MYthsAndSteel_Enum.PhaseDeJeu.ActionJ2) &&
            ((player == 1 && GameManager.Instance.IsJ1Turn && PlayerScript.Instance.J1Infos.EventUseLeft > 0) || (player == 2 && !GameManager.Instance.IsJ1Turn && PlayerScript.Instance.J2Infos.EventUseLeft > 0)))
        {
            if (PlayerScript.Instance.J1Infos.Ressource >= 1 && player == 1 || PlayerScript.Instance.J2Infos.Ressource >= 1 && player == 2)
            {
                LaunchEventTile(2, player == 1 ? true : false, tileList, "Fils Barbel�s", "�tes-vous sur de vouloir faire apparaitre un barbel� entre deux cases?", true);
                GameManager.Instance.filBbarbel�s = true;
                GameManager.Instance._eventCall += Fils_Barbel�s;
            }
            else
            {
                SoundController.Instance.PlaySound(SoundController.Instance.AudioClips[14]);
            }
        }
        else
        {
            SoundController.Instance.PlaySound(SoundController.Instance.AudioClips[14]);
        }

        tileList.Clear();
    }
    #endregion

    #region D�tonation d'Orgone
    public void D�tonation_d_Orgone()
    {
        int player = DeterminArmy(MYthsAndSteel_Enum.EventCard.D�tonation_d_orgone);
        UIInstance.Instance.ActivateNextPhaseButton();

        foreach  (GameObject element in GameManager.Instance.TileChooseList)
        {
         GameObject d�tonation = Instantiate(GameManager.Instance.d�tonationPrefab, element.transform);
            if (player == 1) d�tonation.GetComponent<D�tonation>()._IsInRedArmy = true;
            else if (player == 2) d�tonation.GetComponent<D�tonation>()._IsInRedArmy = false;

            d�tonation.GetComponent<D�tonation>().TileID = element.GetComponent<TileScript>().TileId;
            element.GetComponent<TileScript>().AddEffectToList(MYthsAndSteel_Enum.TerrainType.D�tonation);
            element.GetComponent<TileScript>()._Child.Add(d�tonation);
        }
      

       

        RemovePlayerRessource(MYthsAndSteel_Enum.EventCard.D�tonation_d_orgone);
        //Remove la carte event chez le bon joueur
        GameManager.Instance.TileChooseList.Clear();
   
      
        RemoveEvents(MYthsAndSteel_Enum.EventCard.D�tonation_d_orgone);
        EventCardUse();
    }

    public void LaunchD�tonation_d_Orgone()
    {
        int player = DeterminArmy(MYthsAndSteel_Enum.EventCard.D�tonation_d_orgone);

        List<GameObject> tileList = new List<GameObject>();
        tileList.AddRange(TilesManager.Instance.TileList);

        if ((GameManager.Instance.ActualTurnPhase == MYthsAndSteel_Enum.PhaseDeJeu.ActionJ1 || GameManager.Instance.ActualTurnPhase == MYthsAndSteel_Enum.PhaseDeJeu.ActionJ2) &&
            ((player == 1 && GameManager.Instance.IsJ1Turn && PlayerScript.Instance.J1Infos.EventUseLeft > 0) || (player == 2 && !GameManager.Instance.IsJ1Turn && PlayerScript.Instance.J2Infos.EventUseLeft > 0)))
        {
            if (PlayerScript.Instance.J1Infos.Ressource >= 1 && player == 1 || PlayerScript.Instance.J2Infos.Ressource >= 1 && player == 2)
            {
                LaunchEventTile(3, player == 1 ? true : false, tileList, "D�tonation d'orgone", "�tes-vous sur de vouloir faire faire apparaitre trois d�tonations d'orgone?", true);
             
                GameManager.Instance._eventCall += D�tonation_d_Orgone;
            }
            else
            {
                SoundController.Instance.PlaySound(SoundController.Instance.AudioClips[14]);
            }
        }
        else
        {
            SoundController.Instance.PlaySound(SoundController.Instance.AudioClips[14]);
        }

        tileList.Clear();
    }
    #endregion

    #region PointeursLaser
    public void PointeursLaserOptimis�s()
    {
        UIInstance.Instance.ActivateNextPhaseButton();

        foreach(GameObject unit in GameManager.Instance.UnitChooseList)
        {
            unit.GetComponent<UnitScript>().AttackRangeBonus += 1;
        }

        GameManager.Instance.UnitChooseList.Clear();

        //Remove la carte event chez le bon joueur
        RemoveEvents(MYthsAndSteel_Enum.EventCard.Pointeurs_laser_optimis�s);
        EventCardUse();
    }

    public void LaunchPointeursLaserOptimis�s()
    {
        int player = DeterminArmy(MYthsAndSteel_Enum.EventCard.Pointeurs_laser_optimis�s);

        List<GameObject> unitList = new List<GameObject>();

        unitList.AddRange(player == 2 ? PlayerScript.Instance.UnitRef.UnitListBluePlayer : PlayerScript.Instance.UnitRef.UnitListRedPlayer);

        if((GameManager.Instance.ActualTurnPhase == MYthsAndSteel_Enum.PhaseDeJeu.ActionJ1 || GameManager.Instance.ActualTurnPhase == MYthsAndSteel_Enum.PhaseDeJeu.ActionJ2) &&
            ((player == 1 && GameManager.Instance.IsJ1Turn && PlayerScript.Instance.J1Infos.EventUseLeft > 0) || (player == 2 && !GameManager.Instance.IsJ1Turn && PlayerScript.Instance.J2Infos.EventUseLeft > 0)))
        {
            LaunchEventUnit(2, player == 1 ? true : false, unitList, "Pointeurs Laser Optimis�", "�tes-vous sur de vouloir augmenter de 1 la port�e de ces 2 unit�s?");
            GameManager.Instance._eventCall += PointeursLaserOptimis�s;
        }
        else
        {
            SoundController.Instance.PlaySound(SoundController.Instance.AudioClips[14]);
        }

        unitList.Clear();
    }
    #endregion PointeursLaser

    #region ArmeEpidemiologique
    public void ArmeEpidemiologique()
    {
        UIInstance.Instance.ActivateNextPhaseButton();

        GameManager.Instance.armeEpidemelogiqueStat = DeterminArmy(MYthsAndSteel_Enum.EventCard.Arme_�pid�miologique);

        GameManager.Instance.UnitChooseList[0].GetComponent<UnitScript>().AddStatutToUnit(MYthsAndSteel_Enum.UnitStatut.ArmeEpidemiologique);

        GameManager.Instance.UnitChooseList.Clear();

        //Remove la carte event chez le bon joueur
        RemoveEvents(MYthsAndSteel_Enum.EventCard.Arme_�pid�miologique);
        EventCardUse
            ();
    }

    public void LaunchArmeEpidemiologique()
    {
        int player = DeterminArmy(MYthsAndSteel_Enum.EventCard.Arme_�pid�miologique);

        List<GameObject> unitList = new List<GameObject>();
        unitList.AddRange(player == 2? PlayerScript.Instance.UnitRef.UnitListRedPlayer : PlayerScript.Instance.UnitRef.UnitListBluePlayer);

        if((GameManager.Instance.ActualTurnPhase == MYthsAndSteel_Enum.PhaseDeJeu.ActionJ1 || GameManager.Instance.ActualTurnPhase == MYthsAndSteel_Enum.PhaseDeJeu.ActionJ2) &&
            ((player == 1 && GameManager.Instance.IsJ1Turn && PlayerScript.Instance.J1Infos.EventUseLeft > 0) || (player == 2 && !GameManager.Instance.IsJ1Turn && PlayerScript.Instance.J2Infos.EventUseLeft > 0)))
        {
            LaunchEventUnit(1, player == 1 ? true : false, unitList, "Arme �pid�miologique", "�tes-vous sur de vouloir Infliger l'effet � cette unit�? Cette unit� et toutes celles adjacentes unit� se verront perdre un point de vie � la fin du tour adverse.");
            GameManager.Instance._eventCall += ArmeEpidemiologique;
        }
        else
        {
            SoundController.Instance.PlaySound(SoundController.Instance.AudioClips[14]);
        }

        unitList.Clear();
    }
    #endregion ArmeEpidemiologique

    #region ManeouvreStrat�gique
    public void ManoeuvreStrat�gique()
    {
        UIInstance.Instance.ActivateNextPhaseButton();

        int player = DeterminArmy(MYthsAndSteel_Enum.EventCard.Manoeuvre_strat�gique);
        
        if(player == 1){
            PlayerScript.Instance.J1Infos.ActivationLeft++;
            UIInstance.Instance.UpdateActivationLeft();
        }
        else{
            PlayerScript.Instance.J2Infos.ActivationLeft++;
            UIInstance.Instance.UpdateActivationLeft();
        }

        //Remove la carte event chez le bon joueur
        RemoveEvents(MYthsAndSteel_Enum.EventCard.Manoeuvre_strat�gique);
        EventCardUse();
    }

    public void LaunchManoeuvreStrat�gique()
    {
        int player = DeterminArmy(MYthsAndSteel_Enum.EventCard.Manoeuvre_strat�gique);

        if((GameManager.Instance.ActualTurnPhase == MYthsAndSteel_Enum.PhaseDeJeu.ActionJ1 || GameManager.Instance.ActualTurnPhase == MYthsAndSteel_Enum.PhaseDeJeu.ActionJ2) &&
            ((player == 1 && GameManager.Instance.IsJ1Turn && PlayerScript.Instance.J1Infos.EventUseLeft > 0) || (player == 2 && !GameManager.Instance.IsJ1Turn && PlayerScript.Instance.J2Infos.EventUseLeft > 0)))
        {
            GameManager.Instance._eventCall += ManoeuvreStrat�gique;
            if(PlayerPrefs.GetInt("Avertissement") == 0)
            {
                GameManager.Instance._eventCall();
            }
            UIInstance.Instance.ShowValidationPanel("Manoeuvre strat�gique", "�tes-vous sur de vouloir activer une unit� suppl�mentaire durant ce tour?");
        }
        else
        {
            SoundController.Instance.PlaySound(SoundController.Instance.AudioClips[14]);
        }
    }
    #endregion ManeouvreStrat�gique

    #region SerumExp�rimental
    public void SerumExperimental()
    {
        UIInstance.Instance.ActivateNextPhaseButton();

        foreach(GameObject unit in GameManager.Instance.UnitChooseList)
        {
            unit.GetComponent<UnitScript>().MoveSpeedBonus++;
        }

        GameManager.Instance.UnitChooseList.Clear();

        //Remove la carte event chez le bon joueur
        RemoveEvents(MYthsAndSteel_Enum.EventCard.S�rum_exp�rimental);
        EventCardUse();
    }

    public void LaunchSerumExperimental()
    {
        int player = DeterminArmy(MYthsAndSteel_Enum.EventCard.S�rum_exp�rimental);

        List<GameObject> unitList = new List<GameObject>();

        unitList.AddRange(player == 2 ? PlayerScript.Instance.UnitRef.UnitListBluePlayer : PlayerScript.Instance.UnitRef.UnitListRedPlayer);

        if((GameManager.Instance.ActualTurnPhase == MYthsAndSteel_Enum.PhaseDeJeu.ActionJ1 || GameManager.Instance.ActualTurnPhase == MYthsAndSteel_Enum.PhaseDeJeu.ActionJ2) &&
            ((player == 1 && GameManager.Instance.IsJ1Turn && PlayerScript.Instance.J1Infos.EventUseLeft > 0) || (player == 2 && !GameManager.Instance.IsJ1Turn && PlayerScript.Instance.J2Infos.EventUseLeft > 0)))
        {
            LaunchEventUnit(2, player == 1 ? true : false, unitList, "S�rum Exp�rimental", "�tes-vous sur de vouloir augmenter d'1 point le d�placement de ces deux unit�s?");
            GameManager.Instance._eventCall += SerumExperimental;
        }
        else
        {
            SoundController.Instance.PlaySound(SoundController.Instance.AudioClips[14]);
        }

        unitList.Clear();
    }
    #endregion SerumExp�rimental

    #region ActivationDeNodus
    public void ActivationDeNodus()
    {
        UIInstance.Instance.ActivateNextPhaseButton();

        int player = DeterminArmy(MYthsAndSteel_Enum.EventCard.Activation_de_nodus);
        if(player == 1){
            PlayerScript.Instance.J1Infos.OrgonePowerLeft++;
        }
        else{
            PlayerScript.Instance.J2Infos.OrgonePowerLeft++;
        }

        //Remove la carte event chez le bon joueur
        RemoveEvents(MYthsAndSteel_Enum.EventCard.Activation_de_nodus);
        EventCardUse();
    }

    public void LaunchActivationDeNodus()
    {
        int player = DeterminArmy(MYthsAndSteel_Enum.EventCard.Activation_de_nodus);

        if((GameManager.Instance.ActualTurnPhase == MYthsAndSteel_Enum.PhaseDeJeu.ActionJ1 || GameManager.Instance.ActualTurnPhase == MYthsAndSteel_Enum.PhaseDeJeu.ActionJ2) &&
            ((player == 1 && GameManager.Instance.IsJ1Turn && PlayerScript.Instance.J1Infos.EventUseLeft > 0) || (player == 2 && !GameManager.Instance.IsJ1Turn && PlayerScript.Instance.J2Infos.EventUseLeft > 0)))
        {
            GameManager.Instance._eventCall += ActivationDeNodus;
            if (PlayerPrefs.GetInt("Avertissement") == 0)
            {
                GameManager.Instance._eventCall();
            }
            UIInstance.Instance.ShowValidationPanel("Activation de Nodus", "�tes-vous sur de vouloir utiliser un pouvoir orgonique durant votre phase d'action?");
        }
        else
        {
            SoundController.Instance.PlaySound(SoundController.Instance.AudioClips[14]);
        }
    }
    #endregion ActivationDeNodus

    //Check si l'unit� peut aller sur la case sinon faire un d�g�t � l'unit�
    #region BombardementAerien
    public void BombardementAerien()
    {
        LaunchDeplacementBombardement(GameManager.Instance.UnitChooseList[0]);
        GameManager.Instance._eventCall -= BombardementAerien;
        GameManager.Instance._eventCall += MakeDamageBombardement;
    }

    public void MakeDamageBombardement(){
        GameManager.Instance.UnitChooseList[0].GetComponent<UnitScript>().TakeDamage(1);
        GameManager.Instance._eventCall -= MakeDamageBombardement;

        UIInstance.Instance.ActivateNextPhaseButton();
    }

    public void MoveUnitBombardement(){
        GameManager.Instance._eventCall = null;

        while(GameManager.Instance.UnitChooseList[0].transform.position != GameManager.Instance.TileChooseList[0].transform.position){
            GameManager.Instance.UnitChooseList[0].transform.position = Vector3.MoveTowards(GameManager.Instance.UnitChooseList[0].transform.position, GameManager.Instance.TileChooseList[0].transform.position, .7f);
            GameManager.Instance._waitEvent -= MoveUnitBombardement;
            GameManager.Instance._waitEvent += MoveUnitBombardement;
            GameManager.Instance.WaitToMove(.025f);
            return;
        }

        GameManager.Instance._waitEvent -= MoveUnitBombardement;
        TilesManager.Instance.TileList[GameManager.Instance.UnitChooseList[0].GetComponent<UnitScript>().ActualTiledId].GetComponent<TileScript>().RemoveUnitFromTile();
        GameManager.Instance.TileChooseList[0].GetComponent<TileScript>().AddUnitToTile(GameManager.Instance.UnitChooseList[0]);

        GameManager.Instance.TileChooseList.Clear();
        GameManager.Instance.UnitChooseList.Clear();

        RemoveEvents(MYthsAndSteel_Enum.EventCard.Bombardement_a�rien);
        EventCardUse();
    }

    public void LaunchBombardementAerien()
    {
        int player = DeterminArmy(MYthsAndSteel_Enum.EventCard.Bombardement_a�rien);
        List<GameObject> unitList = new List<GameObject>();

        unitList.AddRange(player == 1 ? PlayerScript.Instance.UnitRef.UnitListBluePlayer : PlayerScript.Instance.UnitRef.UnitListRedPlayer);

        if((GameManager.Instance.ActualTurnPhase == MYthsAndSteel_Enum.PhaseDeJeu.ActionJ1 || GameManager.Instance.ActualTurnPhase == MYthsAndSteel_Enum.PhaseDeJeu.ActionJ2) &&
            ((player == 1 && GameManager.Instance.IsJ1Turn && PlayerScript.Instance.J1Infos.EventUseLeft > 0) || (player == 2 && !GameManager.Instance.IsJ1Turn && PlayerScript.Instance.J2Infos.EventUseLeft > 0)))
        {
            LaunchEventUnit(1, player == 1 ? true : false, unitList, "Bombardement A�rien", "�tes-vous sur de vouloir infliger des d�g�ts � cette unit�?");
            GameManager.Instance._eventCall += BombardementAerien;
        }
        else
        {
            SoundController.Instance.PlaySound(SoundController.Instance.AudioClips[14]);
        }

        unitList.Clear();
    }

    void LaunchDeplacementBombardement(GameObject unit){

        int player = DeterminArmy(MYthsAndSteel_Enum.EventCard.Bombardement_a�rien);
        List<GameObject> tileList = new List<GameObject>();

        List<int> unitNeigh = PlayerStatic.GetNeighbourDiag(unit.GetComponent<UnitScript>().ActualTiledId, TilesManager.Instance.TileList[unit.GetComponent<UnitScript>().ActualTiledId].GetComponent<TileScript>().Line, false);
        foreach(int i in unitNeigh){
            if (TilesManager.Instance.TileList[i].GetComponent<TileScript>().Unit == null)
            {

                tileList.Add(TilesManager.Instance.TileList[i]);
            }

        }

        LaunchEventTile(1, player == 1 ? true : false, tileList, "Bombardement A�rien", "�tes-vous sur de vouloir d�placer l'unit� attaqu�e sur cette case?", false) ;
        GameManager.Instance._eventCall += MoveUnitBombardement;

        tileList.Clear();
    }
    #endregion Reprogrammation

    #region Reprogrammation
    public void Reproggramation(){
        UIInstance.Instance.ActivateNextPhaseButton();

        foreach(GameObject unit in GameManager.Instance.UnitChooseList){
            unit.GetComponent<UnitScript>().AddStatutToUnit(MYthsAndSteel_Enum.UnitStatut.Poss�d�);
            unit.GetComponent<UnitScript>().AddDiceToUnit(-4);
        }
        GameManager.Instance.possesion = true;
        GameManager.Instance.UnitChooseList.Clear();
        RemovePlayerRessource(MYthsAndSteel_Enum.EventCard.Reprogrammation);
        //Remove la carte event chez le bon joueur
        RemoveEvents(MYthsAndSteel_Enum.EventCard.Reprogrammation);
        EventCardUse();
    }

    public void LaunchReproggramation(){
        int player = DeterminArmy(MYthsAndSteel_Enum.EventCard.Reprogrammation);
        List<GameObject> unitList = new List<GameObject>();

       
        foreach(GameObject T in player == 1 ? PlayerScript.Instance.UnitRef.UnitListBluePlayer : PlayerScript.Instance.UnitRef.UnitListRedPlayer)
        {
            if(!T.GetComponent<UnitScript>().UnitStatuts.Contains(MYthsAndSteel_Enum.UnitStatut.Poss�d�))
            {
                unitList.Add(T);
            }
        }
        if ((GameManager.Instance.ActualTurnPhase == MYthsAndSteel_Enum.PhaseDeJeu.ActionJ1 || GameManager.Instance.ActualTurnPhase == MYthsAndSteel_Enum.PhaseDeJeu.ActionJ2) &&
            ((player == 1 && GameManager.Instance.IsJ1Turn && PlayerScript.Instance.J1Infos.EventUseLeft > 0) || (player == 2 && !GameManager.Instance.IsJ1Turn && PlayerScript.Instance.J2Infos.EventUseLeft > 0)))
        {
            if(PlayerScript.Instance.J1Infos.Ressource >= 1 && player == 1 || PlayerScript.Instance.J2Infos.Ressource >= 1 && player == 2)
            {

            GameManager.Instance._eventCall += Reproggramation;
            LaunchEventUnit(1, player == 1? true : false, unitList, "Reproggramation", "�tes-vous sur de vouloir activer cette unit� adverse durant ce tour?");
            }
            else
            {
                SoundController.Instance.PlaySound(SoundController.Instance.AudioClips[14]);
            }
        }
        else
        {
            SoundController.Instance.PlaySound(SoundController.Instance.AudioClips[14]);
        }

        unitList.Clear();
    }
    #endregion Reprogrammation

    #region CessezLeFeu
    public void CessezLeFeu()
    {
        UIInstance.Instance.ActivateNextPhaseButton();

        foreach(GameObject unit in GameManager.Instance.UnitChooseList)
        {
            unit.GetComponent<UnitScript>().AddStatutToUnit(MYthsAndSteel_Enum.UnitStatut.PeutPasCombattre);
            unit.GetComponent<UnitScript>().AddStatutToUnit(MYthsAndSteel_Enum.UnitStatut.Invincible);
            unit.GetComponent<UnitScript>().AddStatutToUnit(MYthsAndSteel_Enum.UnitStatut.PeutPasPrendreDesObjectifs);
        }

        GameManager.Instance.UnitChooseList.Clear();
        RemovePlayerRessource(MYthsAndSteel_Enum.EventCard.Cessez_le_feu);
        //Remove la carte event chez le bon joueur
        RemoveEvents(MYthsAndSteel_Enum.EventCard.Cessez_le_feu);
        EventCardUse();
    }

    public void LaunchCessezLeFeu()
    {
        int player = DeterminArmy(MYthsAndSteel_Enum.EventCard.Cessez_le_feu);
        List<GameObject> unitList = new List<GameObject>();

        unitList.AddRange(PlayerScript.Instance.UnitRef.UnitListBluePlayer);
        unitList.AddRange(PlayerScript.Instance.UnitRef.UnitListRedPlayer);

        if((GameManager.Instance.ActualTurnPhase == MYthsAndSteel_Enum.PhaseDeJeu.ActionJ1 || GameManager.Instance.ActualTurnPhase == MYthsAndSteel_Enum.PhaseDeJeu.ActionJ2) &&
            ((player == 1 && GameManager.Instance.IsJ1Turn && PlayerScript.Instance.J1Infos.EventUseLeft > 0) || (player == 2 && !GameManager.Instance.IsJ1Turn && PlayerScript.Instance.J2Infos.EventUseLeft > 0)))

        {
            if (PlayerScript.Instance.J1Infos.Ressource >= 1 && player == 1 || PlayerScript.Instance.J2Infos.Ressource >= 1 && player == 2)
            {

                LaunchEventUnit(1, player == 1 ? true : false, unitList, "Cessez le feu!", "�tes-vous sur de vouloir emp�cher cette unit� de prendre des d�g�ts, capturer un objectif et d'attaquer durant ce tour?");
            GameManager.Instance._eventCall += CessezLeFeu;
            }
            else
            {
                SoundController.Instance.PlaySound(SoundController.Instance.AudioClips[14]);
            }
        }
        else
        {
            SoundController.Instance.PlaySound(SoundController.Instance.AudioClips[14]);
        }

        unitList.Clear();
    }

    #endregion Reprogrammation

    #region Vol de Ravitaillement

    public void VolDeRavitaillement()
    {
     
        UIInstance.Instance.ActivateNextPhaseButton();
        if(GameManager.Instance.IsJ1Turn)
        {

        GameManager.Instance.VolDeRavitaillementStat = 1;
        }
        else
        {
            GameManager.Instance.VolDeRavitaillementStat = 2;
        }
      
        //Remove la carte event chez le bon joueur
        RemoveEvents(MYthsAndSteel_Enum.EventCard.Vol_de_ravitaillement);
        EventCardUse();

    }
        
   public void LaunchVolDeRavitaillement()
    {
        int player = DeterminArmy(MYthsAndSteel_Enum.EventCard.Vol_de_ravitaillement);

       if ((GameManager.Instance.ActualTurnPhase == MYthsAndSteel_Enum.PhaseDeJeu.ActionJ1 || GameManager.Instance.ActualTurnPhase == MYthsAndSteel_Enum.PhaseDeJeu.ActionJ2) &&
            ((player == 1 && GameManager.Instance.IsJ1Turn && PlayerScript.Instance.J1Infos.EventUseLeft > 0) || (player == 2 && !GameManager.Instance.IsJ1Turn && PlayerScript.Instance.J2Infos.EventUseLeft > 0)))
        {
         

         

            if (PlayerPrefs.GetInt("Avertissement") == 0)
            {
               GameManager.Instance._eventCall();

            }
            GameManager.Instance._eventCall += VolDeRavitaillement;
            UIInstance.Instance.ShowValidationPanel("Vol de Ravitaillement", "�tes-vous sur de vouloir de voler les ressources r�cup�r�es par votre adversaire pendant ce tour?");
        }
        else
        {
            SoundController.Instance.PlaySound(SoundController.Instance.AudioClips[14]);
        }
    }
    #endregion
    #region Sabotage
    public void Sabotage()
    {
        UIInstance.Instance.ActivateNextPhaseButton();
        if (GameManager.Instance.IsJ1Turn)
        {

            GameManager.Instance.SabotageStat = 1;
        }
        else
        {
            GameManager.Instance.SabotageStat = 2;
        }

        //Remove la carte event chez le bon joueur
        RemoveEvents(MYthsAndSteel_Enum.EventCard.Sabotage);
        EventCardUse();
    }
    public void LaunchSabotage()
    {
        int player = DeterminArmy(MYthsAndSteel_Enum.EventCard.Sabotage);
        if ((GameManager.Instance.ActualTurnPhase == MYthsAndSteel_Enum.PhaseDeJeu.ActionJ1 || GameManager.Instance.ActualTurnPhase == MYthsAndSteel_Enum.PhaseDeJeu.ActionJ2) &&
      ((player == 1 && GameManager.Instance.IsJ1Turn && PlayerScript.Instance.J1Infos.EventUseLeft > 0) || (player == 2 && !GameManager.Instance.IsJ1Turn && PlayerScript.Instance.J2Infos.EventUseLeft > 0)))
        {
            GameManager.Instance._eventCall += Sabotage;
            if (PlayerPrefs.GetInt("Avertissement") == 0)
            {
                GameManager.Instance._eventCall();

            }
            UIInstance.Instance.ShowValidationPanel("Sabotage", "�tes-vous sur de vouloir de r�duire la valeur d'activation de votre adversaire lors de sa prochaine phase d'action?");
        }
        else
        {
            SoundController.Instance.PlaySound(SoundController.Instance.AudioClips[14]);
        }
    }
    #endregion
    #region Paralysie
    public void Paralysie()
    {
        List<GameObject> unitList = new List<GameObject>();
        unitList.AddRange(PlayerScript.Instance.UnitRef.UnitListBluePlayer);
        unitList.AddRange(PlayerScript.Instance.UnitRef.UnitListRedPlayer);
        UIInstance.Instance.ActivateNextPhaseButton();
        MYthsAndSteel_Enum.TypeUnite typeUnitChoose = GameManager.Instance.UnitChooseList[0].GetComponent<UnitScript>().UnitSO.typeUnite;
        foreach (GameObject unit in unitList)
        {
            if(unit.GetComponent<UnitScript>().UnitSO.typeUnite == typeUnitChoose)
            {
                unit.GetComponent<UnitScript>().AddStatutToUnit(MYthsAndSteel_Enum.UnitStatut.Paralysie);
            }
        if (GameManager.Instance.IsJ1Turn)
        {

            unit.GetComponent<UnitScript>().ParalysieStat = 1;
        }
        else
        {
           unit.GetComponent<UnitScript>().ParalysieStat = 2;
        }
        }
        RemovePlayerRessource(MYthsAndSteel_Enum.EventCard.Paralysie);
        unitList.Clear();
        typeUnitChoose = MYthsAndSteel_Enum.TypeUnite.Autre;
        GameManager.Instance.UnitChooseList.Clear();
        //Remove la carte event chez le bon joueur
        RemoveEvents(MYthsAndSteel_Enum.EventCard.Paralysie);
        EventCardUse();
    }
    public void LaunchParalysie()
    {
        List<GameObject> unitList = new List<GameObject>();

        int player = DeterminArmy(MYthsAndSteel_Enum.EventCard.Paralysie);
        unitList.AddRange(PlayerScript.Instance.UnitRef.UnitListBluePlayer);
        unitList.AddRange(PlayerScript.Instance.UnitRef.UnitListRedPlayer);
        if ((GameManager.Instance.ActualTurnPhase == MYthsAndSteel_Enum.PhaseDeJeu.OrgoneJ1 || GameManager.Instance.ActualTurnPhase == MYthsAndSteel_Enum.PhaseDeJeu.OrgoneJ2) &&
      ((player == 1 && GameManager.Instance.IsJ1Turn && PlayerScript.Instance.J1Infos.EventUseLeft > 0) || (player == 2 && !GameManager.Instance.IsJ1Turn && PlayerScript.Instance.J2Infos.EventUseLeft > 0)))
        {
            if ((player == 1 && PlayerScript.Instance.J1Infos.Ressource >= 0) || (player == 2 && PlayerScript.Instance.J2Infos.Ressource >= 0))
            {
              

               

                LaunchEventUnit(1, player == 1 ? true : false, unitList, "Paralysie", "�tes-vous sur de vouloir empecher l'activation des unit�s ayant le m�me type que celle s�l�ctionn�e?");
            GameManager.Instance._eventCall += Paralysie;
               
            }
            else
            {
                SoundController.Instance.PlaySound(SoundController.Instance.AudioClips[14]);
            }

        }
        else
        {
            SoundController.Instance.PlaySound(SoundController.Instance.AudioClips[14]);
        }
        unitList.Clear();
    }

    #endregion
    #region R�approvisionnement
    public void Reapprovisionnement()
    {
        UIInstance.Instance.ActivateNextPhaseButton();

        foreach(GameObject unit in GameManager.Instance.UnitChooseList)
        {
         
            

            unit.GetComponent<UnitScript>().GiveLife(1);
            
        }

        GameManager.Instance.UnitChooseList.Clear();

        //Remove la carte event chez le bon joueur
        RemoveEvents(MYthsAndSteel_Enum.EventCard.R�approvisionnement);
        EventCardUse();
    }

    public void LaunchReapprovisionnement()
    {
        int player = DeterminArmy(MYthsAndSteel_Enum.EventCard.R�approvisionnement);

        List<GameObject> unitList = new List<GameObject>();

        unitList.AddRange(player == 2 ? PlayerScript.Instance.UnitRef.UnitListBluePlayer : PlayerScript.Instance.UnitRef.UnitListRedPlayer);

        if ((GameManager.Instance.ActualTurnPhase == MYthsAndSteel_Enum.PhaseDeJeu.ActionJ1 || GameManager.Instance.ActualTurnPhase == MYthsAndSteel_Enum.PhaseDeJeu.ActionJ2) &&
            ((player == 1 && GameManager.Instance.IsJ1Turn && PlayerScript.Instance.J1Infos.EventUseLeft > 0) || (player == 2 && !GameManager.Instance.IsJ1Turn && PlayerScript.Instance.J2Infos.EventUseLeft > 0)))
        {
        
                
            
   

            LaunchEventUnit(2, player == 1 ? true : false, unitList, "R�approvisionnement", "�tes-vous sur de vouloir soigner ces deux unit�s de 1 point de vie?");
            GameManager.Instance._eventCall += Reapprovisionnement;

            
        }
        else
        {
            SoundController.Instance.PlaySound(SoundController.Instance.AudioClips[14]);
        }

        unitList.Clear();
    }
    #endregion R�approvisionnement

    #region ArmesPerforantes
    public void ArmesPerforantes()
    {
        UIInstance.Instance.ActivateNextPhaseButton();

        foreach(GameObject unit in GameManager.Instance.UnitChooseList)
        {
            unit.GetComponent<UnitScript>().AddDamageToUnit(1);
        }

        GameManager.Instance.UnitChooseList.Clear();

        //Remove la carte event chez le bon joueur
        RemoveEvents(MYthsAndSteel_Enum.EventCard.Armes_perforantes);
        EventCardUse();
    }

    public void LaunchArmesPerforantes()
    {
        int player = DeterminArmy(MYthsAndSteel_Enum.EventCard.Armes_perforantes);

        List<GameObject> unitList = new List<GameObject>();

        unitList.AddRange(player == 1 ? PlayerScript.Instance.UnitRef.UnitListRedPlayer : PlayerScript.Instance.UnitRef.UnitListBluePlayer);

        if((GameManager.Instance.ActualTurnPhase == MYthsAndSteel_Enum.PhaseDeJeu.ActionJ1 || GameManager.Instance.ActualTurnPhase == MYthsAndSteel_Enum.PhaseDeJeu.ActionJ2) &&
            ((player == 1 && GameManager.Instance.IsJ1Turn) && PlayerScript.Instance.J1Infos.EventUseLeft > 0 || (player == 2 && !GameManager.Instance.IsJ1Turn && PlayerScript.Instance.J2Infos.EventUseLeft > 0)))
        {
            LaunchEventUnit(2, player == 1 ? true : false, unitList, "Armes perforantes", "�tes-vous sur de vouloir augmenter d'1 les d�g�ts de ces deux unit�s?");
            GameManager.Instance._eventCall += ArmesPerforantes;

        }
        else
        {
            SoundController.Instance.PlaySound(SoundController.Instance.AudioClips[14]);
        }
        unitList.Clear();
    }
    #endregion ArmesPerforantes

    #region EntrainementRigoureux
    public void EntrainementRigoureux()
    {
        UIInstance.Instance.ActivateNextPhaseButton();

        foreach(GameObject unit in GameManager.Instance.UnitChooseList)
        {
            unit.GetComponent<UnitScript>().AddDiceToUnit(3);
        }

        GameManager.Instance.UnitChooseList.Clear();

        //Remove la carte event chez le bon joueur
        RemoveEvents(MYthsAndSteel_Enum.EventCard.Entra�nement_rigoureux);
        EventCardUse();
    }

    public void LaunchEntrainementRigoureux()
    {
        int player = DeterminArmy(MYthsAndSteel_Enum.EventCard.Entra�nement_rigoureux);

        List<GameObject> unitList = new List<GameObject>();

        unitList.AddRange(player == 1 ? PlayerScript.Instance.UnitRef.UnitListRedPlayer : PlayerScript.Instance.UnitRef.UnitListBluePlayer);

        if((GameManager.Instance.ActualTurnPhase == MYthsAndSteel_Enum.PhaseDeJeu.ActionJ1 || GameManager.Instance.ActualTurnPhase == MYthsAndSteel_Enum.PhaseDeJeu.ActionJ2) &&
            ((player == 1 && GameManager.Instance.IsJ1Turn && PlayerScript.Instance.J1Infos.EventUseLeft>0) || (player == 2 && !GameManager.Instance.IsJ1Turn &&  PlayerScript.Instance.J2Infos.EventUseLeft > 0)))
        {
            LaunchEventUnit(1, player == 1 ? true : false, unitList, "Entra�nement rigoureux", "�tes-vous sur de vouloir donner un bonus de 3 aux chances d'attaques de cette unit�?");
            GameManager.Instance._eventCall += EntrainementRigoureux;
        }
        else
        {
            SoundController.Instance.PlaySound(SoundController.Instance.AudioClips[14]);
        }

        unitList.Clear();
    }
    #endregion EntrainementRigoureux

    /// <summary>
    /// Lance un �v�nement en appellant la fonction dans le gameManager pour choisir une unit�
    /// </summary>
    /// <param name="unitNumber"></param>
    /// <param name="opponent"></param>
    /// <param name="army"></param>
    /// <param name="redPlayer"></param>
    void LaunchEventUnit(int unitNumber, bool redPlayer, List<GameObject> unitList, string titleValidation, string descriptionValidation)
    {
        GameManager.Instance.StartEventModeUnit(unitNumber, redPlayer, unitList, titleValidation, descriptionValidation) ;
    }

    /// <summary>
    /// Lance un �v�nement en appellant la fonction dans le gameManager pour choisir une ou plusieurs cases du plateau
    /// </summary>
    /// <param name="numberOfTiles"></param>
    /// <param name="redPlayer"></param>
    /// <param name="gamList"></param>
    void LaunchEventTile(int numberOfTiles, bool redPlayer, List<GameObject> gamList, string titleValidation, string descriptionValidation, bool multiple){
        GameManager.Instance.StartEventModeTiles(numberOfTiles, redPlayer, gamList, titleValidation, descriptionValidation, multiple);
    }

    /// <summary>
    /// Determine � quelle arm�e appartient la carte
    /// </summary>
    int DeterminArmy(MYthsAndSteel_Enum.EventCard cardToFind){
        foreach(MYthsAndSteel_Enum.EventCard card in PlayerScript.Instance.EventCardList._eventCardJ1)
        {
            if(card == cardToFind)
            {
                return 1;
            }
        }

        foreach(MYthsAndSteel_Enum.EventCard card in PlayerScript.Instance.EventCardList._eventCardBluePlayer)
        {
            if(card == cardToFind)
            {
                return 2;
            }
        }

        return 0;
    }
    #endregion Evenement
    void EventCardUse()
    {
        if (GameManager.Instance.IsJ1Turn)
        {
            PlayerScript.Instance.J1Infos.EventUseLeft --;
        }
        else
        {
            PlayerScript.Instance.J2Infos.EventUseLeft --;
        }
    }
    void RemovePlayerRessource(MYthsAndSteel_Enum.EventCard eventCardEnum)
{
        if (DeterminArmy(eventCardEnum) == 1)
        {

            PlayerScript.Instance.J1Infos.Ressource -= 1;
        }
        else
        {
            PlayerScript.Instance.J2Infos.Ressource -= 1;
        }
    }
}
/// <summary>
/// Class qui regroupe toutes les variables pour une carte event
/// </summary>
[System.Serializable]
public class EventCard {
    public string _eventName = "";
    [TextArea] public string _description = "";
    public MYthsAndSteel_Enum.EventCard _eventType = MYthsAndSteel_Enum.EventCard.Activation_de_nodus;
    public int _eventCost = 0;
    public bool _isEventInFinalGame = true;
    public Sprite _eventSprite = null;
    public GameObject _effectToSpawn = null;
    [SerializeField] private VictoryScreen victoryScreen;
}