using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleActive(GameObject aObject)
    {
        if (aObject.activeSelf)
        {
            aObject.SetActive(false);
        } else
        {
            aObject.SetActive(true);
        }
    }
}
