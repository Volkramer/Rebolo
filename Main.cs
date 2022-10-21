using Godot;

public class Main : Node
{
    [Export]
    public PackedScene MobScene;

    public int Score;

    public override void _Ready()
    {
        GD.Randomize();
    }

    public void GameOver()
    {
        GetNode<Timer>("MobTimer").Stop();
        GetNode<Timer>("ScoreTimer").Stop();
        GetNode<HUD>("HUD").ShowGameOver();
    }

    public void NewGame()
    {
        Score = 0;
        var hud = GetNode<HUD>("HUD");
        var player = GetNode<Player>("Player");
        var startPosition = GetNode<Position2D>("StartPosition");

        hud.UpdateScore(Score);
        hud.ShowMessage("Get Ready!");
        player.Start(startPosition.Position);
        GetNode<Timer>("StartTimer").Start();
    }

    public void OnScoreTimerTimeout()
    {
        Score++;
        GetNode<HUD>("HUD").UpdateScore(Score);
    }

    public void OnStartTimerTimeout()
    {
        GetNode<Timer>("MobTimer").Start();
        GetNode<Timer>("ScoreTimer").Start();
    }

    public void OnMobTimerTimeout()
    {

        // Create a new instance of the Mob scene.
        var mob = (Mob)MobScene.Instance();

        // Choose a random location on Path2D.
        var mobSpawnLocation = GetNode<PathFollow2D>("MobPath/MobSpawnLocation");
        mobSpawnLocation.Offset = GD.Randi();

        // Set the mob's direction perpendicular to the path direction.
        float direction = mobSpawnLocation.Rotation + Mathf.Pi / 2;

        // Set the mob's position to a random location.
        mob.Position = mobSpawnLocation.Position;

        // Add some randomness to the direction.
        direction += (float)GD.RandRange(-Mathf.Pi / 4, Mathf.Pi / 4);
        mob.Rotation = direction;

        // Choose the velocity.
        var velocity = new Vector2((float)GD.RandRange(150.0, 250.0), 0);
        mob.LinearVelocity = velocity.Rotated(direction);

        // Spawn the mob by adding it to the Main scene.
        AddChild(mob);
    }
}
