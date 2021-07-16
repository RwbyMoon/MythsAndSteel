using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bosquet : TerrainParent
{
    bool cibledUnitHasToutTerrain;
    public override void CibledByAttack(UnitScript AttackerUnit, TileScript AttackerUnitCase)
    {
        if (!RaycastManager.Instance.Tile.GetComponent<TileScript>().Unit.GetComponent<UnitScript>().ToutTerrain)
        {
            AttackerUnit.DiceBonus += -1;
        }
        else
        {
            cibledUnitHasToutTerrain = true;
        }
        Attaque.Instance._JaugeAttack.SynchAttackBorne(AttackerUnit);
        base.CibledByAttack(AttackerUnit, AttackerUnitCase);
    }

    public override void UnCibledByAttack(UnitScript Unit)
    {
        if (!cibledUnitHasToutTerrain)
        {
            Unit.DiceBonus += 1;
        }
        Attaque.Instance._JaugeAttack.SynchAttackBorne(Unit);
        cibledUnitHasToutTerrain = false;
        base.UnCibledByAttack(Unit);
    }
}
