using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapDatabase", menuName = "Map/Map Database")]
public class MapSelection : ScriptableObject
{
    //Maximum locked locations should ideally match the amount of locations in the map scene.
    const int MAXIMUM_LOCKED_LOCATIONS = 2;

    enum PatientOutcome
    {
        Undefined,
        Win,
        Lose
    }

    [SerializeField] private bool gameBegin = true;
    [SerializeField] private List<string> lockedLocations;
    [SerializeField] private List<string> healedLocations;
    [SerializeField] private List<string> deadLocations;
    private string tempSavedLocation;
    private PatientOutcome tempOutcome = PatientOutcome.Undefined;

    public Vector2 currentCoords;
    public float currentTimer = 0;
    public int currentDay = 1;
    public int maxDay = 30;

    public void SetCurrentLocation(Vector2 aCoords)
    {
        currentCoords = aCoords;
    }

    public Vector2 GetCurrentLocation()
    {
        return currentCoords;
    }

    public void SetCurrentTimer(float aTimer)
    {
        currentTimer = aTimer;
    }

    public float GetCurrentTimer()
    {
        return currentTimer;
    }

    public void SetCurrentDay(int aDay)
    {
        currentDay = aDay;
    }

    public int GetCurrentDay()
    {
        return currentDay;
    }

    public void SetMaxDay(int aDay)
    {
        maxDay = aDay;
    }

    public int GetMaxDay()
    {
        return maxDay;
    }

    public bool isGameBegin()
    {
        return gameBegin;
    }

    public void SetGameBeginFlag(bool aFlag)
    {
        gameBegin = aFlag;
    }

    public void SetWinFlag(bool aFlag)
    {
        if(aFlag == true)
        {
            tempOutcome = PatientOutcome.Win;
        } else
        {
            tempOutcome = PatientOutcome.Lose;
        }
    }

    public void ResetLockedLocations()
    {
        lockedLocations = new List<string>();
        healedLocations = new List<string>();
        deadLocations = new List<string>();
    }

    public void AddLockedLocation(GameObject aLocation)
    {
        if (aLocation.GetComponent<LevelSelection>() != null)
        {
            lockedLocations.Add(aLocation.name);
            tempSavedLocation = aLocation.name;
            Debug.Log("Added Locked Location.");
        } 
        else
        {
            Debug.Log("Invalid Location Object.");
        }
    }

    public void UpdateLockedLocations()
    {
        GameObject tempLocation;

        switch (tempOutcome)
        {
            case PatientOutcome.Win:
                healedLocations.Add(tempSavedLocation);
                break;
            case PatientOutcome.Lose:
                deadLocations.Add(tempSavedLocation);
                break;
            default:
                break;
        }

        foreach (string i in lockedLocations)
        {
            tempLocation = GameObject.Find(i);
            if(tempLocation != null)
            {
                if (tempLocation.GetComponent<LevelSelection>() != null)
                {
                    tempLocation.GetComponent<LevelSelection>().SetLocked(false);
                } else
                {
                    Debug.Log("Level Selection Script Not Found.");
                }
            } else
            {
                Debug.Log("Level Not Found.");
            }
        }
        foreach(string i in healedLocations)
        {
            tempLocation = GameObject.Find(i);
            if(tempLocation != null)
            {
                if (tempLocation.GetComponent<LevelSelection>() != null)
                {
                    tempLocation.GetComponent<LevelSelection>().SetWin(1);
                }
            }
        }
        foreach (string i in deadLocations)
        {
            tempLocation = GameObject.Find(i);
            if (tempLocation != null)
            {
                if (tempLocation.GetComponent<LevelSelection>() != null)
                {
                    tempLocation.GetComponent<LevelSelection>().SetWin(2);
                }
            }
        }
        tempOutcome = PatientOutcome.Undefined;
    }
}