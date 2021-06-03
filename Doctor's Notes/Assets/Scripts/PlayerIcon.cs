// TODO: Percentage to be calculated on player movement
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class PlayerIcon : MonoBehaviour
{
    //Implementing the class as singleton
    public static PlayerIcon instance;

    //[SerializeField] private GameObject playerIcon; 
    [SerializeField] private GameObject _startLevel;
    //speed the player icon moves at
    private float speed = 10.0f;
    [SerializeField] private MapSelection mapSelectionDB;

    private Vector3 _targetPos;
    private Vector3 _startPos;
    private Vector3 _currentPos;
    private bool moving = false;
    
    public float distPercentage = 0.0f; 

    private void Awake()
    {
        instance = this; 
    }

    // Start is called before the first frame update
    void Start()
    {
        if (mapSelectionDB.isGameBegin() || mapSelectionDB.GetCurrentLocation() == null)
        {
            mapSelectionDB.SetCurrentLocation(_startLevel.GetComponent<RectTransform>().anchoredPosition);
            mapSelectionDB.SetGameBeginFlag(false);
        }
        //player Icon is set to start level position when the game starts
        //transform.position = new Vector3(295, 568, 0);
        GetComponent<RectTransform>().anchoredPosition = mapSelectionDB.GetCurrentLocation();
        _startPos = GetComponent<RectTransform>().anchoredPosition;
    }

    public IEnumerator Movement(Vector3 targetPos)
    {
        float step = speed * Time.deltaTime;
        _targetPos = targetPos;
        _currentPos = _startPos;
        LinearTimer.instance.BeginTimer();
        moving = true;

        while (transform.position != targetPos)
        {
            //distPercentage = ToString("F2");
            //Debug.Log("Start: " + _startPos.magnitude + "Current: " + _currentPos.magnitude + "Target: " + _targetPos.magnitude);
            distPercentage = getDistPercentage(_startPos, _currentPos, _targetPos);
            //Debug.Log("Distance Percentage for Timer: " + distPercentage);

            // no movement occurs on z-axis
            //transform.Translate(1 * Time.deltaTime, 1 * Time.deltaTime, 0); 
            transform.position = Vector3.MoveTowards(transform.position, targetPos, step);
            // set the current position to the position of the playerIcon
            _currentPos = transform.position;

            yield return new WaitForEndOfFrame();
        }
        moving = false;
        LinearTimer.instance.EndTimer();
        mapSelectionDB.SetCurrentLocation(GetComponent<RectTransform>().anchoredPosition);
        yield return StartCoroutine(SceneController.LoadScene(LevelSelection.levelSelectionInstance.getLevelIndex(), 2f));
    }

    public float getDistPercentage(Vector3 _startPos, Vector3 _currentPos, Vector3 _targetPos)
    {
        // icon position is 0 at startlevel 
        // length between the icon and target position divided by the total length between the start and target position gives us the 
        float lpercentage = 0.0f;
        float ldistTravelled = (_currentPos - _startPos).magnitude;
        float ldistLeft = (_targetPos - _currentPos).magnitude;
        float ltotalDist = (_targetPos - _startPos).magnitude;
        Debug.Log("Distance Total for Timer: " + ltotalDist);
        Debug.Log("Distance Travelled for Timer: " + ldistLeft);
        // percentage can not be infinity 
        if (ltotalDist != 0)
        {
            lpercentage = ldistLeft / ltotalDist;
        }
        /*        if (_currentPos.magnitude > 0.01f)
                {
                    lpercentage = (_targetPos - _currentPos).magnitude / _targetPos.magnitude;
                }*/
        return lpercentage;
    }
    void OnApplicationQuit()
    {
        mapSelectionDB.SetGameBeginFlag(true);
    }
    
    public bool isMoving()
    {
        return moving;
    }
}   
