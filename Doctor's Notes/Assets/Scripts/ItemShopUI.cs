using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemShopUI : MonoBehaviour
{
    [Header("Layout Settings")]
    [SerializeField] float itemSpacing = .05f;
    float itemHeight;

    [Header("UI elements")]
    //[SerializeField] GameObject ShopPanel;
    [SerializeField] Transform ShopMenu;
    [SerializeField] Transform ShopItemContainer;
    [SerializeField] GameObject itemPrefab;
    //[Space(20)];
    [SerializeField] ItemShopDatabase itemDB;

    [Header("Shop Events")]
    [SerializeField] GameObject shopUI;

    void Start()
    {
        GenerateShopItemsUI();
    }

    void GenerateShopItemsUI()
    {
        //Delete Current Shop Template
        itemHeight = ShopItemContainer.GetChild(0).GetComponent<RectTransform>().sizeDelta.y;
        Destroy(ShopItemContainer.GetChild(0).gameObject);
        ShopItemContainer.DetachChildren();

        //Generate Items from Database
        for (int i = 0; i < itemDB.ItemCount; i++)
        {
            Item item = itemDB.GetItem(i);
            ItemUI uiItem = Instantiate(itemPrefab, ShopItemContainer).GetComponent<ItemUI>();

            //Moving the UI item to line up with the others.
            uiItem.SetItemPosition(Vector2.down * i * (itemHeight + itemSpacing));

            //Set the item gameObject name to that of the item
            uiItem.gameObject.name = "Item" + i + item.itemName;

            //Add data to the UI one item at a time
            uiItem.SetItemImage(item.itemImage);
            uiItem.SetItemName(item.itemName);
            uiItem.SetItemCost(item.itemCost);
            uiItem.SetItemStock(item.itemStock);
            uiItem.OnItemPurchase(i, PurchaseItem);

            //Resize the item container
            ShopItemContainer.GetComponent<RectTransform>().sizeDelta = Vector2.up * ((itemHeight + itemSpacing) * itemDB.ItemCount + itemSpacing);
        }
    }

    public ItemUI GetItemUI(int index)
    {
        return ShopItemContainer.GetChild(index).GetComponent<ItemUI>();
    }

    public void PurchaseItem(int index)
    {
        Item item = itemDB.GetItem(index);
        ItemUI uiItem = GetItemUI(index);
        //Debug.Log(GameDataManager.GetMoney());
        if (item.itemStock > 0)
        {
            if (GameDataManager.CanPurchase(item.itemCost))
            {
                GameDataManager.RemoveMoney(item.itemCost);
                GameSharedUI.Instance.UpdateMoneyUIText();
                //Debug.Log("Purchased:" + item.itemName);
                itemDB.RemoveItemStock(index);

                //Re-generate the shop to account for removing one
                GenerateShopItemsUI();
            }
            else
            {
                //uiItem.OnItemPurchase(index, OnItemPurchased);
                Debug.Log("You poor, not enough cash");
            }
        }
        else
        {
            uiItem.SetItemOutofStock();
        }
    }
}
