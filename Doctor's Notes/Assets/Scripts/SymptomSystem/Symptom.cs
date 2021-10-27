using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Symptom : MonoBehaviour
{
    Patient symptomManager;
    public SymptomDatabase symptomDB;

    private int id;
    private bool isColliding;

    // Start is called before the first frame update
    void Awake()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = symptomDB.getRandomImage();
        symptomManager = GameObject.Find("Patient").GetComponent<Patient>();
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
        symptomManager.recordValues(this, (Mathf.Round(Random.Range(min, max) * 10f) / 10f), (Mathf.Round(Random.Range(min, max) * 10f) / 10f), (Mathf.Round(Random.Range(min, max) * 10f) / 10f), 0);
    }
    public void destroySelf()
    {
        Destroy(gameObject);
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
