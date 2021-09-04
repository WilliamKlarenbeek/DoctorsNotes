using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Symptom : MonoBehaviour
{
    Patient symptomManager;

    private int id;
    private bool isColliding;

    // Start is called before the first frame update
    void Start()
    {
        symptomManager = GameObject.Find("Patient").GetComponent<Patient>();
        symptomManager.recordValues(this, Random.Range(0, 2.5f), Random.Range(0, 2.5f), Random.Range(0, 2.5f), 0);
    }

    // Update is called once per frame
    void Update()
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

        if (!(Input.GetMouseButton(0)) && (collision.gameObject.GetComponent<Potion>() != null))
        {
            Potion givenPotion = collision.gameObject.GetComponent<Potion>();
            Destroy(collision.gameObject);
            symptomManager.updateValues(this, id, givenPotion.Red, givenPotion.Blue, givenPotion.Green, givenPotion.Black);
        }
    }

    public int ID
    {
        get
        {
            return id;
        }
        set
        {
            id = value;
        }
    }
}
