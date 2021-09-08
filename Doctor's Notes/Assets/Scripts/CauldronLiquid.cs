using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CauldronLiquid : MonoBehaviour
{
    private Renderer renderer;
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeColor(float red, float green, float blue, float black)
    {
        renderer.material.color = new Color(Mathf.Clamp(red * (2 - black), 0, 1), Mathf.Clamp(green * (2 - black), 0, 1), Mathf.Clamp(blue * (2 - black), 0, 1));
    }
}
