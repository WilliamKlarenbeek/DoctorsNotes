using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tool : GenericObject
{
    public string state;
    private bool isColliding;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        state = "ready";
    }

    public virtual void Update()
    {
        isColliding = false;
    }

    private void OnTriggerStay(Collider collision)
    {
        if (isColliding)
        {
            return;
        }
        isColliding = true;

        if (!(Input.GetMouseButton(0)))
        {
            PerformAction(collision);
        }
    }

    public abstract void PerformAction(Collider collison);
}
