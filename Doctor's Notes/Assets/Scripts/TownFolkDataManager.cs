using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownFolkDataManager : MonoBehaviour
{
    [SerializeField] TownFolkDatabase townFolkDB;
    [SerializeField] GameObject townFolkPrefab;
    [SerializeField] Transform townFolkCanvas;
    // Start is called before the first frame update
    void Start()
    {
        GenerateTownFolk();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void GenerateTownFolk()
    {
        //Delete Current TownFolk
        Destroy(townFolkCanvas.GetChild(0).gameObject);
        townFolkCanvas.DetachChildren();

        //Generate TownFolk from Database
        for (int i = 0; i < townFolkDB.TownFolkCount; i++)
        {
            TownFolk townFolk = townFolkDB.GetTownFolk(i);
            TownFolkData townFolkGameObject = Instantiate(townFolkPrefab, townFolkCanvas).GetComponent<TownFolkData>();

            //Set the name of the instantiated gameObject.
            townFolkGameObject.gameObject.name = "Villager" + i + townFolk.villagerName;

            //Add data to the object one at a time.
            townFolkGameObject.SetTownFolkSprite(townFolk.villagerImage);
            townFolkGameObject.SetTownFolkName(townFolk.villagerName);
            townFolkGameObject.SetTownFolkDialogue(townFolk.dialogueFileName);
            //Vector3 movePos = new Vector3(0, 0, 93);
            //transform.Translate(movePos, Space.World);
            //townFolkGameObject.MoveVillager(movePos);
            //TownsFolkDialogueManager.loadDialogue(townFolk.dialogueFileName);
        }
    }

}
