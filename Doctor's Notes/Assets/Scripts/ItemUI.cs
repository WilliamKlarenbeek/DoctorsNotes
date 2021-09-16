using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class ItemUI : MonoBehaviour
{
    [SerializeField] Image itemImage;
    [SerializeField] Text itemName;
    [SerializeField] Text itemCost;
    [SerializeField] Text itemStock;
    [SerializeField] Button itemPurchaseButton;
    
    //[Space(20f)];

    public void SetItemPosition(Vector2 pos)
    {
        GetComponent<RectTransform>().anchoredPosition += pos;
    }

    public void SetItemImage(Sprite sprite)
    {
        itemImage.sprite = sprite;
    }

    public void SetItemName(string name)
    {
        itemName.text = name;
    }

    public void SetItemCost(int cost)
    {
        itemCost.text = cost.ToString();
    }

    public void SetItemStock(int stock)
    {
        itemStock.text = stock.ToString();
    }

    public void SetItemOutofStock()
    {
        itemPurchaseButton.gameObject.SetActive(false);
        //itemPurchaseButton.interactable(false);
    }

    public void OnItemPurchase(int itemIndex, UnityAction<int> action)
    {
        itemPurchaseButton.onClick.RemoveAllListeners();
        itemPurchaseButton.onClick.AddListener(delegate { action.Invoke(itemIndex); });
        //itemPurchaseButton.onClick.AddListener(delegate { action.Invoke(itemIndex); });
        //itemPurchaseButton.onClick.AddListener(() => action.Invoke(itemIndex));
    }
}
