using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    public float interval;
    public float amplitude;

    private Light myLight;
    private float intensity;
    private float frame;
    // Start is called before the first frame update
    void Start()
    {
        myLight = GetComponent<Light>();
        intensity = myLight.intensity;
    }

    // Update is called once per frame
    void Update()
    {
        frame += Time.deltaTime;

        if(frame > interval)
        {
            myLight.intensity = intensity + Random.Range(-amplitude, amplitude);
            frame = 0;
        }
    }

    public void SetColor(float red, float green, float blue)
    {
        myLight.color = new Color(red, green, blue);
    }
}
