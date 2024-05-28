using Godot;
using System;
using System.Collections.Generic;

[Tool]
public partial class CutsceneResource : Resource
{
    [Export] public string CutsceneName;                    //Used so the debug code works properly
    [Export] public Resource[] Actions = new Resource[0];   //The array of actions to play for the cutscene 
}
