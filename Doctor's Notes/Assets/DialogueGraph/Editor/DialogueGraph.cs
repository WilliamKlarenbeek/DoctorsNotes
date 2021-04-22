using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor; 
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

/* DialogueGraph.cs v 1.0 
// Adding functionality to GUI elements
*/

public class DialogueGraph : EditorWindow
{
   private DialogueGraphView _graphView;
   private string _fileName = "New Narrative"; 
    
    //Adds a 'Graph' menu item in the editor, with a child called 'Dialogue Graph' 
    [MenuItem("Graph/Dialogue Graph")]

    //Open the Dialogue Graph menu item 
    public static void OpenDialogueGraphWindow()
    {
        var window = GetWindow <DialogueGraph>();
        window.titleContent = new GUIContent (text: "Dialogue Graph");     
    }
    
    private void OnEnable()
    {
        ConstructGraphView();
        GenerateToolbar();
        GenerateMiniMap();
        GenerateBlackBoard();
    }

    private void GenerateBlackBoard()
    {
        var blackboard = new Blackboard(_graphView);
        blackboard.Add(new BlackboardSection {title = "Exposed properties"});
        _graphView.Add(blackboard);
        blackboard.addItemRequested = _blackboard1 => {_graphView.AddPropertyToBlackboard(new ExposedProperty());};
        blackboard.SetPosition(new Rect (10, 30, 200, 300));
        _graphView.Blackboard = blackboard; 
    }

    private void ConstructGraphView()
    {
        _graphView = new DialogueGraphView(this)
        {
            name = "Dialogue Graph"
        };
        _graphView.StretchToParentSize(); 

        rootVisualElement.Add(_graphView); 
    }

    private void GenerateToolbar()
    {
        var toolbar = new Toolbar(); 

        var fileNameTextField = new TextField ( label: "File Name:");
        fileNameTextField.SetValueWithoutNotify (_fileName); 
        fileNameTextField.MarkDirtyRepaint(); // Update text field to new value
        fileNameTextField.RegisterValueChangedCallback(evt => _fileName = evt.newValue); //Change filename from UI
        toolbar.Add(fileNameTextField);

        //Save & load handling
        toolbar.Add(child: new Button( clickEvent: ()=>RequestDataOperation(true)){text = "Save Data"});
        toolbar.Add(child: new Button( clickEvent: ()=>RequestDataOperation(false)){text = "Load Data"});

        rootVisualElement.Add(toolbar);
    }

    private void GenerateMiniMap()
    {
     var miniMap = new MiniMap{anchored = true};  
     //There is a thing to make it hang to the right - 14:57 mins into last video. i cbf tho
     miniMap.SetPosition(new Rect(x:10, y:30, width: 200, height: 140));
     _graphView.Add(miniMap);
    }

    private void RequestDataOperation(bool save)
    {
        if (string.IsNullOrEmpty(_fileName))
        {
            EditorUtility.DisplayDialog(title: "Invalid file name!", message: "Please enter a valid file name.", ok: "OK");
            return; 
        }

        //Conditional save/load graph behaviour
        var saveUtility = GraphSaveUtility.GetInstance(_graphView);
        if(save)
        {
            saveUtility.SaveGraph(_fileName);
        }
        else 
        {
            saveUtility.LoadGraph(_fileName);
        }      
    }

    private void OnDisable()
    {
        rootVisualElement.Remove(_graphView);
    }

}
