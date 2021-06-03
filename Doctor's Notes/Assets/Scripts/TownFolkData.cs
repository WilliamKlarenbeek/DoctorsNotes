using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using LitJson;
using System.Linq;
using UnityEngine.SceneManagement;

public class TownFolkData : MonoBehaviour
{
    [SerializeField] GameObject townFolkDialogueImage;
    [SerializeField] Image townFolkSprite;
    [SerializeField] TMP_Text townFolkName;
    [SerializeField] TMP_Text townFolkDialogue;
    [SerializeField] Button townFolkChoiceA;
    [SerializeField] Button townFolkChoiceB;

    public void SetTownFolkName(string name)
    {
        townFolkName.text = name;
    }

    public void SetTownFolkDialogue(string dialogue)
    {
        townFolkDialogue.text = dialogue;
    }

    public void SetTownFolkSprite(Sprite sprite)
    {
        townFolkSprite.sprite = sprite;
    }

    public void SetVillagerSpawn(Vector3 spawnPos)
    {
        transform.Translate(spawnPos, Space.World);
    }

    public void MoveVillager(Vector3 position, TownFolkData townFolk)
    {
        //Vector3 windowPosition = new Vector3(100.0f, 0.0f, 0.0f);
        //transform.Translate(movedPosition, Space.World);
        townFolk.townFolkSprite.transform.Translate(position, Space.World);
    }

    public void DisableTownFolkDialogue(TownFolkData townFolk)
    {
        townFolk.townFolkDialogueImage.SetActive(false);
    }

    public void EnableTownFolkDialogue(TownFolkData townFolk)
    {
        townFolk.townFolkDialogueImage.SetActive(true);
    }

    public void SetButtonTextA(TownFolkData townFolk, string choiceText)
    {
        townFolk.townFolkChoiceA.GetComponentInChildren<Text>().text = choiceText;
    }

    public void SetButtonTextB(TownFolkData townFolk, string choiceText)
    {
        townFolk.townFolkChoiceB.GetComponentInChildren<Text>().text = choiceText;
    }

    public void ClearDialogue(TownFolkData townFolk)
    {
        townFolk.townFolkDialogue.text = "";
    }

    public void EnableDialogueButtons(TownFolkData townFolk)
    {
        townFolk.townFolkChoiceA.gameObject.SetActive(true);
        townFolk.townFolkChoiceB.gameObject.SetActive(true);
        townFolk.townFolkChoiceA.interactable = true;
        townFolk.townFolkChoiceB.interactable = true;
    }

    public void DisableDialogueButtons(TownFolkData townFolk)
    {
        townFolk.townFolkChoiceA.gameObject.SetActive(false);
        townFolk.townFolkChoiceB.gameObject.SetActive(false);
        townFolk.townFolkChoiceA.interactable = false;
        townFolk.townFolkChoiceB.interactable = false;
    }

    public void PrintDialogueLine(TownFolkData townFolk, string line)
    {
        townFolkDialogue.text = line.ToString();
    }

    public Button SetButtonFunctionalityChoiceA(TownFolkData townFolk)
    {
        return townFolk.townFolkChoiceA.GetComponent<Button>();
    }

    public Button SetButtonFunctionalityChoiceB(TownFolkData townFolk)
    {
        return townFolk.townFolkChoiceB.GetComponent<Button>();
    }
}
