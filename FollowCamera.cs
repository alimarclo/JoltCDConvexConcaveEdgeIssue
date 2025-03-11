using Godot;
using System;

public partial class FollowCamera : Camera3D
{

    [Export]
    public NodePath Player { get; set; }

    private Node3D _player;

    public override void _Ready()
    {
        _player = GetNode<Node3D>(Player);
    }

    public override void _Process(double delta)
    {
        Position = _player.Position+new Vector3(0,5,-5);
    }

}
