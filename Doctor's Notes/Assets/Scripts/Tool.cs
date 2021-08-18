using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tool : GenericObject
{
    public string state;
    private bool isColliding;
    [SerializeField] protected AudioClip workingSound;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
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

    public void OnMouseUp()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 10) == false && collidingWithPatient == false)
        {
            transform.position = prevValidPosition;
        }
        else
        {
            PlayObjectSound(releaseSound);
            isGrabbed = false;
        }
    }

    public abstract void PerformAction(Collider collison);
}
