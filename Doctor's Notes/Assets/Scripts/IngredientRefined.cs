using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientRefined : Ingredient
{
    public override void Start()
    {
        base.Start();
        gameObject.GetComponent<Renderer>().material.color = new Color(red * (2 - black), green * (2 - black), blue * (2 - black), 2 - black);
    }
}
