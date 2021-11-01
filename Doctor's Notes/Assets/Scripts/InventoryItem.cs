using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryItem
{
    public GameObject itemPrefab;
    public Sprite itemImage;
    public Sprite itemImageHighlight;
    public string itemName;
    public string prefabPath;
    public int itemQuantity;
    public string itemDescription;
    public int itemPrice;
    //Individual color values
    public float Red;
    public float Blue;
    public float Green;
    public float Black;

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
                itemImageHighlight = NewGenericObject.itemIconHighlight;
                itemName = newObject.name;
                prefabPath = aPrefabPath;
                itemQuantity++;
            }
        }
    }
}
