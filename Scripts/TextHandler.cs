using Godot;
using System;

public partial class TextHandler : Node2D
{
	int FaceSpriteChange = 2;
	double MaxTimer = 0.1;
	double CurrentTimer = 0;
	[Export] public Texture2D FaceSprite;
	public bool IsFaceSprite2 = false;
	[Export] public Texture2D FaceSprite2;
	[Export] public Sprite2D FaceSpriteDisplay;
	[Export] public AudioStream TextSound;
	[Export] public AudioStreamPlayer2D TextSoundPlayer;
	[Export] public Font Font;
	[Export] public RichTextLabel TextDisplay;
	[Export] public string WhatToSay = "* NULL ERROR   \n* NO TEXT PROVIDED";
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		TextDisplay.Text = WhatToSay;
		TextSoundPlayer.Stream = TextSound;
		TextDisplay.AddThemeFontOverride("normal_font", Font);
		FaceSpriteDisplay.Texture = FaceSprite;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
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
			if(FaceSpriteChange == 0)
			{
				FaceSpriteDisplay.Texture = FaceSprite;
				if(!IsFaceSprite2 && FaceSprite2 != null)
				{
					FaceSpriteDisplay.Texture = FaceSprite2;
				}
				FaceSpriteChange = 3;
				IsFaceSprite2 = !IsFaceSprite2;
			}
			
			TextDisplay.VisibleCharacters++;
			if(WhatToSay[TextDisplay.VisibleCharacters - 1] == '.' || WhatToSay[TextDisplay.VisibleCharacters - 1] == ',' || WhatToSay[TextDisplay.VisibleCharacters - 1] == '!' || WhatToSay[TextDisplay.VisibleCharacters - 1] == '?')
			{
				CurrentTimer = MaxTimer;
			}
			if(char.IsLetter(WhatToSay[TextDisplay.VisibleCharacters - 1]))
			{
				TextSoundPlayer.Play();
			}
			FaceSpriteChange --;
		}
		CurrentTimer -= delta;
		if(TextDisplay.VisibleCharacters >= WhatToSay.Length && FaceSprite2 != null)
		{
			FaceSpriteDisplay.Texture = FaceSprite2;
		}
	}
}
