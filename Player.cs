using Godot;
using System;
using System.Linq;

public partial class Player : RigidBody3D
{

    private const float ROTATION_DAMPING = 70;
    private const float ROTATION_AIR_DAMPING = 70;

    [Export]
    public NodePath SledgeController { get; set; }

    // private bool _push = true;

    private readonly Vector3 normalRotation = new Vector3((float) Math.PI/6, (float) Math.PI, 0);

    private SledgeController _sledgeController;
    private RayCast3D _floorDetectionRayCast;

    private bool _applyForce = true;

    public override void _Ready()
    {
        // GetTree().CreateTimer(7).Timeout += () => _push = false;

        _floorDetectionRayCast = GetChild<RayCast3D>(0);
        _sledgeController = GetNode<SledgeController>(SledgeController);
        GetTree().CreateTimer(4.5).Timeout += () => {
            _applyForce = false;
            GD.Print("Stopped pushing");
        };
    }

    public override void _IntegrateForces(PhysicsDirectBodyState3D state)
    {
        if(_applyForce) {
            ApplyForce(new Vector3(0.00070173637f, -148.47253f, 201.13654f));
        }
    }

}
