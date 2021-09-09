using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapDatabase", menuName = "Map/Map Database")]
public class MapSelection : ScriptableObject
{
    [SerializeField] private bool gameBegin = true;

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
}