using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryItem
{
    public Sprite itemImage;
    public string itemName;
    public string prefabPath;
    public int itemQuantity;

    public InventoryItem(Item aItem)
    {
        itemImage = aItem.itemImage;
        itemName = aItem.itemName;
        prefabPath = aItem.prefabPath;
        itemQuantity++;
    }

    public InventoryItem(string aPrefabPath)
    {
        GameObject newObject = Resources.Load(aPrefabPath) as GameObject;
        if(newObject != null)
        {
            GenericObject NewGenericObject = newObject.GetComponent<GenericObject>();

            if(NewGenericObject != null)
            {
                itemImage = NewGenericObject.itemIcon;
                itemName = newObject.name;
                prefabPath = aPrefabPath;
                itemQuantity++;
            }
        }
    }
}