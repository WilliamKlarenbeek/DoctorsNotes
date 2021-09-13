using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarPestle : Tool
{
    [SerializeField] private AudioClip berrySound;
    Vector3 worldPosition;
    int mouseSpins;
    int rotationQuad;
    string output;
    private TextMesh spinsText;

    public override void Start()
    {
        base.Start();
        spinsText = transform.Find("SpinsCounter").GetComponent<TextMesh>();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if (state == "working")
        {
            Plane plane = new Plane(Vector3.up, 0);

            float distance;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (plane.Raycast(ray, out distance))
            {
                worldPosition = ray.GetPoint(distance);
            }
            if (mouseSpins == 10)
            {
                state = "ready";
                Vector3 dist = Camera.main.WorldToScreenPoint(transform.position);
                Instantiate(Resources.Load("Prefabs/Materials/" + output), Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x - (Input.mousePosition.x - dist.x), Input.mousePosition.y - (Input.mousePosition.y - dist.y), dist.z)), new Quaternion());

                if (sndManager != null)
                {
                    sndManager.PlaySound(berrySound);
                }
                else
                {
                    Debug.Log("Sound Manager Does Not Exist!");
                }
                spinsText.text = "";
            }
            //Up and left check
            else if ((rotationQuad == 0) && (worldPosition.x < this.transform.position.x - 1) && (worldPosition.z > this.transform.position.z - 1))
            {
                rotationQuad = 1;
                spinsText.text = mouseSpins.ToString() + "/10";
                if (sndManager != null)
                {
                    sndManager.PlaySound(workingSound);
                }
                else
                {
                    Debug.Log("Sound Manager Does Not Exist!");
                }
            }
            //Up and right check
            else if ((rotationQuad == 1) && (worldPosition.x > this.transform.position.x - 1) && (worldPosition.z > this.transform.position.z - 1))
            {
                rotationQuad = 2;
                spinsText.text = mouseSpins.ToString() + "/10";
                if (sndManager != null)
                {
                    sndManager.PlaySound(workingSound);
                }
                else
                {
                    Debug.Log("Sound Manager Does Not Exist!");
                }
            }
            //Down and right check
            else if ((rotationQuad == 2) && (worldPosition.x > this.transform.position.x - 1) && (worldPosition.z < this.transform.position.z - 1))
            {
                rotationQuad = 3;
                spinsText.text = mouseSpins.ToString() + "/10";
                if (sndManager != null)
                {
                    sndManager.PlaySound(workingSound);
                }
                else
                {
                    Debug.Log("Sound Manager Does Not Exist!");
                }
            }
            //Down and left check
            else if ((rotationQuad == 3) && (worldPosition.x < this.transform.position.x - 1) && (worldPosition.y < this.transform.position.y - 1))
            {
                rotationQuad = 0;
                mouseSpins += 1;
                spinsText.text = mouseSpins.ToString() + "/10";
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
        if ((collision.gameObject.GetComponent<IngredientBasic>() != null) && (state == "ready"))
        {
            state = "working";
            output = collision.gameObject.GetComponent<IngredientBasic>().refinedVersion;
            Destroy(collision.gameObject);
            mouseSpins = 0;
            spinsText.text = mouseSpins.ToString() + "/10";
        }
    }
}
