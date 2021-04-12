using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Draggable : MonoBehaviour
{
    Vector3 dist;
    float posX;
    float posY;

    void OnMouseDown()
    {
        dist = Camera.main.WorldToScreenPoint(transform.position);
        posX = Input.mousePosition.x - dist.x;
        posY = Input.mousePosition.y - dist.y;

    }

    private void OnMouseDrag()
    {
        Vector3 curPos = new Vector3(Input.mousePosition.x - posX, Input.mousePosition.y - posY, dist.z);
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(curPos);
        if (worldPos.x > 3.5f)
        {
            worldPos.x = 3.5f;
        }
        else if (worldPos.x <  -3.5f)
        {
            worldPos.x = -3.5f;
        }
        if (worldPos.z > 8.5f)
        {
            worldPos.z = 8.5f;
        }
        else if (worldPos.z < -8.5f)
        {
            worldPos.z = -8.5f;
        }
        transform.position = worldPos;

    }
}
