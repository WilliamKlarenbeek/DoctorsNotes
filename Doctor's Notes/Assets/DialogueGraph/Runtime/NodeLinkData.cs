using System; 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Holds information about connections between nodes

[Serializable]
public class NodeLinkData
{
    public string BaseNodeGuid;
    public string PortName;
    public string TargetNodeGuid; 
}
