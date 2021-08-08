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

    // constructor
    public Debugger()
    {
        WriteToFile("[Debugger]: PATH: " + PATH); 
    }

    public void Awake()
    {
        debuggerInstance = this; 
    }

    public void WriteToFile(string s)
    {
        StreamWriter writer = new StreamWriter(PATH, true);
        writer.WriteLine(s);
        writer.Close(); 
    }

    public void WriteString()
    {
        StreamWriter writer = new StreamWriter(PATH, true); 
        writer.WriteLine("[Debugger]: StreamWriter writer initialized....");
        writer.Close();  
    }

    // Read the file and log it in the Unity Console
    public void ReadFile()
    {
        StreamReader reader = new StreamReader(PATH);
        Debug.Log(reader.ReadToEnd());
        reader.Close(); 
    }
}
