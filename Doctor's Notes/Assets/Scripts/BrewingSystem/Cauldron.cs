using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cauldron : Tool
{
    [SerializeField] private AudioClip potionSound;
    public GameObject liquidObject;
    public GameObject fumesObject;
    float timer = 0;
    float redTotal;
    float blueTotal;
    float greenTotal;
    float blackTotal;
    private TextMesh brewingText;
    private Renderer renderer;
    private Color brewingColor;
    private CauldronLiquid liquid;
    private CauldronFumes fumes;
    List<Ingredient> ingredientList = new List<Ingredient>();

    public override void Start()
    {
        base.Start();
        brewingText = transform.Find("BrewingCounter").GetComponent<TextMesh>();
        //Debugger.debuggerInstance.WriteToFileTag("Couldron");
        renderer = GetComponent<Renderer>();
        liquid = liquidObject.GetComponent<CauldronLiquid>();
        fumes = fumesObject.GetComponent<CauldronFumes>();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if (state == "working")
        {
            timer += Time.deltaTime;
            brewingText.text = ((int)timer).ToString();

            brewingColor = new Color(Mathf.Clamp(redTotal - blackTotal, 0, 1), Mathf.Clamp(greenTotal - blackTotal, 0, 1), Mathf.Clamp(blueTotal - blackTotal, 0, 1));
            if (renderer != null)
            {
                liquidObject.SetActive(true);
                renderer.material.color = brewingColor;
                liquid.ChangeColor(redTotal, greenTotal, blueTotal, blackTotal);
                fumes.StartParticles();
                fumes.ChangeColor(redTotal, greenTotal, blueTotal, blackTotal);
            }
        }
        else
        {
            brewingColor = new Color(1, 1, 1);
            if (renderer != null)
            {
                renderer.material.color = brewingColor;
                liquid.ChangeColor(1, 1, 1, 0);
                liquidObject.SetActive(false);
                fumes.StopParticles();
                fumes.ChangeColor(1, 1, 1, 0);
            }
        }
    }

    public override void PerformAction(Collider collision)
    {
        if ((collision.gameObject.GetComponent<Ingredient>() != null))
        {
            Ingredient insertedMaterial = collision.gameObject.GetComponent<Ingredient>();
            ingredientList.Add(insertedMaterial);
            state = "working";
            if (insertedMaterial != null)
            {
                redTotal += insertedMaterial.red;
                blueTotal += insertedMaterial.blue;
                greenTotal += insertedMaterial.green;
                blackTotal += insertedMaterial.black;
            }

            if (sndManager != null)
            {
                StartCoroutine(sndManager.FadeInSound(workingSound, 1f, 1f, true));
            }
            else
            {
                Debug.Log("Sound Manager Does Not Exist!");
            }
            Destroy(collision.gameObject);
        }
        else if ((collision.gameObject.GetComponent<Beaker>() != null) && (state == "working"))
        {
            state = "ready";
            Vector3 dist = Camera.main.WorldToScreenPoint(transform.position);
            GameObject objectInstance = Instantiate(Resources.Load("Prefabs/Potions/Potion"), Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x - (Input.mousePosition.x - dist.x), Input.mousePosition.y - (Input.mousePosition.y - dist.y), dist.z)), new Quaternion()) as GameObject;
            Potion newPotion = objectInstance.GetComponent<Potion>();
            if ((timer >= 10) && (timer <= 25))
            {
                newPotion.Red = redTotal;
                newPotion.Blue = blueTotal;
                newPotion.Green = greenTotal;
                newPotion.Black = blackTotal;
                newPotion.Ingredients = ingredientList;
            }
            else if (timer < 10)
            {
                newPotion.Red = 0;
                newPotion.Blue = 0;
                newPotion.Green = 0;
                newPotion.Black = 1;
            }
            else
            {
                newPotion.Red = redTotal;
                newPotion.Blue = blueTotal;
                newPotion.Green = greenTotal;
                newPotion.Black = blackTotal + (timer - 25) / 10;
            }
            redTotal = 0;
            blueTotal = 0;
            greenTotal = 0;
            blackTotal = 0;
            timer = 0;
            ingredientList.Clear();

            if (sndManager != null)
            {
                sndManager.PlaySound(potionSound);
                StartCoroutine(sndManager.FadeOutSound(workingSound, 1f, true));
            }
            else
            {
                Debug.Log("Sound Manager Does Not Exist!");
            }
            brewingText.text = "";
            Destroy(collision.gameObject);
        }
    }
}