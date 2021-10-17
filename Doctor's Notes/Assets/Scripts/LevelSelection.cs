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
    public GameObject lockObject;
    public Sprite genericLockImage;
    public Sprite winLockImage;
    public Sprite loseLockImage;
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
        // debugger called
        Debugger.debuggerInstance.WriteToFileTag("levelSelection"); 
        /*Debugger.debuggerInstance.ReadFile(); */
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
            lockObject.SetActive(true);
            //Debug.Log("Level is locked"); 
        }
        else //if level is unlocked
        {
            lockObject.SetActive(false);
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
            StartCoroutine(PlayerIcon.instance.Movement(gameObject));
            Debug.Log("Level selected, loading scene: " + aIndex);
            Debugger.debuggerInstance.WriteToFile("Level selected, loading scene: " + aIndex);
        }
    }

    public void LoadScene(int aIndex)
    {
        SceneManager.LoadScene(aIndex);
    }

    public void SetWin(int aFlag)
    {
        switch (aFlag)
        {
            case 1:
                lockObject.GetComponent<Image>().sprite = winLockImage;
                break;
            case 2:
                lockObject.GetComponent<Image>().sprite = loseLockImage;
                break;
            default:
                lockObject.GetComponent<Image>().sprite = genericLockImage;
                break;
        }
    }

    public void SetLocked(bool aFlag)
    {
        unlocked = aFlag;
    }
}
