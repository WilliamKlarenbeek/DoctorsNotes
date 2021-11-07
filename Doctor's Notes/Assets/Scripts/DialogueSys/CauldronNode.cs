using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class CauldronNode : BaseNode
{

    [Input] public int entry;
    [Output] public int exit;
    public string speakerName;
    public string dialogueLine;
    public string cauldronName;
    public Sprite sprite;

    public override string GetString()
    {
        return "CauldronNode/" + speakerName + "/" + dialogueLine + "/" + cauldronName;
    }

    public override Sprite GetSprite()
    {
        return sprite;
    }
}