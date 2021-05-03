using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasObject : MonoBehaviour
{
    public GameObject canvasObject;
    // Start is called before the first frame update
    void Start()
    {
        MakeActive();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MakeActive()
    {
        canvasObject.SetActive(false);
    }
}
