using Godot;
using System;

[Tool]
public partial class DialogueResource : Resource
{
    [Export] public string dialogue = "";
    [Export] public Texture2D FaceSprite;
    [Export] public Texture2D FaceSprite2;
    [Export] public AudioStream TextSound = (AudioStream)ResourceLoader.Load("res://SFX/SND_TXT1.wav");
    [Export] public Font Font = (Font)ResourceLoader.Load("res://Fonts/DeterminationMono.ttf");
}
