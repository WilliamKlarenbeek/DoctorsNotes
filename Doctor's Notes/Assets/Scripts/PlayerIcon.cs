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
    private float speed = 50.0f; 

    private void Awake()
    {
        instance = this; 
    }

    // Start is called before the first frame update
    void Start()
    {
        //player Icon is set to start level position when the game starts
        //transform.position = new Vector3(295, 568, 0);
        transform.position = _startLevel.transform.position; 
    }

    public IEnumerator Movement(Vector3 targetPos)
    {
        float step = speed * Time.deltaTime;

        while (true)
        {
            // no movement occurs on z-axis
            //transform.Translate(1 * Time.deltaTime, 1 * Time.deltaTime, 0); 
            transform.position = Vector3.MoveTowards(transform.position, targetPos, step);
            if (transform.position == targetPos)
            {
                yield return StartCoroutine(LevelSelection.levelSelectionInstance.LoadScene(LevelSelection.levelSelectionInstance.getLevelName()));
            }
            else
            {
                yield return null;
            }
        }
    }
}   
