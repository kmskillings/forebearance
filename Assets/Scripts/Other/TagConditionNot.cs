using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagConditionNot : ITagCondition
{
    public ITagCondition child;
    
    public TagConditionNot(ITagCondition condition)
    {
        child = condition;
    }

    public bool Evaluate(Placeable placeable)
    {
        return !child.Evaluate(placeable);
    }
}
