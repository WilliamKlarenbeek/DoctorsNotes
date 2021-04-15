using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug Menu Key Options
        //Press M for free money
        if (Input.GetKey(KeyCode.M))
        {
            GameDataManager.AddMoney(10);
            GameSharedUI.Instance.UpdateMoneyUIText();
        }
    }
}
