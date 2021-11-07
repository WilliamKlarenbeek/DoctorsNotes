using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatientSymptomNode : BaseNode
{

    [Input] public int entry;
    [Output] public int exit;
    public string speakerName;
    public string dialogueLine;
    public string patientName;
    public string symptomNumber;
    public string redValue;
    public string blueValue;
    public string greenValue;
    public Sprite sprite;

    public override string GetString()
    {
        return "PatientSymptomNode/" + speakerName + "/" + dialogueLine + "/" + patientName + "/" + symptomNumber + "/" + redValue + "/" + blueValue + "/" + greenValue;
    }

    public override Sprite GetSprite()
    {
        return sprite;
    }
}
