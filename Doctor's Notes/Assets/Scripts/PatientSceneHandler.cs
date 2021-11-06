using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PatientSceneHandler : MonoBehaviour
{
    public GameObject resultBox;
    public GameObject resultPanel;

    private Text resultNameText;
    private Text resultDescText;
    private Text resultResultText;
    private GameController Controller;
    private SoundManager sndManager;
    public AudioClip goodEventStinger;
    public AudioClip badEventStinger;

    void Start()
    {
        resultNameText = resultBox.transform.Find("ResultName").GetComponent<Text>();
        resultDescText = resultBox.transform.Find("ResultDesc").GetComponent<Text>();
        resultResultText = resultBox.transform.Find("Result").GetComponent<Text>();
        Controller = gameObject.GetComponent<GameController>();

        if (Controller.GetComponent<SoundManager>() != null)
        {
            sndManager = Controller.GetComponent<SoundManager>();
        }

        resultPanel.SetActive(false);
        resultBox.SetActive(false);
    }

    public void PrintEvent(bool aFlag, int aMoney, bool aForfeit = false)
    {
        resultPanel.SetActive(true);
        resultBox.SetActive(true);

        if(aFlag == true)
        {
            resultNameText.text = "Healed!";
            resultDescText.text = "You have cured your patient successfully!";
            resultResultText.text = "You earned " + aMoney + " Gold.";
            sndManager.PlaySound(goodEventStinger);
        } 
        else
        {
            if(aForfeit == true)
            {
                resultNameText.text = "Failed!";
                resultDescText.text = "You've given up on your patient and left them to die...";
                resultResultText.text = "";
                sndManager.PlaySound(badEventStinger);
            } else
            {
                resultNameText.text = "Failed!";
                resultDescText.text = "Your patient has died!";
                resultResultText.text = "";
                sndManager.PlaySound(badEventStinger);
            }
        }
    }
}
