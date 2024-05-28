using Godot;
using System;
using System.Collections;

public partial class CharaController : CharacterBody2D
{
	public bool Enabled = true;                   //True if you can move, false if you can't
	[Export] public float MoveSpeed = 5f;         //How fast you move
	[Export] public AnimationPlayer animPlayer;   //The animation player that plays the player character's animations
	[Export] public RayCast2D Interaction;        //The raycast that interacts with objects around you
	[Export] public Node2D MyScene;               //The scene that this is derived from
	[Export] public mainChara MySceneScript;      //The script from the scene this is derived from
	public string Dir = "down";                   //The direction the player is facing

	public override void _Process(double delta)
	{
		//Interact with the object in front of the player.
		//Probably should change this from raycast to something else but that's a problem for futue me
		if (Input.IsActionJustPressed("z") && Enabled == true)
    	{
			Node collidingObject = Interaction.GetCollider() as Node;
			if(collidingObject != null && (CutsceneResource)collidingObject.Get("cutsceneObject") != null)
			{
				MySceneScript.RunCutscene((CutsceneResource)collidingObject.Get("cutsceneObject"));  //Run the cutscene
			}
			//For some reason the code commented out causes a null reference error
			/*else if((CutsceneResource)collidingObject.Get("cutsceneObject") == null)
			{
				GD.PrintErr("Error: \"" + collidingObject.Name + "\" is a cutscene object, but no cutscene is provided.");
			}*/
			
		}
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;  //Make temporary velocity variable
		//Movement imput
		if (Input.IsActionPressed("MoveRight") && Enabled == true) 
    	{
			Interaction.TargetPosition = new Vector2(15, 0);  //Change the position the interaction ray is pointing
			Dir = "right";                                    //Change the player's direction
        	velocity.X = MoveSpeed;                           //Set the player's movement
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
	public void LoadScene(Godot.Vector2 Pos) //Unfinished
	{
		GD.Print(Pos);	
	}
}
