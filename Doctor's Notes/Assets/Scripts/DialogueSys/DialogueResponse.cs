using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class DialogueResponseNode : BaseNode {
    [Input] public int entry;
    [Output] public int exit1;
    [Output] public int exit2; 
    public string speakerName;
    public string dialogueLine;
    public Sprite sprite;

    public override string GetString()
    {
        return "DialogueResponseNode/" + speakerName + "/" + dialogueLine;
    }

    public override Sprite GetSprite()
    {
        return sprite;
    }
}