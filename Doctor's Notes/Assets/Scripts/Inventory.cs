using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InventoryDatabase", menuName = "Shopping/Inventory Database")]
public class Inventory : ScriptableObject
{
    private List<List<InventoryItem>> inventoryList = new List<List<InventoryItem>>();

    public List<InventoryItem> toolList = new List<InventoryItem>();
    public List<InventoryItem> materialList = new List<InventoryItem>();
    public List<InventoryItem> otherList = new List<InventoryItem>();

    Inventory()
    {
        inventoryList.Add(toolList);
        inventoryList.Add(materialList);
        inventoryList.Add(otherList);
    }

    public void AddItem(Item aItem)
    {
        bool addItem = true;
        InventoryItem newItem = new InventoryItem(aItem);

        foreach (List<InventoryItem> i in inventoryList)
        {
            foreach (InventoryItem j in i)
            {
                if (j.itemName == newItem.itemName)
                {
                    j.itemQuantity++;
                    addItem = false;
                    break;
                }
            }
        }

        if (addItem)
        {
            GameObject itemPrefab = Resources.Load(newItem.prefabPath) as GameObject;
            if (itemPrefab.GetComponent<Tool>() != null)
            {
                toolList.Add(newItem);
            }
            else if (itemPrefab.GetComponent<Material>() != null)
            {
                materialList.Add(newItem);
            }
            else
            {
                otherList.Add(newItem);
            }
        }
    }

    public void InitialisePlayerInventory()
    {
        foreach (List<InventoryItem> i in inventoryList)
        {
            foreach (InventoryItem j in i)
            {
                j.itemQuantity = j.defaultQuantity;
            }
        }
        PlayerPrefs.SetInt("money", 500);
    }

    public List<List<InventoryItem>> GetInventoryList()
    {
        return inventoryList;
    }
}