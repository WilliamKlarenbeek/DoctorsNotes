using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tool : Draggable
{
    public string state;
    public string output;
    // Start is called before the first frame update
    void Start()
    {
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
