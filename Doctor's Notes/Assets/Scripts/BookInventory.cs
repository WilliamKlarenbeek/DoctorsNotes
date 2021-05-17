using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BookInventory : MonoBehaviour
{
    public void ToggleActive(GameObject aObject)
    {
        if (aObject.activeSelf)
        {
            aObject.SetActive(false);
        } else
        {
            aObject.SetActive(true);
        }
    }
}
