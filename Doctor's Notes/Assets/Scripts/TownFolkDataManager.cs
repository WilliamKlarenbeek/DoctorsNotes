using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using LitJson;
using System.Linq;
using UnityEngine.UI;

public class TownFolkDataManager : MonoBehaviour
{
    [SerializeField] TownFolkDatabase townFolkDB;
    [SerializeField] GameObject townFolkPrefab;
    [SerializeField] Transform townFolkCanvas;
    [SerializeField] GameObject townBell;

    private int x = 0;

    private JsonData dialogue;
    private int index;
    private string speaker;
    private JsonData currentLayer;
    private bool inDialogue;

    // Start is called before the first frame update
    void Start()
    {
        GenerateTownFolk();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray;
        RaycastHit hit;

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (Input.GetMouseButtonDown(0))
                printLine();
        }
    }
    public TownFolkData GetTownFolkUI(int index)
    {
        return townFolkCanvas.GetChild(index).GetComponent<TownFolkData>();
    }

    void GenerateTownFolk()
    {
        //Delete Current TownFolk
        Destroy(townFolkCanvas.GetChild(0).gameObject);
        townFolkCanvas.DetachChildren();

        //Generate TownFolk from Database
        for (int i = 0; i < townFolkDB.TownFolkCount; i++)
        {
            TownFolk townFolkDBData = townFolkDB.GetTownFolk(i);
            TownFolkData townFolkGameObject = Instantiate(townFolkPrefab, townFolkCanvas).GetComponent<TownFolkData>();
            //Set the name of the instantiated gameObject.
            townFolkGameObject.gameObject.name = "Villager" + i + townFolkDBData.villagerName;
            //Add data to the object one at a time.
            townFolkGameObject.SetTownFolkSprite(townFolkDBData.villagerImage);
            townFolkGameObject.SetTownFolkName(townFolkDBData.villagerName);
            townFolkGameObject.SetTownFolkDialogue(townFolkDBData.dialogueFileName);
            townFolkGameObject.DisableTownFolkDialogue(townFolkGameObject);
            townFolkGameObject.DisableDialogueButtons(townFolkGameObject);
            inDialogue = false;
        }
    }

    public bool loadDialogue(string path)
    { 
        index = 0;
        var jsonTextFile = Resources.Load<TextAsset>("Dialogues/" + path);
        dialogue = JsonMapper.ToObject(jsonTextFile.text);
        currentLayer = dialogue;
        return inDialogue = true;      
    }

    public bool printLine()
    {
        //Check not already in a dialogue.
        while (x < townFolkDB.TownFolkCount)
        {
            //Get the current townfolk 
            TownFolk townFolkDBData = townFolkDB.GetTownFolk(x);
            TownFolkData townFolkGameObject = GetTownFolkUI(x);
            if (!inDialogue) 
            {
                loadDialogue(townFolkDBData.dialogueFileName);
                //Move the Villager Sprite to be equal to be at the window.
                Vector3 windowPosition = new Vector3(190.0f, 0.0f, 0.0f);
                townFolkGameObject.MoveVillager(windowPosition, townFolkGameObject);
                inDialogue = true;
            }
            townFolkGameObject.EnableTownFolkDialogue(townFolkGameObject);
            townFolkGameObject.DisableDialogueButtons(townFolkGameObject);
            if (inDialogue)
            {
               //Go through their lines one by one.
               JsonData line = currentLayer[index];
               foreach (JsonData key in line.Keys)
               {
                   //Keys in the JSON files are speaker names / dialogue triggers
                   speaker = key.ToString();
               }
               if (speaker == "EOD")
               {
                    inDialogue = false;
                    townFolkGameObject.DisableTownFolkDialogue(townFolkGameObject);
                    index = 0;
                    x++;
                    //Move the village out of the window frame.
                    Vector3 windowPosition = new Vector3(200.0f, 0.0f, 0.0f);
                    townFolkGameObject.MoveVillager(windowPosition, townFolkGameObject);
                    //Debug.Log("Reached End of File");
               }
               else if (speaker == "BranchStart")
               {
                    //Gets the count of options available
                    JsonData options = line[0];
                    townFolkGameObject.ClearDialogue(townFolkGameObject);
                    townFolkGameObject.SetTownFolkName("Doctor");
                    for (int optionsNumber = 0; optionsNumber < options.Count; optionsNumber++)
                    {
                        activateButton(townFolkGameObject, options[optionsNumber], optionsNumber, townFolkDBData);
                    }
                    //Debug.Log("out of loop");                     
               }
               else if (speaker == "A")
               {
                    townFolkGameObject.PrintDialogueLine(townFolkGameObject, line[0].ToString());
                    townFolkGameObject.SetTownFolkName(townFolkDBData.villagerName);
                    index++;
               }
               else if (speaker == "B")
               {
                    townFolkGameObject.PrintDialogueLine(townFolkGameObject, line[0].ToString());
                    townFolkGameObject.SetTownFolkName("Doctor");
                    index++;
               }
            }
            return true;
        }
        return true;
    }


    private void deactivateButtons(TownFolkData townFolkGameObject)
    {
        //foreach (GameObject button in buttons)
        //{
            //button.SetActive(false);
            //button.GetComponentInChildren<Text>().text = "";
            //button.GetComponent<Button>().onClick.RemoveAllListeners();
        //}
    }

    private void activateButton(TownFolkData townFolkGameObject, JsonData choice, int optionNumber, TownFolk townFolkDBData)
    {
        townFolkGameObject.EnableDialogueButtons(townFolkGameObject);
        if (optionNumber == 0)
        {
            townFolkGameObject.SetButtonTextA(townFolkGameObject, choice[0][0].ToString());            
            townFolkGameObject.SetButtonFunctionalityChoiceA(townFolkGameObject).GetComponent<Button>().onClick.AddListener(delegate { toDoOnClick(choice); });
            townFolkGameObject.SetButtonFunctionalityChoiceA(townFolkGameObject).GetComponent<Button>().onClick.AddListener(delegate { SetButtonFunctionality(townFolkDBData.buttonFunction); });
            //townFolkGameObject.DisableDialogueButtons(townFolkGameObject);
        }
        else if (optionNumber == 1)
        {
            townFolkGameObject.SetButtonTextB(townFolkGameObject, choice[0][0].ToString());
            townFolkGameObject.SetButtonFunctionalityChoiceB(townFolkGameObject).GetComponent<Button>().onClick.AddListener(delegate { toDoOnClick(choice); });
        }
    }
    private void SetButtonFunctionality(string functionality)
    {

    }

    private void toDoOnClick(JsonData choice)
    {
        //Read out the remaining text
        currentLayer = choice[0];
        index = 1;
        inDialogue = true;
        printLine();
    }
}
