using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialNode : BaseNode
{
    [Input] public int entry;
    [Output] public int exit;
    public string speakerName;
    public string dialogueLine;
    public string functionName;
    public string objectType;
    public Sprite sprite;

    public override string GetString()
    {
        return "TutorialNode/" + speakerName + "/" + dialogueLine + "/" + functionName + "/" + objectType;
    }

    public override Sprite GetSprite()
    {
        return sprite;
    }
}
