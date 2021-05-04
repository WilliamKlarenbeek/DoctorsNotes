using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//File --> Build Settings --> Drag in and order scenes according to requirements.
public class SceneController
{
    static int mainScene = 0;

    public static IEnumerator LoadMainScene(float aDuration)
    {
        float frame = 0;
        while(frame < aDuration)
        {
            frame += Time.deltaTime;
            yield return null;
        }
        SceneManager.LoadScene (mainScene);
    }

    public static IEnumerator LoadNextScene(float aDuration)
    {
        float frame = 0;
        while (frame < aDuration)
        {
            frame += Time.deltaTime;
            yield return null;
        }

        int currentScene = SceneManager.GetActiveScene().buildIndex;
        if (currentScene < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(currentScene + 1);
        }
    }

    public static IEnumerator LoadPreviousScene(float aDuration)
    {
        float frame = 0;
        while (frame < aDuration)
        {
            frame += Time.deltaTime;
            yield return null;
        }

        int currentScene = SceneManager.GetActiveScene().buildIndex;
        if (currentScene < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(currentScene - 1);
        }
    }

    public static IEnumerator LoadScene(int index, float aDuration)
    {
        float frame = 0;
        while (frame < aDuration)
        {
            frame += Time.deltaTime;
            yield return null;
        }

        if (index >= 0 && index < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(index);
        }
    }
}
