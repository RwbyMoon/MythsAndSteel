using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Romel : Capacity
{

    GameObject usine;
    [SerializeField] Unit_SO TransformationRomel;
    [SerializeField] RuntimeAnimatorController TransformationRomelAnimator;
    [SerializeField] SpriteRenderer TransformationRomelsprite; 
    public override void StartCpty()
    {
        int ressourcePlayer = 0;
        if (!gameObject.GetComponent<UnitScript>().UnitStatuts.Contains(MYthsAndSteel_Enum.UnitStatut.Poss�d�))
        {
            ressourcePlayer = GetComponent<UnitScript>().UnitSO.IsInRedArmy ? PlayerScript.Instance.J1Infos.Ressource : PlayerScript.Instance.J2Infos.Ressource;
        }
        else if (gameObject.GetComponent<UnitScript>().UnitStatuts.Contains(MYthsAndSteel_Enum.UnitStatut.Poss�d�))
        {
            ressourcePlayer = GetComponent<UnitScript>().UnitSO.IsInRedArmy ? PlayerScript.Instance.J2Infos.Ressource : PlayerScript.Instance.J1Infos.Ressource;
        }
        if (ressourcePlayer >= Capacity1Cost)
        {
            List<GameObject> tilelist = new List<GameObject>();
            if (GameManager.Instance.IsPlayerRedTurn)
            {
              

                    foreach (GameObject gam in TilesManager.Instance.TileList)
                    {
                        if (gam.GetComponent<TileScript>().TerrainEffectList.Contains(MYthsAndSteel_Enum.TerrainType.UsineRouge))
                        {
                            usine = gam;
                            break;
                        }
                    }
                
 
            }
            else
            {
             

                    foreach (GameObject gam in TilesManager.Instance.TileList)
                    {
                        if (gam.GetComponent<TileScript>().TerrainEffectList.Contains(MYthsAndSteel_Enum.TerrainType.UsineBleu))
                        {
                            usine = gam;
                            break;
                        }
                    }
                
           
            }

            List<int> unitNeigh = PlayerStatic.GetNeighbourDiag(usine.GetComponent<TileScript>().TileId, TilesManager.Instance.TileList[usine.GetComponent<TileScript>().TileId].GetComponent<TileScript>().Line, false);
            foreach (int i in unitNeigh)
            {
                if (GetComponent<UnitScript>().ActualTiledId == i)
                {
                    GameManager.Instance._eventCall += EndCpty;
                    if (PlayerPrefs.GetInt("Avertissement") == 0)
                    {

                        GameManager.Instance._eventCall();

                    }

                    UIInstance.Instance.ShowValidationPanel(Capacity1Name, "�tes-vous s�r de vouloir utiliser la transformation de Romel ?");
                    break;
                }
                //tilelist.Add(TilesManager.Instance.TileList[i]);
            }
        }
    }

    public override void EndCpty()
    {
        Debug.Log("test");
        UIInstance.Instance.ActivateNextPhaseButton();
        GetComponent<Animator>().runtimeAnimatorController = null;
        if (TransformationRomel != null) GetComponent<UnitScript>().UnitSO = TransformationRomel;
       
        GetComponent<UnitScript>().UpdateUnitStat();

        if (GetComponent<UnitScript>().UnitSO.IsInRedArmy)
        {

       
                PlayerScript.Instance.J1Infos.Ressource -= Capacity1Cost;
       
        }
        else
        {
           
                PlayerScript.Instance.J2Infos.Ressource -= Capacity1Cost;
            
   
        }
        GetComponent<Animator>().runtimeAnimatorController = TransformationRomelAnimator;
        GetComponent<UnitScript>().UpdateLifeHeartShieldUI(GameManager.Instance.IsPlayerRedTurn ? UIInstance.Instance.J1HeartSprite : UIInstance.Instance.J2HeartSprite,GetComponent<UnitScript>().Life);
     
        Destroy(GetComponent<Romel>());
    }

    public override void StopCpty()
    {

    }
}
