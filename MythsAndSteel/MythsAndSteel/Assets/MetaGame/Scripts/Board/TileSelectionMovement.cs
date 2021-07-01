using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TileSelectionMovement : MonoSingleton<TileSelectionMovement>
{

    #region Variables
    //Est ce que l'objet a fait le déplacement
    bool _hasMakeMovement = false;

    //Est ce que l'objet a fait le déplacement
    [SerializeField] private bool _isVisble = false;    

    //SpriteRenderer de l'objet
    SpriteRenderer _spritRender => GetComponent<SpriteRenderer>();
    public SpriteRenderer SpriteRendererCursor => _spritRender;

    [Range(15,40)]
    [SerializeField] private float _speedTiles = 0f;

    //Objet ressource
    [SerializeField] private Animator RessourceAnimator;
    [SerializeField] private GameObject RessourceObject;

    //Valeur ressource
    [SerializeField] private TextMeshProUGUI Value;
    private Coroutine Last;
    #endregion Variables

    private void Start(){
        RaycastManager.Instance.OnTileChanged += TileChange;
        GetComponent<Animator>().SetInteger("Fade", 1);
        _isVisble = false;
    }

    void Update()
    {
        //Bouge la zone de selection jusqu'à la tiles sous le raycast
        if(!_hasMakeMovement){
            if(RaycastManager.Instance.Tile != null){
                if(_isVisble){
                    transform.position = Vector3.MoveTowards(transform.position,
                    new Vector3(RaycastManager.Instance.Tile.transform.position.x, RaycastManager.Instance.Tile.transform.position.y, transform.position.z),
                    _speedTiles * Vector2.Distance(transform.position, RaycastManager.Instance.Tile.transform.position) * Time.deltaTime);
                }
                else{
                    transform.position = RaycastManager.Instance.Tile.transform.position;
                }
            }
        }
    }

    /// <summary>
    /// A chaque fois que la tile en dessous de la souris change
    /// </summary>
    public void TileChange()
    {
        //Change l'objet de tiles où il doit se déplacer
        if(RaycastManager.Instance.Tile != null && GameManager.Instance.IsInTurn && GameManager.Instance.activationDone == false){
            //Est ce que l'objet est visible
            _isVisble = true;
            GetComponent<Animator>().SetInteger("Fade", 2);
            if (RaycastManager.Instance.Tile.GetComponent<TileScript>().TerrainEffectList.Contains(MYthsAndSteel_Enum.TerrainType.Point_de_ressource))
            {
                if (RessourceAnimator.GetBool("In"))
                {
                    RessourceAnimator.SetBool("In", false);
                    Last = StartCoroutine(Wait());
                }
                else
                {
                    if(Last != null)
                    {
                        StopCoroutine(Last);
                    }
                    RessourceAnimator.SetBool("In", true);
                    RessourceObject.transform.position = new Vector3(RaycastManager.Instance.Tile.transform.position.x, RaycastManager.Instance.Tile.transform.position.y + .5f, -25);
                    Value.text = RaycastManager.Instance.Tile.GetComponent<TileScript>().ResourcesCounter.ToString();
                }
            }
            else
            {
                if (Last != null)
                {
                    StopCoroutine(Last);
                }
                RessourceAnimator.SetBool("In", false);
            }
            _hasMakeMovement = false;
        }
        else 
        {
            if (Last != null)
            {
                StopCoroutine(Last);
            }
            GetComponent<Animator>().SetInteger("Fade", 1);
            RessourceAnimator.SetBool("In", false);
            _isVisble = false;
        }

        //Si le joueur survole le plateau
        if(RaycastManager.Instance.UnitInTile != null && (GameManager.Instance.ActualTurnPhase == MYthsAndSteel_Enum.PhaseDeJeu.ActionJ1 || GameManager.Instance.ActualTurnPhase == MYthsAndSteel_Enum.PhaseDeJeu.ActionJ2) 
           && GameManager.Instance.ChooseUnitForEvent == false && GameManager.Instance.ChooseTileForEvent == false)
        {
            if(RaycastManager.Instance.UnitInTile.GetComponent<UnitScript>().UnitSO.IsInRedArmy == GameManager.Instance.IsPlayerRedTurn || 
               RaycastManager.Instance.UnitInTile.GetComponent<UnitScript>().UnitStatuts.Contains(MYthsAndSteel_Enum.UnitStatut.Possédé) == true){
                GetComponent<Animator>().SetBool("HasUnit",  true);
            }
            else{
                GetComponent<Animator>().SetBool("HasUnit", false);
            }
        }
        //Si le joueur doit choisir une unité
        else if(GameManager.Instance.ChooseUnitForEvent == true){
            if(RaycastManager.Instance.UnitInTile != null){

                if(GameManager.Instance.SelectableUnit.Contains(RaycastManager.Instance.UnitInTile)){
                    GetComponent<Animator>().SetBool("HasUnit", true);
                }
                else
                {
                    GetComponent<Animator>().SetBool("HasUnit", false);
                }
            }
            else{
                GetComponent<Animator>().SetBool("HasUnit", false);
            }
        }
        //Si le joueur doit choisir une case
        else if(GameManager.Instance.ChooseTileForEvent == true){
            if(GameManager.Instance._selectableTiles.Contains(RaycastManager.Instance.Tile)){
                GetComponent<Animator>().SetBool("HasUnit", true);
            }
            else{
                GetComponent<Animator>().SetBool("HasUnit", false);
            }
        }
        else{
            GetComponent<Animator>().SetBool("HasUnit", false);
        }

    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(.14f);
        RessourceObject.transform.position = new Vector3(RaycastManager.Instance.Tile.transform.position.x, RaycastManager.Instance.Tile.transform.position.y + .5f, -25);
        Value.text = RaycastManager.Instance.Tile.GetComponent<TileScript>().ResourcesCounter.ToString();        
        RessourceAnimator.SetBool("In", true);
    }
}
