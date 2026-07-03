using Godot;
using System;
using System.Reflection.Metadata.Ecma335;

public partial class SingularityCasterComponent : BaseSpell
{
    public float castTime = 3;
    private QuantumMage quantumMage;
    private GravitonBuffComponent gravitonBuffComponent;

    public override bool canCastWhileMoving { get; set; } = false;

    public override bool isOnGlobalCooldown { get; set; } = true;


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        quantumMage = (QuantumMage)GetParent<QuantumMage>();
        gravitonBuffComponent = quantumMage.gravitonBuffComponent;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }

    [Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = true)]
    private void CastSingularity(Vector2 aim)
    {

        var scene = GD.Load<PackedScene>("res:///Components/BoltCasterComponent/Bolt.tscn");
        Bolt instance = (Bolt)scene.Instantiate();

        instance.Position = GetParent<Node2D>().GlobalPosition;
        instance.aim = GlobalPosition.DirectionTo(aim);
        instance.damage = 175;

        GetParent<Node2D>().AddChild(instance);
        instance.TopLevel = true;
    }

    public override bool CanCast()
    {
        return true;
    }

    public override void CastSpell()
    {
        gravitonBuffComponent.trySpendOn(2);
        gravitonBuffComponent.addStacks(0.5f);
        Rpc(method: "CastSingularity", GetGlobalMousePosition());
    }

    public override float GetCastTime()
    {
        if (gravitonBuffComponent.CanSpenOn(2))
        {
            return 0;
        }

        return 3;
    }
}
