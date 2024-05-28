using Godot;
using System;

public partial class CutsceneObject : Area2D
{
	[Export] public CutsceneResource cutsceneObject { get; set; } //For an object in the world to have a cutscene
}
