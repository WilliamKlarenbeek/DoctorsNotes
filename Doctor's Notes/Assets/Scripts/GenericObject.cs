using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GenericObject : MonoBehaviour
{
    public GameObject itemPrefab;
    public Sprite itemIcon;
    public Sprite itemIconHighlight;
    public string prefabPath;
    public AudioClip grabSound;
    public AudioClip releaseSound;
    public GameObject Controller;

    private Vector3 mOffset;
    private float mZCoord;
    private Ray ray;
    

    protected BookScript Book;
    protected bool collidingWithPatient;
    protected SoundManager sndManager;
    protected Vector3 prevValidPosition;
    protected RaycastHit hit;
    protected bool isGrabbed = false;

    public virtual void Start()
    {
        if (Controller == null)
        {
            Controller = GameObject.Find("Controller");
        }
        if (Controller != null)
        {
            if (Controller.GetComponent<SoundManager>() != null)
            {
                sndManager = Controller.GetComponent<SoundManager>();
            }
        }

        Book = GameObject.Find("Book_UI").GetComponent<BookScript>();
    }

    public Sprite GetItemIcon()
    {
        return itemIcon;
    }

    void OnMouseDown()
    {
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;

        //Store offset = gameobject world pos - mouse world pos
        mOffset = gameObject.transform.position - GetMouseWorldPos();
    }

    Vector3 GetMouseWorldPos()
    {
        //Pixel coordinates (x,y)
        Vector3 mousePoint = Input.mousePosition;

        //Z coordinate of game object on screen
        mousePoint.z = mZCoord;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    void OnMouseDrag()
    {
        if(isGrabbed == false)
        {
            PlayObjectSound(grabSound);
            isGrabbed = true;
            prevValidPosition = transform.position;
        }
        transform.position = GetMouseWorldPos() + mOffset;
        transform.position = new Vector3(transform.position.x, 1.25f, transform.position.z);

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    }

    public void OnMouseUp()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 10) == false && collidingWithPatient == false)
        {
            Book.IncreaseQuantity(prefabPath);

            Destroy(gameObject);
        }
        else
        {
            PlayObjectSound(releaseSound);
            isGrabbed = false;
        }
    }

    public void SetPrefabPath(InventoryItem aSlot)
    {
        if(aSlot.prefabPath != null && Resources.Load(aSlot.prefabPath) as GameObject != null)
        {
            prefabPath = aSlot.prefabPath;
        }
    }

    protected void CollidingWithPatient(bool aFlag)
    {
        collidingWithPatient = aFlag;
    }

    public void PlayObjectSound(AudioClip aSound)
    {
        if (sndManager != null)
        {
            sndManager.PlaySound(aSound);
        }
        else
        {
            Debug.Log("Sound Manager Does Not Exist!");
        }
    }
}
