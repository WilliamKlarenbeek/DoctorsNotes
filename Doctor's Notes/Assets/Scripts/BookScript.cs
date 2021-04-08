using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BookScript : MonoBehaviour
{
    [System.Serializable]
    public struct ItemParameters
    {
        public GameObject item { get { return _item; } }
        public int number { get { return _number; } }

        [SerializeField] private GameObject _item;
        [SerializeField] private int _number;
    }

    public ItemParameters[] bookItems;
    public GameObject iconTemplate;
    // Start is called before the first frame update
    void Start()
    {
        CreateListOfItems();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateListOfItems()
    {
        int index = 0;
        float YOffset = 0;
        GameObject currentItem;
        
        foreach(ItemParameters i in bookItems)
        {
            currentItem = Instantiate(iconTemplate, transform.position, Quaternion.identity);
            currentItem.GetComponent<ItemSlot>().SetQuantity(i.number);
            currentItem.transform.SetParent(transform);
            currentItem.GetComponent<Image>().sprite = i.item.GetComponent<GenericObject>().GetItemIcon();
            currentItem.GetComponent<ItemSlot>().SetItem(i.item);

            switch (index)
            {
                case 0:
                    currentItem.GetComponent<RectTransform>().anchoredPosition = new Vector2(150, 240 + YOffset);
                    index = 1;
                    break;
                case 1:
                    currentItem.GetComponent<RectTransform>().anchoredPosition = new Vector2(450, 240 + YOffset);
                    index = 0;
                    YOffset -= 160;
                    break;
                default:
                    break;
            }
        }
    }
}
