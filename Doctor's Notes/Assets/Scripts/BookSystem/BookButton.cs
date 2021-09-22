using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BookButton : GenericButton
{
    public AudioClip altClick;

    private bool bookOpen = false;
    [SerializeField] private BookScript Book; 


    protected override void Start()
    {
        Book = GameObject.Find("Book_UI").GetComponent<BookScript>();
        Debugger.debuggerInstance.WriteToFileTag("bookbutton");
        GetController();
    }

    void Update()
    {
        if (Book.IsTransitioning())
        {
            GetComponent<Button>().interactable = false; 


        } else
        {
            GetComponent<Button>().interactable = true;
        }
    }

    protected override void TaskOnClick()
    {
        if (sndManager != null)
        {
            if(bookOpen == false)
            {
                sndManager.PlaySound(Audio);
                bookOpen = true;
                Debugger.debuggerInstance.WriteToFile("bookOpen" + bookOpen); 
            } 
            else
            {
                sndManager.PlaySound(altClick);
                bookOpen = false;
                Debugger.debuggerInstance.WriteToFile("bookOpen" + bookOpen);
            }
        }
        else
        {
            Debug.Log("Sound Manager Does Not Exist!");
        }
    }
}
