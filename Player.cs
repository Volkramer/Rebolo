using Godot;

public class Player : Area2D
{
    [Export]
    public int Speed = 400; //How fast the player will move (pixel/sec).

    public Vector2 ScreenSize; //Size of the game windows.

    public override void _Ready()
    {
        ScreenSize = GetViewportRect().Size;
    }
}
