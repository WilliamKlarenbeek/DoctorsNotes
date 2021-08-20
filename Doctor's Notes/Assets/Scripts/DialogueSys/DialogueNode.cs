using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class DialogueNode : BaseNode {

    [Input] public int entry;
    [Output] public int exit;
    public string speakerName;
    public string dialogueLine;
    public Sprite sprite;

    public override string GetString()
    {
        return "DialogueNode/" + speakerName + "/" + dialogueLine;
    }

    public override Sprite GetSprite()
    {
        return sprite;
    }
}