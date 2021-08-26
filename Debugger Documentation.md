<h1>
 <p align="center">Debugger</p>
</h1>
A class that provides functionality to write debug statements to a file and read them in Unity console. 

To use the Debugger we need to create a game object and add the debugger script component to it.

| Functions                       | Usage                                                        |
| :------------------------------ | ------------------------------------------------------------ |
| `WriteToFileTag(in string tag)` | Pass in a tag to print to the file so that the debug statements after the tag can be categorized. |
| `WriteToFile(in string s)`      | Pass in the string to print to the file.                     |
| `WriteToFile(in Vector3 v)`     | Pass in the `Vector3` variable to write to the file          |
| `ReadFile()`                    | Reads the file and prints it to the unity log.               |
| `ClearAll()`                    | Clears the contents of the file by deleting the file from `AssesDatabase`. |
| `DebugInfoToFile()`             | Prints debug info like the file path to the file.            |



### Example 

```C#
    private void Start()
    {
        // debugger called
        Debugger.debuggerInstance.ClearAll();
        Debugger.debuggerInstance.DebugInfoToFile(); 
        Debugger.debuggerInstance.WriteToFileTag("levelSelection"); 
        Debugger.debuggerInstance.ReadFile(); 
    }
```



### Example Output

<a href="https://imgbb.com/"><img src="https://i.ibb.co/Qf3Hd7f/Log-File-Example.png" alt="Log-File-Example" border="0" ></a>

