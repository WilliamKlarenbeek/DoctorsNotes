using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GenericObject : MonoBehaviour
{
    public Sprite itemIcon;

    private Vector3 mOffset;
    private float mZCoord;
    private RaycastHit hit;
    private Ray ray;
    private GameObject parentSlot;

    public Sprite GetItemIcon()
    {
        return itemIcon;
    }

    void OnMouseDown()
    {
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;

        //Store offset = gameobject world pos - mouse world pos
        mOffset = gameObject.transform.position - GetMouseWorldPos();
    }

    Vector3 GetMouseWorldPos()
    {
        //Pixel coordinates (x,y)
        Vector3 mousePoint = Input.mousePosition;

        //Z coordinate of game object on screen
        mousePoint.z = mZCoord;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    void OnMouseDrag()
    {
        transform.position = GetMouseWorldPos() + mOffset;
        transform.position = new Vector3(transform.position.x, 1, transform.position.z);

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    }

    public void OnMouseUp()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 10) == false)
        {
            if(parentSlot != null)
            {
                parentSlot.GetComponent<ItemSlot>().AddQuantity(1);
            }
            Destroy(gameObject);
        }
    }

    public void SetParentSlot(GameObject aSlot)
    {
        parentSlot = aSlot;
    }
}
