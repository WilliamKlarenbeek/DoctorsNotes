using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PatientSceneHandler : MonoBehaviour
{
    public GameObject resultBox;

    private Text resultNameText;
    private Text resultDescText;
    private Text resultResultText;
    private GameController Controller;

    void Start()
    {
        resultNameText = resultBox.transform.Find("ResultName").GetComponent<Text>();
        resultDescText = resultBox.transform.Find("ResultDesc").GetComponent<Text>();
        resultResultText = resultBox.transform.Find("Result").GetComponent<Text>();
        Controller = gameObject.GetComponent<GameController>();

        resultBox.SetActive(false);
    }

    public void PrintEvent(bool aFlag, int aMoney)
    {
        resultBox.SetActive(true);

        if(aFlag == true)
        {
            resultNameText.text = "Healed!";
            resultDescText.text = "You have cured your patient successfully!";
            resultResultText.text = "You earned " + aMoney + " Gold.";
        } 
        else
        {
            resultNameText.text = "Failed!";
            resultDescText.text = "Your patient has died!";
            resultResultText.text = "";
        }
    }
}
