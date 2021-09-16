using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "PatientDatabase", menuName = "Patient/Patient Database")]
public class PatientDatabase : ScriptableObject
{
    public List<Sprite> patientImageList = new List<Sprite>();

    public Sprite getImage(int aIndex)
    {
        return patientImageList[aIndex];
    }

    public Sprite getRandomImage()
    {
        int randIndex = Mathf.RoundToInt(Random.Range(0, patientImageList.Count));

        return patientImageList[randIndex];
    }
}
