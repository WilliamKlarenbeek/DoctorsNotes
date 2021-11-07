using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

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
    float buildup;

    //Lists to match the dynamic nature of symptom object adding
    private List<List<float>> symptomValues = new List<List<float>>();

    private List<float> tempList = new List<float>();

    //For Debugging Purposes. Auto-Win
    [SerializeField] DebugMode debugMode = DebugMode.None;

    [SerializeField] private MapSelection mapDB;

    //Default gold reward bonuses
    [SerializeField] private int baseBonus = 50;
    [SerializeField] private int maximumBonus = 150;
    private float blackPenalty = 0;
    private int currentBonus;

    void Start()
    {
        if (patientSceneController == null)
        {
            patientSceneController = GameObject.Find("Controller").GetComponent<PatientSceneHandler>();
        }
        if (mapDB == null && patientSceneController != null)
        {
            mapDB = patientSceneController.gameObject.GetComponent<GameController>().GetMapDatabase();
        }

        gameObject.GetComponent<SpriteRenderer>().sprite = patientDB.getRandomImage();

        rgbMin = 0.1f;
        rgbMax = 0.5f;
        buildup = 0.005f;

        for (int i = 0; i <= mapDB.GetCurrentDay(); i = i + 3)
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
            else if (rgbMax - rgbMin <= 0.4f)
            {
                rngVal = Mathf.RoundToInt(Random.Range(1, 3));
            }
            else
            {
                rngVal = Mathf.RoundToInt(Random.Range(0, 3));
            }

            if (rngVal == 2) //increase symptoms by 1, to a max of 3
            {
                Debug.Log("a");
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
                if (symptomLocations.Count == 1)
                {
                    buildup = 0.03f;
                }
                else if (symptomLocations.Count == 2)
                {
                    buildup = 0.015f;
                }
                else if (symptomLocations.Count == 3)
                {
                    buildup = 0.01f;
                }
            }
            else if (rngVal == 1) //increase rbgMax by 0.5
            {
                Debug.Log("b");
                rgbMax += 0.4f;
            }
            else if (rngVal == 0) //increase rbgMin by 0.4
            {
                Debug.Log("c");
                rgbMin += 0.2f;
            }
        }
        for (int i = 0; i < symptomLocations.Count; i++)
        {
            if (SceneManager.GetActiveScene().name == "TutorialScene")
            {
                GameObject objectInstance = Instantiate(Resources.Load("Prefabs/SpriteSymptomPrefab"), new Vector3(-6.64f, 1.785f, 1.24f), Quaternion.Euler(new Vector3(173, -50.38f, 151.66f)), gameObject.transform) as GameObject;
                Symptom newSymptom = objectInstance.GetComponent<Symptom>();
                newSymptom.setValues(0.2f, 0.2f, 0.2f);
            }
            //Debug.Log(symptomLocations[i]);
            else if (symptomLocations[i] == 2) //make head symptom
            {
                GameObject objectInstance = Instantiate(Resources.Load("Prefabs/SpriteSymptomPrefab"), new Vector3(-5.39f, 1.97f, 5.68f), Quaternion.Euler(new Vector3(0, 0, 0)), gameObject.transform) as GameObject;
                Symptom newSymptom = objectInstance.GetComponent<Symptom>();
                newSymptom.calculateValues(rgbMin, rgbMax, buildup);
            }
            else if (symptomLocations[i] == 1) //make arm symptom
            {
                GameObject objectInstance = Instantiate(Resources.Load("Prefabs/SpriteSymptomPrefab"), new Vector3(-6.64f, 1.785f, 1.24f), Quaternion.Euler(new Vector3(173, -50.38f, 151.66f)), gameObject.transform) as GameObject;
                Symptom newSymptom = objectInstance.GetComponent<Symptom>();
                newSymptom.calculateValues(rgbMin, rgbMax, buildup);
            }
            else //make leg symptom
            {
                GameObject objectInstance = Instantiate(Resources.Load("Prefabs/SpriteSymptomPrefab"), new Vector3(-5.72f, 1.97f, -0.835f), Quaternion.Euler(new Vector3(-180, 150.768f, -180)), gameObject.transform) as GameObject;
                Symptom newSymptom = objectInstance.GetComponent<Symptom>();
                newSymptom.calculateValues(rgbMin, rgbMax, buildup);
            }
        }

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
        symp.GetComponent<Renderer>().material.color = new Color(Mathf.Clamp(red * (2 - black), 0, 1), Mathf.Clamp(green * (2 - black), 0, 1), Mathf.Clamp(blue * (2 - black), 0, 1));
        symp.ID = symptomAmount;
        symptomAmount++;
    }

    public void KillPatient()
    {
        mapDB.SetWinFlag(false);
        patientSceneController.PrintEvent(false, 0, true);
    }

    public void updateValues(Symptom symp, int id, float redChange, float blueChange, float greenChange, float blackChange)
    {
        currentBonus = baseBonus;

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

        if (symptomValues[id][0] <= 0 && symptomValues[id][1] <= 0 && symptomValues[id][2] <= 0)
        {
            blackPenalty = blackPenalty + symptomValues[id][3];
            symptomValues[id][3] = 0;
            symp.destroySelf();
        }
        else
        {
            symp.GetComponent<Renderer>().material.color = new Color(Mathf.Clamp(symptomValues[id][0] * (2 - symptomValues[id][3]), 0, 1), Mathf.Clamp(symptomValues[id][2] * (2 - symptomValues[id][3]), 0, 1), Mathf.Clamp(symptomValues[id][1] * (2 - symptomValues[id][3]), 0, 1));
        }
        Debug.Log("Red: " + symptomValues[id][0]);
        Debug.Log("Blue: " + symptomValues[id][1]);
        Debug.Log("Green: " + symptomValues[id][2]);
        Debug.Log("Black: " + symptomValues[id][3]);

        cured = true;
        blackLevels = 0;
        for (int x = 0; x < symptomValues.Count; x++)
        {
            blackLevels += symptomValues[x][3];
            if (symptomValues[x][0] > 0 || symptomValues[x][1] > 0 || symptomValues[x][2] > 0)
            {
                Debug.Log("Not Yet Cured.");
                cured = false;
            }
        }

        if (blackLevels >= 2)
        {
            Debug.Log("Dead");
            symp.StopBuildup();
            mapDB.SetWinFlag(false);
            patientSceneController.PrintEvent(false, 0);
            //resultButton.gameObject.SetActive(true);
            //resultButton.gameObject.GetComponentInChildren<Text>().text = "Patient has Died";
        }
        else if (cured == true)
        {
            Debug.Log("Cured");
            symp.StopBuildup();
            currentBonus += Mathf.RoundToInt(maximumBonus * (1 - (blackPenalty / 2)));
            mapDB.SetWinFlag(true);
            PlayerPrefs.SetInt("money", (PlayerPrefs.GetInt("money") + currentBonus));
            patientSceneController.PrintEvent(cured, currentBonus);
            //resultButton.gameObject.SetActive(true);
            //resultButton.gameObject.GetComponentInChildren<Text>().text = "Patient has been Cured";
        }
    }

    public bool checkValues(int id, float redChange, float blueChange, float greenChange)
    {
        Debug.Log("id" + id);
        Debug.Log("red value" + symptomValues[id][0] + "    " + redChange);
        Debug.Log("blue value" + symptomValues[id][1] + "    " + blueChange);
        Debug.Log("green value" + symptomValues[id][2] + "    " + greenChange);
        if (symptomValues[id][0] <= redChange && symptomValues[id][1] <= blueChange && symptomValues[id][2] <= greenChange)
        {
            return true;
        }
        else return false;
        //symptomValues[id][0] = symptomValues[id][0] - redChange;
        //symptomValues[id][1] = symptomValues[id][1] - blueChange;
        //symptomValues[id][2] = symptomValues[id][2] - greenChange;
    }
}