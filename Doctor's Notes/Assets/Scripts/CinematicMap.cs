using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicMap : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator PlayEffect()
    {
        Debug.Log("Initializing Effect.");

        float duration = 0f;
        GetComponent<Renderer>().material.SetFloat("Phase", 0f);

        while(duration < 60f)
        {
            duration += Time.deltaTime;
            GetComponent<Renderer>().material.SetFloat("Phase", duration);

            yield return null;
        }
        duration = 60f;
        GetComponent<Renderer>().material.SetFloat("Phase", duration);
    }
}
