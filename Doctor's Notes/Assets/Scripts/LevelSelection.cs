using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

public class LevelSelection : MonoBehaviour
{
    [SerializeField] private bool unlocked = false; //By default level is locked
    public Image unlockImage;


    private void Update() //check on every frame 
    {
        UpdateLevelImage(); 
    }

    private void UpdateLevelImage()
    {
        if(!unlocked) //if level is locked 
        {
            unlockImage.gameObject.SetActive(true); 
        }
        else //if level is unlocked
        {
            unlockImage.gameObject.SetActive(false); 
        }
    }

    public void PressSelection(string _levelName)
    {
        if(unlocked)
        {
            SceneManager.LoadScene(_levelName);
        }
    }
}
