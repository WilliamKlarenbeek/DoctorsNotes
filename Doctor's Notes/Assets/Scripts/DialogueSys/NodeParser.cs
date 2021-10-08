using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XNode;

public class NodeParser : MonoBehaviour
{
    public DialogueGraph graph;
    Coroutine _parser;
    public Text speaker;
    public Text dialogueA;
    public Text dialogueB;
    public Button buttonA;
    public Button buttonB;
    public Image speakerImage;
    public GameObject dialoguePanel;

    private void Start()
    {
        foreach (BaseNode b in graph.nodes)
        {
            if (b.GetString() == "Start")
                //Node starting point
                graph.current = b;
            break;
        }
        _parser = StartCoroutine(ParseNode());
    }

    IEnumerator ParseNode()
    {
        BaseNode b = graph.current;
        string data = b.GetString();
        string[] dataParts = data.Split('/');

        if (dataParts[0] == "Start")
        {
            //Disable unused UI elements
            DisableUIElements(); 

            NextNode("exit");
        }

        if (dataParts[0] == "DialogueNode")
        {
            //Disable unused UI elements
            DisableUIElements(); 

            //Run dialogue process to display key info
            speaker.text = dataParts[1];
            dialogueA.text = dataParts[2];
            speakerImage.sprite = b.GetSprite();
            yield return new WaitUntil(() => Input.GetKeyDown("space"));
            yield return new WaitUntil(() => Input.GetKeyUp("space"));
            NextNode("exit");
        }
        if (dataParts[0] == "DialogueResponseNode")
        {
            //Enabke the UI elements before populating
            EnableUIElements(); 

            //Populate the UI elements
            speaker.text = dataParts[1];
            dialogueA.text = dataParts[2];
            dialogueB.text = dataParts[3]; 
            speakerImage.sprite = b.GetSprite();
            yield return new WaitUntil(() => ChooseOne());   
        }
        if (dataParts[0] == "End")
        {
            //Disable unused UI elements
            DisableUIElements();

            /*animationController.SetBool("closePanel", true);*/
            /*            _animationController.Play("Base Layer.Close");
                        _animationController.SetBool("closePanel", true);*/
            dialoguePanel.gameObject.SetActive(false);
        }
    }

    bool ChooseOne()
    {
        bool selection = false; 
        if (Input.GetKeyDown("1"))
        {
            NextNode("exit1");
            selection = true; 
        }
        else if (Input.GetKeyDown("2"))
        {
            NextNode("exit2");
            selection = true;
        }
        return selection;
    }

    public void NextNode(string fieldName)
    {
        if (_parser != null)
        {
            StopCoroutine(_parser);
            _parser = null;
        }
        foreach (NodePort p in graph.current.Ports)
        {
            if (p.fieldName == fieldName)
            {
                graph.current = p.Connection.node as BaseNode;
                break;
            }
        }
        _parser = StartCoroutine(ParseNode());
    }

    public void DisableUIElements()
    {
        //Set the UI elements as false which are not in use
        //because the default is DialogueNode
        dialogueB.gameObject.SetActive(false);
        buttonA.gameObject.SetActive(false);
        buttonB.gameObject.SetActive(false);
    }

    public void EnableUIElements()
    {
        //Set the UI elements to true
        dialogueB.gameObject.SetActive(true);
        buttonA.gameObject.SetActive(true);
        buttonB.gameObject.SetActive(true);
    }
}

