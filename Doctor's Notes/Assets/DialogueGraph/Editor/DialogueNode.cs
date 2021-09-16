using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor; 
using UnityEditor.Experimental.GraphView;

/* DialogueNode.cs v 1.0 
// Node identification data 
*/

public class DialogueNode : Node
{
    // unique ID used to distinguish each node
    public string GUID; 

    public string DialogueText; 

    public bool EntryPoint = false; 
}
