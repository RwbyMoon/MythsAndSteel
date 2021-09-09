using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTurn : MonoBehaviour
{
    [Header("Nombre d'objectif à capturer pour la victoire par équipe.")]
    [SerializeField] private int RedObjCount;
    [SerializeField] private int BlueObjCount;

    [Header("Nombre d'objectif à capturer pour la victoire par équipe Rouge.")]
    [SerializeField] List<GameObject> RedgoalTileList = new List<GameObject>();

    [Header("Nombre d'objectif à capturer pour la victoire par l'équipe Bleu.")]
    [SerializeField] List<GameObject> BluegoalTileList = new List<GameObject>();
    [Space]
    [SerializeField] private GameObject AnimResourcesRed;
    [SerializeField] private GameObject AnimResourcesBlue;
    private void Start()
    {
        foreach (GameObject Tile in TilesManager.Instance.TileList)
        {
            if (Tile.GetComponent<TileScript>().TerrainEffectList.Contains(MYthsAndSteel_Enum.TerrainType.Point_Objectif_Rouge))
            {
                RedgoalTileList.Add(Tile);
            }
        }
        foreach (GameObject Tile in TilesManager.Instance.TileList)
        {
            if (Tile.GetComponent<TileScript>().TerrainEffectList.Contains(MYthsAndSteel_Enum.TerrainType.Point_Objectif_Bleu))
            {
                BluegoalTileList.Add(Tile);
            }
        }
        GameManager.Instance.ManagerSO.GoToStrategyPhase += EndTerrainEffect;
        GameManager.Instance.ManagerSO.GoToStrategyPhase += CheckResources;
        GameManager.Instance.ManagerSO.GoToStrategyPhase += CheckVictory;
      
    }

    public void EndTerrainEffect()
    {
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
                                                    TerrainGestion.Instance.EndTurn(Try3, TS.GetComponent<TileScript>());
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (Type.Child.TryGetComponent<TerrainParent>(out TerrainParent Try))
                                    {
                                        TerrainGestion.Instance.EndTurn(Try, TS.GetComponent<TileScript>());
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    


    /// <summary>
    /// Check si des ressources doivent être distribuées.
    /// </summary>
    public void CheckResources()
    {

        Debug.Log("Ressources");
        foreach (GameObject Tile in TilesManager.Instance.ResourcesList)
        {
            TileScript S = Tile.GetComponent<TileScript>();
            

            IEnumerator WaitAnimResourceRed(float waitTime)
            {
                yield return new WaitForSeconds(waitTime);
                AnimResourcesRed.SetActive(false);
            }

            IEnumerator WaitAnimResourceBlue(float waitTime)
            {
                yield return new WaitForSeconds(waitTime);
                AnimResourcesBlue.SetActive(false);
            }

            if (S.ResourcesCounter != 0)
            {
                if (S.Unit != null && !S.Unit.GetComponent<UnitScript>().DoubleRessource)
                {
                    UnitScript US = S.Unit.GetComponent<UnitScript>();
                    if(GameManager.Instance.VolDeRavitaillementStat != 3)
                    {
                       if(S.Unit.GetComponent<UnitScript>().UnitSO.IsInRedArmy && GameManager.Instance.VolDeRavitaillementStat == 2)
                       {
                            SoundController.Instance.PlaySound(SoundController.Instance.AudioClips[9]);
                            S.RemoveRessources(1, 2);
                            AnimResourcesBlue.SetActive(true);

                            StartCoroutine(WaitAnimResourceRed(1.5f));

                        }
                       else if (!S.Unit.GetComponent<UnitScript>().UnitSO.IsInRedArmy && GameManager.Instance.VolDeRavitaillementStat == 1)
                       {

                            SoundController.Instance.PlaySound(SoundController.Instance.AudioClips[9]);
                            S.RemoveRessources(1, 1);
                            AnimResourcesRed.SetActive(true);
                            StartCoroutine(WaitAnimResourceRed(1.5f));

                       }
                       else
                       {
                            SoundController.Instance.PlaySound(SoundController.Instance.AudioClips[9]);
                            S.RemoveRessources(1, PlayerStatic.CheckIsUnitArmy(US.GetComponent<UnitScript>(), true) == true ? 1 : 2);
                            AnimResourcesRed.SetActive(true);
                            StartCoroutine(WaitAnimResourceRed(1.5f));

                       }
                    }
                    else
                    {
                        SoundController.Instance.PlaySound(SoundController.Instance.AudioClips[9]);
                        S.RemoveRessources(1, PlayerStatic.CheckIsUnitArmy(US.GetComponent<UnitScript>(), true) == true ? 1 : 2);
                        AnimResourcesBlue.SetActive(true);
                        StartCoroutine(WaitAnimResourceBlue(1.5f));

                    }
                }
                if (S.Unit != null && S.Unit.GetComponent<UnitScript>().DoubleRessource)
                {
                    UnitScript US = S.Unit.GetComponent<UnitScript>();
                    if (GameManager.Instance.VolDeRavitaillementStat != 3)
                    {
                        if (S.Unit.GetComponent<UnitScript>().UnitSO.IsInRedArmy && GameManager.Instance.VolDeRavitaillementStat == 2)
                        {
                            SoundController.Instance.PlaySound(SoundController.Instance.AudioClips[9]);
                            S.RemoveRessources(2, 2);
                            AnimResourcesBlue.SetActive(true);

                            StartCoroutine(WaitAnimResourceRed(1.5f));

                        }
                        else if (!S.Unit.GetComponent<UnitScript>().UnitSO.IsInRedArmy && GameManager.Instance.VolDeRavitaillementStat == 1)
                        {

                            SoundController.Instance.PlaySound(SoundController.Instance.AudioClips[9]);
                            S.RemoveRessources(2, 1);
                            AnimResourcesRed.SetActive(true);
                            StartCoroutine(WaitAnimResourceRed(1.5f));

                        }
                        else
                        {
                            SoundController.Instance.PlaySound(SoundController.Instance.AudioClips[9]);
                            S.RemoveRessources(2, PlayerStatic.CheckIsUnitArmy(US.GetComponent<UnitScript>(), true) == true ? 1 : 2);
                            AnimResourcesRed.SetActive(true);
                            StartCoroutine(WaitAnimResourceRed(1.5f));

                        }
                    }
                    else
                    {
                        SoundController.Instance.PlaySound(SoundController.Instance.AudioClips[9]);
                        S.RemoveRessources(2, PlayerStatic.CheckIsUnitArmy(US.GetComponent<UnitScript>(), true) == true ? 1 : 2);
                        AnimResourcesBlue.SetActive(true);
                        StartCoroutine(WaitAnimResourceBlue(1.5f));

                    }
                }
            }

            if (S.ResourcesCounter == 0)
            {
                S.RemoveEffect(MYthsAndSteel_Enum.TerrainType.Point_de_ressource);
                S.TerrainEffectList.Remove(MYthsAndSteel_Enum.TerrainType.Point_de_ressource);
                S.TerrainEffectList.Add(MYthsAndSteel_Enum.TerrainType.Point_de_ressources_vide);
              
            }
        }
        GameManager.Instance.VolDeRavitaillementStat = 3;
    }

    /// <summary>
    /// Prend les nouveaux propriétaires des objectifs et check ensuite les conditions de victoire.
    /// </summary>
    public void CheckOwner()
    {
        foreach (GameObject Tile in RedgoalTileList)
        {
            TileScript S = Tile.GetComponent<TileScript>();
            if (S.TerrainEffectList.Contains(MYthsAndSteel_Enum.TerrainType.Point_Objectif_Rouge))
            {
                if (S.Unit != null)
                {
                    UnitScript US = S.Unit.GetComponent<UnitScript>();
                    if (US.UnitSO.IsInRedArmy && !US.UnitStatuts.Contains(MYthsAndSteel_Enum.UnitStatut.PeutPasPrendreDesObjectifs))
                    {
                        Debug.Log("Objectif dans le camp rouge.");
                        //ChangeOwner(S, true);
                        PlayerScript.Instance.J1Infos.GoalCapturePointsNumber++;
                    }
                    else if (!US.UnitSO.IsInRedArmy && !US.UnitStatuts.Contains(MYthsAndSteel_Enum.UnitStatut.PeutPasPrendreDesObjectifs))
                    {
                        Debug.Log("Objectif dans le camp bleu.");
                        //ChangeOwner(S, false);
                    }
                }
            }
        }

        foreach (GameObject Tile in BluegoalTileList)
        {
            TileScript S = Tile.GetComponent<TileScript>();
            if (S.TerrainEffectList.Contains(MYthsAndSteel_Enum.TerrainType.Point_Objectif_Bleu))
            {
                if (S.Unit != null)
                {
                    UnitScript US = S.Unit.GetComponent<UnitScript>();
                    if (!US.UnitSO.IsInRedArmy && !US.UnitStatuts.Contains(MYthsAndSteel_Enum.UnitStatut.PeutPasPrendreDesObjectifs))
                    {
                        Debug.Log("Objectif dans le camp rouge.");
                        //ChangeOwner(S, true);
                        PlayerScript.Instance.J2Infos.GoalCapturePointsNumber++;
                    }
                    else if (US.UnitSO.IsInRedArmy && !US.UnitStatuts.Contains(MYthsAndSteel_Enum.UnitStatut.PeutPasPrendreDesObjectifs))
                    {
                        Debug.Log("Objectif dans le camp bleu.");
                        //ChangeOwner(S, false);
                    }
                }
            }
        }
        CheckVictory();
    }
    
    /// <summary>
    /// Change le propriétaire d'un objectif.
    /// </summary>
    /// <param name="TileSc"></param>
    /// <param name="RedArmy"></param>
    void ChangeOwner(TileScript TileSc, bool RedArmy)
    {
        if (TileSc.OwnerObjectiv == MYthsAndSteel_Enum.Owner.blue && RedArmy)
        {
            PlayerScript.Instance.J2Infos.GoalCapturePointsNumber--;
            TileSc.ChangePlayerObj(MYthsAndSteel_Enum.Owner.red);
            PlayerScript.Instance.J1Infos.GoalCapturePointsNumber++;
        }
        if (TileSc.OwnerObjectiv == MYthsAndSteel_Enum.Owner.red && !RedArmy)
        {
            PlayerScript.Instance.J1Infos.GoalCapturePointsNumber--;
            TileSc.ChangePlayerObj(MYthsAndSteel_Enum.Owner.blue);
            PlayerScript.Instance.J2Infos.GoalCapturePointsNumber++;
        }
        if (TileSc.OwnerObjectiv == MYthsAndSteel_Enum.Owner.neutral)
        {
            if (RedArmy)
            {
                TileSc.ChangePlayerObj(MYthsAndSteel_Enum.Owner.red);
                PlayerScript.Instance.J1Infos.GoalCapturePointsNumber++;
            }
            else
            {
                TileSc.ChangePlayerObj(MYthsAndSteel_Enum.Owner.blue);
                PlayerScript.Instance.J2Infos.GoalCapturePointsNumber++;
            }
        }
    }

    
    /// <summary>
    /// Called by CheckOwner();
    /// </summary>
    protected void CheckVictory()
    {
        VictoryConditions.Instance.CheckVictoryConditions();
    }
}
