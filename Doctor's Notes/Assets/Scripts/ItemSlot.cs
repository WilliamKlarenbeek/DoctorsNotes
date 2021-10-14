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
    private SoundManager sndManager;
    [SerializeField] private Inventory inventoryDB;

    // Start is called before the first frame update

    void Awake()
    {
        quantityText = transform.Find("Quantity").GetComponent<Text>();
        nameText = transform.Find("Name").GetComponent<Text>();
        descriptionText = transform.Find("Description").GetComponent<Text>();
        inventoryDB = Resources.Load("Databases/InventoryDatabase") as Inventory;
        Book = GameObject.Find("Book_UI").GetComponent<BookScript>();
        worldCamera = GameObject.Find("Main Camera");
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
        inventoryDB = Resources.Load("Databases/InventoryDatabase") as Inventory;
        foreach (List<InventoryItem> i in inventoryDB.GetInventoryList())
        {
            foreach (InventoryItem j in i)
            {
                if (aObject.prefabPath == j.prefabPath)
                {
                    currentItem = j;
                    nameText.text = j.itemName;
                    descriptionText.text = j.itemDescription;
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
}
