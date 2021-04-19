using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ItemShopDatabase", menuName = "Shopping/Item Database")]
public class ItemShopDatabase : ScriptableObject
{
    public Item[] items;

    public int ItemCount {
        get { return items.Length; }
    }

    public Item GetItem(int index)
    {
        return items[index];
    }

    public void RemoveItemStock(int index)
    {
        items[index].itemStock = items[index].itemStock - 1;
    }
}
