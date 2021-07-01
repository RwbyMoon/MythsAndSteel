using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plage : TerrainParent
{
    public override void CibledByAttack(UnitScript AttackerUnit, TileScript AttackerUnitCase)
    {
        AttackerUnit.DiceBonus += -1;
        Attaque.Instance._JaugeAttack.SynchAttackBorne(AttackerUnit);
        base.CibledByAttack(AttackerUnit, AttackerUnitCase);
    }

    public override void UnCibledByAttack(UnitScript Unit)
    {
        Unit.DiceBonus += 1;
        Attaque.Instance._JaugeAttack.SynchAttackBorne(Unit);
        base.UnCibledByAttack(Unit);
    }
}
