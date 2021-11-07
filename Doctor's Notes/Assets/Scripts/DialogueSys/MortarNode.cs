using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarNode : BaseNode
{

    [Input] public int entry;
    [Output] public int exit;
    public string speakerName;
    public string dialogueLine;
    public string mortarName;
    public Sprite sprite;

    public override string GetString()
    {
        return "MortarNode/" + speakerName + "/" + dialogueLine + "/" + mortarName;
    }

    public override Sprite GetSprite()
    {
        return sprite;
    }
}