using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapEventHandler : MonoBehaviour
{
    private List<MapEvent> eventList;
    public MapEventDatabase eventDB;
    public Inventory inventoryDB;
    public MapSelection mapSelectionDB;
    public GameObject eventBox;
    public List<string> patientScenes = new List<string>();

    private Text eventNameText;
    private Text eventDescText;
    private Text eventResultText;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Entered Map Scene");
        UpdateEventList();

        if(eventBox != null)
        {
            eventNameText = eventBox.transform.Find("EventName").GetComponent<Text>();
            eventDescText = eventBox.transform.Find("EventDesc").GetComponent<Text>();
            eventResultText = eventBox.transform.Find("EventResult").GetComponent<Text>();
        }

        eventBox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RandomEvent()
    {
        int index = 0;
        MapEvent selectedEvent = null;

        if (eventList.Count > 0)
        {
            index = Mathf.RoundToInt(Random.Range(0, eventList.Count - 1));
            selectedEvent = eventList[index];

            switch (selectedEvent.eventOutcome)
            {
                case (MapEvent.EventOutcome)0:
                    ResourceEvent(selectedEvent);
                    break;
                case (MapEvent.EventOutcome)1:
                    TimeEvent(selectedEvent);
                    break;
                default:
                    break;
            }
        }
    }

    private void ResourceEvent(MapEvent aEvent)
    {
        int amount = 0;
        int randIndex = 0;
        string resultMessage = "";

        amount = Mathf.RoundToInt(Random.Range(1, 4));
        randIndex = Mathf.RoundToInt(Random.Range(0, inventoryDB.materialList.Count - 1));

        switch (aEvent.eventType)
        {
            case (MapEvent.EventType)0:
                inventoryDB.materialList[randIndex].itemQuantity += amount;
                resultMessage = "You gained " + amount + " " + inventoryDB.materialList[randIndex].itemName;
                break;
            case (MapEvent.EventType)1:
                inventoryDB.materialList[randIndex].itemQuantity -= amount;
                if (inventoryDB.materialList[randIndex].itemQuantity < 0)
                {
                    inventoryDB.materialList[randIndex].itemQuantity = 0;
                }
                resultMessage = "You lost " + amount + " " + inventoryDB.materialList[randIndex].itemName;
                break;
            default:
                break;
        }

        PrintEvent(aEvent, resultMessage);
    }

    private void TimeEvent(MapEvent aEvent)
    {
        int amount = 0;
        int randIndex = 0;
        string resultMessage = "";

        amount = Mathf.RoundToInt(Random.Range(1, 4));
        switch (aEvent.eventType)
        {
            case (MapEvent.EventType)0:
                mapSelectionDB.maxDay += amount;
                resultMessage = "The plague slowed down by " + amount + " days.";
                break;
            case (MapEvent.EventType)1:
                mapSelectionDB.currentDay += amount;
                if(mapSelectionDB.currentDay > mapSelectionDB.maxDay)
                {
                    mapSelectionDB.currentDay = mapSelectionDB.maxDay;
                }
                resultMessage = "You stalled for " + amount + " days.";
                break;
            default:
                break;
        }

        PrintEvent(aEvent, resultMessage);
    }

    private void PrintEvent(MapEvent aEvent, string aOutcome)
    {
        eventBox.SetActive(true);

        eventNameText.text = aEvent.eventName;
        eventDescText.text = aEvent.eventDesc;
        eventResultText.text = aOutcome;
    }

    public void CloseEvent()
    {
        int randIndex = Mathf.RoundToInt(Random.Range(0, patientScenes.Count));

        StartCoroutine(SceneController.LoadScene(patientScenes[randIndex],2f));

        eventBox.SetActive(false);
    }

    public void UpdateEventList() {
        eventList = new List<MapEvent>();

        foreach(MapEvent i in eventDB.eventList)
        {
            for(int j = 0; j < i.eventChance; j++)
            {
                eventList.Add(i);
            }
        }
    }
}
