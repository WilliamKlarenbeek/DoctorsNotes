using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//Apply to buttons for moving between scenes, MainMenu is 0.
public class SceneControlButton : MonoBehaviour
{
    enum TargetScene
    {
        Next,
        Previous,
        Quit,
        MainMenu
    }

    [SerializeField] TargetScene targetScene;
    [SerializeField] float transitionDuration = 1f;
    Button sceneButton;

    void Start()
    {
        sceneButton = GetComponent<Button>();
        if (sceneButton != null)
        {
            switch (targetScene)
            {
                case TargetScene.MainMenu:
                    sceneButton.onClick.AddListener(() => StartCoroutine(SceneController.LoadMainScene(transitionDuration)));
                    break;

                case TargetScene.Next:
                    sceneButton.onClick.AddListener(() => StartCoroutine(SceneController.LoadNextScene(transitionDuration)));
                    break;
                case TargetScene.Previous:
                    sceneButton.onClick.AddListener(() => StartCoroutine(SceneController.LoadPreviousScene(transitionDuration)));
                    break;
                case TargetScene.Quit:
                    //Quit actual application
                    sceneButton.onClick.AddListener(() => Application.Quit());
                    //Quit unity editor playtesting
                    sceneButton.onClick.AddListener(() => UnityEditor.EditorApplication.isPlaying = false);
                    break;
            }
        }
    }
}
