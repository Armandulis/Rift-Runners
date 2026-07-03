using Godot;
using System;
using System.Reflection.Metadata.Ecma335;

public partial class BaseSpell : Node2D
{
    public virtual bool isOnGlobalCooldown { get; set; } = true;
    public virtual bool canCastWhileMoving { get; set; } = false;

    public virtual bool CanCast()
    {
        throw new NotImplementedException();
    }

    public virtual void CastSpell()
    {

        throw new NotImplementedException();
    }

    public virtual float GetCastTime()
    {

        throw new NotImplementedException();
    }
}
