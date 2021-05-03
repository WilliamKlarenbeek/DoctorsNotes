using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LinearTimer : MonoBehaviour
{
    //Implementing this class as SingleTon
    public static LinearTimer instance; 

    private Image timerBar;
    //controls the game timer
    private bool timerGoing;
    //time spent by the player in the scene
    private TimeSpan timePlaying;
    //the time between each frame of the game
    private float elapsedTime;
    //reset time is the number of seconds that we want to keep in a day
    //or however long we want the game's day to be.
    public float resetTime; 

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        timerBar = GetComponent<Image>();
        timerGoing = false;
        BeginTimer();
    }

    private void BeginTimer()
    {
        timerGoing = true;
        elapsedTime = 0f;
        timerBar.fillAmount = elapsedTime; 

        StartCoroutine(UpdateTimer()); 
    }

    public void EndTimer()
    {
        timerGoing = false; 
    }

    IEnumerator UpdateTimer()
    {
        while(timerGoing)
        {
            //time passed between the previous frame and this frame
            elapsedTime += Time.deltaTime;
            //Debug.Log(elapsedTime);
            timePlaying = TimeSpan.FromSeconds(elapsedTime);
            //Debug.Log(timePlaying);
            string timePlayingStr = timePlaying.ToString("mm':'ss'.'ff");
            timerBar.fillAmount = (float)(elapsedTime / resetTime);

            if (elapsedTime == resetTime)
                EndTimer();
            //Debug.Log(timeplayingStr);

            yield return null;
        }
    }
}
