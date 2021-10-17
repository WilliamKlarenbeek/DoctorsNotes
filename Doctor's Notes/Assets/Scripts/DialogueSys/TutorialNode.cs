using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class TutorialNode : BaseNode
{

    [Input] public int entry;
    [Output] public int exit;
    public string speakerName;
    public string dialogueLine;
    public Sprite sprite;
    public GameObject selectedObject;

    public override string GetString()
    {
        return "TutorialNode/" + speakerName + "/" + dialogueLine + '/' + selectedObject.name;
    }

    public override Sprite GetSprite()
    {
        return sprite;
    }
}