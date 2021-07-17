using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HauteColline : TerrainParent
{
    public bool cibled = false;
    public bool cibled2 = false;
    bool cibledIsImmune;

    public override int AttackRangeValue(int i = 0)
    {
        if (!RaycastManager.Instance.ActualUnitSelected.GetComponent<UnitScript>().Volant)
        {
            i = 1;
        }
        return base.AttackRangeValue(i);
    }

    public override void CibledByAttack(UnitScript AttackerUnit, TileScript AttackerUnitCase)
    {
        if (Mouvement.Instance._selectedTileId.Count > 0)
        {
            if (!TilesManager.Instance.TileList[Mouvement.Instance._selectedTileId[Mouvement.Instance._selectedTileId.Count - 1]].GetComponent<TileScript>().TerrainEffectList.Contains(MYthsAndSteel_Enum.TerrainType.Haute_colline) && !TilesManager.Instance.TileList[Mouvement.Instance._selectedTileId[Mouvement.Instance._selectedTileId.Count - 1]].GetComponent<TileScript>().TerrainEffectList.Contains(MYthsAndSteel_Enum.TerrainType.Colline))
            {
                cibled = true;
                if (!RaycastManager.Instance.Tile.GetComponent<TileScript>().Unit.GetComponent<UnitScript>().Volant)
                {
                    AttackerUnit.DiceBonus += -3;
                }
                else
                {
                    cibledIsImmune = true;
                }
                Attaque.Instance._JaugeAttack.SynchAttackBorne(AttackerUnit);
            }
        }
        else
        {
            if (!TilesManager.Instance.TileList[AttackerUnit.ActualTiledId].GetComponent<TileScript>().TerrainEffectList.Contains(MYthsAndSteel_Enum.TerrainType.Haute_colline) && !TilesManager.Instance.TileList[AttackerUnit.ActualTiledId].GetComponent<TileScript>().TerrainEffectList.Contains(MYthsAndSteel_Enum.TerrainType.Colline))
            {
                cibled2 = true;
                if (!RaycastManager.Instance.Tile.GetComponent<TileScript>().Unit.GetComponent<UnitScript>().Volant)
                {
                    AttackerUnit.DiceBonus += -1;
                }
                else
                {
                    cibledIsImmune = true;
                }
                Attaque.Instance._JaugeAttack.SynchAttackBorne(AttackerUnit);
            }

        }
        base.CibledByAttack(AttackerUnit, AttackerUnitCase);
    }

    public override void UnCibledByAttack(UnitScript Unit)
    {

        if (cibled)
        {
            cibled = false;
            if (!cibledIsImmune)
            {
                Unit.DiceBonus += 3;
            }
            Attaque.Instance._JaugeAttack.SynchAttackBorne(Unit);
        }
        else if (cibled2)
        {
            cibled2 = false;
            if (!cibledIsImmune)
            {
                Unit.DiceBonus += 1;
            }
            Attaque.Instance._JaugeAttack.SynchAttackBorne(Unit);
        }
        base.UnCibledByAttack(Unit);
        cibledIsImmune = false;
    }

}
