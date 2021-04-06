using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BookScript : MonoBehaviour
{
    public List<GameObject> bookItems;
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
        GameObject currentItem;
        
        foreach(GameObject item in bookItems)
        {
            currentItem = Instantiate(iconTemplate, transform.position, Quaternion.identity);
            currentItem.transform.SetParent(transform);
            currentItem.GetComponent<Image>().sprite = item.GetComponent<GenericObject>().GetItemIcon();
            currentItem.GetComponent<ItemSlot>().SetItem(item);

            switch (index)
            {
                case 0:
                    currentItem.GetComponent<RectTransform>().anchoredPosition = new Vector2(150, 240);
                    index = 1;
                    break;
                case 1:
                    currentItem.GetComponent<RectTransform>().anchoredPosition = new Vector2(450, 240);
                    index = 0;
                    break;
                default:
                    break;
            }
        }
    }
}
