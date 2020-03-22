using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagConditionOr : ITagCondition
{
    public ITagCondition[] children;

    public TagConditionOr(params ITagCondition[] children)
    {
        this.children = children;
    }

    public bool Evaluate(Placeable placeable)
    {
        foreach(ITagCondition condition in children)
        {
            if (condition.Evaluate(placeable))
            {
                return true;
            }
        }
        return false;
    }
}
