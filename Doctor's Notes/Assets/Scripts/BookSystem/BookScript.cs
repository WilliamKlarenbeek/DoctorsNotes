using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class BookScript : MonoBehaviour
{
    private const int MAX_ITEM_PER_PAGE = 1;

    public enum BookCategory
    {
        Red,
        Green,
        Blue,
        Other
    }

    public List<List<InventoryItem>> Book;
    public List<InventoryItem> bookItems;
    public GameObject iconTemplate;

    private List<InventoryItem[]> dynamicBookList;
    private int currentPage;
    private BookCategory currentCategory;
    private bool opened = false;
    private bool transitioning = false;
    private Button[] buttons;
    [SerializeField] private Inventory inventoryDB;

    // Start is called before the first frame update
    void Start()
    {
        inventoryDB = Resources.Load("Databases/InventoryDatabase") as Inventory;
        buttons = this.GetComponentsInChildren<Button>(true);

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
                    //currentItem.GetComponent<Image>().sprite = i.itemImage;
                    currentItem.GetComponent<ItemSlot>().SetItem(i);
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

    /*void UnloadDatabaseOld()
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
    }*/

    void UnloadDatabase()
    {
        Book = new List<List<InventoryItem>>();

        for(int i = 0; i < 4; i++)
        {
            List<InventoryItem> blankList = new List<InventoryItem>();
            Book.Add(blankList);
        }

        List<List<InventoryItem>> masterList = inventoryDB.GetInventoryList();
        List<InventoryItem> materialList = masterList[1];
        GameObject currentObject = null;
        Ingredient currentIngredient = null;
        float currentValue = 0;
        int bestCategory = 0;

        foreach(InventoryItem i in materialList)
        {
            //Create New Items for Book
            InventoryItem newItem = new InventoryItem(i.prefabPath);
            newItem.itemQuantity = i.itemQuantity;

            //To Test if the current object has Ingredient.
            currentObject = Resources.Load(newItem.prefabPath) as GameObject;

            if (currentObject.GetComponent<Ingredient>() != null)
            {
                currentIngredient = currentObject.GetComponent<Ingredient>();
                bestCategory = 0;
                currentValue = currentIngredient.red;

                if(currentIngredient.green > currentValue)
                {
                    bestCategory = 1;
                    currentValue = currentIngredient.green;
                }
                if(currentIngredient.blue > currentValue)
                {
                    bestCategory = 2;
                    currentValue = currentIngredient.blue;
                }
                if(Mathf.Abs(currentIngredient.black) > currentValue)
                {
                    bestCategory = 3;
                    currentValue = currentIngredient.black;
                }

                Book[bestCategory].Add(newItem);
            }
        }

        SortInventoryItems();
    }

    void SortInventoryItems()
    {
        List<InventoryItem> SortedList = null;
        List<List<InventoryItem>> oldBook = new List<List<InventoryItem>>();
        oldBook = Book;
        int index = 0;

        /*foreach (List<InventoryItem> i in oldBook)
        {
            SortedList = new List<InventoryItem>();
            SortedList = i.OrderBy(o=>o.itemName).ToList();
            Book[index] = SortedList;

            index++;
        }*/

        while(index < oldBook.Count)
        {
            SortedList = new List<InventoryItem>();
            SortedList = oldBook[index].OrderBy(o => o.itemName).ToList();
            Book[index] = SortedList;

            index++;
        }
    }

    public void ToggleActive()
    {
        if (opened)
        {
            opened = false;
        } 
        else
        {
            opened = true;
        }

        if (opened && transitioning == false) {
            StartCoroutine(OpenBook(1f));
        }
        if(opened == false && transitioning == false)
        {
            StartCoroutine(CloseBook(1f));
        }
    }

    IEnumerator OpenBook(float aDuration)
    {
        transitioning = true;
        foreach(Button i in buttons)
        {
            i.interactable = false;
        }
        float frame = 0f;
        float posYOrigin = -1080f;
        float posYCurrent = posYOrigin;
        float posYDest = -75f;

        while(frame < aDuration)
        {
            posYCurrent = Mathf.Lerp(posYOrigin, posYDest, frame / aDuration);
            GetComponent<RectTransform>().anchoredPosition = new Vector2(-750, posYCurrent);
            frame+= Time.deltaTime;
            yield return null;
        }
        GetComponent<RectTransform>().anchoredPosition = new Vector2(-750, posYDest);
        foreach (Button i in buttons)
        {
            i.interactable = true;
        }
        transitioning = false;
    }

    IEnumerator CloseBook(float aDuration)
    {
        transitioning = true;
        foreach (Button i in buttons)
        {
            i.interactable = false;
        }
        float frame = 0f;
        float posYOrigin = -75f;
        float posYCurrent = posYOrigin;
        float posYDest = -1080f;

        while (frame < aDuration)
        {
            posYCurrent = Mathf.Lerp(posYOrigin, posYDest, frame / aDuration);
            GetComponent<RectTransform>().anchoredPosition = new Vector2(-750, posYCurrent);

            frame += Time.deltaTime;
            yield return null;
        }
        GetComponent<RectTransform>().anchoredPosition = new Vector2(-750, posYDest);
        foreach (Button i in buttons)
        {
            i.interactable = true;
        }
        transitioning = false;
    }

    public bool IsTransitioning()
    {
        return transitioning;
    }
}
