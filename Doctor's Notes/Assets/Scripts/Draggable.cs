using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Draggable : MonoBehaviour
{
    Vector3 dist;
    float posX;
    float posY;
    Vector3 worldPos;

    void OnMouseDown()
    {
        dist = Camera.main.WorldToScreenPoint(transform.position);
        posX = Input.mousePosition.x - dist.x;
        posY = Input.mousePosition.y - dist.y;
    }

    private void OnMouseDrag()
    {
        Plane plane = new Plane(Vector3.up, 0);

        float distance;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (plane.Raycast(ray, out distance))
        {
            worldPos = ray.GetPoint(distance);
        }

        worldPos.y = 1;

        if (worldPos.x > 11)
        {
            worldPos.x = 11;
        }
        else if (worldPos.x <  -25)
        {
            worldPos.x = -25;
        }
        if (worldPos.z > 10)
        {
            worldPos.z = 10;
        }
        else if (worldPos.z < -10)
        {
            worldPos.z = -10;
        }
        transform.position = worldPos;

    }
}
