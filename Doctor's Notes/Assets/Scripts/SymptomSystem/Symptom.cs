using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Symptom : MonoBehaviour
{
    Patient symptomManager;

    private int id;
    private bool isColliding;

    // Start is called before the first frame update
    void Awake()
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

    public void calculateValues(float min, float max)
    {
        symptomManager.recordValues(this, (Mathf.Round(Random.Range(min, max) * 100f) / 100f), Random.Range(min, max), Random.Range(min, max), 0);
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
