using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public GameObject Controller;
    public GameObject mouseoverGlow;

    private GameObject worldCamera;
    private BookScript Book;
    [SerializeField] private InventoryItem currentItem;

    private Vector3 origin;
    private RaycastHit hit;
    private Ray ray;
    private int quantity;
    private Text quantityText;
    private Text nameText;
    private Text descriptionText;
    //Images in description 
    private Image[] redImages = new Image[10];
    private Image[] greenImages = new Image[10];
    private Image[] blueImages = new Image[10];
    private Image[] whiteImages = new Image[10];
    private Image[] blackImages = new Image[10];
    private Image[] emptyImages = new Image[50];
    private Text buyButton;
    private SoundManager sndManager;
    [SerializeField] private Inventory inventoryDB;

    [SerializeField] Texture2D hoverCursor;
    [SerializeField] Texture2D downCursor;

    // Start is called before the first frame update

    void Awake()
    {
        quantityText = transform.Find("Quantity").GetComponent<Text>();
        nameText = transform.Find("Name").GetComponent<Text>();
        /*descriptionText = transform.Find("Description").GetComponent<Text>();*/
        descriptionText = transform.Find("Description").GetComponent<Text>();
        buyButton = GameObject.Find("BuyButton").GetComponent<Text>();
        inventoryDB = Resources.Load("Databases/InventoryDatabase") as Inventory;
        Book = GameObject.Find("Book_UI").GetComponent<BookScript>();
        worldCamera = GameObject.Find("Main Camera");
        PopulateRedImages();
        PopulateGreenImages();
        PopulateBlueImages();
        PopulateWhiteImages();
        PopulateBlackImages();
        PopulateEmptyImages();
    }

    void Start()
    {
        origin = new Vector2(Book.gameObject.GetComponent<RectTransform>().position.x + 180, Book.gameObject.GetComponent<RectTransform>().position.y + 180);
        transform.position = origin;
        if (Controller == null)
        {
            Controller = GameObject.Find("Controller");
        }
        if (Controller != null)
        {
            if (Controller.GetComponent<SoundManager>() != null)
            {
                sndManager = Controller.GetComponent<SoundManager>();
            }
        }
        gameObject.GetComponent<Image>().sprite = currentItem.itemImage;
    }

    // Update is called once per frame
    void Update()
    {
        origin = new Vector2(Book.gameObject.GetComponent<RectTransform>().position.x + 180, Book.gameObject.GetComponent<RectTransform>().position.y + 180);
        AdjustQuantityText();
    }

    public void SetItem(InventoryItem aObject)
    {
        int redImagesToDisable;
        int greenImagesToDisable;
        int blueImagesToDisable;
        int whiteImagesToDisable;
        int blackImagesToDisable;
        inventoryDB = Resources.Load("Databases/InventoryDatabase") as Inventory;
        foreach (List<InventoryItem> i in inventoryDB.GetInventoryList())
        {
            foreach (InventoryItem j in i)
            {
                if (aObject.prefabPath == j.prefabPath)
                {
                    currentItem = j;
                    nameText.text = j.itemName;
                    /*descriptionText.text = j.itemDescription;*/
                    if(j.itemPrice >  0)
                    {
                        buyButton.text = "Buy $" + j.itemPrice.ToString();
                    }
                    else
                    {
                        buyButton.text = "N/A";
                    }

                    redImagesToDisable = 10 - j.Red;
                    greenImagesToDisable = 10 - j.Green;
                    blueImagesToDisable = 10 - j.Blue;
                    whiteImagesToDisable = 10 - j.White;
                    blackImagesToDisable = 10 - j.Black;
                    Debug.Log(redImagesToDisable);
                    Debug.Log(blueImagesToDisable);
                    Debug.Log(greenImagesToDisable);
                    Debug.Log(blackImagesToDisable);

                    for (int n = 0; n < redImagesToDisable; n++)
                        redImages[n].enabled = false;

                    for (int m = 0; m < greenImagesToDisable; m++)
                        greenImages[m].enabled = false;

                    for (int p = 0; p < blueImagesToDisable; p++)
                        blueImages[p].enabled = false;

                    for (int v = 0; v < whiteImagesToDisable; v++)
                        whiteImages[v].enabled = false;

                    for (int q = 0; q < blackImagesToDisable; q++)
                        blackImages[q].enabled = false;

                    break;
                }
            }
        }
    }

    public InventoryItem GetItem()
    {
        return currentItem;
    }

    public void OnMouseOver()
    {
        gameObject.GetComponent<Image>().sprite = currentItem.itemImageHighlight;
        Cursor.SetCursor(hoverCursor, Vector2.zero, CursorMode.Auto);
    }

    public void OnMouseExit()
    {
        gameObject.GetComponent<Image>().sprite = currentItem.itemImage;
        Cursor.SetCursor(default, Vector2.zero, CursorMode.Auto);
    }

    public void OnMouseDown()
    {
        Cursor.SetCursor(downCursor, Vector2.zero, CursorMode.Auto);
        ItemSlot objectClone = Instantiate(this) as ItemSlot;
        GameObject foundCanvas = GameObject.Find("Book_UI");
        objectClone.transform.SetParent(foundCanvas.transform);
        objectClone.transform.localScale = Vector3.one;
        objectClone.transform.localPosition = Vector3.zero;
        objectClone.name = "itemCopy";
    }

    public void OnMouseUp()
    {
        GameObject objectClone = GameObject.Find("itemCopy");
        Destroy(objectClone);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Cursor.SetCursor(downCursor, Vector2.zero, CursorMode.Auto);
        if (Book.IsTransitioning() == false)
        {
            transform.position = Input.mousePosition;
            foreach (Image redImage in redImages)
            {
                redImage.enabled = false;
            }
            foreach (Image greenImage in greenImages)
            {
                greenImage.enabled = false;
            }
            foreach (Image blueImage in blueImages)
            {
                blueImage.enabled = false;
            }
            foreach (Image whiteImage in whiteImages)
            {
                whiteImage.enabled = false;
            }
            foreach (Image blackImage in blackImages)
            {
                blackImage.enabled = false;
            }
            foreach (Image emptyImage in emptyImages)
            {
                emptyImage.enabled = false;
            }
            nameText.enabled = false;
            descriptionText.enabled = false;

            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Cursor.SetCursor(default, Vector2.zero, CursorMode.Auto);
        if (Book.IsTransitioning() == false)
        {
            transform.position = origin;
           
            for (int n = 0; n < currentItem.Red; n++)
                redImages[9 - n].enabled = true;
            for (int n = 0; n < currentItem.Green; n++)
                greenImages[9 - n].enabled = true;
            for (int n = 0; n < currentItem.Blue; n++)
                blueImages[9 - n].enabled = true;
            for (int n = 0; n < currentItem.White; n++)
                whiteImages[9 - n].enabled = true;
            for (int n = 0; n < currentItem.Black; n++)
                blackImages[9 - n].enabled = true;
            foreach (Image emptyImage in emptyImages)
            {
                emptyImage.enabled = true;
            }
            nameText.enabled = true;
            descriptionText.enabled = true;

            if (Physics.Raycast(ray, out hit, 100.0f) && quantity > 0)
            {
                GameObject obj;
                if(currentItem.itemName != "Beaker")
                {
                    obj = Instantiate(Resources.Load(currentItem.prefabPath) as GameObject, new Vector3(hit.point.x, 2.25f, hit.point.z), Quaternion.Euler(new Vector3(80, 0, 0)));
                }
                else
                {
                    obj = Instantiate(Resources.Load(currentItem.prefabPath) as GameObject, new Vector3(hit.point.x, 1.25f, hit.point.z), Quaternion.Euler(new Vector3(0, 0, 0)));
                }
                if (obj.GetComponent<GenericObject>() != null)
                {
                    GenericObject createdItem = obj.GetComponent<GenericObject>();
                    createdItem.SetPrefabPath(currentItem);
                    CreationSound(createdItem);
                }
                Book.DecreaseQuantity(currentItem.prefabPath);
            }
        }
    }


    public void AdjustQuantityText()
    {
        if (currentItem != null)
        {
            quantity = currentItem.itemQuantity;
        }

        if (quantity <= 0)
        {
            quantity = 0;
            quantityText.color = Color.red;
        } else
        {
            quantityText.color = Color.black;
        }
        quantityText.text = quantity.ToString();
    }

    public void IncreaseQuantityText()
    {
        currentItem.itemQuantity++;
    }

    public void DecreaseQuantityText()
    {
        currentItem.itemQuantity--;
    }

    void CreationSound(GenericObject aObject)
    {
        if (sndManager != null)
        {
            sndManager.PlaySound(aObject.releaseSound);
        }
        else
        {
            Debug.Log("Sound Manager Does Not Exist!");
        }
    }

    //populate the images array with image elements from the scene. 
    //we need the array to disable the images and the images need to be disabled 
    //from the last, hence, the opposite population. 
    void PopulateRedImages()
    {
        redImages[0] = transform.Find("redDot9").GetComponent<Image>();
        redImages[1] = transform.Find("redDot8").GetComponent<Image>();
        redImages[2] = transform.Find("redDot7").GetComponent<Image>();
        redImages[3] = transform.Find("redDot6").GetComponent<Image>();
        redImages[4] = transform.Find("redDot5").GetComponent<Image>();
        redImages[5] = transform.Find("redDot4").GetComponent<Image>();
        redImages[6] = transform.Find("redDot3").GetComponent<Image>();
        redImages[7] = transform.Find("redDot2").GetComponent<Image>();
        redImages[8] = transform.Find("redDot1").GetComponent<Image>();
        redImages[9] = transform.Find("redDot0").GetComponent<Image>();
    }
    void PopulateGreenImages()
    {
        greenImages[0] = transform.Find("greenDot9").GetComponent<Image>();
        greenImages[1] = transform.Find("greenDot8").GetComponent<Image>();
        greenImages[2] = transform.Find("greenDot7").GetComponent<Image>();
        greenImages[3] = transform.Find("greenDot6").GetComponent<Image>();
        greenImages[4] = transform.Find("greenDot5").GetComponent<Image>();
        greenImages[5] = transform.Find("greenDot4").GetComponent<Image>();
        greenImages[6] = transform.Find("greenDot3").GetComponent<Image>();
        greenImages[7] = transform.Find("greenDot2").GetComponent<Image>();
        greenImages[8] = transform.Find("greenDot1").GetComponent<Image>();
        greenImages[9] = transform.Find("greenDot0").GetComponent<Image>();
    }
    void PopulateBlueImages()
    {
        blueImages[0] = transform.Find("blueDot9").GetComponent<Image>();
        blueImages[1] = transform.Find("blueDot8").GetComponent<Image>();
        blueImages[2] = transform.Find("blueDot7").GetComponent<Image>();
        blueImages[3] = transform.Find("blueDot6").GetComponent<Image>();
        blueImages[4] = transform.Find("blueDot5").GetComponent<Image>();
        blueImages[5] = transform.Find("blueDot4").GetComponent<Image>();
        blueImages[6] = transform.Find("blueDot3").GetComponent<Image>();
        blueImages[7] = transform.Find("blueDot2").GetComponent<Image>();
        blueImages[8] = transform.Find("blueDot1").GetComponent<Image>();
        blueImages[9] = transform.Find("blueDot0").GetComponent<Image>();
    }

    void PopulateWhiteImages()
    {
        whiteImages[0] = transform.Find("whiteDot9").GetComponent<Image>();
        whiteImages[1] = transform.Find("whiteDot8").GetComponent<Image>();
        whiteImages[2] = transform.Find("whiteDot7").GetComponent<Image>();
        whiteImages[3] = transform.Find("whiteDot6").GetComponent<Image>();
        whiteImages[4] = transform.Find("whiteDot5").GetComponent<Image>();
        whiteImages[5] = transform.Find("whiteDot4").GetComponent<Image>();
        whiteImages[6] = transform.Find("whiteDot3").GetComponent<Image>();
        whiteImages[7] = transform.Find("whiteDot2").GetComponent<Image>();
        whiteImages[8] = transform.Find("whiteDot1").GetComponent<Image>();
        whiteImages[9] = transform.Find("whiteDot0").GetComponent<Image>();
    }

    void PopulateBlackImages()
    {
        blackImages[0] = transform.Find("blackDot9").GetComponent<Image>();
        blackImages[1] = transform.Find("blackDot8").GetComponent<Image>();
        blackImages[2] = transform.Find("blackDot7").GetComponent<Image>();
        blackImages[3] = transform.Find("blackDot6").GetComponent<Image>();
        blackImages[4] = transform.Find("blackDot5").GetComponent<Image>();
        blackImages[5] = transform.Find("blackDot4").GetComponent<Image>();
        blackImages[6] = transform.Find("blackDot3").GetComponent<Image>();
        blackImages[7] = transform.Find("blackDot2").GetComponent<Image>();
        blackImages[8] = transform.Find("blackDot1").GetComponent<Image>();
        blackImages[9] = transform.Find("blackDot0").GetComponent<Image>();
    }

    void PopulateEmptyImages()
    {
        for(int x = 0; x < emptyImages.Length; x++)
        {
            emptyImages[x] = transform.Find("emptyDot" + x).GetComponent<Image>();
        }
        //for(int x = 0; x > emptyImages.Length; x++)
        //emptyImages[x] = transform.Find("emptyDot" + x).GetComponent<Image>();
    }
}
