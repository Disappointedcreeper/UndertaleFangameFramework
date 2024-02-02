using Godot;
using System;
using System.Collections.Generic;

[Tool]
public partial class CutsceneAnimation : Resource
{
    [Export] public AnimationPlayer AnimPlayer;
    [Export] public string AnimationName;
}
