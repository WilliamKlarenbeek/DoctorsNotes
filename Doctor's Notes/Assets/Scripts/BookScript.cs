using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BookScript : MonoBehaviour
{
    private const int MAX_ITEM_PER_PAGE = 6;

    [System.Serializable]
    public struct ItemParameters
    {
        public GameObject item { set { _item = value; } get { return _item; } }
        public int number { set { _number = value; }  get { return _number; } }
        public int itemID { set { _numberID = value; } get { return _numberID; } }

        [SerializeField] private GameObject _item;
        [SerializeField] private int _number;
        private int _numberID;
    }

    public List<ItemParameters> bookItems;
    public GameObject iconTemplate;

    private List<ItemParameters[]> dynamicBookList;
    private int currentPage;

    // Start is called before the first frame update
    void Start()
    {
        CreateListOfItems();
        currentPage = 0;
        ViewPage(currentPage);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateListOfItems()
    {
        dynamicBookList = new List<ItemParameters[]>();
        int bookListIndex = 0;
        int masterIndex = 0;
        int index = 0;
        int uniqueID = 0;

        foreach (ItemParameters i in bookItems.ToArray())
        {
            var newList = bookItems[masterIndex];
            newList.itemID = uniqueID;
            bookItems[masterIndex] = newList;
            masterIndex++;
            uniqueID++;
        }

        foreach (ItemParameters i in bookItems)
        {
            if(index == 0)
            {
                dynamicBookList.Add(new ItemParameters[MAX_ITEM_PER_PAGE]);
            }

            dynamicBookList[bookListIndex][index] = i;
            index++;

            if (index >= MAX_ITEM_PER_PAGE) {
                bookListIndex++;
                index = 0;
            }
        }
    }
    
    public void ClearPage()
    {
        foreach(Transform child in transform)
        {
            if(child.name == "Item(Clone)")
            {
                Destroy(child.gameObject);
            }
        }
    }

    public void NextPage()
    {
        if((currentPage + 1) < dynamicBookList.Count)
        {
            currentPage++;
        }
        ViewPage(currentPage);
    }

    public void PrevPage()
    {
        if ((currentPage - 1) > -1)
        {
            currentPage--;
        }
        ViewPage(currentPage);
    }

    void ViewPage(int aPageNumber)
    {
        ClearPage();

        int index = 0;
        float YOffset = 0;
        GameObject currentItem;

        if (aPageNumber < dynamicBookList.Count && aPageNumber > -1)
        {
            foreach (ItemParameters i in dynamicBookList[aPageNumber])
            {
                if(i.item != null)
                {
                    currentItem = Instantiate(iconTemplate, transform.position, Quaternion.identity);
                    currentItem.transform.SetParent(transform);
                    currentItem.GetComponent<Image>().sprite = i.item.GetComponent<GenericObject>().GetItemIcon();
                    currentItem.GetComponent<ItemSlot>().SetItem(i);

                    switch (index % 2)
                    {
                        case 0:
                            currentItem.GetComponent<RectTransform>().anchoredPosition = new Vector2(150, 240 + YOffset);
                            break;
                        case 1:
                            currentItem.GetComponent<RectTransform>().anchoredPosition = new Vector2(450, 240 + YOffset);
                            YOffset -= 160;
                            break;
                        default:
                            break;
                    }
                    index++;
                }
            }
        }

        if (aPageNumber + 1 >= dynamicBookList.Count)
        {
            transform.Find("Next_Button").gameObject.SetActive(false);
        } 
        else
        {
            transform.Find("Next_Button").gameObject.SetActive(true);
        }

        if (aPageNumber - 1 <= -1)
        {
            transform.Find("Back_Button").gameObject.SetActive(false);
        } 
        else
        {
            transform.Find("Back_Button").gameObject.SetActive(true);
        }
    }

    public void AddItem(GameObject aItem, int aQuantity)
    {
        bool createNew = true;

        if(bookItems.Exists(element => element.item == aItem))
        {
            createNew = false;
            int index = 0;
            foreach(ItemParameters i in bookItems.ToArray())
            {
                if(i.item == aItem)
                {
                    var newList = bookItems[index];
                    newList.number++;
                    bookItems[index] = newList;
                    break;
                }
                index++;
            }
        }
        if (createNew)
        {
            
            ItemParameters newItem = new ItemParameters();
            int newID = 0;
            newItem.item = aItem;
            newItem.number = aQuantity;

            while (bookItems.Exists(element => element.itemID == newID))
            {
                newID++;
            }
            newItem.itemID = newID;
            bookItems.Add(newItem);
        }

        CreateListOfItems();
        ViewPage(currentPage);
    }

    public void IncreaseQuantity(int aItemID)
    {
        int index = 0;
        foreach (ItemParameters i in bookItems.ToArray())
        {
            if (i.item != null && i.itemID == aItemID)
            {
                var newList = bookItems[index];
                newList.number++;
                bookItems[index] = newList;
                foreach (Transform child in transform)
                {
                    if (child.name == "Item(Clone)")
                    {
                        if(child.GetComponent<ItemSlot>().GetCurrentItemID() == bookItems[index].itemID)
                        {
                            child.GetComponent<ItemSlot>().IncreaseQuantityText();
                        }
                    }
                }
            }
            index++;
        }

        CreateListOfItems();
    }

    public void DecreaseQuantity(int aItemID)
    {
        int index = 0;
        foreach (ItemParameters i in bookItems.ToArray())
        {
            if (i.item != null && i.itemID == aItemID)
            {
                var newList = bookItems[index];
                newList.number--;
                bookItems[index] = newList;
                foreach (Transform child in transform)
                {
                    if (child.name == "Item(Clone)")
                    {
                        if (child.GetComponent<ItemSlot>().GetCurrentItemID() == bookItems[index].itemID)
                        {
                            child.GetComponent<ItemSlot>().DecreaseQuantityText();
                        }
                    }
                }
            }
            index++;
        }

        CreateListOfItems();
    }
}
