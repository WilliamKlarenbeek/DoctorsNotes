using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using UnityEditor; 
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

/* GraphSaveUtility.cs v 1.0 
// Save/Load functionality
*/

public class GraphSaveUtility
{
    private DialogueGraphView _targetGraphView;
    private DialogueContainer _containerCache; 

    private List<Edge> Edges => _targetGraphView.edges.ToList();
    private List<DialogueNode> Nodes => _targetGraphView.nodes.ToList().Cast<DialogueNode>().ToList(); 


    public static GraphSaveUtility GetInstance(DialogueGraphView targetGraphView)
    {
        return new GraphSaveUtility
        {
            _targetGraphView = targetGraphView
        };
    }

    //Input ports are ignored as every node has an input port
    public void SaveGraph(string fileName)
    {
        //If no new nodes and connections are made, nothing will be saved 
        if(!Edges.Any()) return; 

        var dialogueContainer = ScriptableObject.CreateInstance<DialogueContainer>();

        //Identifying output ports
        var connectedPorts = Edges.Where(x => x.input.node != null).ToArray();
        for (var i = 0; i < connectedPorts.Length; i++)
        {
            var outputNode = connectedPorts[i].output.node as DialogueNode; 
            var inputNode = connectedPorts[i].input.node as DialogueNode; 

        
            dialogueContainer.NodeLinks.Add(new NodeLinkData
            {

                BaseNodeGuid = outputNode.GUID,
                PortName = connectedPorts[i].output.portName, 
                TargetNodeGuid = inputNode.GUID 
            }); 

        }

        foreach(var dialogueNode in Nodes.Where(node=>!node.EntryPoint))
        {
            dialogueContainer.DialogueNodeData.Add(item: new DialogueNodeData
            {
                Guid = dialogueNode.GUID, 
                DialogueText = dialogueNode.DialogueText, 
                Position = dialogueNode.GetPosition().position
            }); 
        }

        //Creates Assets/Resources folder if it does not exist, to save the file there
        if(!AssetDatabase.IsValidFolder(path: "Assets/Resources"))
            AssetDatabase.CreateFolder( parentFolder: "Assets", newFolderName: "Resources");
    
        AssetDatabase.CreateAsset(dialogueContainer, path: $"Assets/Resources/{fileName}.asset");
        AssetDatabase.SaveAssets(); 
    }

    public void LoadGraph (string fileName)
    {
        _containerCache = Resources.Load<DialogueContainer>(fileName);
        if (_containerCache == null)
        {
            EditorUtility.DisplayDialog(title: "File Not Found", message: "Target dialogue graph file does not exist!", ok: "OK");
            return;
        }

        //Called once a graph file is successfully loaded
        ClearGraph(); 
        CreateNodes(); 
        ConnectNodes(); 
    }

    private void ConnectNodes()
    {
        for (var i = 0; i < Nodes.Count; i++)
        {
            //Cycle through connected nodes 
            var connections = _containerCache.NodeLinks.Where(x => x.BaseNodeGuid == Nodes[i].GUID).ToList();

                for (var j = 0; j < connections.Count; j++ )
                {
                    var targetNodeGuid = connections[j].TargetNodeGuid;
                    var targetNode = Nodes.First(x => x.GUID == targetNodeGuid);
                    LinkNodes(Nodes[i].outputContainer[j].Q<Port>(), (Port) targetNode.inputContainer[0]);

                    targetNode.SetPosition(new Rect(
                        _containerCache.DialogueNodeData.First(x => x.Guid == targetNodeGuid).Position,
                        _targetGraphView.DefaultNodeSize
                    ));
                }
        }
    }

    private void LinkNodes(Port output, Port input)
    {   
        //Ensures edges between nodes are maintained when loaded
        var tempEdge = new Edge
        {
            output = output, 
            input = input
        };

        tempEdge.input.Connect(tempEdge);
        tempEdge.output.Connect(tempEdge);
        _targetGraphView.Add(tempEdge);
    }

    private void CreateNodes()
    {
        foreach(var nodeData in _containerCache.DialogueNodeData)
        {
            var tempNode = _targetGraphView.CreateDialogueNode(nodeData.DialogueText,Vector2.zero);
            tempNode.GUID = nodeData.Guid;
            _targetGraphView.AddElement(tempNode);

            var nodePorts  = _containerCache.NodeLinks.Where(x => x.BaseNodeGuid == nodeData.Guid).ToList();
            nodePorts.ForEach(x => _targetGraphView.AddChoicePort(tempNode, x.PortName));

        }
    }

    private void ClearGraph()
    {   
        //Set entry points guid back from the save. Discard existing guid. 
        Nodes.Find(x => x.EntryPoint).GUID = _containerCache.NodeLinks[0].BaseNodeGuid;

        foreach(var node in Nodes)
        {
            if (node.EntryPoint) continue; 

            //Remove edges connected to this node
            Edges.Where(x => x.input.node==node).ToList()
                .ForEach(edge=>_targetGraphView.RemoveElement(edge));

            //Then remove the node
            _targetGraphView.RemoveElement(node);
        }
    }

}