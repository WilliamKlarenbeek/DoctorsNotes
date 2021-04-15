using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cauldron : Tool
{
    float timer = 0;

    // Update is called once per frame
    void Update()
    {
        if (state == "working")
        {
            timer += Time.deltaTime;
            //Debug.Log("Brewing for " + timer.ToString());
        }
    }

    public override void PerformAction(Collider collision)
    {
        if ((collision.gameObject.GetComponent<Material>() != null) && (state == "ready"))
        {
            state = "working";
            if (collision.gameObject.GetComponent<Berry>() != null)
            {
                output = "Prefabs / Potions / BluePotion";
            }
            else if (collision.gameObject.GetComponent<RefinedBerry>() != null)
            {
                output = "Prefabs/Potions/AquaPotion";
            }
            Destroy(collision.gameObject);
        }
        else if ((collision.gameObject.GetComponent<Beaker>() != null) && (state == "working"))
        {
            state = "ready";
            Vector3 dist = Camera.main.WorldToScreenPoint(transform.position);
            if ((timer > 10) && (timer < 30))
            {
                Instantiate(Resources.Load(output), Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x - (Input.mousePosition.x - dist.x), Input.mousePosition.y - (Input.mousePosition.y - dist.y), dist.z)), new Quaternion());
            }
            else
            {
                Instantiate(Resources.Load("Prefabs/Potions/BurntPotion"), Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x - (Input.mousePosition.x - dist.x), Input.mousePosition.y - (Input.mousePosition.y - dist.y), dist.z)), new Quaternion());
            }
            Destroy(collision.gameObject);
        }
    }
}
