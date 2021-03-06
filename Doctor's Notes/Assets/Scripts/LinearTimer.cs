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
    //Current Day is self explanatory; the day the player is currently on.
    private int currentDay = 1;
    //End day is how many days the player has before the blight consumes the world.
    private int endDay;

    //Blight Effect
    [SerializeField] private GameObject blightEffect;
    private ParticleSystem blightParticleSystem;
    private ParticleSystem.ShapeModule blightParticleSystemShape;
    private ParticleSystem.EmissionModule blightParticleSystemEmission;

    //The initial values of the blight effect (when the timer is zero)
    private float blightOriginPointY = 1.12f;
    private float blightOriginScaleY = 0f;
    private float blightOriginEmission = 20f;
    private float blightMaxYScale = 1.5f;

    //Light
    [SerializeField] private GameObject lightObject;
    private LightFlicker light;

    //Map
    [SerializeField] private GameObject mapObject;
    private Image map;
    [SerializeField] private MapSelection mapSelectionDB;
    [SerializeField] private MapEventHandler mapEventHandler;

    private void Awake()
    {
        instance = this;
        if (blightEffect != null)
        {
            blightParticleSystem = blightEffect.GetComponent<ParticleSystem>();
            blightParticleSystemShape = blightParticleSystem.shape;
            blightParticleSystemEmission = blightParticleSystem.emission;
        }
        if (lightObject != null)
        {
            light = lightObject.GetComponent<LightFlicker>();
        }
        if(mapObject != null)
        {
            map = mapObject.GetComponent<Image>();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        elapsedTime = mapSelectionDB.GetCurrentTimer(); 
        currentDay = mapSelectionDB.GetCurrentDay();
        endDay = mapSelectionDB.GetMaxDay() + mapSelectionDB.GetBonusDay();
        timerBar = GetComponent<Image>();
        timerBar.fillAmount = (float)(elapsedTime / resetTime);
        timerGoing = false;
        Debugger.debuggerInstance.WriteToFileTag("LinearTimer"); 
    }

    void Update()
    {
        UpdateBlightFX();
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
        if (mapEventHandler != null && currentDay >= endDay)
        {
            mapEventHandler.EndingInit(0);
        }
    }

    public void ResetTimer()
    {
        currentDay++;
        elapsedTime = 0;
    }

    private void UpdateBlightFX()
    {
        blightParticleSystemShape.position = new Vector2(0, Mathf.Lerp(blightOriginPointY, 0.5f, ((float)mapSelectionDB.GetCurrentDay() / ((float)mapSelectionDB.GetMaxDay() + (float)mapSelectionDB.GetBonusDay()))));
        blightParticleSystemShape.scale = new Vector3(2.5f, Mathf.Lerp(blightOriginScaleY, blightMaxYScale, ((float)mapSelectionDB.GetCurrentDay() / ((float)mapSelectionDB.GetMaxDay() + (float)mapSelectionDB.GetBonusDay()))), 1);
        blightParticleSystemEmission.rateOverTime = Mathf.Lerp(blightOriginEmission, 100f, ((float)mapSelectionDB.GetCurrentDay() / ((float)mapSelectionDB.GetMaxDay() + (float)mapSelectionDB.GetBonusDay())));
        Debug.Log("Current Day: " + currentDay);
        Debug.Log("End Day: " + endDay);
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

            if (elapsedTime > resetTime)
            {
                if(currentDay >= endDay)
                {
                    EndTimer();
                } 
                else
                {
                    ResetTimer();
                }
            }
            if(light != null)
            {
                light.SetColor((Mathf.Cos(((elapsedTime / resetTime) * 360) * Mathf.Deg2Rad) / 3) + 0.66f, (Mathf.Cos(((elapsedTime / resetTime) * 360) * Mathf.Deg2Rad) / 3) + 0.66f, 0.5f);
            }
            if(map != null)
            {
                map.color = new Color((Mathf.Cos(((elapsedTime / resetTime) * 360) * Mathf.Deg2Rad) / 3) + 0.66f, (Mathf.Cos(((elapsedTime / resetTime) * 360) * Mathf.Deg2Rad) / 3) + 0.5f, 0.33f);
            }
            //Debug.Log((Mathf.Cos(((elapsedTime / resetTime) * 360) * Mathf.Deg2Rad) / 4) + 0.5f);

            mapSelectionDB.SetCurrentTimer(elapsedTime);
            mapSelectionDB.SetCurrentDay(currentDay);

            yield return null;
        }

    }
}
