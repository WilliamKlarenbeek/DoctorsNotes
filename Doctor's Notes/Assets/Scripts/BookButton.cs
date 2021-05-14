using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookButton : GenericButton
{
    public AudioClip altClick;

    private bool bookOpen = false;

    protected override void TaskOnClick()
    {
        if (sndManager != null)
        {
            if(bookOpen == false)
            {
                sndManager.PlaySound(Audio);
                Debug.Log("Book Opened");
                bookOpen = true;
            } 
            else
            {
                sndManager.PlaySound(altClick);
                Debug.Log("Book Closed");
                bookOpen = false;
            }
        }
        else
        {
            Debug.Log("Sound Manager Does Not Exist!");
        }
    }
}
