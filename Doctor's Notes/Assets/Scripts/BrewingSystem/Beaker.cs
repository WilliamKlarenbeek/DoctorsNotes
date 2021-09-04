using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beaker : Tool
{
    private Vector3 screenPoint;
    private Vector3 offset;
    private float objectYPos; 

    [SerializeField] Texture2D cursorHover;
    [SerializeField] Texture2D cursorClick;
    [SerializeField] Texture2D cursorDefault;

    // Update is called once per frame
    void Update()
    {

    }

    public override void PerformAction(Collider collision)
    {

    }

    void OnMouseDown()
    {
        Cursor.SetCursor(cursorClick, Vector2.zero, CursorMode.Auto);

        objectYPos = transform.position.y;

        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);

        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));

    }

    void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);

        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        transform.position = curPosition;
    }

    void OnMouseOver()
    {
       Cursor.SetCursor(cursorHover, Vector2.zero, CursorMode.Auto);
    }

    //private void OnMouseDown()
    //{
    //    Cursor.SetCursor(cursorClick, Vector2.zero, CursorMode.Auto);
    //}

    void OnMouseExit()
    {
        Cursor.SetCursor(cursorDefault, Vector2.zero, CursorMode.Auto);
    }
}
