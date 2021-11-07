using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateNode : BaseNode
{

    [Input] public int entry;
    [Output] public int exit;
    public string speakerName;
    public string dialogueLine;
    public string objectName;
    public Sprite sprite;

    public override string GetString()
    {
        return "InstantiateNode/" + speakerName + "/" + dialogueLine + "/" + objectName;
    }

    public override Sprite GetSprite()
    {
        return sprite;
    }
}