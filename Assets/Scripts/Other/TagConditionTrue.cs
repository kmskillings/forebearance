using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagConditionTrue : ITagCondition
{
    public bool Evaluate(Placeable placeable) { return true; }
}
