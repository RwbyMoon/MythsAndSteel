using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colline : TerrainParent
{
    public override int AttackRangeValue(int i = 0)
    {
        i = 1;
        return base.AttackRangeValue(i);
    }
}
