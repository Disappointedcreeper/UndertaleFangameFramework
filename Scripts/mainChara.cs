using Godot;
using System;
using System.Collections;
using System.Threading.Tasks;

public partial class mainChara : Node2D
{
	[Export] public Camera2D Camera;
	[Export] public int CameraLimitLeft = -152;
	[Export] public int CameraLimitTop = -114;
	[Export] public int CameraLimitRight = 152;
	[Export] public int CameraLimitBottom = 114;
	[Export] public PackedScene SpeechBox;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Camera.LimitLeft = CameraLimitLeft;
		Camera.LimitTop = CameraLimitTop;
		Camera.LimitRight = CameraLimitRight;
		Camera.LimitBottom = CameraLimitBottom;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}

	public async void RunCutscene(CutsceneResource Resource)
	{
		for(int i = 0; i < Resource.Actions.Length; i++)
		{
			if(Resource.Actions[i] is DialogueResource)
			{
				DialogueResource Dialoge = Resource.Actions[i] as DialogueResource;
				await TextBox(Dialoge);
			}
			else if(Resource.Actions[i] is CutsceneAnimation)
			{
				CutsceneAnimation Animation = Resource.Actions[i] as CutsceneAnimation;
				PlayAnimation(Animation);
			}
			else
			{
				GD.PrintErr("Error " + Resource.CutsceneName + " at index " + i + " is of type " + Resource.Actions[i].GetType() + " which is invalid.");
			}
		}
		return;
	}
	public async Task TextBox(DialogueResource action)
	{
		string TrueDialogue = action.dialogue.Replace("\\n", "\n");
		GD.Print(TrueDialogue);
        Node2D Object = (Node2D)SpeechBox.Instantiate();
	    Object.Set("WhatToSay", TrueDialogue);
        Object.Set("FaceSprite", action.FaceSprite);
	    Object.Set("FaceSprite2", action.FaceSprite2);
    	Object.Set("TextSound", action.TextSound);
        Object.Set("Font", action.Font);
	    Object.Name = "SpeechBox";
        AddChild(Object);
    	while (GetNodeOrNull("SpeechBox") != null)
    	{
        	await ToSignal(GetTree().CreateTimer(0.001), "timeout");
        }
		return;
    }

	public IEnumerator PlayAnimation(CutsceneAnimation action)
	{
		action.AnimPlayer.Play(action.AnimationName);
		return null;
	}
    
}