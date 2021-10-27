using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "SymptomDatabase", menuName = "Symptom/Symptom Database")]
public class SymptomDatabase : ScriptableObject
{
    public List<Sprite> symptomImageList = new List<Sprite>();

    public Sprite getImage(int aIndex)
    {
        return symptomImageList[aIndex];
    }

    public Sprite getRandomImage()
    {
        int randIndex = Mathf.RoundToInt(Random.Range(0, symptomImageList.Count-1));

        return symptomImageList[randIndex];
    }
}
