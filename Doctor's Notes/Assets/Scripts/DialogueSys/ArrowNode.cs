using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowNode : BaseNode
{

    [Input] public int entry;
    [Output] public int exit;
    public string speakerName;
    public string dialogueLine;
    public string bookName;
    public Sprite sprite;

    public override string GetString()
    {
        return "ArrowNode/" + speakerName + "/" + dialogueLine + "/" + bookName;
    }

    public override Sprite GetSprite()
    {
        return sprite;
    }
}
