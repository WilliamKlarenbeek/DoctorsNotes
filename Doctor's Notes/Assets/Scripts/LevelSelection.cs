using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

public class LevelSelection : MonoBehaviour
{
    [SerializeField] private bool unlocked = false; //By default level is locked
    public Image unlockImage;
    public Image playerPositionImage; 

    private void Update() //check on every frame 
    {
        UpdateLevelImage(); 
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

    //Seems outdated.
    public void PressSelection(string _levelName)
    {
        if(unlocked)
        {
            // Player icon is displayed only when the level is selected
            playerPositionImage.gameObject.SetActive(true);
            SceneManager.LoadScene(_levelName);
            //Debug.Log("Level selected, loading scene: " + _levelName);
        }
    }
}
