using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TextButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Color highlightTextColor;

    private Color originalTextColor;
    private Text textObject;
    // Start is called before the first frame update
    void Start()
    {
        textObject = transform.Find("Text").gameObject.GetComponent<Text>();
        originalTextColor = textObject.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        textObject.color = highlightTextColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        textObject.color = originalTextColor;
    }
}
