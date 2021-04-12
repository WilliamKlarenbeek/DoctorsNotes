using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tool : Draggable
{
    public string state;
    GameObject output;
    // Start is called before the first frame update
    void Start()
    {
        state = "ready";
    }

    private void OnTriggerStay(Collider collision)
    {
        if ((collision.gameObject.GetComponent<Material>() != null) && !(Input.GetMouseButton(0)) && (state == "ready"))
        {
            Debug.Log("Material Collision");
            state = "working";
            PerformAction(collision);
        }
        else if ((collision.gameObject.GetComponent<Beaker>() != null) && !(Input.GetMouseButton(0)) && (state == "working"))
        {
            Debug.Log("Beaker Collision");
            state = "ready";
            PerformAction(collision);
        }
    }

    public abstract void PerformAction(Collider collison);
}
