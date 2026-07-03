using Godot;
using System;

public partial class QuantumFluxCasterComponent : BaseSpell
{
    [Export]
    private Timer cooldownTimer;
    private QuantumMage quantumMage;
    private GravitonBuffComponent gravitonBuffComponent;
    private int stacks = 3;


    public override bool canCastWhileMoving { get; set; } = true;

    public override bool isOnGlobalCooldown { get; set; } = false;

    public override void _Ready()
    {
        quantumMage = GetParent<QuantumMage>();
        gravitonBuffComponent = quantumMage.gravitonBuffComponent;
    }

    public override void _Process(double delta)
    {
    }

    public override bool CanCast()
    {
        if (stacks == 0)
        {
            return false;
        }

        return true;
    }

    public override void CastSpell()
    {
        gravitonBuffComponent.addStacks(1);
        stacks -= 1;
        cooldownTimer.Start(2.5);
        Rpc(method: "CastQuantumFlux", GetGlobalMousePosition());
    }

    public void OnCooldownTimerTimeout()
    {
        stacks += 1;
        if (stacks < 3)
        {
            cooldownTimer.Start(2.5);
        }
    }

    [Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = true)]
    private void CastQuantumFlux(Vector2 aim)
    {
        var scene = GD.Load<PackedScene>("res://Components/Spells/QuantumMage/QuantumFluxCasterComponent/QuantumFlux.tscn");
        QuantumFlux instance = (QuantumFlux)scene.Instantiate();

        instance.Position = aim;
        instance.aim = GlobalPosition.DirectionTo(aim);
        instance.damage = 125;

        GetParent<Node2D>().AddChild(instance);
        instance.TopLevel = true;
    }

    public override float GetCastTime()
    {
        return 0;
    }
}
