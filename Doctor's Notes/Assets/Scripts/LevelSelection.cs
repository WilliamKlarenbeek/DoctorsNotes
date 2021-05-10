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
            Debug.Log("Level is locked"); 
        }
        else //if level is unlocked
        {
            unlockImage.gameObject.SetActive(false);
            Debug.Log("Level is unlocked");
        }
    }

    public void PressSelection(string _levelName)
    {
        if(unlocked)
        {
            StartCoroutine(PlayerIcon.instance.Movement(transform.position));
            //SceneManager.LoadScene(_levelName);
            Debug.Log("Level selected, loading scene: " + _levelName);
        }
    }
}
