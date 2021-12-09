using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fumigene : TerrainParent
{
    bool cibledIsImmune;
    public override void CibledByAttack(UnitScript AttackerUnit, TileScript AttackerUnitCase)
    {
        if (!RaycastManager.Instance.Tile.GetComponent<TileScript>().Unit.GetComponent<UnitScript>().Volant || !RaycastManager.Instance.Tile.GetComponent<TileScript>().Unit.GetComponent<UnitScript>().ToutTerrain)
        {
            AttackerUnit.DiceBonus += -3;
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
            Unit.DiceBonus += 3;
        }
        Attaque.Instance._JaugeAttack.SynchAttackBorne(Unit);
        cibledIsImmune = false;
        base.UnCibledByAttack(Unit);
    }

    public override void OnUnityAdd(UnitScript Unit)
    {
        Unit.DiceBonus += -3;
        base.OnUnityAdd(Unit);
    }

    public override void OnUnityDown(UnitScript Unit)
    {
        Unit.DiceBonus += +3;
        base.OnUnityDown(Unit);
    }
}
