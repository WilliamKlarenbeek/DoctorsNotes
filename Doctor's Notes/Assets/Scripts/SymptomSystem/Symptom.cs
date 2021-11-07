using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Symptom : MonoBehaviour
{
    Patient symptomManager;
    public SymptomDatabase symptomDB;

    float timer = 0;
    float buildUpAmount;
    bool buildingUp = true;
    int nextTic = 1;
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
        timer += Time.deltaTime;
        if(timer > nextTic && buildingUp)
        {
            symptomManager.updateValues(this, id, 0, 0, 0, (Mathf.Round(buildUpAmount * 1000f) / 1000f));
            nextTic += 1;
        }
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

    public void calculateValues(float min, float max, float buildup)
    {
        symptomManager.recordValues(this, (Mathf.Round(Random.Range(min, max) * 10f) / 10f), (Mathf.Round(Random.Range(min, max) * 10f) / 10f), (Mathf.Round(Random.Range(min, max) * 10f) / 10f), 0);
        buildUpAmount = buildup;
    }
    public void destroySelf()
    {
        Destroy(gameObject);
    }

    public void StopBuildup()
    {
        buildingUp = false;
    }

    public void setValues(float red, float blue, float green)
    {
        symptomManager.recordValues(this, red, blue, green, 0);
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
