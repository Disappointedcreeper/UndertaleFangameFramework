using Godot;
using System;

public partial class loopmusic : AudioStreamPlayer2D
{
    public override void _Ready()
    {
        Play();
    }
    public void finished()
	{
		Play();
	}
}
