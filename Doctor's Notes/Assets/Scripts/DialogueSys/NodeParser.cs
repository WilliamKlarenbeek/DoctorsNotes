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
    public Text dialogue;
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
            NextNode("exit");
        }

        if (dataParts[0] == "DialogueNode")
        {
            //Run dialogue process to display key info
            speaker.text = dataParts[1];
            dialogue.text = dataParts[2];
            speakerImage.sprite = b.GetSprite();
            yield return new WaitUntil(() => Input.GetKeyDown("space"));
            yield return new WaitUntil(() => Input.GetKeyUp("space"));
            NextNode("exit");
        }
        if (dataParts[0] == "TutorialNode")
        {
            //Run dialogue process to display key info
            speaker.text = dataParts[1];
            dialogue.text = dataParts[2];
            string dialogueObjectName = dataParts[3];
            speakerImage.sprite = b.GetSprite();
            //StartCoroutine(DialogueTutorialObject(dialogueObjectName));

            //var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //RaycastHit hit;
            //if (Physics.Raycast(ray, out hit))
            //{
            //    var selection = hit.transform;
            //    if (dataParts[3] == selection.gameObject.name)
            //    {
            //        Debug.Log("Hit" + selection.gameObject.name);
            //        yield return new WaitUntil(() => Input.GetKeyDown("space"));
            //        yield return new WaitUntil(() => Input.GetKeyUp("space"));
            //        NextNode("exit");
            //    }
            //}
            yield return new WaitUntil(() => Input.GetKeyDown("space"));
            yield return new WaitUntil(() => Input.GetKeyUp("space"));
            NextNode("exit");
            if (dataParts[0] == "End")
            {
                /*animationController.SetBool("closePanel", true);*/
                /*            _animationController.Play("Base Layer.Close");
                            _animationController.SetBool("closePanel", true);*/
                dialoguePanel.gameObject.SetActive(false);
            }
        }
    }

    IEnumerator DialogueTutorialObject(string dialogueObjectName)
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Debug.Log("Hit made it here");
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("Hit made it here02");
                var selection = hit.transform;
                while (dialogueObjectName != selection.gameObject.name)
                {
                    Debug.Log("is not the same");
                    //Debug.Log("Hit" + selection.gameObject.name);
                    //yield return new WaitUntil(() => dialogueObjectName == selection.gameObject.name);
                    ////yield return new WaitUntil(() => Input.GetKeyUp("space"));
                    //NextNode("exit");
                }
                Debug.Log("Hit" + selection.gameObject.name);

                yield return new WaitUntil(() => dialogueObjectName == selection.gameObject.name);
                //yield return new WaitUntil(() => Input.GetKeyUp("space"));
                NextNode("exit");
            }
        Debug.Log("Hit made it here04");
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
}

