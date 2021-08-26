/* 
 * Debugger class is a singleton class that provides functionality to write debug statements to a file. 
 * Author: Shiva Gupta[102262514] 
 * Project: Doctor's Notes 
 */ 
using UnityEngine;
using UnityEditor; 
using System.IO; 

public class Debugger : MonoBehaviour
{
    const string PATH = @"Assets\Resources\Log.txt";
    // Debugger will only have one instance so it is being implemented as singleton 
    public static Debugger debuggerInstance; 

    public void Awake()
    {
        debuggerInstance = this;
    }

    // in keyword is used here to provide readonly access to the passed in variables 
    public void WriteToFileTag(in string tag)
    {
        StreamWriter writer = new StreamWriter(PATH, true);
        writer.WriteLine("[" + tag.ToUpper() + "]");
        writer.Close();
    }

    public void WriteToFile(in string s)
    {
        StreamWriter writer = new StreamWriter(PATH, true); 
        writer.WriteLine(s);
        writer.Close(); 
    }


    public void WriteToFile(in Vector3 vector3)
    {
        StreamWriter writer = new StreamWriter(PATH, true); 
        writer.WriteLine("(x, y, z): " + "(" + vector3.x + ", " + vector3.y + ", " + vector3.z + ")"); 
    }

    // Read the file and log it in the Unity Console
    public void ReadFile()
    {
        StreamReader reader = new StreamReader(PATH);
        
        
        
        (reader.ReadToEnd());
        reader.Close(); 
    }

    public void ClearAll()
    {
        AssetDatabase.DeleteAsset(PATH); 
    }

    public void DebugInfoToFile()
    {
        WriteToFileTag("Debugger");
        WriteToFile(PATH);
        WriteToFile("StreamWriter writer initialized....");
    }
}
