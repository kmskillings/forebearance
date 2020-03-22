using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlaceableTag
{
    public PlaceableTag[] implies;
    public PlaceableTag[] excludes;

    public string name;

    public PlaceableTag(string name)
    {
        this.name = name;
    }

    public void SetImplies(PlaceableTag[] tags)
    {
        implies = tags;
    }

    public void SetExcludes(PlaceableTag[] tags)
    {
        excludes = tags;
    }

    public static PlaceableTag tagToken;
    public static PlaceableTag tagSkillToken;
    public static PlaceableTag tagRedSkillToken;
    public static PlaceableTag tagGreenSkillToken;
    public static PlaceableTag tagBlueSkillToken;

    static PlaceableTag()
    {
        tagToken = new PlaceableTag("token");
        tagSkillToken = new PlaceableTag("token_skill");
        tagRedSkillToken = new PlaceableTag("token_skill_red");
        tagGreenSkillToken = new PlaceableTag("token_skill_green");
        tagBlueSkillToken = new PlaceableTag("token_skill_blue");

        tagToken.SetImplies(new PlaceableTag[] { });
        tagToken.SetExcludes(new PlaceableTag[] { });

        tagSkillToken.SetImplies(new PlaceableTag[] { tagToken });
        tagSkillToken.SetExcludes(new PlaceableTag[] { });

        tagRedSkillToken.SetImplies(new PlaceableTag[] { tagSkillToken });
        tagRedSkillToken.SetExcludes(new PlaceableTag[] { tagGreenSkillToken, tagBlueSkillToken });

        tagGreenSkillToken.SetImplies(new PlaceableTag[] { tagSkillToken });
        tagGreenSkillToken.SetExcludes(new PlaceableTag[] { tagRedSkillToken, tagBlueSkillToken });

        tagBlueSkillToken.SetImplies(new PlaceableTag[] { tagSkillToken });
        tagBlueSkillToken.SetExcludes(new PlaceableTag[] { tagRedSkillToken, tagGreenSkillToken });
    }
}
