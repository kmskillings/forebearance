using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Placeable
{
    public string name;

    public List<PlaceableTag> tags;

    public Placeable(string name)
    {
        this.name = name;
        tags = new List<PlaceableTag>();
    }

    public void AddTag(PlaceableTag tag)
    {
        if (tags.Contains(tag)) return; //Return immediately if this Placeable already has this tag. This avoids duplicating tags, as well as infinite loops.
        
        tags.Add(tag);  //Adds the tag to the actual list

        foreach(PlaceableTag impliedTag in tag.implies)
        {
            AddTag(impliedTag); //Recursively adds all the implied tags
        }

        foreach(PlaceableTag excludedTag in tag.excludes)
        {
            if (tags.Contains(excludedTag)) //Throws a warning if there are any excluded tags. This can be useful in some cases, but usually is a problem.
            {
                Debug.LogWarning("Warning: Placeable '" + name + "' has the tag '" + excludedTag.name + "' which is excluded by '" + tag.name + "'.");
            }
        }
    }
}