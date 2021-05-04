using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : GenericObject
{

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
