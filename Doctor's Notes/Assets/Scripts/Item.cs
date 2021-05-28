using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public Sprite itemImage;
    public string itemName;
    public string prefabPath;
    public int itemCost;
    public int itemStock;
    public bool itemInStock;

    public Item(Sprite aItemImage, string aItemName, string aPrefabPath, int aItemCost = 0, int aItemStock = 0, bool aItemInStock = false)
    {
        itemImage = aItemImage;
        itemName = aItemName;
        prefabPath = aPrefabPath;
        itemCost = aItemCost;
        itemStock = aItemStock;
        itemInStock = aItemInStock;
    }
}
