using Godot;
using System;
using System.Collections.Generic;

[Tool]
public partial class CutsceneAnimation : Resource
{
    [Export] public string SceneName;        //The name of the scene that the animation player is in
    [Export] public string AnimPlayerPath;   //The path to the animation player
    [Export] public string AnimationName;    //The name of the animation to play
}
