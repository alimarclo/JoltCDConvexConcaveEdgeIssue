using Godot;
using System;

public partial class SledgeController : Node
{

    public float GetDirection(Player player)
    {
        return Input.GetAxis("right", "left");
    }

}
