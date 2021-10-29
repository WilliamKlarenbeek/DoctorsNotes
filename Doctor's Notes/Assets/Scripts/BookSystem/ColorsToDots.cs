using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorsToDots : MonoBehaviour
{
    public GameObject myPrefabRed0; 
    public GameObject myPrefabRed1; 
    public GameObject myPrefabRed2; 
    public GameObject myPrefabRed3; 
    public GameObject myPrefabRed4;  
    public GameObject myPrefabGreen0;  
    public GameObject myPrefabBlue0;  

    // Start is called before the first frame update
    void Start()
    {
        /*Instantiate(myPrefabRed0, new Vector3(0, 0, 0), Quaternion.identity);*/
        myPrefabRed0.gameObject.SetActive(true); 
        myPrefabRed1.gameObject.SetActive(false); 
        myPrefabRed2.gameObject.SetActive(true); 
        myPrefabRed3.gameObject.SetActive(false); 
        myPrefabRed4.gameObject.SetActive(true);
        myPrefabGreen0.gameObject.SetActive(true);
        myPrefabBlue0.gameObject.SetActive(true); 
    }

    // Update is called once per frame
    void Update()
    {
    }
}
