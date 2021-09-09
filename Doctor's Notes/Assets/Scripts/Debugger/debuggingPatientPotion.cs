using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class debuggingPatientpotion_ : MonoBehaviour
{
    const string PATH = @"Assets\Resources\MouseClick.csv";

    void Start()
    {
        Debugger.debuggerInstance.WriteToFileTag("patientpotion");

        StreamWriter writer = new StreamWriter(PATH, true);
        writer.WriteLine("Mouse Position");
        writer.Close();
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(WriteMousePositionToFile());
    }

    IEnumerator WriteMousePositionToFile()
    {
        StreamWriter writer = new StreamWriter(PATH, true);
        //Debugger.debuggerInstance.WriteToFile("Printing mouse position from " + _activeScene); 
        writer.WriteLine(Input.mousePosition.x + "," + Input.mousePosition.y);
        writer.Close();

        yield return new WaitForSeconds(2f);
    }
}
