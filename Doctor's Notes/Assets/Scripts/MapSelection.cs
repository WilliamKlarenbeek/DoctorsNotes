using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapDatabase", menuName = "Map/Map Database")]
public class MapSelection : ScriptableObject
{
    //Maximum locked locations should ideally match the amount of locations in the map scene.
    const int MAXIMUM_LOCKED_LOCATIONS = 6;

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
    [SerializeField] private int currentDay = 1;
    private int maxDay;
    private int bonusDays = 0;

    public int EndStateCheck()
    {
        if(lockedLocations.Count >= MAXIMUM_LOCKED_LOCATIONS)
        {
            if(healedLocations.Count >= deadLocations.Count)
            {
                return 1;
            } else
            {
                return 2;
            }
        }
        else
        {
            return -1;
        }
    }

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

    public void SetBonusDay(int aDay)
    {
        bonusDays = aDay;
    }

    public int GetBonusDay()
    {
        return bonusDays;
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
        } 
        else
        {
            Debug.Log("Invalid Location Object.");
        }
    }

    public void AddHealedLocation(GameObject aLocation)
    {
        if (aLocation.GetComponent<LevelSelection>() != null)
        {
            healedLocations.Add(aLocation.name);
        }
        else
        {
            Debug.Log("Invalid Location Object.");
        }
    }

    public void AddDeadLocation(GameObject aLocation)
    {
        if (aLocation.GetComponent<LevelSelection>() != null)
        {
            deadLocations.Add(aLocation.name);
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
                    tempLocation.GetComponent<LevelSelection>().SetUnlocked(false);
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
                    //Doesn't hurt to double check now, does it?
                    tempLocation.GetComponent<LevelSelection>().SetUnlocked(false);
                    tempLocation.GetComponent<LevelSelection>().SetWin(1);
                }
            }

            deadLocations.Remove(i);
        }
        foreach (string i in deadLocations)
        {
            tempLocation = GameObject.Find(i);
            if (tempLocation != null)
            {
                if (tempLocation.GetComponent<LevelSelection>() != null)
                {
                    tempLocation.GetComponent<LevelSelection>().SetUnlocked(false);
                    tempLocation.GetComponent<LevelSelection>().SetWin(2);
                }
            }
        }
        tempOutcome = PatientOutcome.Undefined;
    }

    public int GetLocationSaved()
    {
        return healedLocations.Count;
    }
}