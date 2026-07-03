using Godot;
using System;

public partial class TimeDilation : Node2D
{
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Hitbox hitbox = (Hitbox)FindChild("Hitbox");

        SpellMetadata spellMetadata = new SpellMetadata();
        spellMetadata.spellId = "QuantumFlux";
        spellMetadata.casterId = GetMultiplayerAuthority().ToString();

        // spellMetadata.isCrit = IsCrit();
        // spellMetadata.value = spellMetadata.isCrit ? damage * 1.5f : damage;
        // spellMetadata.isDot = false;

        // hitbox.spellMetadata = spellMetadata;
        // LookAt(aim);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
}
