using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDragHandler, IEndDragHandler
{
    private GameObject worldCamera;
    private GameObject currentItem;

    private Vector3 origin;
    private RaycastHit hit;
    private Ray ray;
    private int quantity;
    private Text quantityText;
    // Start is called before the first frame update

    void Start()
    {
        worldCamera = GameObject.Find("Main Camera");
        origin = transform.position;

        quantityText = gameObject.GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        AdjustQuantityText();
    }

    public void SetItem(GameObject aObject)
    {
        currentItem = aObject;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
            
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.position = origin;

        if (Physics.Raycast(ray, out hit, 100.0f) && quantity > 0)
        {
            var obj = Instantiate(currentItem, hit.point, Quaternion.identity);
            obj.GetComponent<GenericObject>().SetParentSlot(gameObject);
            quantity--;
        }
    }

    public void AdjustQuantityText()
    {
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

    public void SetQuantity(int aNumber)
    {
        quantity = aNumber;
    }

    public void AddQuantity(int aNumber)
    {
        quantity += aNumber;
    }
}
