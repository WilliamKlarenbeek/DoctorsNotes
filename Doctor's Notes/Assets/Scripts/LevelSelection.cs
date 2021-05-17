using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

public class LevelSelection : MonoBehaviour
{
    [SerializeField] private bool unlocked = false; //By default level is locked
    public static LevelSelection levelSelectionInstance; 
    public Image unlockImage;
    private string levelName; 

    private void Awake()
    {
        levelSelectionInstance = this; 
    }

    private void Update() //check on every frame 
    {
        UpdateLevelImage(); 
    }

    private void UpdateLevelImage()
    {
        if(!unlocked) //if level is locked 
        {
            unlockImage.gameObject.SetActive(true);
            Debug.Log("Level is locked"); 
        }
        else //if level is unlocked
        {
            unlockImage.gameObject.SetActive(false);
            Debug.Log("Level is unlocked");
        }
    }

    public string getLevelName()
    {
        return levelName;
    }

    public void PressSelection(string _levelName)
    {
        if(unlocked)
        {
            levelName = _levelName;
            // Once the player Icon moves to the selected location then 
            // Load Scene coroutine is called from the Movement Coroutine inside the PlayerIcon
            StartCoroutine(PlayerIcon.instance.Movement(transform.position));
            Debug.Log("Level selected, loading scene: " + _levelName);
        }
    }

    public IEnumerator LoadScene(string _levelName)
    {
        SceneManager.LoadScene(_levelName);
        yield return null; 
    }
}
