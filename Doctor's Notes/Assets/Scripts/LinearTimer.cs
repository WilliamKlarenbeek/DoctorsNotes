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
    private float elapsedTime = 0f;
    //reset time is the number of seconds that we want to keep in a day
    //or however long we want the game's day to be.
    public float resetTime;

    //Blight Effect
    [SerializeField] private GameObject blightEffect;
    private ParticleSystem blightParticleSystem;
    private ParticleSystem.ShapeModule blightParticleSystemShape;
    private ParticleSystem.EmissionModule blightParticleSystemEmission;

    //The initial values of the blight effect (when the timer is zero)
    private float blightOriginPointY = 4f;
    private float blightOriginScaleY = 0f;
    private float blightOriginEmission = 20f;
    private float blightMaxYScale = 6f;
    
    [SerializeField] private MapSelection mapSelectionDB;

    private void Awake()
    {
        instance = this;
        if (blightEffect != null)
        {
            blightParticleSystem = blightEffect.GetComponent<ParticleSystem>();
            blightParticleSystemShape = blightParticleSystem.shape;
            blightParticleSystemEmission = blightParticleSystem.emission;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (mapSelectionDB.isGameBegin() || mapSelectionDB.GetCurrentLocation() == null)
        {
            mapSelectionDB.SetCurrentTimer(0f);
        }

        elapsedTime = mapSelectionDB.GetCurrentTimer(); 
        timerBar = GetComponent<Image>();
        timerBar.fillAmount = (float)(elapsedTime / resetTime);
        timerGoing = false;
        Debugger.debuggerInstance.WriteToFileTag("LinearTimer"); 
    }

    public void BeginTimer()
    {
        timerGoing = true;
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
            //timerBar.fillAmount = PlayerIcon.instance.distPercentage;
            blightParticleSystemShape.position = new Vector2(0, Mathf.Lerp(blightOriginPointY, 0, (float)(elapsedTime / resetTime)));
            blightParticleSystemShape.scale = new Vector3(10, Mathf.Lerp(blightOriginScaleY, blightMaxYScale, (float)(elapsedTime / resetTime)), 1);
            blightParticleSystemEmission.rateOverTime = Mathf.Lerp(blightOriginEmission, 100f, (float)(elapsedTime / resetTime));

            if (elapsedTime == resetTime)
                EndTimer();
            //Debug.Log(timeplayingStr);

            mapSelectionDB.SetCurrentTimer(elapsedTime);

            yield return null;
        }
    }
}
