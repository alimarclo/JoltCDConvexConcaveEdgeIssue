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

    public override void _Ready()
    {
        // GetTree().CreateTimer(7).Timeout += () => _push = false;

        _floorDetectionRayCast = GetChild<RayCast3D>(0);
        _sledgeController = GetNode<SledgeController>(SledgeController);
    }

    public override void _IntegrateForces(PhysicsDirectBodyState3D state)
    {
        // if(_push) {
        //     var delta = state.Step;
        //     ApplyForce(new Vector3(0, 0, -20000).Rotated(new Vector3(1,0,0), (float) (-36f*Math.PI/180f))*delta);
        //     GD.Print(new Vector3(0, 0, -20000).Rotated(new Vector3(1,0,0), (float) (36f*Math.PI/180f)));
        // }

        // Linear: (0.00070173637, -148.47253, 201.13654)Angular: (-7.8600826, -0.0020768414, -0.0015443421)
        ApplyForce(new Vector3(0.00070173637f, -148.47253f, 201.13654f));
        // ApplyTorque(new Vector3(-7.8600826f, -0.0020768414f, -0.0015443421f));


        // var delta = state.Step;
        // var direction = _sledgeController.GetDirection(this);
        // float correctRotationY;
        // if(Rotation.Y > 0) {
        //     correctRotationY = normalRotation.Y-Rotation.Y-AngularVelocity.Y;
        // } else {
        //     correctRotationY = -normalRotation.Y-Rotation.Y-AngularVelocity.Y;
        // }

        // float correctRotationX;
        // if(Rotation.X > 0) {
        //     correctRotationX = normalRotation.X+Rotation.X-AngularVelocity.X;
        // } else {
        //     correctRotationX = normalRotation.X+Rotation.X-AngularVelocity.X;
        // }

        // float correctRotationZ;
        // if(Rotation.Z > 0) {
        //     correctRotationZ = normalRotation.Z+Rotation.Z-AngularVelocity.Z;
        // } else {
        //     correctRotationZ = normalRotation.Z+Rotation.Z-AngularVelocity.Z;
        // }

        // var force = new Vector3();
        // var torque = new Vector3();

        // force.X = direction*150;

        // torque.Y = direction*100;

        // // GD.Print("Rotation: " + Rotation.X);
        // // GD.Print("Correct: " + correctRotationX);
        // // GD.Print(Rotation);

        // //bool facingForward = Math.Abs(Rotation.X)-_config.Slope < Math.PI/8 && Math.Abs(Rotation.Z) < Math.PI/8;

        // if(_floorDetectionRayCast.IsColliding()) {
        //     torque.X += correctRotationX*ROTATION_DAMPING;
        //     torque.Y += correctRotationY*ROTATION_DAMPING;
        //     torque.Z += correctRotationZ*ROTATION_DAMPING;

        //     // if(facingForward) {
        //     //     force.Z = 500;
        //     // }
        //     // force.Z = 500;
        //     var vx = (float) (-Math.Sin(Rotation.Y)*Math.Cos(Rotation.X));
		//     var vy = (float) (Math.Sin(Rotation.X));
		//     var vz = (float) (-Math.Cos(Rotation.Y)*Math.Cos(Rotation.X));
        //     force.X += 250*vx;
        //     force.Y += 250*vy;
        //     force.Z += 250*vz;
        // } else {
        //     torque.X += correctRotationX*ROTATION_AIR_DAMPING;
        //     torque.Y += correctRotationY*ROTATION_AIR_DAMPING;
        //     torque.Z += correctRotationZ*ROTATION_AIR_DAMPING;

        //     force.Y = (float) Math.Min(3.5, 100);

        //     // if(facingForward) {
        //     //     force.Z = 200;
        //     // }
        //     force.Z = 1;
        // }

        // //force.Z = 500;

        // ApplyForce(force);
        // ApplyTorque(torque);
        // GD.Print("Linear: ", force, "Angular: ", torque);
    }

}
