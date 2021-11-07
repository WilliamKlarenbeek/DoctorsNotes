using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookTabNode : BaseNode
{

    [Input] public int entry;
    [Output] public int exit;
    public string speakerName;
    public string dialogueLine;
    public string bookName;
    public string bookTabNum;
    public Sprite sprite;

    public override string GetString()
    {
        return "BookTabNode/" + speakerName + "/" + dialogueLine + "/" + bookName + "/" + bookTabNum;
    }

    public override Sprite GetSprite()
    {
        return sprite;
    }
}