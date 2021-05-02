using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] public class Apprenticedata
{
    public float apprenticeExperience = 0;
}

public class ApprenticeDataManager
{
    static Apprenticedata apprenticeData = new Apprenticedata();

    public static void AddApprenticeExperience(float value)
    {
        PlayerPrefs.SetFloat("ApprenticeExperience", (PlayerPrefs.GetFloat("ApprenticeExperience") + value));
    }

    public static void RemoveApprenticeExperience(float value)
    {
        PlayerPrefs.SetFloat("ApprenticeExperience", (PlayerPrefs.GetFloat("ApprenticeExperience") - value));
    }

    public static float GetApprenticeExperience()
    {
        return PlayerPrefs.GetFloat("ApprenticeExperience");
    }

    public static void DeleteApprenticeExperience()
    {
        PlayerPrefs.DeleteKey("ApprenticeExperience");
    }

    public static void ScaleApprentice(GameObject apprentice)
    {
        float scale = ApprenticeDataManager.GetApprenticeExperience();
        Vector3 scaleChange = new Vector3(scale, scale, scale);
        apprentice.transform.localScale = scaleChange;
    }

    public static bool CheckScale(Apprentice apprentice)
    {
        bool scalable = true;
        if (apprentice.maximumExperience < GetApprenticeExperience())
        {
            Debug.Log("Maximum Size Reached");
            PlayerPrefs.SetFloat("ApprenticeExperience", apprentice.maximumExperience);
            return !scalable;
        }
        else if (apprentice.minimumExperience > GetApprenticeExperience())
        {
            Debug.Log("Minimum Size Reached");
            PlayerPrefs.SetFloat("ApprenticeExperience", apprentice.minimumExperience);
            return !scalable;
        } else
           return scalable; 
    }
}
