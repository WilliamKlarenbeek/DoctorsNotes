using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class TownFolkData : MonoBehaviour
{
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

    public void MoveVillager(Vector3 movedPosition)
    {
        transform.Translate(movedPosition, Space.World);
    }
}
