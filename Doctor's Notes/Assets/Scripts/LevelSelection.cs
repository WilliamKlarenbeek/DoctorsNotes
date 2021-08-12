using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO; 

public class LevelSelection : MonoBehaviour
{
    [SerializeField] private bool unlocked = false; //By default level is locked
    public static LevelSelection levelSelectionInstance; 
    public Image unlockImage;
    private int levelIndex;
    private PlayerIcon player;

    private void Awake()
    {
        if (GameObject.Find("PlayerIcon") != null)
        {
            player = GameObject.Find("PlayerIcon").GetComponent<PlayerIcon>();
        }
        levelSelectionInstance = this; 
    }

    private void Start()
    {
        Debugger.debuggerInstance.ClearAll();
        Debugger.debuggerInstance.DebugInfoToFile(); 
        // debugger called
        Debugger.debuggerInstance.WriteToFileTag("levelSelection"); 
        Debugger.debuggerInstance.WriteToFile(" LevelSelection test...");
        Debugger.debuggerInstance.ReadFile(); 
        }

    private void Update() //check on every frame 
    {
        UpdateLevelImage();
        if (player.isMoving()) {
            GetComponent<Button>().enabled = false;
        } else
        {
            GetComponent<Button>().enabled = true;
        }
    }

    private void UpdateLevelImage()
    {
        if(!unlocked) //if level is locked 
        {
            unlockImage.gameObject.SetActive(true);
            //Debug.Log("Level is locked"); 
        }
        else //if level is unlocked
        {
            unlockImage.gameObject.SetActive(false);
            //Debug.Log("Level is unlocked");
        }
    }

    public int getLevelIndex()
    {
        return levelIndex;
    }

    public void PressSelection(int aIndex)
    {
        if(unlocked && player.isMoving() == false)
        {
            levelIndex = aIndex;
            // Once the player Icon moves to the selected location then 
            // Load Scene coroutine is called from the Movement Coroutine inside the PlayerIcon
            StartCoroutine(PlayerIcon.instance.Movement(transform.position));
            Debug.Log("Level selected, loading scene: " + aIndex);
        }
    }

    public void LoadScene(int aIndex)
    {
        SceneManager.LoadScene(aIndex);
    }
}
