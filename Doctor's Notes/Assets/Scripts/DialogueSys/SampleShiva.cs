using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class SampleShiva : MonoBehaviour
{
    private int _buttonAclicks;
    private int _buttonBClicks; 
    public Button buttonA;
    public Button buttonB; 

    void Start()
    {
        buttonA.onClick.AddListener(TaskOnClick); 
    }

    void TaskOnClick()
    {
        _buttonAclicks++; 
        Debug.Log("Button A pressed: " + _buttonAclicks + "times");
        buttonA.gameObject.SetActive(false);
    }
}