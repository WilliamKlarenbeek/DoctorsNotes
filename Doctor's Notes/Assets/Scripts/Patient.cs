using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Patient : MonoBehaviour
{
    private float redLevels = 0;
    private float blueLevels = 1.7f;
    private float yellowLevels = 0;
    private float blackLevels = 0;
    private bool isColliding;

    [SerializeField] Button resultButton;

    // Start is called before the first frame update
    void Start()
    {
        Debugger.debuggerInstance.WriteToFileTag("Patient"); 
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
            redLevels -= givenPotion.Red;
            blueLevels -= givenPotion.Blue;
            yellowLevels -= givenPotion.Green;
            blackLevels += givenPotion.Black;

            Debug.Log("Red left: " + redLevels);
            Debug.Log("Blue left: " + blueLevels);
            Debug.Log("Yellow left: " + yellowLevels);
            Debug.Log("Black buildup: " + blackLevels);

            Destroy(collision.gameObject);

            if (blackLevels >= 1)
            {
                Debug.Log("Dead");
                resultButton.gameObject.SetActive(true);
                resultButton.gameObject.GetComponentInChildren<Text>().text = "Patient has Died";
            }
            else if ((redLevels <= 0) && (blueLevels <= 0) && (yellowLevels <= 0))
            {
                Debug.Log("Cured");
                PlayerPrefs.SetInt("money", (PlayerPrefs.GetInt("money") + 50));
                resultButton.gameObject.SetActive(true);
                resultButton.gameObject.GetComponentInChildren<Text>().text = "Patient has been Cured";
            }
        }
    }
}