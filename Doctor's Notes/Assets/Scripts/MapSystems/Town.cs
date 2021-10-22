using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Town : MonoBehaviour
{
    [SerializeField] Material hoverMaterial;
    [SerializeField] Material defaultMaterial;
    [SerializeField] int sceneSelected;

    public void OnMouseOver()
    {
        GetComponent<MeshRenderer>().material = hoverMaterial;
    }

    public void OnMouseExit()
    {
        GetComponent<MeshRenderer>().material = defaultMaterial;
    }

    public Vector3 GetTownLocation()
    {
        return gameObject.transform.localPosition;
    }

    public int GetTownScene()
    {
        return sceneSelected;
    }
}
