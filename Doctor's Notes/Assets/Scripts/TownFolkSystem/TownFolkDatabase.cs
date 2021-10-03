using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TownFolkDatabase", menuName = "Town/TownFolk Database")]
public class TownFolkDatabase : ScriptableObject
{
    public TownFolk[] townFolk;

    public int TownFolkCount
    {
        get {return townFolk.Length;}
    }

    public TownFolk GetTownFolk(int index)
    {
        return townFolk[index];
    }
}
