using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapDatabase", menuName = "Map/Map Database")]
public class MapSelection : ScriptableObject
{
    [SerializeField] private bool gameBegin = true;
    [SerializeField] private List<string> lockedLocations;

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

    public void ResetLockedLocations()
    {
        lockedLocations = new List<string>();
    }

    public void AddLockedLocation(GameObject aLocation)
    {
        if (aLocation.GetComponent<LevelSelection>() != null)
        {
            lockedLocations.Add(aLocation.name);
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

        Debug.Log("Updating Locked Locations");

        foreach (string i in lockedLocations)
        {
            tempLocation = GameObject.Find(i);
            if(tempLocation != null)
            {
                Debug.Log("Level Found.");
                if (tempLocation.GetComponent<LevelSelection>() != null)
                {
                    tempLocation.GetComponent<LevelSelection>().SetLocked(false);
                    Debug.Log("Level Selection Script Found.");
                } else
                {
                    Debug.Log("Level Selection Script Not Found.");
                }
            } else
            {
                Debug.Log("Level Not Found.");
            }
        }
    }
}