using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XNode;
//using System.Diagnostics;

public class NodeParser : MonoBehaviour
{
    public DialogueGraph graph;
    Coroutine _parser;
    public Text speaker;
    public Text dialogue;
    public Image speakerImage;
    public GameObject dialoguePanel;
    public float textDelay = 0.2f;

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
            speakerImage.sprite = b.GetSprite();
            for (int i = 0; i <= dataParts[2].Length; i++)
            {
                    dialogue.text = dataParts[2].Substring(0, i);
                    yield return new WaitForSecondsRealtime(textDelay);
            }
            yield return new WaitUntil(() => Input.GetKeyDown("space"));
            yield return new WaitUntil(() => Input.GetKeyUp("space"));
            NextNode("exit");
        }

        if (dataParts[0] == "CauldronNode")
        {
            //Run dialogue process to display key info
            speaker.text = dataParts[1];
            speakerImage.sprite = b.GetSprite();
            for (int i = 0; i <= dataParts[2].Length; i++)
            {
                dialogue.text = dataParts[2].Substring(0, i);
                if (Input.GetKeyDown("space")) {
                    dialogue.text = dataParts[2];
                }
                yield return new WaitForSeconds(textDelay);
            }
            GameObject cauldron = GameObject.Find(dataParts[3]);            
            yield return new WaitUntil(() => cauldron.GetComponent<Cauldron>().StateWorking());
            NextNode("exit");
        }

        if (dataParts[0] == "BookScriptNode")
        {
            //Run dialogue process to display key info
            speaker.text = dataParts[1];
            speakerImage.sprite = b.GetSprite();
            for (int i = 0; i <= dataParts[2].Length; i++)
            {
                dialogue.text = dataParts[2].Substring(0, i);
                yield return new WaitForSeconds(textDelay);
            }
            GameObject book = GameObject.Find(dataParts[3]);
            yield return new WaitUntil(() => book.GetComponent<BookScript>().GetBookOpen());
            NextNode("exit");
        }

        if (dataParts[0] == "BookTabNode")
        {
            //Run dialogue process to display key info
            speaker.text = dataParts[1];
            speakerImage.sprite = b.GetSprite();
            for (int i = 0; i <= dataParts[2].Length; i++)
            {
                dialogue.text = dataParts[2].Substring(0, i);
                yield return new WaitForSeconds(textDelay);
            }
            GameObject book = GameObject.Find(dataParts[3]);
            int bookTabNum = System.Int32.Parse(dataParts[4]);

            yield return new WaitUntil(() => book.GetComponent<BookScript>().GetCategory(bookTabNum));
            NextNode("exit");
        }

        if (dataParts[0] == "ArrowNode")
        {
            //Run dialogue process to display key info
            speaker.text = dataParts[1];
            speakerImage.sprite = b.GetSprite();
            for (int i = 0; i <= dataParts[2].Length; i++)
            {
                dialogue.text = dataParts[2].Substring(0, i);
                yield return new WaitForSeconds(textDelay);
            }
            GameObject book = GameObject.Find(dataParts[3]);

            yield return new WaitUntil(() => book.GetComponent<BookScript>().GetBookButton());
            NextNode("exit");
        }

        if (dataParts[0] == "InstantiateNode")
        {
            //Run dialogue process to display key info
            speaker.text = dataParts[1];
            speakerImage.sprite = b.GetSprite();
            for (int i = 0; i <= dataParts[2].Length; i++)
            {
                dialogue.text = dataParts[2].Substring(0, i);
                yield return new WaitForSeconds(textDelay);
            }
            //GameObject book = GameObject.Find(dataParts[3]);
            //if(GameObject instantiatedObject)
            yield return new WaitUntil(() => GameObject.Find(dataParts[3]));
            NextNode("exit");
        }

        if (dataParts[0] == "InstantiateTwoNode")
        {
            //Run dialogue process to display key info
            speaker.text = dataParts[1];
            speakerImage.sprite = b.GetSprite();
            for (int i = 0; i <= dataParts[2].Length; i++)
            {
                dialogue.text = dataParts[2].Substring(0, i);
                yield return new WaitForSeconds(textDelay);
            }
            //GameObject book = GameObject.Find(dataParts[3]);
            //if(GameObject instantiatedObject)
            yield return new WaitUntil(() => GameObject.Find(dataParts[3]) && GameObject.Find(dataParts[4]));
            NextNode("exit");
        }

        if (dataParts[0] == "MortarNode")
        {
            //Run dialogue process to display key info
            speaker.text = dataParts[1];
            speakerImage.sprite = b.GetSprite();
            for (int i = 0; i <= dataParts[2].Length; i++)
            {
                dialogue.text = dataParts[2].Substring(0, i);
                yield return new WaitForSeconds(textDelay);
            }
            GameObject cauldron = GameObject.Find(dataParts[3]);
            yield return new WaitUntil(() => cauldron.GetComponent<MortarPestle>().StateWorking());
            NextNode("exit");
        }

        if (dataParts[0] == "PatientSymptomNode")
        {
            //Run dialogue process to display key info
            speaker.text = dataParts[1];
            speakerImage.sprite = b.GetSprite();
            for (int i = 0; i <= dataParts[2].Length; i++)
            {
                dialogue.text = dataParts[2].Substring(0, i);
                yield return new WaitForSeconds(textDelay);
            }
            GameObject patient = GameObject.Find(dataParts[3]);
            int symptomNum = System.Int32.Parse(dataParts[4]);
            Debug.Log("Symptom Num" + symptomNum);
            int symptomRed = System.Int32.Parse(dataParts[5]);
            int symptomBlue = System.Int32.Parse(dataParts[6]);
            int symptomGreen = System.Int32.Parse(dataParts[7]);
            yield return new WaitUntil(() => patient.GetComponent<Patient>().checkValues(symptomNum, symptomRed, symptomBlue, symptomGreen));
            NextNode("exit");
        }

        if (dataParts[0] == "TutorialNode")
        {
            speaker.text = dataParts[1];
            dialogue.text = dataParts[2];
            speakerImage.sprite = b.GetSprite();
            //if (GameObject.Find(dataParts[3]))
            //{
            var tutorialObject = GameObject.Find(dataParts[3]);
            //dataParts[4] enabledTutorialObject = GameObject.Find(dataParts[3]);
            //enabledTutorialObject.
                //yield return new WaitUntil(() => tutorialObject.);
                //yield return new WaitUntil(() => Input.GetKeyUp("space"));
            //}
                NextNode("exit");
        }

        if (dataParts[0] == "End")
        {
            /*animationController.SetBool("closePanel", true);*/
            /*            _animationController.Play("Base Layer.Close");
                        _animationController.SetBool("closePanel", true);*/
            dialoguePanel.gameObject.SetActive(false);
        }
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

