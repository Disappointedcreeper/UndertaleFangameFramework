using Godot;
using System;

public partial class TextHandler : Node2D
{
	double MaxTimer = 0.1;
	double CurrentTimer = 0;
	public string anim;
	public string idleAnim;
	[Export] public AnimationPlayer AnimPlayer;
	[Export] public Sprite2D FaceSpriteDisplay;
	[Export] public Sprite2D BodySpriteDisplay;
	[Export] public AudioStream TextSound;
	[Export] public AudioStreamPlayer2D TextSoundPlayer;
	[Export] public Font Font;
	[Export] public RichTextLabel TextDisplay;
	[Export] public string WhatToSay = "* NULL ERROR   \n* NO TEXT PROVIDED";
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		AnimPlayer.Play(anim);
		TextDisplay.Text = WhatToSay;
		TextSoundPlayer.Stream = TextSound;
		TextDisplay.AddThemeFontOverride("normal_font", Font);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(TextDisplay.VisibleCharacters >= WhatToSay.Length - 1 && idleAnim != "")
		{
			AnimPlayer.Play(idleAnim);
			idleAnim = "";
		}
		if (Input.IsActionJustPressed("z") && TextDisplay.VisibleCharacters >= WhatToSay.Length - 1)
		{
			QueueFree();
		}
		if (Input.IsActionJustPressed("x"))
		{
			TextDisplay.VisibleCharacters = WhatToSay.Length;
		}
		if(CurrentTimer <= 0 && TextDisplay.VisibleCharacters <= WhatToSay.Length - 1)
		{
			
			TextDisplay.VisibleCharacters++;
			if(WhatToSay[TextDisplay.VisibleCharacters - 1] == '.' || WhatToSay[TextDisplay.VisibleCharacters - 1] == ',' || WhatToSay[TextDisplay.VisibleCharacters - 1] == '!' || WhatToSay[TextDisplay.VisibleCharacters - 1] == '?')
			{
				CurrentTimer = MaxTimer;
			}
			if(char.IsLetter(WhatToSay[TextDisplay.VisibleCharacters - 1]))
			{
				TextSoundPlayer.Play();
			}
		}
		CurrentTimer -= delta;
	}
}
