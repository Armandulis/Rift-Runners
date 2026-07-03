using Godot;
using System;

public partial class Tychon : Node2D
{

    public Vector2 aim;
    public float damage = 100;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Hitbox hitbox = (Hitbox)FindChild("Hitbox");

        SpellMetadata spellMetadata = new SpellMetadata();
        spellMetadata.spellId = "Tychon";
        spellMetadata.casterId = GetMultiplayerAuthority().ToString();

        spellMetadata.isCrit = IsCrit();
        spellMetadata.value = spellMetadata.isCrit ? damage * 1.5f : damage;
        spellMetadata.isDot = false;

        hitbox.spellMetadata = spellMetadata;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }

    public bool IsCrit()
    {
        Random random = new Random();
        int chance = random.Next(1, 100);
        if (chance > 50)
        {
            return true;
        }

        return false;
    }
}
