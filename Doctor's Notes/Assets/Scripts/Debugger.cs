/* 
 * Debugger class is a singleton class that provides functionality to write debug statements to a file. 
 * Author: Shiva Gupta[102262514] 
 * Project: Doctor's Notes 
 */ 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor; 
using System.IO; 

public class Debugger : MonoBehaviour
{
    const string PATH = @"Assets\Resources\test.txt";
    // Debugger will only have one instance so it is being implemented as singleton 
    public static Debugger debuggerInstance; 

    public void Awake()
    {
        debuggerInstance = this; 
    }

    public void WriteString()
    {

        StreamWriter writer = new StreamWriter(PATH, true); 
        writer.WriteLine("[Debugger]: StreamWriter writer initialized....");
        writer.Close();  
    }

    public void WriteToUnityConsole()
    {
        AssetDatabase.ImportAsset(PATH);
        TextAsset asset = (TextAsset)Resources.Load("test");

        Debug.Log(asset.text); 
    } 
}
