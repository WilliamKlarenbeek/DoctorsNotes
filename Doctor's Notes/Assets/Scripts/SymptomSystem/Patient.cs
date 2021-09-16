using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Patient : MonoBehaviour
{
    [SerializeField] Button resultButton;
    public PatientDatabase patientDB;

    private Color symptomColour;
    bool cured = false;
    float blackLevels;

    //Objects representing symptoms in the scene
    private List<Symptom> symptoms = new List<Symptom>();
    int symptomAmount;

    //Lists to match the dynamic nature of symptom object adding
    private List<List<float>> symptomValues = new List<List<float>>();

    private List<float> tempList = new List<float>();

    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = patientDB.getRandomImage();
    }

    public void recordValues(Symptom symp, float red, float blue, float green, float black)
    {
        tempList.Add(red);
        tempList.Add(blue);
        tempList.Add(green);
        tempList.Add(black);
        symptomValues.Add(new List<float>(tempList));
        tempList.Clear();
        symp.GetComponent<Renderer>().material.color = new Color(red, green, blue, 1);
        symp.ID = symptomAmount;
        symptomAmount++;
    }

    public void updateValues(Symptom symp, int id,  float redChange, float blueChange, float greenChange, float blackChange)
    {
        symptomValues[id][0] = symptomValues[id][0] - redChange;
        symptomValues[id][1] = symptomValues[id][1] - blueChange;
        symptomValues[id][2] = symptomValues[id][2] - greenChange;
        symptomValues[id][3] = symptomValues[id][3] + blackChange;
        symp.GetComponent<Renderer>().material.color = new Color(symptomValues[id][0], symptomValues[id][2], symptomValues[id][1], 1);
        Debug.Log("Red: " + symptomValues[id][0]);
        Debug.Log("Blue: " + symptomValues[id][1]);
        Debug.Log("Green: " + symptomValues[id][2]);
        Debug.Log("Black: " + symptomValues[id][3]);

        cured = true;
        blackLevels = 0;
        for(int x = 0; x < symptomValues.Count; x++)
        {
            blackLevels += symptomValues[x][3];
            if(symptomValues[x][0] > 0 || symptomValues[x][1] > 0 || symptomValues[x][2] > 0)
            {
                cured = false;
            }
        }

        if (blackLevels >= 2)
        {
            Debug.Log("Dead");
            resultButton.gameObject.SetActive(true);
            resultButton.gameObject.GetComponentInChildren<Text>().text = "Patient has Died";
        }
        else if (cured == true)
        {
            Debug.Log("Cured");
            PlayerPrefs.SetInt("money", (PlayerPrefs.GetInt("money") + 50));
            resultButton.gameObject.SetActive(true);
            resultButton.gameObject.GetComponentInChildren<Text>().text = "Patient has been Cured";
        }
    }
}