<h1><p align="center"> 
    Static Testing
</h1>

Static testing is a software technique which is used to check defects in software applications without executing the code. It is performed to avoid errors in the early stages of development as it is easier to identify and solve errors. 
It’s counterpart is dynamic testing which checks an application when the code is run.

#### Manual Testing 

**The testing is being done in the branch ‘dev’ on August 19/08 at 3:45 pm. **
The first scene upon entry is `MainMenu` and the scripts used in that scene are 





### Game Controller

Game controller is a game object that is assigned to every scene. It handles **scene transition, sound management and map selection database**. It handles scene transition using a `BlackCanvas` game object  and it provides functions for Fade In and Fade Out transition. 
**SoundManager.cs**
Sound manager class uses the `Audio listener` and `Audio Source` objects from metadata and implements functions to handle audio clips. This class is used by the game controller object. 
**MapSelection.cs**
map Selection is a scriptable object that saves the position of the map button, current game timer and a bool if the game began or not. This object can then be used to create new playable scenes in the future. 

<img src="D:\Semester 2 2021\SWE40002 - SEPB\DoctorsNotes\Snips\Game Controller - DoctorsNotes.png" style="zoom:80%;" />

