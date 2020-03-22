using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITagCondition
{
    bool Evaluate(Placeable placeable);
}