using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bosquet : TerrainParent
{
    bool cibledIsImmune;
    public override void CibledByAttack(UnitScript AttackerUnit, TileScript AttackerUnitCase)
    {
        if (!RaycastManager.Instance.Tile.GetComponent<TileScript>().Unit.GetComponent<UnitScript>().Volant)
        {
            AttackerUnit.DiceBonus += -1;
        }
        else
        {
            cibledIsImmune = true;
        }
        Attaque.Instance._JaugeAttack.SynchAttackBorne(AttackerUnit);
        base.CibledByAttack(AttackerUnit, AttackerUnitCase);
    }

    public override void UnCibledByAttack(UnitScript Unit)
    {
        if (!cibledIsImmune)
        {
            Unit.DiceBonus += 1;
        }
        Attaque.Instance._JaugeAttack.SynchAttackBorne(Unit);
        cibledIsImmune = false;
        base.UnCibledByAttack(Unit);
    }
}
