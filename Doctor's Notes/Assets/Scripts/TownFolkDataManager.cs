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

    private JsonData dialogue;
    private int index;
    private string speaker;
    private JsonData currentLayer;
    private bool inDialogue;

    // Start is called before the first frame update
    void Start()
    {
        GenerateTownFolk();
        //loadDialogue("JSON/Dialogue0");
        //loadDialogue(fileName);
        //loadDialogue(townFolk.villagerName);
        //deactivateButtons();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
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
            //Vector3 movePos = new Vector3(0, 0, 93);
            //transform.Translate(movePos, Space.World);
            //townFolkGameObject.MoveVillager(movePos);
            if (loadDialogue(townFolkDBData.dialogueFileName))
            {
                townFolkGameObject.DisableTownFolkDialogue(townFolkGameObject);
                townFolkGameObject.DisableDialogueButtons(townFolkGameObject);
            }
            Debug.Log("loadDialogue Failure");
        }
    }

    public bool loadDialogue(string path)
    { 
        //if (!inDialogue)
        //{
            index = 0;
            var jsonTextFile = Resources.Load<TextAsset>("Dialogues/" + path);
            dialogue = JsonMapper.ToObject(jsonTextFile.text);
            currentLayer = dialogue;
            inDialogue = true;
            return true;
        //}
        //return false;
    }

    public bool printLine()
    {
        //Check not already in a dialogue.
        if (inDialogue)
        {
            for (int i = 0; i < townFolkDB.TownFolkCount; i++)
            {
                //Get the current townfolk 
                TownFolk townFolkDBData = townFolkDB.GetTownFolk(i);
                TownFolkData townFolkGameObject = GetTownFolkUI(i);
                townFolkGameObject.EnableTownFolkDialogue(townFolkGameObject);
                townFolkGameObject.DisableDialogueButtons(townFolkGameObject);
                if (inDialogue)
                {
                    //Go through their lines one by one.
                    JsonData line = currentLayer[index];
                    //townFolkDB.townFolk[1].villagerName;
                    //townFolkGameObject.SetTownFolkName(townFolkDB.townFolk[1].villagerName);
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
                        Debug.Log("Reached End of File");
                        //townFolkDialogueImage.SetActive(false);
                        //textPrompt.text = "End of Dialogue (restart to be implemented later)";
                    }
                    else if (speaker == "BranchStart")
                    {
                        //Gets the count of options available
                        JsonData options = line[0];
                        //townFolkDialogue.text = "";
                        townFolkGameObject.ClearDialogue(townFolkGameObject);
                        for (int optionsNumber = 0; optionsNumber < options.Count; optionsNumber++)
                        {
                            //activateButton(buttons[optionsNumber], options[optionsNumber]);
                            activateButton(townFolkGameObject, options[optionsNumber], optionsNumber, townFolkDBData);
                            Debug.Log(speaker);
                            //Add listeners to the button 
                        }
                        Debug.Log("out of loop");
                        //townFolkChoiceA.gameObject.SetActive(true);                     
                    }
                    else if (speaker == "A")
                    {
                        //townFolkDialogue.text = line[0].ToString();
                        townFolkGameObject.PrintDialogueLine(townFolkGameObject, line[0].ToString());
                        townFolkGameObject.SetTownFolkName(townFolkDBData.villagerName);
                        //textDialogueB.text = " ";
                        index++;
                    }

                    else if (speaker == "B")
                    {
                        //townFolkDialogue.text = line[0].ToString();
                        townFolkGameObject.PrintDialogueLine(townFolkGameObject, line[0].ToString());
                        townFolkGameObject.SetTownFolkName("Doctor");
                        index++;
                    }
                }
                return true;
            }
            Debug.Log("Exceeded Database Count");
            return true;
        }
        Debug.Log("Already in dialogue");
        return false;
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
        //Set the button Text

        //button.SetActive(true);
        if (optionNumber == 0)
        {
            townFolkGameObject.SetButtonTextA(townFolkGameObject, choice[0][0].ToString());            
            townFolkGameObject.SetButtonFunctionalityChoiceA(townFolkGameObject).GetComponent<Button>().onClick.AddListener(delegate { toDoOnClick(choice); });
            //townFolkGameObject.DisableDialogueButtons(townFolkGameObject);
        }
        else if (optionNumber == 1)
        {
            townFolkGameObject.SetButtonTextB(townFolkGameObject, choice[0][0].ToString());
            townFolkGameObject.SetButtonFunctionalityChoiceB(townFolkGameObject).GetComponent<Button>().onClick.AddListener(delegate { toDoOnClick(choice); });
        }
        //townFolkGameObject.townFolkChoiceA.GetComponentInChildren<Text>().text = choice[0][0].ToString();
        //button.GetComponent<Button>().onClick.AddListener(delegate { toDoOnClick(choice); });
    }

    private void toDoOnClick(JsonData choice)
    {
        //Read out the remaining text
        currentLayer = choice[0];
        index = 1;
        inDialogue = true;
        printLine();
        //deactivateButtons();
    }
}
