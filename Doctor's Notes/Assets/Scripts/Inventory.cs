using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InventoryDatabase", menuName = "Shopping/Inventory Database")]
public class Inventory : ScriptableObject
{
    public List<GameObject> toolList;
    public List<GameObject> materialList;
    public List<GameObject> otherList;

    public void AddItem(GameObject aItem)
    {
        if (aItem.GetComponent<Tool>() != null) {
            toolList.Add(aItem);
        } 
        else if (aItem.GetComponent<Material>() != null)
        {
            materialList.Add(aItem);
        } 
        else
        {
           otherList.Add(aItem);
        }
    }
}

