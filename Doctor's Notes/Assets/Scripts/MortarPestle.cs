using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarPestle : Tool
{
    GameObject output;

    // Update is called once per frame
    void Update()
    {

    }

    public override void PerformAction(Collider collision)
    {
        if ((collision.gameObject.GetComponent<Berry>() != null) && (state == "working"))
        {
            Destroy(collision.gameObject);
        }
        else if (state == "ready")
        {
            Destroy(collision.gameObject);
            Vector3 dist = Camera.main.WorldToScreenPoint(transform.position);
            Instantiate(Resources.Load("Prefabs/Materials/RefinedBerry"), Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x - (Input.mousePosition.x - dist.x), Input.mousePosition.y - (Input.mousePosition.y - dist.y), dist.z)), new Quaternion());
        }
    }
}
