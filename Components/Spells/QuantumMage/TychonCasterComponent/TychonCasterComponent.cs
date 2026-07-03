using Godot;
using System;

public partial class TychonCasterComponent : BaseSpell
{


    [Export]
    private Timer successionTimer;

    [Export]
    private Timer cooldownTimer;
    private QuantumMage quantumMage;
    private GravitonBuffComponent gravitonBuffComponent;
    private int stacks = 2;

    public override bool canCastWhileMoving { get; set; } = true;

    public override bool isOnGlobalCooldown { get; set; } = true;
    private Vector2 aim;

    private int successionCounter = 0;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {

        quantumMage = GetParent<QuantumMage>();
        gravitonBuffComponent = quantumMage.gravitonBuffComponent;


    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
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
        Rpc(method: "CastTychon", GetGlobalMousePosition());
    }

    public void _OnCooldownTimerTimeout()
    {
        stacks += 1;
        if (stacks < 3)
        {
            cooldownTimer.Start(2.5);
        }
    }

    [Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = true)]
    private void CastTychon(Vector2 aim)
    {
        this.aim = aim;
        successionTimer.Start(0.1f);
    }

    public override float GetCastTime()
    {
        return 0;
    }

    public void _OnSuccessionTimerTimeout()
    {
        if (successionCounter == 7)
        {
            successionCounter = 0;
            return;
        }


        var scene = GD.Load<PackedScene>("res://Components/Spells/QuantumMage/TychonCasterComponent/Tychon.tscn");
        Tychon instance = (Tychon)scene.Instantiate();


        Random _random = new Random();
        // Generate a random offset around the 'aim'

        float angle = (float)(_random.NextDouble() * 2 * Math.PI);
        float distance = (float)(_random.NextDouble() * 50);
        Vector2 randomOffset = new Vector2(
            (float)(Math.Cos(angle) * distance),
            (float)(Math.Sin(angle) * distance)
        );

        // Set the position and other properties
        instance.Position = aim + randomOffset;

        // instance.aim = GlobalPosition.DirectionTo(aim);
        instance.damage = 10;

        GetParent<Node2D>().AddChild(instance);
        instance.TopLevel = true;


        successionCounter++;
        successionTimer.Start(0.1f);
        GD.Print(successionCounter);
    }
}
