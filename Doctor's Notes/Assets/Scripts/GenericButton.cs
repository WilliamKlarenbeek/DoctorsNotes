using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GenericButton : MonoBehaviour
{
    public AudioClip Audio;
    public GameObject Controller;

    protected SoundManager sndManager;
    private Button genericButton;

    protected virtual void Start()
    {
        GetController();
    }

    protected virtual void TaskOnClick()
    {
        if (sndManager != null)
        {
            sndManager.PlaySound(Audio);
        } else
        {
            Debug.Log("Sound Manager Does Not Exist!");
        }
    }

    protected void GetController()
    {
        if (Controller == null)
        {
            Controller = GameObject.Find("Controller");
        }
        if (Controller != null)
        {
            if (Controller.GetComponent<SoundManager>() != null)
            {
                sndManager = Controller.GetComponent<SoundManager>();
            }
        }

        genericButton = GetComponent<Button>();
        if (genericButton != null)
        {
            genericButton.onClick.AddListener(TaskOnClick);
        }
    }
}