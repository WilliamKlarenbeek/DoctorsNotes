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

    private Image[] redImages = new Image[10];
    private Image[] greenImages = new Image[10];
    private Image[] blueImages = new Image[10];
    private Image[] blackImages = new Image[10];

    private SoundManager sndManager;
    [SerializeField] private Inventory inventoryDB;


    void Awake()
    {
        quantityText = transform.Find("Quantity").GetComponent<Text>();
        nameText = transform.Find("Name").GetComponent<Text>();
        /*descriptionText = transform.Find("Description").GetComponent<Text>();*/

        PopulateRedImages();
        PopulateGreenImages();
        PopulateBlueImages();
        PopulateBlackImages();
        inventoryDB = Resources.Load("Databases/InventoryDatabase") as Inventory;
        Book = GameObject.Find("Book_UI").GetComponent<BookScript>();
        worldCamera = GameObject.Find("Main Camera");
    }

    void Start()
    {
        origin = new Vector2(Book.gameObject.GetComponent<RectTransform>().position.x + 300, Book.gameObject.GetComponent<RectTransform>().position.y + 240);
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
        origin = new Vector2(Book.gameObject.GetComponent<RectTransform>().position.x + 300, Book.gameObject.GetComponent<RectTransform>().position.y + 240);
        AdjustQuantityText();
    }

    public void SetItem(InventoryItem aObject)
    {
        int redImagesToDisable;
        int greenImagesToDisable;
        int blueImagesToDisable;
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
                    //descriptionText.text = j.itemDescription
                    Debug.Log(j.itemDescription);

                    redImagesToDisable = 10 - (int)(j.Red * 10);
                    greenImagesToDisable = 10 - (int)(j.Green * 10);
                    blueImagesToDisable = 10 - (int)(j.Blue * 10);
                    blackImagesToDisable = 10 - (int)(j.Black * 10);

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

                    for (int q = 0; q < blackImagesToDisable; q++)
                        blackImages[q].enabled = false;

                    break;
                }
            }
        }
    }

    public void OnMouseOver()
    {
        gameObject.GetComponent<Image>().sprite = currentItem.itemImageHighlight;
    }

    public void OnMouseExit()
    {
        gameObject.GetComponent<Image>().sprite = currentItem.itemImage;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(Book.IsTransitioning() == false)
        {
            transform.position = Input.mousePosition;

            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(Book.IsTransitioning() == false)
        {
            transform.position = origin;

            if (Physics.Raycast(ray, out hit, 100.0f) && quantity > 0)
            {
                var obj = Instantiate(Resources.Load(currentItem.prefabPath) as GameObject, hit.point + new Vector3(0,1,0), Quaternion.Euler(new Vector3(80, 0, 0)));
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
}
