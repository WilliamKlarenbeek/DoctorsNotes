using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateTwoDialogue : BaseNode
{

    [Input] public int entry;
    [Output] public int exit;
    public string speakerName;
    public string dialogueLine;
    public string objectName;
    public string secondObjectName;
    public Sprite sprite;

    public override string GetString()
    {
        return "InstantiateTwoNode/" + speakerName + "/" + dialogueLine + "/" + objectName + "/" + secondObjectName;
    }

    public override Sprite GetSprite()
    {
        return sprite;
    }
}