using UnityEngine;
using UnityEngine.UI; 
using LitJson; 
using System.Linq; 

public class DialogueManager : MonoBehaviour
{
    //Unity objects
    public Text textDialogueA;
    public Text textDialogueB;
    public Text textPrompt;
    public GameObject[] buttons; 
    public GameObject characterA;
    public GameObject characterB;
    public string fileName;

    //Data 
    private JsonData dialogue;
    private int index; 
    private string speaker; 
    private JsonData currentLayer;  
    private bool inDialogue; 


    private bool loadDialogue (string path)
    {
        if (!inDialogue)
        {
            index = 0; 
            var jsonTextFile = Resources.Load<TextAsset>("Dialogues/" + path);
            dialogue = JsonMapper.ToObject(jsonTextFile.text);
            currentLayer = dialogue; 
            inDialogue = true; 
        }
        return false; 
    }

    public bool printLine()
    {
       if (inDialogue)
        {
            JsonData line = currentLayer[index]; 

            foreach(JsonData key in line.Keys)
            {
                //Keys in the JSON files are speaker names / dialogue triggers
                speaker = key.ToString(); 
            }

            if(speaker == "EOD")
            {
                inDialogue = false; 
                characterA.SetActive(false);
                characterB.SetActive(false);
                textPrompt.text = "End of Dialogue (restart to be implemented later)"; 
            }

            else if (speaker == "BranchStart")
            {
                JsonData options = line[0];
                textDialogueB.text = "";
                for (int optionsNumber = 0; optionsNumber < options.Count; optionsNumber++)
                {
                    activateButton(buttons[optionsNumber], options[optionsNumber]);
                }
            }

            else if (speaker == "A")
            {
                textDialogueA.text =  line[0].ToString();
                textDialogueB.text = " ";
                index++;
            }

            else if (speaker == "B")
            {
                textDialogueB.text =  line[0].ToString();
                index++;
            }
        }
            return true;
    }      
     private void deactivateButtons()
    {
        foreach(GameObject button in buttons)
        {
            button.SetActive(false);
            button.GetComponentInChildren<Text>().text = "";
            button.GetComponent<Button>().onClick.RemoveAllListeners();
        }
    }
    
    private void activateButton(GameObject button, JsonData choice)
    {
        button.SetActive(true);
        button.GetComponentInChildren<Text>().text = choice[0][0].ToString();
        button.GetComponent<Button>().onClick.AddListener(delegate {toDoOnClick(choice); });
    }

    void toDoOnClick(JsonData choice)
    {
        currentLayer = choice[0];
        index = 1;
        printLine();
        deactivateButtons();
    }

   void Start()
    {
        //loadDialogue("JSON/Dialogue0");
        loadDialogue(fileName);
        deactivateButtons();
    }
    
    void Update()
    {
       if(Input.GetKeyDown(KeyCode.C))
       {
         printLine();   
       } 
    }
}

