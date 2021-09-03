using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryConditions : MonoBehaviour
{
    public int ObjectiveNeedRed;
    public int ObjectiveNeedBlue;

    public int ObjectiveRed;
    public int ObjectiveBlue;

    public int WinTurnForDefense;
    public bool RedDefends;

    public int MinTurnToWin;

    //0 = null, 1 = red, 2 = blue, 3 = both
    public int ChaineDeCommandement;
    bool RedHasCommand;
    bool BlueHasCommand;

    public void CheckVictory()
    {
        if (ChaineDeCommandement != 0)
        {


            foreach (GameObject unit in TilesManager.Instance.TileList)
            {
                if (unit.GetComponent<TileScript>().Unit.GetComponent<UnitScript>().UnitSO.IsInRedArmy && (unit.GetComponent<TileScript>().Unit.GetComponent<UnitScript>().UnitSO.typeUnite == MYthsAndSteel_Enum.TypeUnite.Leader || unit.GetComponent<TileScript>().Unit.GetComponent<UnitScript>().UnitSO.typeUnite == MYthsAndSteel_Enum.TypeUnite.Mythe || unit.GetComponent<TileScript>().Unit.GetComponent<UnitScript>().UnitSO.typeUnite == MYthsAndSteel_Enum.TypeUnite.Mecha))
                {
                    if (!RedHasCommand)
                    {
                        RedHasCommand = true;
                    }
                }
                if (!unit.GetComponent<TileScript>().Unit.GetComponent<UnitScript>().UnitSO.IsInRedArmy && (unit.GetComponent<TileScript>().Unit.GetComponent<UnitScript>().UnitSO.typeUnite == MYthsAndSteel_Enum.TypeUnite.Leader || unit.GetComponent<TileScript>().Unit.GetComponent<UnitScript>().UnitSO.typeUnite == MYthsAndSteel_Enum.TypeUnite.Mythe || unit.GetComponent<TileScript>().Unit.GetComponent<UnitScript>().UnitSO.typeUnite == MYthsAndSteel_Enum.TypeUnite.Mecha))
                {
                    if (!BlueHasCommand)
                    {
                        BlueHasCommand = true;
                    }
                }
            }
            CheckCommand();
        }
        if (ObjectiveNeedRed <= ObjectiveRed && ObjectiveNeedBlue > ObjectiveBlue && GameManager.Instance.ActualTurnNumber + 1  >= MinTurnToWin && ((ChaineDeCommandement == 0 || ChaineDeCommandement == 2) || ((ChaineDeCommandement == 1 || ChaineDeCommandement == 3) && RedHasCommand)))
        {
            VictoryForArmy(true);
        }
        else if(ObjectiveNeedRed > ObjectiveRed && ObjectiveNeedBlue <= ObjectiveBlue && GameManager.Instance.ActualTurnNumber + 1 >= MinTurnToWin && ((ChaineDeCommandement == 0 || ChaineDeCommandement == 1) || ((ChaineDeCommandement == 2 || ChaineDeCommandement == 3) && BlueHasCommand)))
        {
            VictoryForArmy(false);
        }
        else if(WinTurnForDefense == GameManager.Instance.ActualTurnNumber + 1)
        {
            if (RedDefends)
            {
                VictoryForArmy(true);
            }
            else
            {
                VictoryForArmy(false);
            }
        }
        if (PlayerScript.Instance.UnitRef.UnitListBluePlayer.Count == 0)
        {
            VictoryForArmy(true);
        }

        if (PlayerScript.Instance.UnitRef.UnitListRedPlayer.Count == 0)
        {
            VictoryForArmy(false);
        }
    }

    void CheckCommand()
    {
        if(ChaineDeCommandement == 1)
        {
            if (!RedHasCommand)
            {
                VictoryForArmy(false);
            }
        }
        if(ChaineDeCommandement == 2)
        {
            if (!BlueHasCommand)
            {
                VictoryForArmy(true);
            }
        }
        if(ChaineDeCommandement == 3)
        {
            if(!BlueHasCommand && RedHasCommand)
            {
                VictoryForArmy(true);
            }
            if (BlueHasCommand && !RedHasCommand)
            {
                VictoryForArmy(true);
            }
        }
    }

    void VictoryForArmy(bool RedWin)
    {
        if (RedWin)
        {
            GameManager.Instance.VictoryForArmy(1);
        }
        else
        {
            GameManager.Instance.VictoryForArmy(2);
        }
    }
}
