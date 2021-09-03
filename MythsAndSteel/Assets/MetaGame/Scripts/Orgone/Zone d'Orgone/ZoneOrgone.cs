using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
Ce Script fonctionne pour les deux zones d'orgones � condition de bien assigner les variables propres � ces derni�res. Il va g�rer les conditions qui permettent le d�placement de la zone, son d�placement en lui-m�me,
puis la validation de son d�placement avec le fait de attribuer et de retirer les effets de terrains RedOrgone et BlueOrgone.
 */
public class ZoneOrgone : MonoBehaviour
{
    #region Variables
    //Variables communes au deux zones
    [Range(15, 40)]
    [SerializeField] private float _speedTiles = 0f;

    //Case o� la zone peut etre d�plac�e
    [Header("GAMEOBJECT IMPORTANTS")]
    [SerializeField] List<GameObject> _tilesInRange = new List<GameObject>();
    [SerializeField] GameObject centerOrgoneArea;
    public GameObject _centerOrgoneArea
    {
        get
        {
            return centerOrgoneArea; 
        }
        set
        {
            centerOrgoneArea = value;            
            OrgoneManager.Instance.CheckZoneOrgone();
        }
    }
    [SerializeField] GameObject _targetTile;

    //Variables propres � chaque Zone d'orgone � Assigner dans l'Inspecteur � mettre dans le Player Class
    [Header("INFO ZONE")]
    [SerializeField] bool _redPlayerZone;
    [SerializeField] bool _hasMoveOrgoneArea;
    public bool HasMoveOrgoneArea => _hasMoveOrgoneArea;
    [SerializeField] bool _isInValidation = false;
    public bool IsInValidation => _isInValidation;

    [SerializeField] GameObject _childGam = null;

    GameObject _lastTileInRange = null;
    #endregion Variables

    private void Start()
    {
        HideChild();
        GameManager.Instance.ManagerSO.GoToActionJ1Phase += HideChild;
        GameManager.Instance.ManagerSO.GoToActionJ2Phase += HideChild;

        if(_redPlayerZone){
            PlayerScript.Instance.RedPlayerInfos.TileCentreZoneOrgone = _centerOrgoneArea;
        }
        else{
            PlayerScript.Instance.BluePlayerInfos.TileCentreZoneOrgone = _centerOrgoneArea;
        }

        List<int> neighZone = PlayerStatic.GetNeighbourDiag(_centerOrgoneArea.GetComponent<TileScript>().TileId, _centerOrgoneArea.GetComponent<TileScript>().Line, true);
        _centerOrgoneArea.GetComponent<TileScript>().TerrainEffectList.Remove(_redPlayerZone ? MYthsAndSteel_Enum.TerrainType.OrgoneRed : MYthsAndSteel_Enum.TerrainType.OrgoneBlue);

        foreach(int i in neighZone)
        {
            TilesManager.Instance.TileList[i].GetComponent<TileScript>().TerrainEffectList.Remove(_redPlayerZone ? MYthsAndSteel_Enum.TerrainType.OrgoneRed : MYthsAndSteel_Enum.TerrainType.OrgoneBlue);
        }

        List<int> newNeighZone = PlayerStatic.GetNeighbourDiag(_centerOrgoneArea.GetComponent<TileScript>().TileId, _centerOrgoneArea.GetComponent<TileScript>().Line, true);
        newNeighZone.Add(_centerOrgoneArea.GetComponent<TileScript>().TileId);

        foreach(int i in newNeighZone)
        {
            TilesManager.Instance.TileList[i].GetComponent<TileScript>().TerrainEffectList.Add(_redPlayerZone ? MYthsAndSteel_Enum.TerrainType.OrgoneRed : MYthsAndSteel_Enum.TerrainType.OrgoneBlue);
        }
    }

    private void Update(){
        if(_isInValidation == false && _redPlayerZone == GameManager.Instance.IsPlayerRedTurn)
        {
            //D�place la zone si il est dans la bonne phase
            if(OrgoneManager.Instance.Selected && GameManager.Instance.IsPlayerRedTurn == _redPlayerZone && RaycastManager.Instance.Tile != null && !_hasMoveOrgoneArea)
            {
                if(_tilesInRange.Contains(RaycastManager.Instance.Tile))
                {
                    _lastTileInRange = RaycastManager.Instance.Tile;
                    //D�place la zone � la position
                    transform.position = Vector3.MoveTowards(transform.position, new Vector3(RaycastManager.Instance.Tile.transform.position.x, RaycastManager.Instance.Tile.transform.position.y, transform.position.z),
                                         _speedTiles * Vector2.Distance(transform.position, RaycastManager.Instance.Tile.transform.position) * Time.deltaTime);
                }
                else if(_lastTileInRange != null)
                {
                    transform.position = Vector3.MoveTowards(transform.position, new Vector3(_lastTileInRange.transform.position.x, _lastTileInRange.transform.position.y, transform.position.z),
                                         _speedTiles * Vector2.Distance(transform.position, _lastTileInRange.transform.position) * Time.deltaTime);
                }
            }
            else
            {
                if(_lastTileInRange != null)
                {
                    transform.position = Vector3.MoveTowards(transform.position, new Vector3(_lastTileInRange.transform.position.x, _lastTileInRange.transform.position.y, transform.position.z),
                                         _speedTiles * Vector2.Distance(transform.position, _lastTileInRange.transform.position) * Time.deltaTime);
                }
            }

        }
        else { }
    }

    /// <summary>
    /// Lorsque le joueur commence � cliquer sur la case centrale de la zone d'orgone
    /// </summary>
    public void AddOrgoneAtRange(){
        UIInstance.Instance.DesactivateNextPhaseButton();
        List<int> OrgoneNeigh = _redPlayerZone ? PlayerStatic.GetNeighbourDiag(PlayerScript.Instance.RedPlayerInfos.TileCentreZoneOrgone.GetComponent<TileScript>().TileId, 
                                                                               PlayerScript.Instance.RedPlayerInfos.TileCentreZoneOrgone.GetComponent<TileScript>().Line,   
                                                                               false) : 
                                                 PlayerStatic.GetNeighbourDiag(PlayerScript.Instance.BluePlayerInfos.TileCentreZoneOrgone.GetComponent<TileScript>().TileId,
                                                                               PlayerScript.Instance.BluePlayerInfos.TileCentreZoneOrgone.GetComponent<TileScript>().Line,
                                                                               false);

        foreach(int i in OrgoneNeigh){
            if(!_tilesInRange.Contains(TilesManager.Instance.TileList[i]))
            {
                if(TilesManager.Instance.TileList[i].GetComponent<TileScript>().TileId >= 8 && TilesManager.Instance.TileList[i].GetComponent<TileScript>().TileId <= 72 &&
                   9 * (TilesManager.Instance.TileList[i].GetComponent<TileScript>().Line - 1) != TilesManager.Instance.TileList[i].GetComponent<TileScript>().TileId &&
                   (9 * TilesManager.Instance.TileList[i].GetComponent<TileScript>().Line) - 1 != TilesManager.Instance.TileList[i].GetComponent<TileScript>().TileId)
                {
                _tilesInRange.Add(TilesManager.Instance.TileList[i]);
                }


                List<int> newNeigh = PlayerStatic.GetNeighbourDiag(i, TilesManager.Instance.TileList[i].GetComponent<TileScript>().Line, false);

                foreach(int o in newNeigh)
                {
                    if(!_tilesInRange.Contains(TilesManager.Instance.TileList[o]))
                    {
                        if(TilesManager.Instance.TileList[o].GetComponent<TileScript>().TileId >= 8 && TilesManager.Instance.TileList[o].GetComponent<TileScript>().TileId <= 72 &&
                           9 * (TilesManager.Instance.TileList[o].GetComponent<TileScript>().Line - 1) != TilesManager.Instance.TileList[o].GetComponent<TileScript>().TileId &&
                           (9 * TilesManager.Instance.TileList[o].GetComponent<TileScript>().Line) - 1 != TilesManager.Instance.TileList[o].GetComponent<TileScript>().TileId)
                        {
                            _tilesInRange.Add(TilesManager.Instance.TileList[o]);
                        }
                    }
                }
            }
        }

        foreach(GameObject gam in _tilesInRange){
            gam.GetComponent<TileScript>().ActiveChildObj(MYthsAndSteel_Enum.ChildTileType.EventSelect);
        }
    }

    /// <summary>
    /// Quand le joueur lache le clic
    /// </summary>
    public void ReleaseZone(){
        if(RaycastManager.Instance.Tile == null || RaycastManager.Instance.Tile == _centerOrgoneArea || !_tilesInRange.Contains(RaycastManager.Instance.Tile))
        {
            CancelValidation();
        }
        else
        {
            //Case cibl� par le joueur
            _targetTile = RaycastManager.Instance.Tile;

            //Checks si la case est dans la liste des cases atteignables par la zone
            if(_tilesInRange.Contains(_targetTile)){
                _isInValidation = true;
                GameManager.Instance._eventCall += WhenValidate;
                GameManager.Instance._eventCallCancel += CancelValidation;
                if(PlayerPrefs.GetInt("Avertissement") == 0)
                {
                    GameManager.Instance._eventCall();
                }
                UIInstance.Instance.ShowValidationPanel("Zone d'orgone", "�tes-vous sur de vouloir d�placer votre zone d'orgone sur cette case? Toutes unit�s qui prend des d�g�ts ou perde de la vie dans cette zone vous fera gagner de l'orgone!");
            }
            else{
                transform.position = _centerOrgoneArea.transform.position;
            }
        }
    }

    /// <summary>
    /// Quand le joueur clique sur valider sur le panneau de validation
    /// </summary>
    public void WhenValidate(){
        SoundController.Instance.PlaySound(SoundController.Instance.AudioClips[13]);
        foreach (GameObject gam in _tilesInRange)
        {
            gam.GetComponent<TileScript>().DesActiveChildObj(MYthsAndSteel_Enum.ChildTileType.EventSelect);
        }

        List<int> neighZone = PlayerStatic.GetNeighbourDiag(_centerOrgoneArea.GetComponent<TileScript>().TileId, _centerOrgoneArea.GetComponent<TileScript>().Line, true);
        _centerOrgoneArea.GetComponent<TileScript>().TerrainEffectList.Remove(_redPlayerZone ? MYthsAndSteel_Enum.TerrainType.OrgoneRed : MYthsAndSteel_Enum.TerrainType.OrgoneBlue);

        foreach(int i in neighZone){
            TilesManager.Instance.TileList[i].GetComponent<TileScript>().TerrainEffectList.Remove(_redPlayerZone? MYthsAndSteel_Enum.TerrainType.OrgoneRed : MYthsAndSteel_Enum.TerrainType.OrgoneBlue);
        }

        if(GameManager.Instance.IsPlayerRedTurn) PlayerScript.Instance.RedPlayerInfos.TileCentreZoneOrgone = _targetTile;
        else PlayerScript.Instance.BluePlayerInfos.TileCentreZoneOrgone = _targetTile;

        _centerOrgoneArea = _targetTile;
        _targetTile = null;

        //Chercher les voisins de la case
        List<int> newNeighZone = PlayerStatic.GetNeighbourDiag(_centerOrgoneArea.GetComponent<TileScript>().TileId, _centerOrgoneArea.GetComponent<TileScript>().Line, true);
        newNeighZone.Add(_centerOrgoneArea.GetComponent<TileScript>().TileId);

        foreach(int i in newNeighZone)
        {
            TilesManager.Instance.TileList[i].GetComponent<TileScript>().TerrainEffectList.Add(_redPlayerZone ? MYthsAndSteel_Enum.TerrainType.OrgoneRed : MYthsAndSteel_Enum.TerrainType.OrgoneBlue);
        }

        newNeighZone.Clear();

        GameManager.Instance._eventCall -= WhenValidate;
        //A bouger la zone d'orgone
        _hasMoveOrgoneArea = true;
        _tilesInRange.Clear();

        _isInValidation = false;
        HideChild();

        Debug.Log("Orgone : OK (Gauthier)");
        GameManager.Instance.ChangePhase();
        //UIInstance.Instance.ActivateNextPhaseButton();
    }


    /// <summary>
    /// Quand le joueur clique sur cancel sur le panneau de validation
    /// </summary>
    public void CancelValidation(){
        transform.position = _centerOrgoneArea.transform.position;
        _lastTileInRange = null;
        UIInstance.Instance.ActivateNextPhaseButton();
        GameManager.Instance._eventCallCancel -= CancelValidation;

        foreach(GameObject gam in _tilesInRange)
        {
            gam.GetComponent<TileScript>().DesActiveChildObj(MYthsAndSteel_Enum.ChildTileType.EventSelect);
        }
        _tilesInRange.Clear();
        _targetTile = null;
        _isInValidation = false;
    }

    /// <summary>
    /// Active la zone d'orgone au d�but de la phase d'orgone
    /// </summary>
    public void ActivationArea(){
        _hasMoveOrgoneArea = false;
        _childGam.SetActive(true);
    }
    
    /// <summary>
    /// Rend invisible l'enfant qui permet de comprendre qu'il faut bouger la zone
    /// </summary>
    public void HideChild(){
        _childGam.SetActive(false);
    }
}