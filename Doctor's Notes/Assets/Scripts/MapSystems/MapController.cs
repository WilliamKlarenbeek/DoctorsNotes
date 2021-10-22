using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    [SerializeField] Town[] towns;
    [SerializeField] PlayerMapIcon playerMapIcon;
    private string townsTag = "Town";

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            var selection = hit.transform;
            if (selection.CompareTag(townsTag) && Input.GetMouseButton(0))
            {
                Town selectedTown = hit.transform.gameObject.GetComponent<Town>();
                MovePlayerIcon(selectedTown);
                //InvokeRepeating("MovePlayerIcon(selectedTown)", 1.0f, 1.0f);
                //var selection = hit.transform;           
                //playerMapIcon.transform.localPosition = hit.collider.gameObject.GetComponent<Town>().GetTownLocation();
                //playerMapIcon.transform.localPosition = Vector3.MoveTowards(playerMapIcon.transform.localPosition, selectedTown.gameObject.GetComponent<Town>().GetTownLocation(), Time.deltaTime * playerMapIcon.GetPlayerSpeed());
            }
        }
    }

    void MovePlayerIcon(Town selectedTown)
    {
        //playerMapIcon.playerMoving = true;
        playerMapIcon.transform.localPosition = Vector3.MoveTowards(playerMapIcon.transform.localPosition, selectedTown.gameObject.transform.localPosition, Time.deltaTime * playerMapIcon.GetPlayerSpeed());
    }
}
