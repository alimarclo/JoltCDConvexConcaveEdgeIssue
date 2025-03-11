using Godot;
using System;
using System.Linq;

public partial class Player : RigidBody3D
{

    private bool _applyForce = true;

    public override void _Ready()
    {
        GetTree().CreateTimer(4.5).Timeout += () => {
            _applyForce = false;
            GD.Print("Stopped pushing");
        };
    }

    public override void _IntegrateForces(PhysicsDirectBodyState3D state)
    {
        if(_applyForce) {
            ApplyForce(new Vector3(0, -15, 20));
        }
    }

}
