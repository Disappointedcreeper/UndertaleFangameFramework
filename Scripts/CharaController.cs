using Godot;
using System;
using System.Collections;

public partial class CharaController : CharacterBody2D
{
	public bool Enabled = true;
	[Export] public float MoveSpeed = 5f;
	[Export] public AnimationPlayer animPlayer;
	[Export] public RayCast2D Interaction;
	[Export] public PackedScene SpeechBox;
	[Export] public Node2D MyScene;
	[Export] public mainChara MySceneScript;
	private Node SpeechBoxInstance;

	public string Dir = "down";
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
		//Interact with the object in front of the player.
		if (Input.IsActionJustPressed("z") && Enabled == true)
    	{
			Node collidingObject = Interaction.GetCollider() as Node;
			if(true)
			{
				MySceneScript.RunCutscene((CutsceneResource)collidingObject.Get("cutsceneObject"));
			}
			
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		//Make temporary velocity variable
		Vector2 velocity = Velocity;
		//Handle movement imput
		if (Input.IsActionPressed("MoveRight") && Enabled == true)
    	{
			Interaction.TargetPosition = new Vector2(15, 0);
			Dir = "right";
        	velocity.X = MoveSpeed;
    	}
		else if(Input.IsActionPressed("MoveLeft") && Enabled == true)
		{
			Interaction.TargetPosition = new Vector2(-15, 0);
			Dir = "left";
			velocity.X = -MoveSpeed;
		}
		else
		{
			velocity.X = 0;
		}
		if (Input.IsActionPressed("MoveUp") && Input.IsActionPressed("MoveDown") && Velocity.Y == 0 && Enabled == true) //murder dance
		{
			Dir = "down";
			velocity.Y = MoveSpeed;
		}
		else if (Input.IsActionPressed("MoveUp") && Enabled == true)
    	{
			Interaction.TargetPosition = new Vector2(0, -10);
			Dir = "up";
        	velocity.Y = -MoveSpeed;
    	}
		else if(Input.IsActionPressed("MoveDown") && Enabled == true)
		{
			Interaction.TargetPosition = new Vector2(0, 10);
			Dir = "down";
			velocity.Y = MoveSpeed;
		}
		else
		{
			velocity.Y = 0;
		}
		//Play the correct animation
		if(Enabled == true && Velocity.X != 0 || Enabled == true && Velocity.Y != 0)
		{
			animPlayer.Play("walk" + Dir);
		}
		else
		{
			animPlayer.Play("idle" + Dir);
		}
		//Set the characterbody's Velocity to the temporary velocity variable
		Velocity = velocity;
		//Move the characterbody
		if (Enabled == true)
		{
			MoveAndSlide();
		}
	}
	public void LoadScene(Godot.Vector2 Pos)
	{
		GD.Print(Pos);	
	}
}
