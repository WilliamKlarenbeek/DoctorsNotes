using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patient : MonoBehaviour
{
    private int currHP = 2;
    private int neededHP = 8;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider collision)
    {
        if (!(Input.GetMouseButton(0)) && (collision.gameObject.GetComponent<Potion>() != null))
        {
            switch (collision.gameObject.name)
            {
                case "BluePotion(Clone)":
                    currHP += 1;
                    break;
                case "AquaPotion(Clone)":
                    currHP += 2;
                    break;
                case "BurntPotion(Clone)":
                    currHP -= 1;
                    break;
            }
            
            Destroy(collision.gameObject);

            if (currHP <= 0)
            {
                Debug.Log("Dead");
            }
            else if (currHP >= neededHP)
            {
                Debug.Log("Cured");
            }
        }
    }
}
