using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagConditionMustInclude : ITagCondition
{
    public PlaceableTag tag;

    public TagConditionMustInclude(PlaceableTag tag)
    {
        this.tag = tag;
    }

    public bool Evaluate(Placeable placeable)
    {
        return placeable.tags.Contains(tag);
    }
}
