using Godot;
using System;
using System.Collections.Generic;

[Tool]
public partial class CutsceneMovement : Resource
{
    [Export] public string SceneName;      //The scene of the node to move
    [Export] public string NodePlayerPath; //The path to the node
    [Export] public Vector2 TargetPos;     //The target position (position it moves to)
    [Export] public float MoveSpeed;       //The speed in which the node moves to the position
}
