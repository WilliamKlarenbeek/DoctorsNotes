using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Patient : MonoBehaviour
{
    enum DebugMode
    {
        None,
        AutoWin,
        AutoLose
    }

    //[SerializeField] Button resultButton;
    [SerializeField] PatientSceneHandler patientSceneController;
    public PatientDatabase patientDB;

    private Color symptomColour;
    bool cured = false;
    float blackLevels;

    //Objects representing symptoms in the scene
    private List<Symptom> symptoms = new List<Symptom>();
    int symptomAmount;

    int rngVal;
    private List<int> symptomLocations = new List<int>();
    float rgbMax;
    float rgbMin;

    //Lists to match the dynamic nature of symptom object adding
    private List<List<float>> symptomValues = new List<List<float>>();

    private List<float> tempList = new List<float>();

    //For Debugging Purposes. Auto-Win
    [SerializeField] DebugMode debugMode = DebugMode.None;

    [SerializeField] private MapSelection mapDB;

    void Start()
    {
        if(patientSceneController == null)
        {
            patientSceneController = GameObject.Find("Controller").GetComponent<PatientSceneHandler>();
        }
        if(mapDB == null && patientSceneController != null)
        {
            mapDB = patientSceneController.gameObject.GetComponent<GameController>().GetMapDatabase();
        }
        gameObject.GetComponent<SpriteRenderer>().sprite = patientDB.getRandomImage();

        rgbMin = 0.1f;
        rgbMax = 0.6f;

        for (int i = 0; i < mapDB.GetCurrentDay(); i = i + 5)
        {
            if (symptomLocations.Count == 0)
            {
                rngVal = 2;
            }
            else if ((symptomLocations.Count == 3) && (rgbMax - rgbMin <= 0.5))
            {
                rngVal = 1;
            }
            else if (symptomLocations.Count == 3)
            {
                rngVal = Mathf.RoundToInt(Random.Range(0, 2));
            }
            else if (rgbMax - rgbMin <= 0.5)
            {
                rngVal = Mathf.RoundToInt(Random.Range(1, 3));
            }
            else
            {
                rngVal = Mathf.RoundToInt(Random.Range(0, 3));
            }

            //increase symptoms by 1, increase rgb max by 0.5, increase rgb min by 0.5
            if (rngVal == 2) //increase symptoms by 1, to a max of 3
            {
                rngVal = Mathf.RoundToInt(Random.Range(0, 3));
                while ((symptomLocations.Contains(rngVal)))
                {
                    rngVal = Mathf.RoundToInt(Random.Range(0, 3));
                }
                if (rngVal == 2) //head symptom
                {
                    symptomLocations.Add(2);
                }
                else if (rngVal == 1) //arm symptom
                {
                    symptomLocations.Add(1);
                }
                else //leg symptom
                {
                    symptomLocations.Add(0);
                }
            }
            else if (rngVal == 1) //increase rbgMax by 0.5
            {
                rgbMax += 0.5f;
            }
            else //increase rbgMin by 0.5
            {
                rgbMin += 0.5f;
            }
        }
        for (int i = 0; i < symptomLocations.Count; i++)
        {
            if (symptomLocations[i] == 2) //make head symptom
            {
                GameObject objectInstance = Instantiate(Resources.Load("Prefabs/SpriteSymptomPrefab"), new Vector3(-6f, 1.97f, 4.91f), Quaternion.Euler(new Vector3(0, 0, 0)), gameObject.transform) as GameObject;
                Symptom newSymptom = objectInstance.GetComponent<Symptom>();
                newSymptom.calculateValues(rgbMin, rgbMax);
            }
            else if (symptomLocations[i] == 1) //make arm symptom
            {
                GameObject objectInstance = Instantiate(Resources.Load("Prefabs/SpriteSymptomPrefab"), new Vector3(-7.24f, 1.785f, 1.355f), Quaternion.Euler(new Vector3(0, -77.27f, 0)), gameObject.transform) as GameObject;
                Symptom newSymptom = objectInstance.GetComponent<Symptom>();
                newSymptom.calculateValues(rgbMin, rgbMax);
            }
            else //make leg symptom
            {
                GameObject objectInstance = Instantiate(Resources.Load("Prefabs/SpriteSymptomPrefab"), new Vector3(-5.72f, 1.97f, -0.835f), Quaternion.Euler(new Vector3(-180, 150.768f, -180)), gameObject.transform) as GameObject;
                Symptom newSymptom = objectInstance.GetComponent<Symptom>();
                newSymptom.calculateValues(rgbMin, rgbMax);
            }
        }

        //Debug.Log("Symptom Amount: " + symptomLocations.Count);

        symptomLocations.Clear();
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

        switch (debugMode)
        {
            case DebugMode.AutoWin:
                symptomValues[id][0] = 0;
                symptomValues[id][1] = 0;
                symptomValues[id][2] = 0;
                symptomValues[id][3] = 0;
                break;
            case DebugMode.AutoLose:
                symptomValues[id][0] = 1;
                symptomValues[id][1] = 1;
                symptomValues[id][2] = 1;
                symptomValues[id][3] = 2;
                break;
            default:
                break;
        }

        symp.GetComponent<Renderer>().material.color = new Color(symptomValues[id][0], symptomValues[id][2], symptomValues[id][1], 1);

        cured = true;
        blackLevels = 0;

        Debug.Log("Symptom Amount: " + symptomValues.Count);

        Debug.Log("Red: " + symptomValues[id][0]);
        Debug.Log("Blue: " + symptomValues[id][1]);
        Debug.Log("Green: " + symptomValues[id][2]);
        Debug.Log("Black: " + symptomValues[id][3]);

        for (int x = 0; x < symptomValues.Count; x++)
        {
            blackLevels += symptomValues[x][3];
            if(symptomValues[x][0] > 0 || symptomValues[x][1] > 0 || symptomValues[x][2] > 0)
            {
                cured = false;
                Debug.Log("Not Cured Yet.");
            }
        }

        if (blackLevels >= 2)
        {
            Debug.Log("Dead");
            patientSceneController.PrintEvent(false, 0);
            //resultButton.gameObject.SetActive(true);
            //resultButton.gameObject.GetComponentInChildren<Text>().text = "Patient has Died";
        }
        else if (cured == true)
        {
            Debug.Log("Cured");
            PlayerPrefs.SetInt("money", (PlayerPrefs.GetInt("money") + 50));
            patientSceneController.PrintEvent(cured, 50);
            //resultButton.gameObject.SetActive(true);
            //resultButton.gameObject.GetComponentInChildren<Text>().text = "Patient has been Cured";
        }
        
    }
}