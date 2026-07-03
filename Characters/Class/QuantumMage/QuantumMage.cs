using Godot;
using System;

public partial class QuantumMage : PlayerClass
{
    [Export]
    public GravitonBuffComponent gravitonBuffComponent;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        actionSpells["Spell 1"] = LoadSpell("res://Components/Spells/QuantumMage/QuasarCasterComponent/QuasarCasterComponent.tscn");
        actionSpells["Spell 2"] = LoadSpell("res://Components/Spells/QuantumMage/QuantumFluxCasterComponent/QuantumFluxCasterComponent.tscn");
        actionSpells["Spell 3"] = LoadSpell("res://Components/Spells/QuantumMage/SingularityCasterComponent/SingularityCasterComponent.tscn");
        actionSpells["Spell 4"] = LoadSpell("res://Components/Spells/QuantumMage/TychonCasterComponent/TychonCasterComponent.tscn");
        actionSpells["Spell 5"] = LoadSpell("res://Components/Spells/QuantumMage/TimeDilationCasterComponent/TimeDilationCasterComponent.tscn");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }

    public BaseSpell LoadSpell(string path)
    {
        PackedScene scene = GD.Load<PackedScene>(path);
        BaseSpell spell = (BaseSpell)scene.Instantiate();
        AddChild(spell);
        return spell;
    }
}
