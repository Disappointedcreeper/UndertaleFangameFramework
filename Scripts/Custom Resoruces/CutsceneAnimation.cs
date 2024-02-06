using Godot;
using System;
using System.Collections.Generic;

[Tool]
public partial class CutsceneAnimation : Resource
{
    [Export] public string SceneName;
    [Export] public string AnimPlayerPath;
    [Export] public string AnimationName;
}
