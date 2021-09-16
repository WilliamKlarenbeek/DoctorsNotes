using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Patient : MonoBehaviour
{
    [SerializeField] Button resultButton;

    private bool isColliding;
    private Color symptomColour; 
    public GameObject patient; 

    //Objects representing symptoms in the scene 
    public GameObject[] symptomObj; 


    //Lists to match the dynamic nature of symptom object adding
    private List<float> redLevels = new List<float>(); 
    private List<float> greenLevels = new List<float>(); 
    private List<float> blueLevels = new List<float>(); 
    private List<float> blackLevels = new List<float>();

    //Saved colour variables for potions to work with
    private List<float> redValues = new List<float>(); 
    private List<float> blueValues = new List<float>(); 
    private List<float> greenValues = new List<float>(); 
    private List<float> blackValues = new List<float>();


    // Start is called before the first frame update
    void Start()
    {
        GenerateSymptoms();
    }

    // Update is called once per frame
    void Update()
    {
        isColliding = false;
    }

    private void GenerateSymptoms()
    {   
        int symptomNum = symptomObj.Length; 
        
        //Randomly generate an RBG value for each symptom game object 
        for(int i = 0; i < symptomNum; i++)
        {
            redLevels.Add(Random.Range(0.5f, 1));
            greenLevels.Add(Random.Range(0.5f, 1));
            blueLevels.Add(Random.Range(0.5f, 1));
            
            redValues.Add(redLevels[i]);
            greenValues.Add(greenLevels[i]);
            blueValues.Add(greenLevels[i]);
            
            symptomColour = new Color(redLevels[i], greenLevels[i], blueLevels[i], 1);   
            symptomObj[i].GetComponent<Renderer>().material.color = symptomColour; 
        }
    }

//  private void OnTriggerStay(Collider collision)
//     {
//         for (int i = 0; i < symptomNum; i++)
//         {
//              if (collision.symptomObj[i] ==true)
//              {
//                  Debug.Log("hello " + symptomObj[i]); 
//              }
//         }

//     }
// }


//     private void OnTriggerStay(Collider collision)
//     {
//         if (isColliding)
//         {
//             return;
//         }
//         isColliding = true;

//         if (!(Input.GetMouseButton(0)) && (collision.gameObject.GetComponent<Potion>() != null))
//         {
//             Potion givenPotion = collision.gameObject.GetComponent<Potion>();


//             redLevels -= givenPotion.Red;
//             blueLevels -= givenPotion.Blue;
//             yellowLevels -= givenPotion.Green;
//             blackLevels += givenPotion.Black;

//             Debug.Log("Red left: " + redLevels);
//             Debug.Log("Blue left: " + blueLevels);
//             Debug.Log("Yellow left: " + yellowLevels);
//             Debug.Log("Black buildup: " + blackLevels);

//             Destroy(collision.gameObject);

//             if (blackLevels >= 1)
//             {
//                 Debug.Log("Dead");
//                 resultButton.gameObject.SetActive(true);
//                 resultButton.gameObject.GetComponentInChildren<Text>().text = "Patient has Died";
//             }
//             else if ((redLevels <= 0) && (blueLevels <= 0) && (yellowLevels <= 0))
//             {
//                 Debug.Log("Cured");
//                 PlayerPrefs.SetInt("money", (PlayerPrefs.GetInt("money") + 50));
//                 resultButton.gameObject.SetActive(true);
//                 resultButton.gameObject.GetComponentInChildren<Text>().text = "Patient has been Cured";
//             }
//         }
//     }
}