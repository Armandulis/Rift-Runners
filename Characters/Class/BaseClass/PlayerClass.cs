using Godot;
using System;
using System.Collections.Generic;

public partial class PlayerClass : Node2D
{
    protected Dictionary<string, BaseSpell> actionSpells = new Dictionary<string, BaseSpell>();

    public override void _Ready()
    {
    }

    public override void _Process(double delta)
    {
    }

    public BaseSpell GetSpellForAction(string actionName)
    {
        if (actionSpells.TryGetValue(actionName, out BaseSpell spell))
        {
            return spell;
        }
        throw new KeyNotFoundException($"No spell assigned for action: {actionName}");
    }

    public float GetGlobalCooldown()
    {
        return 0.5f;
    }
}
