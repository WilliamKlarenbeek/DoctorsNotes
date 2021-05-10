using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patient : MonoBehaviour
{
    private int currHP = 1;
    private int neededHP = 4;
    private bool isColliding;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        isColliding = false;
    }

    private void OnTriggerStay(Collider collision)
    {
        if (isColliding)
        {
            return;
        }
        isColliding = true;

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

            Debug.Log("HP: " + currHP);

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