using UnityEngine;

public class TreeData
{
    // These 'public' variables make up our tree data schema
    public string treeFamilyName;
    public int treeSize;
    public int treeColour;

    public string Stringify() 
    {
        return JsonUtility.ToJson(this);
    }

    public static TreeData Parse(string json)
    {
        return JsonUtility.FromJson<TreeData>(json);
    }
}