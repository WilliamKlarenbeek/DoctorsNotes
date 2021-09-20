using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Town : MonoBehaviour
{
    public GameObject mapObject;
    public int sceneSelection;
    public Cursor hoverCursor;

    public void OnMouseDown()
    {
        Cursor.SetCursor(hoverCursor);
    }
    //public Town(GameObject aMapObject, int aSceneSelection)
    //{
    //    mapObject = aMapObject;
    //    sceneSelection = aSceneSelection;
    //}
}
