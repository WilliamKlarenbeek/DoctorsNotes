using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemCursor : MonoBehaviour
{
    [SerializeField] Texture2D hoverCursor;
    [SerializeField] Texture2D downCursor;

    private void OnMouseOver()
    {
        Cursor.SetCursor(hoverCursor, Vector2.zero, CursorMode.Auto);
    }

    private void OnMouseDown()
    {
        Cursor.SetCursor(downCursor, Vector2.zero, CursorMode.Auto);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Cursor.SetCursor(downCursor, Vector2.zero, CursorMode.Auto);
    }

    private void OnMouseExit()
    {
        Cursor.SetCursor(default, Vector2.zero, CursorMode.Auto);
    }

    private void OnMouseUp()
    {
        Cursor.SetCursor(default, Vector2.zero, CursorMode.Auto);
    }
}
