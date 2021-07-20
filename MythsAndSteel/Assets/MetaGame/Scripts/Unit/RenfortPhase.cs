using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class RenfortPhase : MonoSingleton<RenfortPhase>
{
    #region AppelDeScript
    Player player;
    UnitReference unitReference;
    
    #endregion

    [SerializeField] private List<GameObject> _createTileJ1;
    public List<GameObject> CreateTileJ1 => _createTileJ1;

    [SerializeField] private List<GameObject> _createTileJ2;
    public List<GameObject> CreateTileJ2 => _createTileJ2;

    [SerializeField] private List<GameObject> _createLeader1;
    public List<GameObject> CreateLeader1 => _createLeader1;

    [SerializeField] private List<GameObject> _createLeader2;
    public List<GameObject> CreateLeader2 => _createLeader2;

    List<GameObject> _usineListRed = new List<GameObject>();
    List<GameObject> _usineListBlue = new List<GameObject>();

    List<GameObject> _leaderListRed = new List<GameObject>();
    List<GameObject> _leaderListBlue = new List<GameObject>();

  public int idCreate = -1;
    public bool redPlayerCreation = false;

    private void Start()
    {
        foreach(GameObject typeTile in TilesManager.Instance.TileList)
        {
            if(typeTile.GetComponent<TileScript>().TerrainEffectList.Contains((MYthsAndSteel_Enum.TerrainType.UsineRouge)))
            {
                _usineListRed.Add(typeTile);
            }
            else if(typeTile.GetComponent<TileScript>().TerrainEffectList.Contains((MYthsAndSteel_Enum.TerrainType.UsineBleu)))
            {
                _usineListBlue.Add(typeTile);
            }
        }

        foreach(GameObject unit in PlayerScript.Instance.UnitRef.UnitListRedPlayer)
        {
            foreach  (MYthsAndSteel_Enum.Attributs att in unit.GetComponent<UnitScript>().UnitSO.UnitAttributs)
            {
                if(att == MYthsAndSteel_Enum.Attributs.AppelDeRenforts)
                {
                    _leaderListRed.Add(unit);
                }
            }   
            
            
            
        }

        foreach(GameObject unit in PlayerScript.Instance.UnitRef.UnitListBluePlayer)
        {
            foreach (MYthsAndSteel_Enum.Attributs att in unit.GetComponent<UnitScript>().UnitSO.UnitAttributs)
            {
                if (att == MYthsAndSteel_Enum.Attributs.AppelDeRenforts)
                {
                    _leaderListBlue.Add(unit);
                }
            }
        }
    }

    /// <summary>
    /// Fais une liste des cases s�lectionnables autour de l'usine
    /// </summary>
    public void CreateRenfort(bool playerRed)
    {
        if(GameManager.Instance.ActualTurnPhase == MYthsAndSteel_Enum.PhaseDeJeu.ActionJ1 ||
            GameManager.Instance.ActualTurnPhase == MYthsAndSteel_Enum.PhaseDeJeu.ActionJ2 || OrgoneManager.Instance.DoingOrgoneCharge)
        {
            AroundCreateTileUsine(playerRed);
            
            AroundLeader(playerRed);
            ChangeButtonStatut(true, playerRed);
        }
        else
        {
            ChangeButtonStatut(false, playerRed);
        }
    }

    /// <summary>
    /// Active ou d�sactive les boutons du menu renfort
    /// </summary>
    /// <param name="activate"></param>
    void ChangeButtonStatut(bool activate, bool playerRed){
        UIInstance.Instance.ButtonRenfort._clicSurUnit�1.GetComponent<CanvasGroup>().interactable = activate;
        UIInstance.Instance.ButtonRenfort._clicSurUnit�1.GetComponent<CanvasGroup>().blocksRaycasts = activate;

        UIInstance.Instance.ButtonRenfort._clicSurUnit�2.GetComponent<CanvasGroup>().interactable = activate;
        UIInstance.Instance.ButtonRenfort._clicSurUnit�2.GetComponent<CanvasGroup>().blocksRaycasts = activate;

        UIInstance.Instance.ButtonRenfort._clicSurUnit�3.GetComponent<CanvasGroup>().interactable = activate;
        UIInstance.Instance.ButtonRenfort._clicSurUnit�3.GetComponent<CanvasGroup>().blocksRaycasts = activate;

        UIInstance.Instance.ButtonRenfort._clicSurUnit�4.GetComponent<CanvasGroup>().interactable = activate;
        UIInstance.Instance.ButtonRenfort._clicSurUnit�4.GetComponent<CanvasGroup>().blocksRaycasts = activate;

        UIInstance.Instance.ButtonRenfort._clicSurUnit�5.GetComponent<CanvasGroup>().interactable = activate;
        UIInstance.Instance.ButtonRenfort._clicSurUnit�5.GetComponent<CanvasGroup>().blocksRaycasts = activate;

        UIInstance.Instance.ButtonRenfort._clicSurUnit�6.GetComponent<CanvasGroup>().interactable = activate;
        UIInstance.Instance.ButtonRenfort._clicSurUnit�6.GetComponent<CanvasGroup>().blocksRaycasts = activate;

        if(activate){
            for(int i = 0; i < (playerRed? PlayerScript.Instance.UnitRef.UnitClassCreableListRedPlayer.Count : PlayerScript.Instance.UnitRef.UnitClassCreableListBluePlayer.Count); i++){
                switch(i)
                {
                    case 0:
                        if(playerRed){
                            if(PlayerScript.Instance.UnitRef.UnitClassCreableListRedPlayer[i].GetComponent<UnitScript>().UnitSO.CreationCost <= PlayerScript.Instance.RedPlayerInfos.Ressource || OrgoneManager.Instance.DoingOrgoneCharge){
                                UIInstance.Instance.ButtonRenfort._clicSurUnit�1.GetComponent<CanvasGroup>().interactable = true;
                                UIInstance.Instance.ButtonRenfort._clicSurUnit�1.GetComponent<CanvasGroup>().blocksRaycasts = true;
                            }
                            else{
                                UIInstance.Instance.ButtonRenfort._clicSurUnit�1.GetComponent<CanvasGroup>().interactable = false;
                                UIInstance.Instance.ButtonRenfort._clicSurUnit�1.GetComponent<CanvasGroup>().blocksRaycasts = false;
                                UIInstance.Instance.ButtonRenfort._clicSurUnit�1.GetComponent<RenfortBtnUI>().HideCanvas();
                            }
                        }
                        else{
                            if(PlayerScript.Instance.UnitRef.UnitClassCreableListBluePlayer[i].GetComponent<UnitScript>().UnitSO.CreationCost <= PlayerScript.Instance.BluePlayerInfos.Ressource || OrgoneManager.Instance.DoingOrgoneCharge)
                            {
                                UIInstance.Instance.ButtonRenfort._clicSurUnit�1.GetComponent<CanvasGroup>().interactable = true;
                                UIInstance.Instance.ButtonRenfort._clicSurUnit�1.GetComponent<CanvasGroup>().blocksRaycasts = true;
                            }
                            else{
                                UIInstance.Instance.ButtonRenfort._clicSurUnit�1.GetComponent<CanvasGroup>().interactable = false;
                                UIInstance.Instance.ButtonRenfort._clicSurUnit�1.GetComponent<CanvasGroup>().blocksRaycasts = false;
                                UIInstance.Instance.ButtonRenfort._clicSurUnit�1.GetComponent<RenfortBtnUI>().HideCanvas();
                            }
                        }
                        break;

                    case 1:
                        if(playerRed)
                        {
                            if(PlayerScript.Instance.UnitRef.UnitClassCreableListRedPlayer[i].GetComponent<UnitScript>().UnitSO.CreationCost <= PlayerScript.Instance.RedPlayerInfos.Ressource || OrgoneManager.Instance.DoingOrgoneCharge)
                            {
                                UIInstance.Instance.ButtonRenfort._clicSurUnit�2.GetComponent<CanvasGroup>().interactable = true;
                                UIInstance.Instance.ButtonRenfort._clicSurUnit�2.GetComponent<CanvasGroup>().blocksRaycasts = true;
                            }
                            else
                            {
                                UIInstance.Instance.ButtonRenfort._clicSurUnit�2.GetComponent<CanvasGroup>().interactable = false;
                                UIInstance.Instance.ButtonRenfort._clicSurUnit�2.GetComponent<CanvasGroup>().blocksRaycasts = false;
                                UIInstance.Instance.ButtonRenfort._clicSurUnit�1.GetComponent<RenfortBtnUI>().HideCanvas();
                            }
                        }
                        else
                        {
                            if(PlayerScript.Instance.UnitRef.UnitClassCreableListBluePlayer[i].GetComponent<UnitScript>().UnitSO.CreationCost <= PlayerScript.Instance.BluePlayerInfos.Ressource || OrgoneManager.Instance.DoingOrgoneCharge)
                            {
                                UIInstance.Instance.ButtonRenfort._clicSurUnit�2.GetComponent<CanvasGroup>().interactable = true;
                                UIInstance.Instance.ButtonRenfort._clicSurUnit�2.GetComponent<CanvasGroup>().blocksRaycasts = true;
                            }
                            else
                            {
                                UIInstance.Instance.ButtonRenfort._clicSurUnit�2.GetComponent<CanvasGroup>().interactable = false;
                                UIInstance.Instance.ButtonRenfort._clicSurUnit�2.GetComponent<CanvasGroup>().blocksRaycasts = false;
                                UIInstance.Instance.ButtonRenfort._clicSurUnit�1.GetComponent<RenfortBtnUI>().HideCanvas();
                            }
                        }
                        break;

                    case 2:
                        if(playerRed)
                        {
                            if(PlayerScript.Instance.UnitRef.UnitClassCreableListRedPlayer[i].GetComponent<UnitScript>().UnitSO.CreationCost <= PlayerScript.Instance.RedPlayerInfos.Ressource || OrgoneManager.Instance.DoingOrgoneCharge)
                            {
                                UIInstance.Instance.ButtonRenfort._clicSurUnit�3.GetComponent<CanvasGroup>().interactable = true;
                                UIInstance.Instance.ButtonRenfort._clicSurUnit�3.GetComponent<CanvasGroup>().blocksRaycasts = true;
                            }
                            else
                            {
                                UIInstance.Instance.ButtonRenfort._clicSurUnit�3.GetComponent<CanvasGroup>().interactable = false;
                                UIInstance.Instance.ButtonRenfort._clicSurUnit�3.GetComponent<CanvasGroup>().blocksRaycasts = false;
                                UIInstance.Instance.ButtonRenfort._clicSurUnit�1.GetComponent<RenfortBtnUI>().HideCanvas();
                            }
                        }
                        else
                        {
                            if(PlayerScript.Instance.UnitRef.UnitClassCreableListBluePlayer[i].GetComponent<UnitScript>().UnitSO.CreationCost <= PlayerScript.Instance.BluePlayerInfos.Ressource || OrgoneManager.Instance.DoingOrgoneCharge)
                            {
                                UIInstance.Instance.ButtonRenfort._clicSurUnit�3.GetComponent<CanvasGroup>().interactable = true;
                                UIInstance.Instance.ButtonRenfort._clicSurUnit�3.GetComponent<CanvasGroup>().blocksRaycasts = true;
                            }
                            else
                            {
                                UIInstance.Instance.ButtonRenfort._clicSurUnit�3.GetComponent<CanvasGroup>().interactable = false;
                                UIInstance.Instance.ButtonRenfort._clicSurUnit�3.GetComponent<CanvasGroup>().blocksRaycasts = false;
                                UIInstance.Instance.ButtonRenfort._clicSurUnit�1.GetComponent<RenfortBtnUI>().HideCanvas();
                            }
                        }
                        break;

                    case 3:
                        if(playerRed)
                        {
                            if(PlayerScript.Instance.UnitRef.UnitClassCreableListRedPlayer[i].GetComponent<UnitScript>().UnitSO.CreationCost <= PlayerScript.Instance.RedPlayerInfos.Ressource || OrgoneManager.Instance.DoingOrgoneCharge)
                            {
                                UIInstance.Instance.ButtonRenfort._clicSurUnit�4.GetComponent<CanvasGroup>().interactable = true;
                                UIInstance.Instance.ButtonRenfort._clicSurUnit�4.GetComponent<CanvasGroup>().blocksRaycasts = true;
                            }
                            else
                            {
                                UIInstance.Instance.ButtonRenfort._clicSurUnit�4.GetComponent<CanvasGroup>().interactable = false;
                                UIInstance.Instance.ButtonRenfort._clicSurUnit�4.GetComponent<CanvasGroup>().blocksRaycasts = false;
                                UIInstance.Instance.ButtonRenfort._clicSurUnit�1.GetComponent<RenfortBtnUI>().HideCanvas();
                            }
                        }
                        else
                        {
                            if(PlayerScript.Instance.UnitRef.UnitClassCreableListBluePlayer[i].GetComponent<UnitScript>().UnitSO.CreationCost <= PlayerScript.Instance.BluePlayerInfos.Ressource || OrgoneManager.Instance.DoingOrgoneCharge)
                            {
                                UIInstance.Instance.ButtonRenfort._clicSurUnit�4.GetComponent<CanvasGroup>().interactable = true;
                                UIInstance.Instance.ButtonRenfort._clicSurUnit�4.GetComponent<CanvasGroup>().blocksRaycasts = true;
                            }
                            else
                            {
                                UIInstance.Instance.ButtonRenfort._clicSurUnit�4.GetComponent<CanvasGroup>().interactable = false;
                                UIInstance.Instance.ButtonRenfort._clicSurUnit�4.GetComponent<CanvasGroup>().blocksRaycasts = false;
                                UIInstance.Instance.ButtonRenfort._clicSurUnit�1.GetComponent<RenfortBtnUI>().HideCanvas();
                            }
                        }
                        break;

                    case 4:
                        if(playerRed)
                        {
                            if(PlayerScript.Instance.UnitRef.UnitClassCreableListRedPlayer[i].GetComponent<UnitScript>().UnitSO.CreationCost <= PlayerScript.Instance.RedPlayerInfos.Ressource || OrgoneManager.Instance.DoingOrgoneCharge)
                            {
                                UIInstance.Instance.ButtonRenfort._clicSurUnit�5.GetComponent<CanvasGroup>().interactable = true;
                                UIInstance.Instance.ButtonRenfort._clicSurUnit�5.GetComponent<CanvasGroup>().blocksRaycasts = true;
                            }
                            else
                            {
                                UIInstance.Instance.ButtonRenfort._clicSurUnit�5.GetComponent<CanvasGroup>().interactable = false;
                                UIInstance.Instance.ButtonRenfort._clicSurUnit�5.GetComponent<CanvasGroup>().blocksRaycasts = false;
                                UIInstance.Instance.ButtonRenfort._clicSurUnit�1.GetComponent<RenfortBtnUI>().HideCanvas();
                            }
                        }
                        else
                        {
                            if(PlayerScript.Instance.UnitRef.UnitClassCreableListBluePlayer[i].GetComponent<UnitScript>().UnitSO.CreationCost <= PlayerScript.Instance.BluePlayerInfos.Ressource || OrgoneManager.Instance.DoingOrgoneCharge)
                            {
                                UIInstance.Instance.ButtonRenfort._clicSurUnit�5.GetComponent<CanvasGroup>().interactable = true;
                                UIInstance.Instance.ButtonRenfort._clicSurUnit�5.GetComponent<CanvasGroup>().blocksRaycasts = true;
                            }
                            else
                            {
                                UIInstance.Instance.ButtonRenfort._clicSurUnit�5.GetComponent<CanvasGroup>().interactable = false;
                                UIInstance.Instance.ButtonRenfort._clicSurUnit�5.GetComponent<CanvasGroup>().blocksRaycasts = false;
                                UIInstance.Instance.ButtonRenfort._clicSurUnit�1.GetComponent<RenfortBtnUI>().HideCanvas();
                            }
                        }
                        break;

                    case 5:
                        if(playerRed)
                        {
                            if(PlayerScript.Instance.UnitRef.UnitClassCreableListRedPlayer[i].GetComponent<UnitScript>().UnitSO.CreationCost <= PlayerScript.Instance.RedPlayerInfos.Ressource || OrgoneManager.Instance.DoingOrgoneCharge)
                            {
                                UIInstance.Instance.ButtonRenfort._clicSurUnit�6.GetComponent<CanvasGroup>().interactable = true;
                                UIInstance.Instance.ButtonRenfort._clicSurUnit�6.GetComponent<CanvasGroup>().blocksRaycasts = true;
                            }
                            else
                            {
                                UIInstance.Instance.ButtonRenfort._clicSurUnit�6.GetComponent<CanvasGroup>().interactable = false;
                                UIInstance.Instance.ButtonRenfort._clicSurUnit�6.GetComponent<CanvasGroup>().blocksRaycasts = false;
                                UIInstance.Instance.ButtonRenfort._clicSurUnit�1.GetComponent<RenfortBtnUI>().HideCanvas();
                            }
                        }
                        else
                        {
                            if(PlayerScript.Instance.UnitRef.UnitClassCreableListBluePlayer[i].GetComponent<UnitScript>().UnitSO.CreationCost <= PlayerScript.Instance.BluePlayerInfos.Ressource || OrgoneManager.Instance.DoingOrgoneCharge)
                            {
                                UIInstance.Instance.ButtonRenfort._clicSurUnit�6.GetComponent<CanvasGroup>().interactable = true;
                                UIInstance.Instance.ButtonRenfort._clicSurUnit�6.GetComponent<CanvasGroup>().blocksRaycasts = true;
                            }
                            else
                            {
                                UIInstance.Instance.ButtonRenfort._clicSurUnit�6.GetComponent<CanvasGroup>().interactable = false;
                                UIInstance.Instance.ButtonRenfort._clicSurUnit�6.GetComponent<CanvasGroup>().blocksRaycasts = false;
                                UIInstance.Instance.ButtonRenfort._clicSurUnit�1.GetComponent<RenfortBtnUI>().HideCanvas();
                            }
                        }
                        break;
                }
            }
        }
    }

    #region GetTiles
    /// <summary>
    /// Cr�er une liste des tiles autour des usines
    /// </summary>
    /// <param name="usine1"></param>
    void AroundCreateTileUsine(bool usine1)
    {
        //Si l'arm�e est jouer par l'arm�e Bleu.
        if(usine1 == true)
        {
            _createTileJ1 = CreateTileList(_usineListRed);
        }
        else
        {
            _createTileJ2 = CreateTileList(_usineListBlue);
        }
    }

    /// <summary>
    /// Obtient les cases s�lectionnables
    /// </summary>
    /// <param name="usineList"></param>
    /// <returns></returns>
    List<GameObject> CreateTileList(List<GameObject> usineList)
    {
        List<GameObject> tempList = new List<GameObject>();

        foreach(GameObject typeTile in usineList)
        {
            int typeTileID = TilesManager.Instance.TileList.IndexOf(typeTile);
            //Debug.Log(typeTile);
            foreach(int idtyleIndex in PlayerStatic.GetNeighbourDiag(typeTileID, TilesManager.Instance.TileList[typeTileID].GetComponent<TileScript>().Line, false))
            {
                //Tu ajoutes la tile correspondant � l'usineJ2
                if(!tempList.Contains(typeTile))
                {
                    if(typeTile.GetComponent<TileScript>().Unit == null)
                    {
                        tempList.Add(typeTile);
                    }
                }
                //Si le num�ro n'est pas pr�sent dans la liste, tu l'ajoutes.
                if(!tempList.Contains(TilesManager.Instance.TileList[idtyleIndex]))
                {
                    tempList.Add(TilesManager.Instance.TileList[idtyleIndex]);
                    //Si il y un boolean qui est retourn� alors tu enl�ves les �l�ments de la liste.
                    if(CheckConditions(TilesManager.Instance.TileList[idtyleIndex], typeTileID) == true)
                    {
                        tempList.Remove(TilesManager.Instance.TileList[idtyleIndex]);
                    }
                }
            }
        }

        //Pour chaque �l�ment dans la liste, tu ajoutes l'effet TileCr�able
        foreach(GameObject typeWithoutEffect in tempList)
        {
            if(!typeWithoutEffect.GetComponent<TileScript>().EffetProg.Contains(MYthsAndSteel_Enum.EffetProg.Zone_creable))
            {
                typeWithoutEffect.GetComponent<TileScript>().EffetProg.Add(MYthsAndSteel_Enum.EffetProg.Zone_creable);
            }
        }

        return tempList;
    }

    /// <summary>
    ///  Fais une liste des cases s�lectionnables autour des leader
    /// </summary>
    /// <param name="leaderArmy1"></param>
    void AroundLeader(bool leaderArmy1)
    {
        //Si l'arm�e est jouer par l'arm�e Bleu.
        if(leaderArmy1 == true)
        {
            foreach(GameObject unit in _leaderListRed)
            {
                if(unit != null)
                {
                    int typeTileID = unit.GetComponent<UnitScript>().ActualTiledId;

                    //Pour chaque num�ro pr�sent dans le PlayerStatic avec la valeur qu'on a convertit pr�c�demment.
                    foreach(int idtyleIndex in PlayerStatic.GetNeighbourDiag(typeTileID, TilesManager.Instance.TileList[typeTileID].GetComponent<TileScript>().Line, false))
                    {
                        GameObject tile = TilesManager.Instance.TileList[idtyleIndex];
                        //Si le num�ro n'est pas pr�sent dans la liste, tu l'ajoutes.
                        if(!_createLeader1.Contains(tile))
                        {
                            _createLeader1.Add(tile);

                            //Si il y un boolean qui est retourn� alors tu enl�ves les �l�ments de la liste.
                            if(CheckConditions(tile, typeTileID) == true)
                            {
                                _createLeader1.Remove(tile);
                            }
                        }
                    }
                }
            }
            //Pour chaque �l�ment dans la liste, tu ajoutes l'effet TileCr�able
            foreach(GameObject typeWithoutEffect in _createLeader1)
            {
                if(!typeWithoutEffect.GetComponent<TileScript>().EffetProg.Contains(MYthsAndSteel_Enum.EffetProg.Zone_creable))
                {
                    typeWithoutEffect.GetComponent<TileScript>().EffetProg.Add(MYthsAndSteel_Enum.EffetProg.Zone_creable);
                }
            }
        }


        else
        {
            foreach(GameObject unit in _leaderListBlue)
            {
                if(unit != null)
                {
                    int typeTileID = unit.GetComponent<UnitScript>().ActualTiledId;

                    //Pour chaque num�ro pr�sent dans le PlayerStatic avec la valeur qu'on a convertit pr�c�demment.
                    foreach(int idtyleIndex in PlayerStatic.GetNeighbourDiag(typeTileID, TilesManager.Instance.TileList[typeTileID].GetComponent<TileScript>().Line, false))
                    {
                        GameObject tile = TilesManager.Instance.TileList[idtyleIndex];
                        //Si le num�ro n'est pas pr�sent dans la liste, tu l'ajoutes.
                        if(!_createLeader2.Contains(tile))
                        {
                            _createLeader2.Add(tile);

                            //Si il y un boolean qui est retourn� alors tu enl�ves les �l�ments de la liste.
                            if(CheckConditions(tile, typeTileID) == true)
                            {
                                _createLeader2.Remove(tile);
                            }
                        }
                    }
                }
            }
            //Pour chaque �l�ment dans la liste, tu ajoutes l'effet TileCr�able
            foreach(GameObject typeWithoutEffect in _createTileJ2)
            {
                if(!typeWithoutEffect.GetComponent<TileScript>().EffetProg.Contains(MYthsAndSteel_Enum.EffetProg.Zone_creable))
                {
                    typeWithoutEffect.GetComponent<TileScript>().EffetProg.Add(MYthsAndSteel_Enum.EffetProg.Zone_creable);
                }
            }
        }
    }
    #endregion GetTiles

    /// <summary>
    /// Permet de d�terminer si autour des cases adjacentes il y a des effets de terrain qui pourraient g�ner le d�ploiement de troupes.
    /// </summary>
    /// <param name="tileCheck"></param>
    /// <returns></returns>
    bool CheckConditions(GameObject tileCheck, int origin)
    {
        //Y a une unit�?
        if(tileCheck.GetComponent<TileScript>().Unit != null)
        {
            return true;
        }

        //Y a de l'eau ou un ravin?
        if(tileCheck.GetComponent<TileScript>().TerrainEffectList.Contains(MYthsAndSteel_Enum.TerrainType.Ravin)
            || tileCheck.GetComponent<TileScript>().TerrainEffectList.Contains(MYthsAndSteel_Enum.TerrainType.Eau))
        {
            return true;
        }

        //RIVIERE?
        if(tileCheck.GetComponent<TileScript>().TerrainEffectList.Contains(MYthsAndSteel_Enum.TerrainType.Rivi�re_Nord)
        && PlayerStatic.CheckDirection(origin, tileCheck.GetComponent<TileScript>().TileId) == MYthsAndSteel_Enum.Direction.Sud)
        {
            if(!tileCheck.GetComponent<TileScript>().TerrainEffectList.Contains(MYthsAndSteel_Enum.TerrainType.Pont_Nord))
            {
                return true;
            }
        }

        if(tileCheck.GetComponent<TileScript>().TerrainEffectList.Contains(MYthsAndSteel_Enum.TerrainType.Rivi�re_Sud)
        && PlayerStatic.CheckDirection(origin, tileCheck.GetComponent<TileScript>().TileId) == MYthsAndSteel_Enum.Direction.Nord)
        {
            if(!tileCheck.GetComponent<TileScript>().TerrainEffectList.Contains(MYthsAndSteel_Enum.TerrainType.Pont_Sud))
            {
                return true;
            }
        }

        if(tileCheck.GetComponent<TileScript>().TerrainEffectList.Contains(MYthsAndSteel_Enum.TerrainType.Rivi�re_Ouest)
        && PlayerStatic.CheckDirection(origin, tileCheck.GetComponent<TileScript>().TileId) == MYthsAndSteel_Enum.Direction.Est)
        {
            if(!tileCheck.GetComponent<TileScript>().TerrainEffectList.Contains(MYthsAndSteel_Enum.TerrainType.Rivi�re_Ouest))
            {
                return true;
            }
        }

        if(tileCheck.GetComponent<TileScript>().TerrainEffectList.Contains(MYthsAndSteel_Enum.TerrainType.Rivi�re_Est)
        && PlayerStatic.CheckDirection(origin, tileCheck.GetComponent<TileScript>().TileId) == MYthsAndSteel_Enum.Direction.Ouest)
        {
            if(!tileCheck.GetComponent<TileScript>().TerrainEffectList.Contains(MYthsAndSteel_Enum.TerrainType.Rivi�re_Est))
            {
                return true;
            }
        }

        return false;

        //Ajouter les barbelets.
    }

    #region Cr�erUnit�
    /// <summary>
    /// Permet de cr�er une unit�
    /// </summary>
    public void craftUnit(int unitId)
    {
        if (GameManager.Instance.IsPlayerRedTurn)
        {
            if (PlayerScript.Instance.UnitRef.UnitClassCreableListRedPlayer[unitId].GetComponent<UnitScript>().UnitSO.CreationCost <= PlayerScript.Instance.RedPlayerInfos.Ressource || OrgoneManager.Instance.DoingOrgoneCharge)
            {
                List<GameObject> tileList = new List<GameObject>();
                tileList.AddRange(GameManager.Instance.RenfortPhase.CreateLeader1);
                tileList.AddRange(GameManager.Instance.RenfortPhase.CreateTileJ1);
                if(tileList.Count != 0)
                {
                idCreate = unitId;

                redPlayerCreation = true;
                    
                GameManager.Instance.StartEventModeTiles(1, true, tileList, "Cr�ation d'unit�", "�tes-vous sur de vouloir cr�er une unit� sur cette case");
                GameManager.Instance._eventCall += CreateNewUnit;
                GameManager.Instance._eventCall += UIInstance.Instance.ActivateNextPhaseButton;

                }
                else if (tileList.Count == 0)
                {
           
                    UIInstance.Instance.ActivateNextPhaseButton();
                }
                RaycastManager.Instance._mouseCommand.QuitRenfortPanel();
            }
        }
        else
        {
            if (PlayerScript.Instance.UnitRef.UnitClassCreableListBluePlayer[unitId].GetComponent<UnitScript>().UnitSO.CreationCost <= PlayerScript.Instance.BluePlayerInfos.Ressource ||OrgoneManager.Instance.DoingOrgoneCharge)
            {
                
                List<GameObject> tileList = new List<GameObject>();
                tileList.AddRange(GameManager.Instance.RenfortPhase.CreateLeader2);
                tileList.AddRange(GameManager.Instance.RenfortPhase.CreateTileJ2);
                if(tileList.Count != 0)
                {

                idCreate = unitId;
                redPlayerCreation = false;
                GameManager.Instance.StartEventModeTiles(1, false, tileList, "Cr�ation d'unit�", "�tes-vous sur de vouloir cr�er une unit� sur cette case");
                GameManager.Instance._eventCall += CreateNewUnit;
               GameManager.Instance._eventCall += UIInstance.Instance.ActivateNextPhaseButton;
                }
                else if (tileList.Count == 0)
                {
                    UIInstance.Instance.ActivateNextPhaseButton();
                }
                RaycastManager.Instance._mouseCommand.QuitRenfortPanel();
            }
        }
    }

    /// <summary>
    /// Cr�e une nouvelle unit� sur le terrain au niveau de la tile s�lectionn�e
    /// </summary>
    public void CreateNewUnit(){
      
        if(redPlayerCreation)
        {
            GameObject obj = Instantiate(PlayerScript.Instance.UnitRef.UnitClassCreableListRedPlayer[idCreate], GameManager.Instance.TileChooseList[0].transform.position, Quaternion.identity);
            GameManager.Instance.TileChooseList[0].GetComponent<TileScript>().AddUnitToTile(obj);
            PlayerScript.Instance.RedPlayerInfos.HasCreateUnit = true;
            PlayerScript.Instance.UnitRef.UnitListRedPlayer.Add(obj);
            if (!OrgoneManager.Instance.DoingOrgoneCharge)
            {
                PlayerScript.Instance.RedPlayerInfos.Ressource -= PlayerScript.Instance.UnitRef.UnitClassCreableListRedPlayer[idCreate].GetComponent<UnitScript>().UnitSO.CreationCost;
                UIInstance.Instance.UpdateRessourceLeft();
            UIInstance.Instance.RedRenfortCount += 1;
                if (UIInstance.Instance.RedRenfortCount == 1)
                {
                    UIInstance.Instance.ButtonRenfortJ1.GetComponent<Button>().interactable = false;
                    Debug.Log("fdljs");
                }
            }
            GameManager.Instance.victoryScreen.redRessourcesUsed += PlayerScript.Instance.UnitRef.UnitClassCreableListRedPlayer[idCreate].GetComponent<UnitScript>().UnitSO.CreationCost;
        }
        else
        {
            GameObject obj = Instantiate(PlayerScript.Instance.UnitRef.UnitClassCreableListBluePlayer[idCreate], GameManager.Instance.TileChooseList[0].transform.position, Quaternion.identity);
            GameManager.Instance.TileChooseList[0].GetComponent<TileScript>().AddUnitToTile(obj);
            PlayerScript.Instance.BluePlayerInfos.HasCreateUnit = true;
            PlayerScript.Instance.UnitRef.UnitListBluePlayer.Add(obj);
            
            if (!OrgoneManager.Instance.DoingOrgoneCharge)
            {
                UIInstance.Instance.BlueRenfortCount += 1;
                PlayerScript.Instance.BluePlayerInfos.Ressource -= PlayerScript.Instance.UnitRef.UnitClassCreableListBluePlayer[idCreate].GetComponent<UnitScript>().UnitSO.CreationCost;
                UIInstance.Instance.UpdateRessourceLeft();
                if(UIInstance.Instance.BlueRenfortCount == 1)
                {
                    UIInstance.Instance.ButtonRenfortJ2.GetComponent<Button>().interactable = false;

                }
            }
            
            else
            {
                OrgoneManager.Instance.DoingOrgoneCharge = false;
            }

            GameManager.Instance.victoryScreen.blueResourcesUsed += PlayerScript.Instance.UnitRef.UnitClassCreableListBluePlayer[idCreate].GetComponent<UnitScript>().UnitSO.CreationCost;
        }
        SoundController.Instance.PlaySound(SoundController.Instance.AudioClips[8]);
        GameManager.Instance.TileChooseList.Clear();
        OrgoneManager.Instance.DoingOrgoneCharge = false;
        UIInstance.Instance.boutonAnnulerRenfort.GetComponent<Button>().enabled = true ;
       UIInstance.Instance.boutonAnnulerRenfort.GetComponent<Image>().color = new Color(0f, 0f, 0f, 0f);

    }
    #endregion Cr�erUnit�

    public void UpdateGareControl()
    {
        _usineListBlue.Clear();
        _usineListRed.Clear();
        foreach (GameObject typeTile in TilesManager.Instance.TileList)
        {
            if (typeTile.GetComponent<TileScript>().TerrainEffectList.Contains((MYthsAndSteel_Enum.TerrainType.UsineRouge)))
            {
                _usineListRed.Add(typeTile);
            }
            else if (typeTile.GetComponent<TileScript>().TerrainEffectList.Contains((MYthsAndSteel_Enum.TerrainType.UsineBleu)))
            {
                _usineListBlue.Add(typeTile);
            }
        }

        foreach (GameObject unit in PlayerScript.Instance.UnitRef.UnitListRedPlayer)
        {
            foreach (MYthsAndSteel_Enum.Attributs att in unit.GetComponent<UnitScript>().UnitSO.UnitAttributs)
            {
                if (att == MYthsAndSteel_Enum.Attributs.AppelDeRenforts)
                {
                    _leaderListRed.Add(unit);
                }
            }



        }

        foreach (GameObject unit in PlayerScript.Instance.UnitRef.UnitListBluePlayer)
        {
            foreach (MYthsAndSteel_Enum.Attributs att in unit.GetComponent<UnitScript>().UnitSO.UnitAttributs)
            {
                if (att == MYthsAndSteel_Enum.Attributs.AppelDeRenforts)
                {
                    _leaderListBlue.Add(unit);
                }
            }
        }
    }
}