using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BookScript : MonoBehaviour
{
    private const int MAX_ITEM_PER_PAGE = 6;

    public enum BookCategory
    {
        Tool,
        Material,
        Other
    }

    public List<List<InventoryItem>> Book;
    public List<InventoryItem> bookItems;
    public GameObject iconTemplate;

    private List<InventoryItem[]> dynamicBookList;
    private int currentPage;
    private BookCategory currentCategory;
    [SerializeField] private Inventory inventoryDB;

    // Start is called before the first frame update
    void Start()
    {
        inventoryDB = Resources.Load("Databases/InventoryDatabase") as Inventory;

        currentCategory = (BookCategory)0;
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
        UnloadDatabase();
        CreateDynamicPage();
    }

    void CreateDynamicPage()
    {
        dynamicBookList = new List<InventoryItem[]>();
        int bookListIndex = 0;
        int index = 0;

        foreach (InventoryItem i in Book[Convert.ToInt32(currentCategory)])
        {
            if (index == 0)
            {
                dynamicBookList.Add(new InventoryItem[MAX_ITEM_PER_PAGE]);
            }

            dynamicBookList[bookListIndex][index] = i;
            index++;

            if (index >= MAX_ITEM_PER_PAGE)
            {
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

    public void SelectCategory(int aCategory)
    {
        currentPage = 0;
        currentCategory = (BookCategory)aCategory;

        CreateDynamicPage();
        ViewPage(currentPage);
    }

    void ViewPage(int aPageNumber)
    {
        ClearPage();

        int index = 0;
        float YOffset = 0;
        GameObject currentItem;
        //GameObject prefabItem;

        if (aPageNumber < dynamicBookList.Count && aPageNumber > -1)
        {
            foreach (InventoryItem i in dynamicBookList[aPageNumber])
            {
                if(i != null)
                {
                    currentItem = Instantiate(iconTemplate, transform.position, Quaternion.identity);
                    currentItem.transform.SetParent(transform);
                    currentItem.GetComponent<Image>().sprite = i.itemImage;
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
        GenericObject argItem = aItem.GetComponent<GenericObject>();
        if(argItem != null)
        {
            Item newItem = new Item(argItem.itemIcon, aItem.name, argItem.prefabPath);
            inventoryDB.AddItem(newItem);
        }

        CreateListOfItems();
        ViewPage(currentPage);
    }

    /*public void IncreaseQuantity(int aItemID)
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
    }*/

    public void IncreaseQuantity(string aPrefabPath)
    {
        bool addNewItem = true;
        foreach(List<InventoryItem> i in inventoryDB.GetInventoryList())
        {
            foreach(InventoryItem j in i)
            {
                if(j.prefabPath == aPrefabPath)
                {
                    j.itemQuantity++;
                    addNewItem = false;
                    break;
                }
            }
        }

        if (addNewItem)
        {
            AddItem(Resources.Load(aPrefabPath) as GameObject, 1);
        }

        CreateListOfItems();
    }

    /*public void DecreaseQuantity(int aItemID)
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
    }*/

    public void DecreaseQuantity(string aPrefabPath)
    {
        foreach (List<InventoryItem> i in inventoryDB.GetInventoryList())
        {
            foreach (InventoryItem j in i)
            {
                if (j.prefabPath == aPrefabPath)
                {
                    j.itemQuantity--;
                }
            }
        }

        CreateListOfItems();
    }

    void UnloadDatabase()
    {
        Book = new List<List<InventoryItem>>();

        foreach (List<InventoryItem> i in inventoryDB.GetInventoryList())
        {
            bookItems = new List<InventoryItem>();
            foreach (InventoryItem j in i)
            {
                InventoryItem newItem = new InventoryItem(j.prefabPath);
                newItem.itemQuantity = j.itemQuantity;

                bookItems.Add(newItem);
            }
            Book.Add(bookItems);
        }
    }
}
