using Godot;
using System;

public partial class QuasarCasterComponent : BaseSpell
{
    public float castTime = 1.5f;
    public override bool canCastWhileMoving { get; set; } = true;

    public override bool isOnGlobalCooldown { get; set; } = true;

    private QuantumMage quantumMage;
    private GravitonBuffComponent gravitonBuffComponent;
    private SpellCastManagerComponent spellCastManagerComponent;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        quantumMage = (QuantumMage)GetParent<QuantumMage>();
        gravitonBuffComponent = quantumMage.gravitonBuffComponent;
    }

    public override void _Process(double delta)
    {
    }

    [Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = true)]
    private void CastQuasar(Vector2 aim)
    {
        var scene = GD.Load<PackedScene>("res:///Components/BoltCasterComponent/Bolt.tscn");
        Bolt instance = (Bolt)scene.Instantiate();

        instance.Position = GetParent<Node2D>().GlobalPosition;
        instance.aim = GlobalPosition.DirectionTo(aim);
        instance.LookAt(aim);
        instance.damage = 75;

        GetParent<Node2D>().AddChild(instance);
        instance.TopLevel = true;
    }


    public override bool CanCast()
    {
        return true;
    }

    public override void CastSpell()
    {
        gravitonBuffComponent.addStacks(1);
        Rpc(method: "CastQuasar", GetGlobalMousePosition());
    }

    public override float GetCastTime()
    {
        return castTime;
    }
}
