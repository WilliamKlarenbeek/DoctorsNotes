using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UIElements.Button;

/* DialogueGraphView.cs v 1.0 
// GUI elements 
*/

public class DialogueGraphView : GraphView
{
    public Blackboard Blackboard; 
    public readonly Vector2 DefaultNodeSize = new Vector2(x: 150, y:200);
    public List<ExposedProperty> ExposedProperties = new List<ExposedProperty>();
    private NodeSearchWindow _searchWindow; 

    public DialogueGraphView(EditorWindow editorWindow )
    {
        styleSheets.Add( styleSheet: Resources.Load<StyleSheet>( path: "DialogueGraph")); //Graph line style 

        SetupZoom(ContentZoomer.DefaultMinScale,ContentZoomer.DefaultMaxScale);

        this.AddManipulator(new ContentDragger()); 
        this.AddManipulator(new SelectionDragger()); 
        this.AddManipulator(new RectangleSelector()); 

        var grid = new GridBackground();
        Insert( index: 0,grid);
        grid.StretchToParentSize();

        AddElement(GenerateEntryPointNode());  

        AddSearchWindow(editorWindow);
    }

    private void AddSearchWindow(EditorWindow editorWindow)
    {
        _searchWindow = ScriptableObject.CreateInstance<NodeSearchWindow>();
        _searchWindow.Init(editorWindow,this);
        nodeCreationRequest = context => 
            SearchWindow.Open(new SearchWindowContext(context.screenMousePosition), _searchWindow); 
    }

    public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
    {
        var compatiblePorts = new List<Port>();

        //Prevents ports connecting to themselves / input ports being connected to the nodes own output ports
        ports.ForEach( funcCall: (port) => 
        {
            if(startPort!=port && startPort.node!=port.node)
                compatiblePorts.Add(port); 
        });

        return compatiblePorts;
    }

    private Port GeneratePort(DialogueNode node, Direction portDirection, Port.Capacity capacity = Port.Capacity.Single)
    {
        return node.InstantiatePort(Orientation.Horizontal,portDirection,capacity, typeof(float)); //Arbitrary type, we're not transmitting data between ports
    }


    private DialogueNode GenerateEntryPointNode()
    {
        var node = new DialogueNode
        {
            title = "START",
            GUID = Guid.NewGuid().ToString(),
            DialogueText = "START",
            EntryPoint = true
        };

        //Generate first node's output port
        var generatedPort = GeneratePort(node, Direction.Output);
        generatedPort.portName = "Next";
        node.outputContainer.Add(generatedPort);

        //The start node cannot be moved or deleted
        node.capabilities &= ~Capabilities.Movable; 
        node.capabilities &= ~Capabilities.Deletable;

        //Called when adding a port to the output container
        node.RefreshExpandedState(); 
        node.RefreshPorts();
        // node.SetPosition(new Rect (position: Vector2.zero,DefaultNodeSize)); 
        node.SetPosition(new Rect(100, 200, 100, 150));
        return node; 
    }

    //Display newly created nodes on the graph view
    public void CreateNode(string nodeName, Vector2 position)
    {
        AddElement(CreateDialogueNode(nodeName, position));
    }

    public DialogueNode CreateDialogueNode(string nodeName, Vector2 position)
    {
        var dialogueNode = new DialogueNode
        {
            title = nodeName,
            DialogueText = nodeName, 
            GUID = Guid.NewGuid().ToString()
        };
        
        //Generate new node's input port
        var inputPort = GeneratePort(dialogueNode, Direction.Input, Port.Capacity.Multi); 
        inputPort.portName = "Input"; 
        dialogueNode.inputContainer.Add(inputPort);

        dialogueNode.styleSheets.Add(Resources.Load<StyleSheet>("Node"));

        //Button to add new output choices
        var button = new Button( () => { AddChoicePort(dialogueNode);});
        button.text = "New Choice";
        dialogueNode.titleContainer.Add(button);

        //Text field to allow editing the node title
        var textField = new TextField(string.Empty);
        textField.RegisterValueChangedCallback(evt =>
        {
            dialogueNode.DialogueText = evt.newValue;
            dialogueNode.title = evt.newValue;

        });
        textField.SetValueWithoutNotify(dialogueNode.title); 
        dialogueNode.mainContainer.Add(textField);

        //Refresh to ensure modified state is displayed 
        dialogueNode.RefreshExpandedState();
        dialogueNode.RefreshPorts(); 
        dialogueNode.SetPosition(new Rect (position,DefaultNodeSize)); 
        
        return dialogueNode; 

    }

    //Create output dialogue choices 
    public void AddChoicePort(DialogueNode dialogueNode, string overiddenPortName = "")
    {
        var generatedPort = GeneratePort(dialogueNode, Direction.Output); 

        //Removes the label to the right that duplicated the content of the editable text field
        //BUGGED: slight offset to where mouse has to click to make an edge appear
        var oldLabel = generatedPort.contentContainer.Q<Label>(name: "type");
        generatedPort.contentContainer.Remove(oldLabel);

        //Each newly created dialogue choice is listed by number starting at 0
        var outputPortCount = dialogueNode.outputContainer.Query("connector").ToList().Count; 

        //For ClearGraph()
        var choicePortName =  string.IsNullOrEmpty(overiddenPortName) 
            ? $"Choice {outputPortCount + 1 }" 
            : overiddenPortName;

        var textField = new TextField
        {
            name = string.Empty, 
            value = choicePortName
        };

        //Delete choice 
        textField.RegisterValueChangedCallback(evt => generatedPort.portName = evt.newValue);
        generatedPort.contentContainer.Add(new Label("  "));
        generatedPort.contentContainer.Add(textField);
        var deleteButton = new Button (() => RemovePort(dialogueNode, generatedPort))
        {
            text = "X"
        };
        generatedPort.contentContainer.Add(deleteButton);

        generatedPort.portName = choicePortName;

        //Refresh to ensure changes made  display correctly
        dialogueNode.outputContainer.Add(generatedPort);
        dialogueNode.RefreshPorts();
        dialogueNode.RefreshExpandedState();
    }

    private void RemovePort(DialogueNode dialogueNode, Port generatedPort)
    {
        var targetEdge = edges.ToList().Where (x => 
            x.output.portName == generatedPort.portName && x.output.node == generatedPort.node);

        if(targetEdge.Any())
        {
            var edge =  targetEdge.First();
            edge.input.Disconnect(edge);
            RemoveElement(targetEdge.First());
        } 

        dialogueNode.outputContainer.Remove(generatedPort); 
        dialogueNode.RefreshPorts();
        dialogueNode.RefreshExpandedState();
    }

    public void AddPropertyToBlackboard(ExposedProperty exposedProperty)
    {
        //New default property names have a number appended to them
        var localPropertyName = exposedProperty.PropertyName;
        var localPropertyValue = exposedProperty.PropertyValue; 
        while(ExposedProperties.Any(x => x.PropertyName == localPropertyName))
            localPropertyName = $"{localPropertyName}(1)";
        
        var property = new ExposedProperty();
        property.PropertyName = localPropertyName;
        property.PropertyValue = localPropertyValue;
        ExposedProperties.Add(property);

        //to actually see it
        var container = new VisualElement();
        var blackboardField = new BlackboardField {text = property.PropertyName, typeText = "String property"}; 
        container.Add(blackboardField);

        var propertyValueTextField = new TextField("value:")
        {
            value = localPropertyValue
        };

        //Updating changed text field name *and* value
        //BUGGED: value window too narrow
        propertyValueTextField.RegisterValueChangedCallback(evt => 
        {
            var changingPropertyIndex = ExposedProperties.FindIndex(x => x.PropertyName == property.PropertyName);
            ExposedProperties[changingPropertyIndex].PropertyValue = evt.newValue;
        });
        var blackBoardValueRow = new BlackboardRow(blackboardField, propertyValueTextField);
        container.Add(blackBoardValueRow);
     

        Blackboard.Add(container);
    }
}
