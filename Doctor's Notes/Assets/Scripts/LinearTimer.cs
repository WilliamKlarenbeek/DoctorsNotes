// TODO: Timer is based on PlayerMovement
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
    [SerializeField] private MapSelection mapSelectionDB;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (mapSelectionDB.isGameBegin() || mapSelectionDB.GetCurrentLocation() == null)
        {
            mapSelectionDB.SetCurrentTimer(0f);
            mapSelectionDB.SetGameBeginFlag(false);
        }

        elapsedTime = mapSelectionDB.GetCurrentTimer(); 
        timerBar = GetComponent<Image>();
        timerBar.fillAmount = (float)(elapsedTime / resetTime);
        timerGoing = false;
    }

    public void BeginTimer()
    {
        timerGoing = true;

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
            //timerBar.fillAmount = PlayerIcon.instance.distPercentage;

            if (elapsedTime == resetTime)
                EndTimer();
            //Debug.Log(timeplayingStr);

            mapSelectionDB.SetCurrentTimer(elapsedTime);

            yield return null;
        }
    }

    void OnApplicationQuit()
    {
        mapSelectionDB.SetGameBeginFlag(true);
    }
}
