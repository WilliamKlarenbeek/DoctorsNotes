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
    // Start is called before the first frame update
    void Start()
    {
        worldCamera = GameObject.Find("Main Camera");
        origin = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
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

        if (Physics.Raycast(ray, out hit, 100.0f))
        {
            Debug.Log(hit);
            Instantiate(currentItem, hit.point, Quaternion.identity);
        }
    }
}
