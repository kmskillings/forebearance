using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagConditionAnd : ITagCondition
{
    public ITagCondition[] children;

    public TagConditionAnd(params ITagCondition[] children)
    {
        this.children = children;
    }

    public bool Evaluate(Placeable placeable)
    {
        bool returnValue = true;
        foreach(ITagCondition condition in children)
        {
            if (!condition.Evaluate(placeable))
            {
                returnValue = false;
            }
        }
        return returnValue;
    }
}
