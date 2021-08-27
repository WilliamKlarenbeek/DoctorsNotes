using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MapEvent
{
    public enum EventType
    {
        Good,
        Bad
    }
    public enum EventOutcome
    {
        Resource,
        Time
    }

    public EventType eventType;
    public EventOutcome eventOutcome;
    public string eventName;
    public string eventDesc;
    public int eventChance = 1;

    public MapEvent(EventType aType, EventOutcome aOutcome, string aName, string aDesc, int aChance)
    {
        eventType = aType;
        eventOutcome = aOutcome;
        eventName = aName;
        eventDesc = aDesc;
        eventChance = aChance;
    }

    public void setChance(int aChance)
    {
        eventChance = aChance;
    }
}
