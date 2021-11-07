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
    public AudioClip goodEventStinger;
    public AudioClip badEventStinger;
    public AudioClip goodEndingStinger;
    public AudioClip badEndingStinger;

    private Text eventNameText;
    private Text eventDescText;
    private Text eventResultText;
    private GameController Controller;
    private bool endState = false;
    private int endingNumber = -1;
    private SoundManager sndManager;

    // Start is called before the first frame update
    void Start()
    {
        Controller = GameObject.Find("Controller").GetComponent<GameController>();

        if (Controller.GetComponent<SoundManager>() != null)
        {
            sndManager = Controller.GetComponent<SoundManager>();
        }

        if (mapSelectionDB.EndStateCheck() != -1)
        {
            EndingInit(mapSelectionDB.EndStateCheck());
        }

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
        if(Controller.isTransitioning() == false && endState == true)
        {
            EndingEvent();
            endState = false;
        }
    }

    public void RandomEvent()
    {
        int index = 0;
        MapEvent selectedEvent = null;
        gameObject.GetComponent<PauseScript>().ToggleCanPause(false);

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

        amount = Mathf.RoundToInt(Random.Range(10, 50));

        switch (aEvent.eventType)
        {
            case (MapEvent.EventType)0:
                PlayerPrefs.SetInt("money", (PlayerPrefs.GetInt("money") + amount));
                resultMessage = "You gained " + amount + " gold.";
                sndManager.PlaySound(goodEventStinger);
                break;
            case (MapEvent.EventType)1:
                PlayerPrefs.SetInt("money", (PlayerPrefs.GetInt("money") - amount));
                if(PlayerPrefs.GetInt("money") < 0)
                {
                    PlayerPrefs.SetInt("money", 0);
                }
                resultMessage = "You lost " + amount + " gold.";
                sndManager.PlaySound(badEventStinger);
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
                mapSelectionDB.SetBonusDay(mapSelectionDB.GetBonusDay() + amount);
                resultMessage = "The plague slowed down by " + amount + " days.";
                sndManager.PlaySound(goodEventStinger);
                break;
            case (MapEvent.EventType)1:
                mapSelectionDB.SetCurrentDay(mapSelectionDB.GetCurrentDay() + amount);
                if(mapSelectionDB.GetCurrentDay() > (mapSelectionDB.GetMaxDay() + mapSelectionDB.GetBonusDay()))
                {
                    mapSelectionDB.SetCurrentDay((mapSelectionDB.GetMaxDay() + mapSelectionDB.GetBonusDay()));
                }
                resultMessage = "You stalled for " + amount + " days.";
                sndManager.PlaySound(badEventStinger);
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

        if (PlayerPrefs.GetString("scene") == "TutorialScene")
        {
            StartCoroutine(SceneController.LoadScene("TutorialScene", 2f));
        }
        else if (endingNumber == -1)
        {
            StartCoroutine(SceneController.LoadScene(patientScenes[randIndex], 2f));
        } else
        {
            mapSelectionDB.SetGameBeginFlag(true);
            StartCoroutine(SceneController.LoadScene("Ending" + endingNumber, 2f));
        }
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

    public void EndingInit(int aEnding)
    {
        endState = true;
        endingNumber = aEnding;
        sndManager.StopMusic(true);
    }

    //0 - Out of Time Ending
    //1 - Most People Dead Ending
    //2 - Most People Saved Ending
    public void EndingEvent()
    {
        Debug.Log("End State Reached");

        string endingName = "";
        string endingDesc = "";
        string endingOutcome = "";

        eventBox.SetActive(true);

        switch (endingNumber)
        {
            case 0:
                endingName = "Game Over!";
                endingDesc = "The plague has consumed the entire country. Seems you were too late in preventing its total eradication...";
                sndManager.PlayMusic(badEndingStinger);
                break;
            case 1:
                endingName = "Most Towns Saved!";
                endingDesc = "Most of the towns of the country have been saved from the spread of this deadly plague!";
                sndManager.PlayMusic(goodEndingStinger);
                break;
            case 2:
                endingName = "Most Towns Dead!";
                endingDesc = "Most of the towns of the country that have been infected, have unfortunately died out...";
                sndManager.PlayMusic(badEndingStinger);
                break;
            default:
                endingName = "Game Over!";
                endingDesc = "The plague has consumed the entire country. Seems you were too late in preventing its total eradication...";
                sndManager.PlayMusic(badEndingStinger);
                break;
        }
        endingOutcome = "Patients Saved: " + mapSelectionDB.GetLocationSaved();
        eventNameText.text = endingName;
        eventDescText.text = endingDesc;
        eventResultText.text = endingOutcome;
    }
}
