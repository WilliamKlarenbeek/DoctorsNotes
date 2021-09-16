// TODO: Player accepts the event so a dialog box appears and player selects the challenge
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class RandomEvents : MonoBehaviour
{
    [SerializeField] private GameObject _dialogBox;
    [SerializeField] private Button _yesButton;
    private bool _yesButtonClick;
    [SerializeField] private Button _noButton;

    [SerializeField] public RandomEventScene[] _scenes;

    // Start is called before the first frame update
    void Start()
    {
        _dialogBox.SetActive(false);
        RandomEvent();
    }

    private void RandomEvent()
    {
        float f = Random.Range(0.0f, 1.0f);
        Debug.Log("Random float: " + f);

        for (int i = 0; i < _scenes.Length; i++)
        {
            if (f >= _scenes[i].minProbabilityRange && f <= _scenes[i].maxProbabilityRange)
            {
                //we can set this dialog box active when we want to ask the player if he wants to accept the challange or not
                _dialogBox.SetActive(true);

                /*                _yesButton.onClick.AddListener(() =>
                               {
                                   Debug.Log("yes button clicked: " + _scenes[i]._sceneName);
                                   _yesButtonClick = true;
                               });*/

                /*                if (_yesButtonClick)
                                {
                                    Debug.Log("Loading Scene: " + _scenes[i]._sceneName);
                                    SceneManager.LoadScene(_scenes[i]._sceneName);

                                }*/
                SceneManager.LoadScene(_scenes[i]._sceneName);

                break;
            }
        }
    }

/*    public void OnPointerClick(PointerEventData eventData)
    {
        ((IPointerClickHandler)_yesButton).OnPointerClick(eventData);
        Debug.Log("Yes Button Pressed");
        _yesButtonClick = true;
    }*/
}

// Every random event scene will have the following attributes 
// Every scene will contain a reference to itself, will have a min probability and a max probability
[System.Serializable]
public class RandomEventScene
{
    public string _sceneName;
    public float minProbabilityRange = 0.0f;
    public float maxProbabilityRange = 1.0f; 
}
