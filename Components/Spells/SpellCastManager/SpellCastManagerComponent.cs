using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class SpellCastManagerComponent : Node2D
{
    [Export]
    public AnimatedSprite2D animatedSprite2D;

    [Export]
    public Timer globalCooldownTimer;

    private bool isInGlobalCooldown = false;
    public bool isCasting = false;

    [Export]
    public Player player;

    [Export]
    public CastBarComponent castBarComponent;
    public PlayerClass playerClass;

    private BaseSpell castedSpell = null;
    private BaseSpell queuedSpell = null;

    public override void _Ready()
    {
        player = GetParent<Player>();
        playerClass = player.GetPlayerClass();
        castBarComponent.CastFinished += () =>
        {
            CastFinished();
        };
    }

    private void CastFinished()
    {
        animatedSprite2D.Play("idle");
        isCasting = false;
        castedSpell.CastSpell();
        castedSpell = null;

        // Cast queued Spell
        if (queuedSpell != null && isInGlobalCooldown == false)
        {
            CastSpell(queuedSpell);
            queuedSpell = null;
        }
    }

    public override void _Process(double delta)
    {
        // Only listen for current player's input
        if (!player.isMultiplayerAuthority())
        {
            return;
        }

        HandleInput();

        if (CastInterupted() == true)
        {
            castBarComponent.castInerupted();
            isCasting = false;
            castedSpell = null;
            queuedSpell = null;
        }

    }

    private bool CastingMovableSpell()
    {
        return castedSpell.canCastWhileMoving;
    }

    public void HandleInput()
    {

        if (Input.IsActionJustPressed("Spell 1"))
        {
            BaseSpell spell = playerClass.GetSpellForAction("Spell 1");
            TryCastingSpell(spell);
            return;
        }

        if (Input.IsActionJustPressed("Spell 2"))
        {
            BaseSpell spell = playerClass.GetSpellForAction("Spell 2");
            TryCastingSpell(spell);
            return;
        }

        if (Input.IsActionJustPressed("Spell 3"))
        {
            BaseSpell spell = playerClass.GetSpellForAction("Spell 3");
            TryCastingSpell(spell);
            return;
        }


        if (Input.IsActionJustPressed("Spell 4"))
        {
            BaseSpell spell = playerClass.GetSpellForAction("Spell 4");
            TryCastingSpell(spell);
            return;
        }

        if (Input.IsActionJustPressed("Spell 5"))
        {
            BaseSpell spell = playerClass.GetSpellForAction("Spell 5");
            TryCastingSpell(spell);
            return;
        }
    }

    private void TryCastingSpell(BaseSpell spell)
    {
        if (spell.CanCast() == false)
        {
            return;
        }

        // Just cast spells that are off global cooldown
        if (spell.isOnGlobalCooldown == false)
        {
            spell.CastSpell();
            return;
        }

        // If we are already casting or we're in global cooldown
        if (isCasting || isInGlobalCooldown)
        {
            // Queue spell so it's casted right after cast finishes
            if (castBarComponent.GetTimeLeft() <= 0.4f && globalCooldownTimer.TimeLeft <= 0.4f)
            {
                queuedSpell = spell;
            }

            return;
        }

        CastSpell(spell);
    }

    public void CastSpell(BaseSpell spell)
    {
        if (spell.GetCastTime() == 0)
        {
            // Instant spells should have different animation from the ones casting
            isCasting = false;
            spell.CastSpell();
        }
        else
        {
            // Can't cast if we're moving
            if (CastInterupted(spell) == true)
            {
                return;
            }
            castBarComponent.startCast(spell.GetCastTime());
            animatedSprite2D.Play("cast");
            castedSpell = spell;
            isCasting = true;
        }

        isInGlobalCooldown = true;
        globalCooldownTimer.Start(playerClass.GetGlobalCooldown());

        // If we got to this point, we need to clear spell queue
        queuedSpell = null;
    }

    public bool CastInterupted(BaseSpell spell = null)
    {
        if (spell != null)
        {
            return player.isMoving && spell.canCastWhileMoving == false;
        }

        return player.isMoving && isCasting && CastingMovableSpell() == false;
    }

    public void _OnGlobalCooldownTimeout()
    {
        isInGlobalCooldown = false;
        if (queuedSpell != null && isCasting == false)
        {
            CastSpell(queuedSpell);
            queuedSpell = null;
        }
    }
}
