using Godot;
using System;

public partial class CutsceneObject : Area2D
{
	[Export] public CutsceneResource cutsceneObject { get; set; }
}
