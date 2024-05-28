using Godot;
using System;
using System.Collections;
using System.Threading.Tasks;

public partial class mainChara : Node2D
{
	[Export] public Camera2D Camera;             //Reference to the camera node
	[Export] public int CameraLimitLeft = -320;  //Sets the CameraLimit in the camera node
	[Export] public int CameraLimitTop = -240;   //Sets the CameraLimit in the camera node
	[Export] public int CameraLimitRight = 320;  //Sets the CameraLimit in the camera node
	[Export] public int CameraLimitBottom = 240; //Sets the CameraLimit in the camera node
	[Export] public PackedScene TextBox;       //Reference to the textbox scene
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
		GetNode("mainChara").Set("Enabled", false);
		for(int i = 0; i < Resource.Actions.Length; i++)
		{
			if(Resource.Actions[i] is DialogueResource)
			{
				DialogueResource Dialoge = Resource.Actions[i] as DialogueResource;
				await CreateTextBox(Dialoge);
			}
			else if(Resource.Actions[i] is CutsceneAnimation)
			{
				CutsceneAnimation Animation = Resource.Actions[i] as CutsceneAnimation;
				PlayAnimation(Animation);
			}
			else if(Resource.Actions[i] is CutsceneMovement)
			{
				CutsceneMovement Movement = Resource.Actions[i] as CutsceneMovement;
				await Move(Movement);
			}
			else
			{
				GD.PrintErr("Error " + Resource.CutsceneName + " at index " + i + " is of type " + Resource.Actions[i].GetType() + " which is invalid.");
			}
		}
		GetNode("mainChara").Set("Enabled", true);
		return;
	}
	public async Task CreateTextBox(DialogueResource action)
	{
		Node2D MainChara = (Node2D)GetNode("mainChara");
		string TrueDialogue = action.dialogue.Replace("\\n", "\n");
		Vector2 CameraTransform = Camera.GetViewportTransform().Y;
		Node2D Object = (Node2D)TextBox.Instantiate();
		if(MainChara.GlobalPosition.Y - CameraTransform.Y > 0)
		{
			Object.Translate(new Vector2(0, -311));
		}
		Object.Set("WhatToSay", TrueDialogue);
		Object.Set("anim", action.anim);
		Object.Set("idleAnim", action.idleAnim);
    	Object.Set("TextSound", action.TextSound);
        Object.Set("Font", action.Font);
	    Object.Name = "TextBox";
    	GetParent().AddChild(Object);
        
    	while (GetParent().GetNodeOrNull("TextBox") != null)
    	{
        	await ToSignal(GetTree().CreateTimer(0.001), "timeout");
        }
		return;
    }

	public IEnumerator PlayAnimation(CutsceneAnimation action) //Runs a CutsceneAnimation
	{
		AnimationPlayer AnimPlayer = (AnimationPlayer)GetNode("/root").GetNode(action.SceneName).GetNode(action.AnimPlayerPath);
		AnimPlayer.Play(action.AnimationName);
		return null;
	}
	public async Task Move(CutsceneMovement action)
	{
		Node2D Node = (Node2D)GetNode("/root").GetNode(action.SceneName).GetNode(action.NodePlayerPath);
		Vector2 direction = (action.TargetPos - Node.Position).Normalized();
		float distance = (action.TargetPos - Node.Position).Length();
		while(Node.Position.DistanceTo(action.TargetPos) > 1f)
		{
			Node.Position += direction * action.MoveSpeed;
			await ToSignal(GetTree().CreateTimer(0.001), "timeout");
		}
		Node.Position = action.TargetPos;
		return;
	}
    
}