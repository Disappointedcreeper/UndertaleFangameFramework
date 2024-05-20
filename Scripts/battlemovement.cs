using Godot;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;

public partial class battlemovement : CharacterBody2D
{
	int ButtonPos = 0;
	string behavior = "buttons";
	// Fight button pos: -134, 96
	// Act button pos: -60, 96
	// Item button pos: 23, 96
	// Mercy button pos: 96, 96
	public string[] Button = new string[4];
	[Export] public Sprite2D[] ButtonSprites = new Sprite2D[4];
	[Export] public AudioStreamPlayer2D SqueakSound;
	[Export] public AudioStreamPlayer2D SelectSound;
	public Dictionary<string, Godot.Vector2> MenuPos = new Dictionary<string, Godot.Vector2>();
	public override void _Ready()
	{
		Button[0] = "Fight";
		Button[1] = "Act";
		Button[2] = "Item";
		Button[3] = "Mercy";
		MenuPos.Add("Fight", new Godot.Vector2(-134, 96));
		MenuPos.Add("Act", new Godot.Vector2(-60, 96));
		MenuPos.Add("Item", new Godot.Vector2(23, 96));
		MenuPos.Add("Mercy", new Godot.Vector2(96, 96));
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(behavior == "buttons")
		{
			ButtonSprites[ButtonPos].Frame = 1;
			if(Input.IsActionJustPressed("MoveRight"))
			{
				SqueakSound.Play();
				ButtonSprites[ButtonPos].Frame = 0;
				ButtonPos++;
				if(ButtonPos > 3)
				{
					ButtonPos = 0;
				}
			}
			if(Input.IsActionJustPressed("MoveLeft"))
			{
				SqueakSound.Play();
				ButtonSprites[ButtonPos].Frame = 0;
				ButtonPos--;
				if(ButtonPos < 0)
				{
					ButtonPos = 3;
				}
			}
			if(Input.IsActionJustPressed("z"))
			{
				SelectSound.Play();
			}
			Position = MenuPos[Button[ButtonPos]];
		}
	}
}
