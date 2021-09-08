using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : GenericObject
{
    public float red;
    public float blue;
    public float green;
    public float black;
    List<Ingredient> ingredients = new List<Ingredient>();

    public override void Start()
    {
        base.Start();
        gameObject.GetComponent<Renderer>().material.color = new Color(red * (2 - black), green * (2 - black), blue * (2 - black), 1);
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.GetComponent<Patient>() != null)
        {
            Debug.Log("Colliding with patient?");
            CollidingWithPatient(true);
        }
        else
        {
            Debug.Log("Not colliding with patient?");
            CollidingWithPatient(false);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.GetComponent<Patient>() != null)
        {
            CollidingWithPatient(false);
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

    public float Red
    {
        get
        {
            return this.red;
        }
        set
        {
            this.red = value;
        }
    }
    public float Blue
    {
        get
        {
            return this.blue;
        }
        set
        {
            this.blue = value;
        }
    }
    public float Green
    {
        get
        {
            return this.green;
        }
        set
        {
            this.green = value;
        }
    }
    public float Black
    {
        get
        {
            return this.black;
        }
        set
        {
            this.black = value;
        }
    }
    public List<Ingredient> Ingredients
    {
        get
        {
            return this.ingredients;
        }
        set
        {
            this.ingredients = value;
        }
    }
}
