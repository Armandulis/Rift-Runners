using Godot;
using System;

public partial class TimeDilationCasterComponent : BaseSpell
{
    [Export]
    private Timer cooldownTimer;

    public override bool canCastWhileMoving { get; set; } = true;

    public override bool isOnGlobalCooldown { get; set; } = true;
    public bool isOnCooldown = false;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }


    public override bool CanCast()
    {
        return !isOnCooldown;
    }

    public void _OnCooldownTimerTimeout()
    {
        isOnCooldown = false;
    }

    public override void CastSpell()
    {
        isOnCooldown = true;
        cooldownTimer.Start(timeSec: 2);
        Rpc(method: "CastTimeDilation", GetGlobalMousePosition());
    }


    [Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = true)]
    private void CastTimeDilation(Vector2 aim)
    {
        var scene = GD.Load<PackedScene>("res://Components/Spells/QuantumMage/TimeDilationCasterComponent/TimeDilation.tscn");
        TimeDilation instance = (TimeDilation)scene.Instantiate();

        instance.Position = aim;
        // instance.Scale = 5;
        // instance.aim = GlobalPosition.DirectionTo(aim);
        // instance.damage = 125;

        GetParent<Node2D>().AddChild(instance);
        instance.TopLevel = true;
    }

    public override float GetCastTime()
    {
        return 0;
    }

}
