using Godot;
using System;

[Tool]
public partial class DialogueResource : Resource
{
    [Export] public string dialogue = "";   //The dialogue to print
    [Export] public string anim = "";       //The animation the textbox should use (used for facesprites and when it has no facesprite)
    [Export] public string idleAnim = "";   //The animation the textbox should use when the character is not speaking
    [Export] public AudioStream TextSound = (AudioStream)ResourceLoader.Load("res://SFX/SND_TXT1.wav");  //The talk sound to use
    [Export] public Font Font = (Font)ResourceLoader.Load("res://Fonts/DeterminationMono.ttf");          //The font to be used
}
