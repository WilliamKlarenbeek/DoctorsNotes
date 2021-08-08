using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor; 
using System.IO; 

public class Debugger : MonoBehaviour
{
    private static void WriteString()
    {
        const string PATH = @"Assets\Resources\test.txt"; 

        StreamWriter writer = new StreamWriter(PATH, true); 
        writer.WriteLine("test.txt");
        writer.Close();

        AssetDatabase.ImportAsset(PATH);
        TextAsset asset = (TextAsset)Resources.Load("test");

        Debug.Log(asset.text); 
    }

    public void Awake()
    {
        WriteString(); 
    }
}
