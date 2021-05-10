using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tool : GenericObject
{
    public string state;
    public string output;

    [SerializeField] protected AudioClip workingSound;
    // Start is called before the first frame update
    void Start()
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

        Book = GameObject.Find("Book_UI").GetComponent<BookScript>();
        state = "ready";
    }

    private void OnTriggerStay(Collider collision)
    {
        if(!(Input.GetMouseButton(0)))
        {
            PerformAction(collision);
        }
    }

    public abstract void PerformAction(Collider collison);
}
