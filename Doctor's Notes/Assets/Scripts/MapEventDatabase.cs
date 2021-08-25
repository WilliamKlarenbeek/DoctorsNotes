using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EventDatabase", menuName = "Map/Event Database")]
public class MapEventDatabase : ScriptableObject
{
    public List<MapEvent> eventList = new List<MapEvent>();

    public void ResetChances()
    {
        foreach(MapEvent i in eventList)
        {
            i.setChance(1);
        }
    }
}
