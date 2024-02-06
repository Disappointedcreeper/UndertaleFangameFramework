using Godot;
using System;
using System.Collections.Generic;

[Tool]
public partial class CutsceneMovement : Resource
{
    [Export] public string SceneName;
    [Export] public string NodePlayerPath;
    [Export] public Vector2 TargetPos;
    [Export] public float MoveSpeed;
}
