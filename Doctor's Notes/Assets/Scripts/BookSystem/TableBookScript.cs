using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableBookScript : MonoBehaviour
{
    [SerializeField] BookScript bookUI;
    [SerializeField] Material defaultMaterial;
    [SerializeField] Material hoverMaterial;

    public void OnMouseDown()
    {
        bookUI.ToggleActive();
    }

    public void OnMouseOver()
    {
        GetComponent<Renderer>().material = hoverMaterial;
    }

    public void OnMouseExit()
    {
        GetComponent<Renderer>().material = defaultMaterial;
    }
}
