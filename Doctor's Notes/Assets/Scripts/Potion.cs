using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : GenericObject
{
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.GetComponent<Patient>() != null)
        {
            CollidingWithPatient(true);
        } else
        {
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
}
