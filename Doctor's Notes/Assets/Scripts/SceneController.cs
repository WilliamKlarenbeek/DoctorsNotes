using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//File --> Build Settings --> Drag in and order scenes according to requirements.
public class SceneController
{
    static public List<string> patientScenes = new List<string>();

    static int mainScene = 0;

    public static IEnumerator LoadMainScene(float aDuration)
    {
        float frame = 0;
        FadeMusic(aDuration);

        while (frame < aDuration)
        {
            frame += Time.deltaTime;
            yield return null;
        }
        SceneManager.LoadScene (mainScene);
    }

    public static IEnumerator LoadNextScene(float aDuration)
    {
        float frame = 0;
        FadeMusic(aDuration);

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
        FadeMusic(aDuration);

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

    public static IEnumerator LoadScene(string index, float aDuration)
    {
        float frame = 0;
        FadeMusic(aDuration);

        while (frame < aDuration)
        {
            frame += Time.deltaTime;
            yield return null;
        }

        SceneManager.LoadScene(index);
    }

    static void FadeMusic(float aDuration)
    {
        if (GameObject.Find("Controller").GetComponent<GameController>() != null)
        {
            GameObject.Find("Controller").GetComponent<GameController>().FadeOutCoroutine(aDuration);
        }
        else
        {
            Debug.Log("Controller Doesn't Exist");
        }
    } 
}
