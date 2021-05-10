using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarPestle : Tool
{
    [SerializeField] private AudioClip berrySound;

    Vector3 worldPosition;
    int mouseSpins;
    // Update is called once per frame
    void Update()
    {
        if(state == "working")
        {
            Plane plane = new Plane(Vector3.up, 0);

            float distance;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (plane.Raycast(ray, out distance))
            {
                worldPosition = ray.GetPoint(distance);
            }

            if ((mouseSpins%2 == 0) && (worldPosition.x < this.transform.position.x - 1))
            {
                mouseSpins += 1;
                Debug.Log("Spins: " + mouseSpins);
                if (sndManager != null)
                {
                    sndManager.PlaySound(workingSound);
                }
                else
                {
                    Debug.Log("Sound Manager Does Not Exist!");
                }
            }
            else if ((mouseSpins % 2 != 0) && (worldPosition.x > this.transform.position.x + 1))
            {
                mouseSpins += 1;
                Debug.Log("Spins: " + mouseSpins);
                if (sndManager != null)
                {
                    sndManager.PlaySound(workingSound);
                }
                else
                {
                    Debug.Log("Sound Manager Does Not Exist!");
                }
            }
        }
    }

    public override void PerformAction(Collider collision)
    {
        if ((collision.gameObject.GetComponent<Berry>() != null) && (state == "ready"))
        {
            state = "working";
            Destroy(collision.gameObject);
            mouseSpins = 0;
        }
        else if ((collision.gameObject.GetComponent<Beaker>() != null) && (mouseSpins > 3) && (state == "working"))
        {
            state = "ready";
            Destroy(collision.gameObject);
            Vector3 dist = Camera.main.WorldToScreenPoint(transform.position);
            Instantiate(Resources.Load("Prefabs/Materials/RefinedBerry"), Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x - (Input.mousePosition.x - dist.x), Input.mousePosition.y - (Input.mousePosition.y - dist.y), dist.z)), new Quaternion());

            if (sndManager != null)
            {
                sndManager.PlaySound(berrySound);
            }
            else
            {
                Debug.Log("Sound Manager Does Not Exist!");
            }
        }
    }
}
