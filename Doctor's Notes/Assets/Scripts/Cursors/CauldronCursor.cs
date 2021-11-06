using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CauldronCursor : MonoBehaviour
{
    [SerializeField] Texture2D hoverCursor;

    private void OnMouseOver()
    {
        //Cursor.SetCursor(hoverCursor, Vector2.zero, CursorMode.Auto);
    }
}
