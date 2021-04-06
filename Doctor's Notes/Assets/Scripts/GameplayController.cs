using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameplayController : MonoBehaviour, IDropHandler
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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

    public void OnDrop(PointerEventData eventData) {
        RectTransform invPanel = transform as RectTransform;

        if(!RectTransformUtility.RectangleContainsScreenPoint(invPanel,
            Input.mousePosition))
        {
            Debug.Log("Dropped?");
        }
    }

    public void OnEndDrop(PointerEventData eventData)
    {

    }
}
