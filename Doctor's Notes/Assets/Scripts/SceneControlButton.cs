using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Apply to buttons for moving between scenes, MainMenu is 0.
public class SceneControlButton : MonoBehaviour
{
    enum TargetScene
    {
        Next,
        Previous,
        MainMenu
    }

    [SerializeField] TargetScene targetScene;
    Button sceneButton;

    void Start()
    {
        sceneButton = GetComponent<Button> ();
        switch (targetScene)
        {
            case TargetScene.MainMenu:
                sceneButton.onClick.AddListener(() => SceneController.LoadMainScene());
                break;

            case TargetScene.Next:
                sceneButton.onClick.AddListener(() => SceneController.LoadNextScene());
                break;

            case TargetScene.Previous:
                sceneButton.onClick.AddListener(() => SceneController.LoadPreviousScene());
                break;
        }
    }
}
