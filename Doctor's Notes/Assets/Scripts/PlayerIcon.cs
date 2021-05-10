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

    // Update is called once per frame
    void Update()
    {
    }

    public IEnumerator Movement(Vector3 targetPos)
    {
        float step = speed * Time.deltaTime; 
        //While the playerIcon reaches the targetPos keeping moving
        //while(transform.position != targetPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, step);
            // no movement occurs on z-axis
            //transform.Translate(1 * Time.deltaTime, 1 * Time.deltaTime, 0); 
        }
        yield return new WaitForSeconds(0.3f); 
    }
}   
