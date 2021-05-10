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
            GameDataManager.AddMoney(100);
            GameSharedUI.Instance.UpdateMoneyUIText();
        }

        if (Input.GetKey(KeyCode.N))
        {
            GameDataManager.RemoveMoney(10);
            GameSharedUI.Instance.UpdateMoneyUIText();
        }

        if (Input.GetKey(KeyCode.L))
        {
            if (ApprenticeDataManager.CheckScale(Apprentice.Instance))
            {
                ApprenticeDataManager.AddApprenticeExperience(1);
                //Debug.Log(ApprenticeDataManager.GetApprenticeExperience());
                Apprentice.Instance.UpdateApprenticeScale();
                //Debug.Log(ApprenticeDataManager.GetApprenticeExperience());
            }
                //Debug.Log("YOU HAVE NO SCALE HERE");
        }

        if (Input.GetKey(KeyCode.K))
        {
            if (ApprenticeDataManager.CheckScale(Apprentice.Instance))
            {
                ApprenticeDataManager.RemoveApprenticeExperience(1);
                //Debug.Log(ApprenticeDataManager.GetApprenticeExperience());
                Apprentice.Instance.UpdateApprenticeScale();
                //Debug.Log(ApprenticeDataManager.GetApprenticeExperience());
            }
                //Debug.Log("YOU HAVE NO SCALE HERE");
        }


    }
}
