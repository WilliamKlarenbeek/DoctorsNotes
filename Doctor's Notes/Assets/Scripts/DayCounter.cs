using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayCounter : MonoBehaviour
{
    [SerializeField] private MapSelection mapSelectionDB;
    private Text dayText;

    void Start()
    {
        dayText = gameObject.GetComponent<Text>();
        dayText.text = "Day: " + mapSelectionDB.GetCurrentDay();
    }

    void Update()
    {
        dayText.text = "Day: " + mapSelectionDB.GetCurrentDay();
    }
}
